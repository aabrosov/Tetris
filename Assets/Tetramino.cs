using UnityEngine;
using System.Collections.Generic;

namespace Tetris
{
    /// <summary>
    /// abstract class for creating tetraminos
    /// </summary>
    public abstract class Tetramino
    {
        public Color color;
        public int probability;
        public bool allowrotate;
        public int x;
        public int y;

        public List<Tile> tiles;

        /// <summary>
        /// rotate tetramino ccw
        /// </summary>
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

        /// <summary>
        /// rotate tetramino cw
        /// </summary>
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

        /// <summary>
        /// move tetramino left
        /// </summary>
        public void MoveLeft()
        {
            x--;
        }

        /// <summary>
        /// move tetramino right
        /// </summary>
        public void MoveRight()
        {
            x++;
        }

        /// <summary>
        /// move tetramino up
        /// </summary>
        public void MoveUp()
        {
            y--;
        }

        /// <summary>
        /// move tetramino down
        /// </summary>
        public void MoveDown()
        {
            y++;
        }

        /// <summary>
        /// execute command from user
        /// </summary>
        /// <param name="UserInput"></param>
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
        }

        /// <summary>
        /// rollback if after trymove we have collision
        /// </summary>
        /// <param name="UserInput"></param>
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
        }
    }
}
