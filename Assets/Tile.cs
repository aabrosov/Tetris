using UnityEngine;

namespace Tetris
{
    /// <summary>
    /// one tile of tetramino
    /// x and y are relative coordinates from the center of rotation
    /// </summary>
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
