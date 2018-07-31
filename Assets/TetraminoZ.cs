using UnityEngine;

namespace Tetris
{
    public class TetraminoZ : Tetramino
    {
        public TetraminoZ()
        {
            color = Color.green;
            count = 4;
            tiles = new int[,] { { 0, 0 }, { 0, -1 }, { 1, 0 }, { -1, -1 } };
            probability = 15;
            allowrotate = true;
        }
    }
}
