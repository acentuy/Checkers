using System;
internal class BeatChecker
{
    public int BeatX { get; private set; }
    public int BeatY { get; private set; }
    public BeatChecker(BoardController newBoard) => board = newBoard;
    
    private readonly BoardController board;
    private int beatX;
    private int beatY;
    
    public bool CanCounterBeat(InfoField startF, InfoField endF)
    {
        var maxX = Math.Max(startF.X, endF.X) - 1;
        var maxY = Math.Max(startF.Y, endF.Y) - 1;
        if (Math.Abs(startF.X - endF.X) == 2 && Math.Abs(startF.Y - endF.Y) == 2)
        {
            if (board.fields[maxX, maxY].PlayerPosition != startF.PlayerPosition &&
                board.fields[maxX, maxY].PlayerPosition != PlayerPosition.Empty &&
                board.fields[maxX, maxY].Free == false)
                return endF.Free;
        }
        return false;
    }

    public bool CanQueenBeat(InfoField startF, InfoField endF)
    {
        var signX = startF.X > endF.X? -1 : 1;
        var signY = startF.Y > endF.Y? -1 : 1;
        
        if (Math.Abs(startF.X - endF.X) == Math.Abs(startF.Y - endF.Y))
        {
            var counter = 0;
            var it = 1;

            var count = Math.Abs(startF.X - endF.X);
            
            while (count >= it)
            {
                beatX = startF.X + it * signX;
                beatY = startF.Y + it * signY;
                
                if (board.fields[beatX, beatY].Free != true)
                {
                    if (board.fields[beatX, beatY].PlayerPosition == startF.PlayerPosition) return false;
                    if (board.fields[beatX, beatY].PlayerPosition != PlayerPosition.Empty )
                    {
                        BeatX = beatX;
                        BeatY = beatY;
                        counter++;
                    }
                }
                it++;
            }
            return counter == 1;
        }
        return false;
    }
}