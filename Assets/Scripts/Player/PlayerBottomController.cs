using Checkers;

public class PlayerBottomController : Player
{
    internal override void Start()
    {
        playerPosition = PlayerPosition.Bottom;
        base.Start();
    }

    protected override void Initialization()
    {
        row = 10;
        col = 9;
        
        board.fields[8, 9].Free = true;

        base.Initialization();
    }
    protected override void Counting()
    {
        if (--col <= 0)
        {
            col = 9;
            row -= 1;
        }
    }
}

