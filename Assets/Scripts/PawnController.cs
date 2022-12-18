using UnityEngine;

public class PawnController : MonoBehaviour
{
    internal State state;
    internal PlayerPosition playerPosition;
    public InfoField InfoField { get; set; }

    private MoveManager moveController;
    private bool dragging;
    private float distance;
    private float z = 10;
    
    private void Start()
    {
        state = State.Counter;
        moveController = GameObject.Find("GameManager").GetComponent<MoveManager>();
    }
    
    private void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x, rayPoint.y, z);
        }
    }
    
    private void OnMouseDown()
    {
        if (TurnManager.turn == playerPosition)
        {
            moveController.StartHolding(gameObject);
            dragging = true;
        }
    }
    
    private void OnMouseUp()
    {
        if (TurnManager.turn == playerPosition)
        {
            moveController.StopHolding(gameObject);
            dragging = false;
        }
    }
    
    internal void TransformToDead()
    {
        InfoField.Free = true;
        state = State.Dead;
        Destroy(gameObject);
    }
    
    internal void TransformToQueen()
    {
        var path = "";
        state = State.Queen;
        switch (playerPosition)
        {
            case PlayerPosition.Bottom:
                path = "player_1_q";
                break;
            case PlayerPosition.Upper:
                path = "player_3_q";
                break;
            case PlayerPosition.Right:
                path = "player_2_q";
                break;
            case PlayerPosition.Left:
                path = "player_4_q";
                break;
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(path);
    }
}