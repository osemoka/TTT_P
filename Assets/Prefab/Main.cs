using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Main : MonoBehaviour
{

    public Board mBoard;
    public GameObject mWinner;

    private bool mXTurn = true;
    private int mTurnCount = 0;
    private void Awake()
    {
        mBoard.Build(this);
    }

    public void Switch()
    {
        mTurnCount++;
        bool hasWinner = mBoard.CheckForWinner();

        if (hasWinner || mTurnCount == 9)
        {
            //End Game
            StartCoroutine(EndGame(hasWinner));
            

            return;
        }

        mXTurn = !mXTurn;
    }

    public string GetTurnCharacter()
    {
        if (mXTurn)
        {
            return "X";
        }
        else
        {
            return "O";
        }
    }

    private IEnumerator EndGame(bool hasWinner)
    {
        Text winnerLabel = mWinner.GetComponentInChildren<Text>();
        if (hasWinner)
        {
            winnerLabel.text = GetTurnCharacter() + " " + "Won!";
        }
        else
        {
            winnerLabel.text = " Draw";
        }

        mWinner.SetActive(true);

        WaitForSeconds wait = new WaitForSeconds(2.0f);
        yield return wait;

        mBoard.Reset();
        mTurnCount = 0;
        mWinner.SetActive(false);
    }
}
