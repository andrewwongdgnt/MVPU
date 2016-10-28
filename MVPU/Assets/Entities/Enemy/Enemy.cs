using UnityEngine;
using System.Collections;

public abstract class Enemy : Entity
{
    public Enemy(int x, int y, Cell[,] grid, GameModel gameModel) : base(x, y, grid, gameModel)
    {
    }

    public abstract void do_react(GameObject gameObject, float vDisplacement, float hDisplacement);
}
