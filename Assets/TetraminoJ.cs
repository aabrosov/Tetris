using UnityEngine;
using System.Collections.Generic;

namespace Tetris
{
    public class TetraminoJ : Tetramino
    {
        public TetraminoJ()
        {
            color = Color.magenta;
            tiles = new List<Tile>();
            tiles.Add(new Tile(0, 0));
            tiles.Add(new Tile(1, 0));
            tiles.Add(new Tile(-1, 0));
            tiles.Add(new Tile(-1, -1));
            probability = 15;
            allowrotate = true;
        }
    }
}
