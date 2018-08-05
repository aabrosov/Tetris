using UnityEngine;
using System.Collections.Generic;

namespace Tetris
{
    public class TetraminoS : Tetramino
    {
        public TetraminoS()
        {
            color = Color.blue;
            tiles = new List<Tile>();
            tiles.Add(new Tile(0, 0));
            tiles.Add(new Tile(0, -1));
            tiles.Add(new Tile(1, -1));
            tiles.Add(new Tile(-1, 0));
            probability = 15;
            allowrotate = true;
        }
    }
}
