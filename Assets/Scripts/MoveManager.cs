using System;
using Checkers;
using UnityEngine;

internal class MoveManager : MonoBehaviour
{
    internal static readonly int[] Count = new int[]{12, 12, 12, 12};
    
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject gameManager;
    
    private const int LAST_INDEX = 10;
    private const int FIRST_INDEX = 0;
    
    private Player[] players = new Player[4];
    private BoardController board;
    private PawnController pawnController;
    private BeatChecker beatChecker;
    
    private Vector3 startPosition;
    private Vector3 pos;
    private int posStartX;
    private int posStartY;
    private int posEndX;
    private int posEndY;
    private int beatX;
    private int beatY;
    
    private bool move;
    private bool queen;
    private bool beat;
        
    private void Start()
    {
        players[(int)PlayerPosition.Upper] = gameManager.GetComponent<PlayerUpperController>();
        players[(int)PlayerPosition.Bottom] = gameManager.GetComponent<PlayerBottomController>();
        players[(int)PlayerPosition.Left] = gameManager.GetComponent<PlayerLeftController>();
        players[(int)PlayerPosition.Right] = gameManager.GetComponent<PlayerRightController>();
        
        board = canvas.GetComponent<BoardController>();
        beatChecker = new BeatChecker(board);
    }
        
    internal void StartHolding(GameObject pawn)
    {
        startPosition = pawn.transform.localPosition;
            
        posStartX = board.CalculateFieldX(startPosition.x);
        posStartY = board.CalculateFieldY(startPosition.y);
    }
        
    internal void StopHolding(GameObject pawn)
    {
        pos = pawn.transform.localPosition;
            
        posEndX = board.CalculateFieldX(pos.x);
        posEndY = board.CalculateFieldY(pos.y);
            
        pawnController = pawn.GetComponent<PawnController>();
            
        if (CanMove()) ExecuteMove(pawn);
        else pawn.transform.localPosition = startPosition;
            
        queen = false;
        move = false;
        beat = false;
    }

    private bool CanMove()
    {
        var startField = board.fields[posStartX, posStartY];
        var endField = board.fields[posEndX, posEndY];
        
        if (posEndX > LAST_INDEX || posEndX < FIRST_INDEX || posEndY > LAST_INDEX || posEndY < FIRST_INDEX) 
            return false;
        
        switch (pawnController.state)
        {
            case State.Counter:
                CheckQueen(startField, endField);
                if (Math.Abs(posStartX - posEndX) > 1 && beatChecker.CanCounterBeat(startField, endField))
                {
                    if (beatChecker.CanCounterBeat(startField, endField))
                    {
                        move = true;
                        beat = true;
                        beatX = Math.Max(startField.X, endField.X) - 1;
                        beatY = Math.Max(startField.Y, endField.Y) - 1;
                    }
                }
                else if (CanCounterMove(startField, endField)) move = true;
                break;
            
            case State.Queen:
                if (CanQueenMove(startField, endField)) move = true;
                else if (beatChecker.CanQueenBeat(startField, endField))
                {
                    move = true;
                    beat = true;
                    beatX = beatChecker.BeatX;
                    beatY = beatChecker.BeatY;
                }
                break;
            
            default:
                return false;
        }
        return move;
    }

    private void ExecuteMove(GameObject pawn)
    {
        var startField = board.fields[posStartX, posStartY];
        var endField = board.fields[posEndX, posEndY];

        TurnManager.NextTurn();
        
        if (beat)
        {
            board.fields[beatX, beatY].PawnController.TransformToDead();
            TurnManager.turn = pawnController.playerPosition;
            beat = false;
            
            Count[(int)pawnController.playerPosition]--;
        }
            
        startField.Free = true;
        startField.PlayerPosition = PlayerPosition.Empty;
        startField.PawnController = null;
        endField.Free = false;
        endField.PlayerPosition = pawnController.playerPosition;
        endField.PawnController = pawnController;

        if (queen) pawnController.TransformToQueen();
        
        pawn.transform.localPosition = board.CalculatePosition(posEndX, posEndY);
    }

    private void CheckQueen(InfoField startF, InfoField endF)
    {
        switch (startF.PlayerPosition)
        {
            case PlayerPosition.Upper:
                if (endF.Y == LAST_INDEX) queen = true;
                break;
            case PlayerPosition.Bottom:
                if (endF.Y == FIRST_INDEX) queen = true;
                break;
            case PlayerPosition.Right:
                if (endF.X == FIRST_INDEX ) queen = true;
                break;
            case PlayerPosition.Left:
                if (endF.X == LAST_INDEX) queen = true;
                break;
        }
    }
    
    private bool CanCounterMove(InfoField startF, InfoField endF)
    {
        if (Math.Abs(startF.X - endF.X) == 1 && Math.Abs(startF.Y - endF.Y) == 1 && endF.Free &&
            ((startF.PlayerPosition == PlayerPosition.Upper && endF.Y > startF.Y) ||
             (startF.PlayerPosition == PlayerPosition.Bottom && endF.Y < startF.Y) ||
             (startF.PlayerPosition == PlayerPosition.Right && endF.X < startF.X) ||
             (startF.PlayerPosition == PlayerPosition.Left && endF.X > startF.X)))
            return true;
        return false;
    }

    private bool CanQueenMove(InfoField startF, InfoField endF)
    {
        var signX = startF.X > endF.X? -1 : 1;
        var signY = startF.Y > endF.Y? -1 : 1;
        
        if (Math.Abs(startF.X - endF.X) == Math.Abs(startF.Y - endF.Y))
        {
            var count = Math.Abs(startF.X - endF.X);
            var it = 1;
            while (count >= it)
            {
                if (board.fields[startF.X + it * signX, startF.Y + it * signY].Free != true) return false;
                it++;
            }
            return true;
        }
        return false;
    }
}