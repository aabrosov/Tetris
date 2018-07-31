using UnityEngine;

namespace Tetris
{
    public class TetraminoL : Tetramino
    {
        public TetraminoL()
        {
            color = Color.cyan;
            count = 4;
            tiles = new int[,] { { 0, 0 }, { 1, 0 }, { -1, 0 }, { 1, -1 } };
            probability = 15;
            allowrotate = true;
        }
    }
}
