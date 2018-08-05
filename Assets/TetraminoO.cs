using UnityEngine;
using System.Collections.Generic;

namespace Tetris
{
    public class TetraminoO : Tetramino
    {
        public TetraminoO()
        {
            color = Color.red;
            tiles = new List<Tile>();
            tiles.Add(new Tile(0, 0));
            tiles.Add(new Tile(0, -1));
            tiles.Add(new Tile(-1, 0));
            tiles.Add(new Tile(-1, -1));
            probability = 10;
            allowrotate = false;
        }
    }
}
