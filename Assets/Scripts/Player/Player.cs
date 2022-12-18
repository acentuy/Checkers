using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Checkers
{
    public abstract class Player : MonoBehaviour
    {
        protected PlayerPosition playerPosition;
        protected int col;
        protected int row;
        protected BoardController board;
        
        private const float WIDTH = 500f;
        private const float FIELD_SIZE = WIDTH / 11f;
        
        private Object pwanPrefab;
        private List<PawnController> pawns = new List<PawnController>();
        private Transform corner;
        private int count;

        internal virtual void Start()
        {
            corner = GameObject.FindGameObjectWithTag("Corner").transform;
            board = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardController>();
            
            pwanPrefab = Resources.Load("Pawn4");

            Indicate();

            Initialization();
            ArrangePawns();
        }

        private void Indicate()
        {
            for (int i = 0; i < 12; i++)
            {
                var pawn = (GameObject)Instantiate(pwanPrefab, Vector3.zero, Quaternion.identity);
                pawns.Add(pawn.GetComponent<PawnController>());
                pawns[i].playerPosition = playerPosition;
                pawn.transform.parent = corner;
            }
        }

        private void ArrangePawns()
        {
            while (count < pawns.Count)
            {
                var pawn = pawns[count];
                if (board.fields[col, row].Free)
                {
                    board.fields[col, row].PawnController = pawn.GetComponent<PawnController>();
                    board.fields[col, row].Free = false;
                    board.fields[col, row].PlayerPosition = playerPosition;
                    pawn.transform.localPosition = new Vector3(col * FIELD_SIZE + FIELD_SIZE / 2f,
                        WIDTH - row * FIELD_SIZE - FIELD_SIZE / 2f, 10f);
                    count += 1;
                }

                Counting();
            }
        }
        
        protected virtual void Initialization()
        {
            var path = (int)playerPosition switch
            {
                (int)PlayerPosition.Bottom => "player_1",
                (int)PlayerPosition.Right => "player_2",
                (int)PlayerPosition.Upper => "player_3",
                (int)PlayerPosition.Left => "player_4",
                _ => ""
            };

            var sprite = Resources.Load<Sprite>(path);
            foreach (var pawn in pawns)
            {
                pawn.GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }

        protected virtual void Counting()
        {

        }
    }
}