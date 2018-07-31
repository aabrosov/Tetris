using UnityEngine;

namespace Tetris
{
    public class TetraminoS : Tetramino
    {
        public TetraminoS()
        {
            color = Color.blue;
            count = 4;
            tiles = new int[,] { { 0, 0 }, { 0, -1 }, { 1, -1 }, { -1, 0 } };
            probability = 15;
            allowrotate = true;
        }
    }
}
