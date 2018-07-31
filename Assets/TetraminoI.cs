using UnityEngine;

namespace Tetris
{
    public class TetraminoI : Tetramino
    {
        public TetraminoI()
        {
            color = Color.yellow;
            count = 4;
            tiles = new int[,] { { 1, 0 }, { 0, 0 }, { -1, 0 }, { -2, 0 } };
            probability = 10;
            allowrotate = true;
        }
    }
}
