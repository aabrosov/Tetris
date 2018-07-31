using UnityEngine;

namespace Tetris
{
    public class TetraminoX : Tetramino
    {
        public TetraminoX()
        {
            color = Color.green;
            count = 5;
            tiles = new int[,] { { 0, 0 }, { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };
            probability = 5;
            allowrotate = false;
        }
    }
}
