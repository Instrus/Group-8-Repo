using System;
using TMPro;
using UnityEngine;

// An extra script attached to Question Box in FlashCards game mode
public class FlashCardsAnswerContainers : MonoBehaviour
{
    private void OnEnable()
    {
        //Subscribe to nextQuestion event.
        GameManager.instance.nextQuestion += ChangeSet;
    }

    private void OnDisable()
    {
        GameManager.instance.nextQuestion -= ChangeSet;
    }

    // reference to each answer button texts
    [SerializeField] TextMeshProUGUI[] text;

    [Serializable]
    public class AnswerSets
    {
        public string[] answer;
    }

    [SerializeField] public AnswerSets[] sets;

    // Changes set of answers
    public void ChangeSet(int x) //x is the number generated by GameManager
    {
        if (GameManager.instance.gameState == GameManager.GameMode.FlashCards)
        {
            print("In FlashCards!");
            for (int i = 0; i < text.Length; i++)
            {
                text[i].text = sets[x].answer[i];
            }
        }
    }

}
