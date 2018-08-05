using UnityEngine;

namespace Tetris
{
    public class Tile
    {
        public int x { set; get; }
        public int y { set; get; }

        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
