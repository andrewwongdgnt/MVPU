using UnityEngine;
using System.Collections;


public class Player : Entity
{
    public Player(int x, int y, Cell[,] grid, GameModel gameModel) : base(x, y, grid, gameModel)
    {
    }
}
