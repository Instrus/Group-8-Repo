using UnityEngine;
using System;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{

    // singleton pattern
    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // events
    public event Action gameStarted, gameFinished, scoreIncremented;

    // game states (game modes / minigames)
    public enum GameMode
    {
        None,
        FlashCards,
        FIB,
        Matching
    }

    public GameMode gameState;

    public GameObject score;


    // changes gameState
    public void changeState(string newState)
    {
        switch(newState)
        {
            case "FlashCards":
                gameState = GameMode.FlashCards;
                Debug.Log("Flashcards");
                break;
            case "FIB":
                gameState = GameMode.FIB;
                Debug.Log("FIB");
                break;
            case "Matching":
                gameState = GameMode.Matching;
                Debug.Log("Matching");
                break;
            default: 
                gameState = GameMode.None;
                Debug.Log("Default");
                break;
        }
    }

    public void clearState() { gameState = GameMode.None; }

    // Event calls

    // Score enabled at start
    public void StartGame() { gameStarted?.Invoke(); enableScore(); }

    public void IncrementPoints() { scoreIncremented?.Invoke(); }

    // Score disabled and state cleared at end
    public void EndGame() { gameFinished?.Invoke(); disableScore(); clearState(); }

    public void enableScore() { score.gameObject.SetActive(true); }

    public void disableScore() { score.gameObject.SetActive(false); }

}