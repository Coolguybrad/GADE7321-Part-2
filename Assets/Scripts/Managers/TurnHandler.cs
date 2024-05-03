using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    public int teamTurn;

    public TMP_Text turnText;

    private void Start()
    {
        teamTurn = 0;
        turnText.text = "BLUE TURN";
        turnText.color = Color.blue;
    }

    public void SetBlueTurn()
    {
        teamTurn = 0;
        turnText.text = "BLUE TURN";
        turnText.color = Color.blue;
    }

    public void SetRedTurn()
    {
        teamTurn = 1;
        turnText.text = "RED TURN";
        turnText.color = Color.red;
    }
}
