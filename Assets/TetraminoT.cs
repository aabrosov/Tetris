using UnityEngine;
using System.Collections.Generic;

namespace Tetris
{
    public class TetraminoT : Tetramino
    {
        public TetraminoT()
        {
            color = Color.yellow;
            tiles = new List<Tile>();
            tiles.Add(new Tile(0, 0));
            tiles.Add(new Tile(1, 0));
            tiles.Add(new Tile(0, -1));
            tiles.Add(new Tile(-1, 0));
            probability = 20;
            allowrotate = true;
        }
    }
}
