using UnityEngine;

namespace Tetris
{
    public class TetraminoO : Tetramino
    {
        public TetraminoO()
        {
            color = Color.red;
            count = 4;
            tiles = new int[,] { { 0, 0 }, { 0, -1 }, { -1, 0 }, { -1, -1 } };
            probability = 10;
            allowrotate = false;
        }
    }
}
