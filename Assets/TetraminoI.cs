using UnityEngine;
using System.Collections.Generic;

namespace Tetris
{
    public class TetraminoI : Tetramino
    {
        public TetraminoI()
        {
            color = Color.red;
            tiles = new List<Tile>();
            tiles.Add(new Tile(1, 0));
            tiles.Add(new Tile(0, 0));
            tiles.Add(new Tile(-1, 0));
            tiles.Add(new Tile(-2, 0));
            probability = 10;
            allowrotate = true;
        }
    }
}
