using UnityEngine;
using System.Collections.Generic;

namespace Tetris
{
    public class TetraminoX : Tetramino
    {
        public TetraminoX()
        {
            color = new Color(0.5f, 0.5f, 0.5f);
            tiles = new List<Tile>();
            tiles.Add(new Tile(0, 0));
            tiles.Add(new Tile(0, 1));
            tiles.Add(new Tile(0, -1));
            tiles.Add(new Tile(1, 0));
            tiles.Add(new Tile(-1, 0));
            probability = 5;
            allowrotate = false;
        }
    }
}
