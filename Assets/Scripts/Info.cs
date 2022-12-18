public abstract class Info
{
    internal PawnController PawnController
    {
        get => pawnController;
        set
        {
            pawnController = value;
            if (value != null) value.InfoField = (InfoField)this;
        }
    }
    public PlayerPosition PlayerPosition { get; set; }
    public bool Free { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    
    private PawnController pawnController;
}

public class InfoField : Info { }