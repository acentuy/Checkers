using Checkers;

public class PlayerLeftController : Player
{
    internal override void Start()
    {
        playerPosition = PlayerPosition.Left;
        base.Start();
    }

    protected override void Initialization()
    { 
        row = 0;
        col = 0;
        
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
