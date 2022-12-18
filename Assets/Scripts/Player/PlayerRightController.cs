using Checkers;

public class PlayerRightController : Player
{
    internal override void Start()
    {
        playerPosition = PlayerPosition.Right;
        base.Start();
    }

    protected override void Initialization()
    {
        row = 0;
        col = 8;

        board.fields[8, 9].Free = false;
        board.fields[9, 2].Free = true;
        
        base.Initialization();
    }

    protected override void Counting()
    {
        if (++row >= 10)
        {
            row = 0;
            col += 1;
        }
    }
}
