using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    public int teamTurn;

    [SerializeField] private TMP_Text turnText;

    public static TurnHandler Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

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
