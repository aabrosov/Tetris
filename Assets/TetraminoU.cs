using UnityEngine;

namespace Tetris
{
    public class TetraminoU : Tetramino
    {
        public TetraminoU()
        {
            color = Color.blue;
            count = 5;
            tiles = new int[,] { { 0, 0 }, { 1, 0 }, { -1, 0 }, { 1, 1 }, { -1, 1 } };
            probability = 5;
            allowrotate = true;
        }
    }
}
