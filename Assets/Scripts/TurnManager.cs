using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

internal class TurnManager : MonoBehaviour
{
    internal static PlayerPosition turn;
    private static Image image;
    
    private void Start()
    {
        GameObject imageObject = GameObject.FindWithTag("ImageTurn");
        image = imageObject.GetComponent<Image>();
        ChooseRandom();
    }

    internal static void NextTurn()
    {
        turn++;
        if ((int)turn == 4) turn = 0;
        while (MoveManager.Count[(int)turn] == 0)
        {
            turn++;
        }
        TurnColor(turn);
    }

    private void ChooseRandom()
    {
        turn = (PlayerPosition)Random.Range(0, 3);
        TurnColor(turn);
    }

    private static void TurnColor(PlayerPosition turn)
    {
        switch (turn)
        {
            case PlayerPosition.Bottom:
                image.color = Color.red;
                break;
            case PlayerPosition.Left:
                image.color = Color.green;
                break;
            case PlayerPosition.Upper:
                image.color = Color.blue;
                break;
            case PlayerPosition.Right:
                image.color = new Color(0.5f, 0f, 0.5f, 1f);;
                break;
        }
    }
}