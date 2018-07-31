using UnityEngine;

namespace Tetris
{
    public class TetraminoT : Tetramino
    {
        public TetraminoT()
        {
            color = Color.red;
            count = 4;
            tiles = new int[,] { { 0, 0 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
            probability = 20;
            allowrotate = true;
        }
    }
}
