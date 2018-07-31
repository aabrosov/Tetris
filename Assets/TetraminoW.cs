using UnityEngine;

namespace Tetris
{
    public class TetraminoW : Tetramino
    {
        public TetraminoW()
        {
            color = Color.cyan;
            count = 5;
            tiles = new int[,] { { 0, 0 }, { 1, 1 }, { 0, 1 }, { -1, 0 }, { -1, -1 } };
            probability = 5;
            allowrotate = true;
        }
    }
}
