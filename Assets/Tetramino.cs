using UnityEngine;

namespace Tetris
{
    public abstract class Tetramino
    {
        public Color color;
        public int count;
        public int[,] tiles;
        public int probability;
        public bool allowrotate;
        public int x;
        public int y;
    }
}
