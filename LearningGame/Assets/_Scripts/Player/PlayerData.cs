using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    [SerializeField] PlayerDataManager playerDM;

    // Default PlayerData members. Stored on local device.
    [SerializeField] private int playerID = 1;
    [SerializeField] private string username = "Blueberry";
    [SerializeField] private int pin = 1234;
    [SerializeField] private int score = 0;
    [SerializeField] private int currency = 0;
    [SerializeField] private int avatarIndex = 0;
    [SerializeField] private int hatIndex = 0;
    [SerializeField] private int shirtIndex = 0;
    [SerializeField] private List<ContactInfo> contacts = new List<ContactInfo>();

    private void Awake() { 
        // ensures playerDM is always set
        playerDM = GameObject.Find("PlayerDataManager").GetComponent<PlayerDataManager>();
    }

    // Save all serialized player data to a persistent JSON file.
    public void Save() { 
        if (playerDM != null)
            playerDM.SavePlayerDataToFile();
        else
            Debug.LogError("PlayerDM does not exist!");
        }

    // Player ID
    public int GetPlayerID() { return playerID; }
    public void SetPlayerID(int playerID) { this.playerID = playerID; Save(); }

    // Player username
    public string GetUsername() { return username; }
    public void SetUsername(string userName) { this.username = userName; Save(); }

    // Player PIN
    public int GetPin() { return pin; }
    public void SetPIN(int pin) { this.pin = pin; Save(); }

    // Player currency
    public int GetCurrency() { return currency; }
    public void SetCurrency(int currency) // Do not call. Call AddCurrency
    { this.currency = currency; Save(); }
    public void AddCurrency(int currency) { SetCurrency(GetCurrency() + currency); }

    // Scores
    public int GetScore() { return score; }
    public void SetScore(int score) // Do not call. Call AddScore
    { this.score = score; Save(); }
    public void AddScore(int score) { SetScore(GetScore() + score); }

    // Avatar cosmetics
    public int[] GetAvatar() { return new int[] { avatarIndex, hatIndex, shirtIndex }; }
    public void SetAvatar(int avatarIndex, int hatIndex, int shirtIndex)
    {
        this.avatarIndex = avatarIndex;
        this.hatIndex = hatIndex;
        this.shirtIndex = shirtIndex;
        Save();
    }

    // Contact information
    public List<ContactInfo> GetContacts() { return new List<ContactInfo>(contacts); }
    public void AddContact(string name, string phoneNumber)
    { contacts.Add(new ContactInfo(name, phoneNumber)); Save(); }
    public void RemoveContact(string phoneNumber)
    { contacts.RemoveAll(c => c.phoneNumber == phoneNumber); Save(); } // Delete a contact based on the number

}

// Ensure names and phone numbers stay paired up
public class ContactInfo
{
    public string name;
    public string phoneNumber;

    public ContactInfo(string name, string phoneNumber)
    {
        this.name = name;
        this.phoneNumber = phoneNumber;
    }
}