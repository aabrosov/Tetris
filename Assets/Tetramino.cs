using UnityEngine;

namespace Tetris
{
    public abstract class Tetramino
    {
        public Color color;
        public int count;
        public int[,] tiles;
        public int probability;
        public bool allowrotate;
        public int x;
        public int y;

        public void RotateLeft()
        {
            if (allowrotate)
            {
                int temp;
                for (int i = 0; i < count; i++)
                {
                    temp = tiles[i, 0];
                    tiles[i, 0] = tiles[i, 1];
                    tiles[i, 1] = -temp;
                }
            }
        }

        public void RotateRight()
        {
            if (allowrotate)
            {
                int temp;
                for (int i = 0; i < count; i++)
                {
                    temp = tiles[i, 0];
                    tiles[i, 0] = -tiles[i, 1];
                    tiles[i, 1] = temp;
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
    }
}
