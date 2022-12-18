using Checkers;

public class PlayerUpperController : Player
{
    internal override void Start()
    {
        playerPosition = PlayerPosition.Upper;
        base.Start();
    }

    protected override void Initialization()
    {
        row = 0;
        col = 9;

        board.fields[9, 2].Free = false;

        base.Initialization();
    }

    protected override void Counting()
    {
        if (--col <= 0)
        {
            col = 9;
            row += 1;
        }
    }
}
