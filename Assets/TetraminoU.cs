using UnityEngine;
using System.Collections.Generic;

namespace Tetris
{
    public class TetraminoU : Tetramino
    {
        public TetraminoU()
        {
            color = Color.blue;
            tiles = new List<Tile>();
            tiles.Add(new Tile(0, 0));
            tiles.Add(new Tile(1, 0));
            tiles.Add(new Tile(-1, 0));
            tiles.Add(new Tile(1, 1));
            tiles.Add(new Tile(-1, 1));
            probability = 5;
            allowrotate = true;
        }
    }
}
