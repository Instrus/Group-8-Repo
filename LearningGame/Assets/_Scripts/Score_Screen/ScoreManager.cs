using UnityEngine;
using System.Collections.Generic;
using System.IO;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Path to the scoreboard JSON file
    private string jsonFilePath;

    // Separate the top 3 scores from the rest
    //public TextMeshProUGUI[] topThreeEntries;
    public TextMeshProUGUI[] otherEntries;

    // Instantiate the scoreboard
    private List<Player> scoreboard;

    // instead of using a singleton pattern
    [SerializeField] PlayerData playerData; 

    private void Start()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();

        // Set the path to the scoreboard
        jsonFilePath = Path.Combine(Application.persistentDataPath, "scoreboard.json");

        // Load the scoreboard
        scoreboard = LoadPlayerScores();
    }

    // A function to call in Unity to update the scoreboard when its button is clicked.
    public void UpdateScoreboardOnClick()
    {
        // Fetch and log the player's most up to date score
        int playerScore = playerData.GetScore();
        Debug.Log("Player Score: " + playerScore);

        // Update the player's score in scoreboard.JSON
        UpdatePlayerScore(playerScore);

        // Update the scoreboard display
        UpdateScoreboard();
    }

    private void UpdatePlayerScore(int playerScore)
    {
        string username = playerData.GetUsername();

        // Find the player in the scoreboard
        Player player = scoreboard.Find(p => p.username == username);

        // If the player exists
        if (player != null)
        {
            // Update their score
            player.score = playerScore;
        }
        else
        {
            // Add the player to the scoreboard
            scoreboard.Add(new Player { username = username, score = playerScore });
        }

        // Keep the scoreboard sorted in ascending order
        scoreboard.Sort((a, b) => a.score.CompareTo(b.score));
    }

    private void UpdateScoreboard()
    {
        // Find the player's rank
        string playerUsername = playerData.GetUsername();
        int playerRank = scoreboard.FindIndex(p => p.username == playerUsername);

        if (playerRank != -1)
        {
            int playerScore = scoreboard[playerRank].score;

            // Display the previous 2 and next 2 scores relative to the player's rank
            int startIndex = Mathf.Max(0, playerRank - 2);
            int endIndex = Mathf.Min(startIndex + 5, scoreboard.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                int entryIndex = i - startIndex;
                otherEntries[entryIndex].text = $"{scoreboard.Count - i + 1}. {scoreboard[i].username}: {scoreboard[i].score}";
            }
        }
        else
        {
            Debug.LogWarning("Player not found in the scoreboard.");
        }
    }

    private List<Player> LoadPlayerScores()
    {
        if (File.Exists(jsonFilePath))
        {
            // Parse the JSON file
            string json = File.ReadAllText(jsonFilePath);
            return JsonUtility.FromJson<PlayerList>(json).players;
        }
        else
        {
            Debug.LogWarning("Scoreboard file not found. path: " + jsonFilePath);
            return new List<Player>();
        }
    }
}

// The data contained in each scoreboard entry - maybe we should move this to a separate script
[System.Serializable]
public class Player
{
    public string username;
    public int score;
    public int currency;
}

// The list of all entries in the scoreboard
[System.Serializable]
public class PlayerList
{
    public List<Player> players;
}