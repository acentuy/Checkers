using UnityEngine;

public class BoardController : MonoBehaviour
{
    public InfoField[,] fields = new InfoField[11, 11];
    
    private const float WIDTH = 500f;
    private const int NUMBER_OF_CELLS = 11;
    private float FIELD_SIZE = WIDTH / NUMBER_OF_CELLS;
    private readonly float z = 10;

    void Awake() => CheckCells();
    
    private void CheckCells()
    {
        for (int i = 0; i < NUMBER_OF_CELLS; i++)
        {
            for (int j = 0; j < NUMBER_OF_CELLS; j++)
            {
                fields[i, j] = new InfoField
                {
                    PlayerPosition = PlayerPosition.Empty
                };
                
                if (i % 2 != j % 2) SetUpField(i, j, true);
            }
        }
    }
    internal int CalculateFieldX(float range)
    {
        return (int)(range / FIELD_SIZE);
    }

    internal int CalculateFieldY(float range)
    {
        return (int)((WIDTH - range) / FIELD_SIZE);
    }

    internal Vector3 CalculatePosition(int x, int y)
    {
        return new Vector3(x * FIELD_SIZE + FIELD_SIZE / 2f, WIDTH - y * FIELD_SIZE - FIELD_SIZE / 2f, z);
    }

    private void SetUpField(int x, int y, bool free)
    {
        fields[x, y].Free = free;
        fields[x, y].X = x;
        fields[x, y].Y = y;
    }
}
