using UnityEngine;
using System.Collections.Generic;

namespace Tetris
{
    public abstract class Tetramino
    {
        public Color color;
        public int probability;
        public bool allowrotate;
        public int x;
        public int y;

        public List<Tile> tiles;

        public void RotateLeft()
        {
            if (allowrotate)
            {
                int temp;
                foreach (Tile tile in tiles)
                {
                    temp = tile.x;
                    tile.x = tile.y;
                    tile.y = -temp;
                }
            }
        }

        public void RotateRight()
        {
            if (allowrotate)
            {
                int temp;
                foreach (Tile tile in tiles)
                {
                    temp = tile.x;
                    tile.x = -tile.y;
                    tile.y = temp;
                }
            }
        }

        public void MoveLeft()
        {
            x--;
        }

        public void MoveRight()
        {
            x++;
        }

        public void MoveUp()
        {
            y--;
        }

        public void MoveDown()
        {
            y++;
        }

        public void DoNothing()
        {
        }

        public void TryMove(string UserInput)
        {
            if (UserInput == "RotateLeft")
                RotateLeft();
            else if (UserInput == "RotateRight")
                RotateRight();
            else if (UserInput == "MoveDown")
                MoveDown();
            else if (UserInput == "MoveRight")
                MoveRight();
            else if (UserInput == "MoveLeft")
                MoveLeft();
            else
                DoNothing();
        }

        public void Rollback(string UserInput)
        {
            if (UserInput == "RotateLeft")
                RotateRight();
            else if (UserInput == "RotateRight")
                RotateLeft();
            else if (UserInput == "MoveDown")
                MoveUp();
            else if (UserInput == "MoveRight")
                MoveLeft();
            else if (UserInput == "MoveLeft")
                MoveRight();
            else
                DoNothing();
        }
    }
}
