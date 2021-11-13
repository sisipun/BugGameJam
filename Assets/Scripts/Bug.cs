using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Side side;
    private int moveRange;
    private int attackRange;

    private int health;
    private Vector2Int position;
    private int stepsLeft;
    private int attacksLeft;

    public bool IsUserSide => side == Side.USER;
    public bool IsDead => health == 0;
    public Vector2Int Position
    {
        get
        {
            return position;
        }

        set
        {
            int stepsCount = Mathf.Abs(position.x - value.x) + Mathf.Abs(position.y - value.y);
            stepsLeft -= stepsCount;
            position = value;
        }
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(Vector2Int position, Side side, BugData data)
    {
        this.position = position;
        this.side = side;
        this.moveRange = data.MoveRange;
        this.attackRange = data.AttackRange;
        this.health = data.Health;
        this.stepsLeft = moveRange;
        this.attacksLeft = 1;
        this.spriteRenderer.sprite = IsUserSide ? data.UserBody : data.AiBody;
    }

    public void Hit()
    {
        health--;
    }

    public void Attack()
    {
        attacksLeft--;
        stepsLeft = 0;
    }

    public void ResetTurn()
    {
        stepsLeft = moveRange;
        attacksLeft = 1;
    }

    public List<Vector2Int> PossibleMoves(Cell[,] map)
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        if (stepsLeft == 0)
        {
            return moves;
        }
        
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                Vector2Int move = new Vector2Int(x, y);
                if (PathFinder.Find(map, position, move, stepsLeft).Count != 0)
                {
                    moves.Add(move);
                }
            }
        }
        return moves;
    }

    public List<Vector2Int> PossibleAttacks(Cell[,] map)
    {
        List<Vector2Int> attacks = new List<Vector2Int>();
        if (attacksLeft == 0)
        {
            return attacks;
        }

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                int range = Mathf.Abs(position.x - x) + Mathf.Abs(position.y - y);
                Bug attacked = map[x, y].Bug;
                if (range <= attackRange && attacked != null && attacked.side != side)
                {
                    attacks.Add(new Vector2Int(x, y));
                }
            }
        }
        return attacks;
    }

    public enum Side
    {
        USER,
        AI
    }
}
