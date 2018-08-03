using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    public class Glass
    {
        public int Width { set; get; }
        public int Height { set; get; }
        public Color[,] Board;

        public Glass(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Board = new Color[width, height];
        }
    }
}
