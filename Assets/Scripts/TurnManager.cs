using UnityEngine;

internal class TurnManager : MonoBehaviour
{
    internal static PlayerPosition turn;

    private void Start() => ChooseRandom();

    internal static void NextTurn()
    {
        turn++;
        if ((int)turn == 4) turn = 0;
        while (MoveManager.Count[(int)turn] == 0)
        {
            turn++;
        }
    }

    private void ChooseRandom() => turn = (PlayerPosition)Random.Range(0, 3);
    
}