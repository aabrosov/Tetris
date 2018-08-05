using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    /// <summary>
    /// class to check is this tetramino intersects with
    /// other tetraminos or walls
    /// </summary>
    public class Checker
    {
        private int Width { set; get; }
        private int Height { set; get; }
        private Color[,] Board;

        /// <summary>
        /// constructor to recieve glass size and content
        /// </summary>
        /// <param name="glass"></param>
        public Checker(Glass glass)
        {
            this.Width = glass.Width;
            this.Height = glass.Height;
            this.Board = glass.Board;
        }

        /// <summary>
        /// is it something under the tetramino
        /// </summary>
        /// <param name="tmino"></param>
        /// <returns>true if exist something under</returns>
        public bool Overlay(Tetramino tmino)
        {
            int newx, newy;
            foreach (Tile tile in tmino.tiles)
            {
                newx = tile.x + tmino.x;
                newy = tile.y + tmino.y;
                while (newx < 0)
                {
                    newx += Width;
                }
                if (newx >= Width)
                {
                    newx %= Width;
                }
                if (Board[newx, newy] != Color.white)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// check for collision with the left and right walls
        /// </summary>
        /// <param name="tmino"></param>
        /// <param name="mode">if mode=2 it always false</param>
        /// <returns>true if collision</returns>
        public bool Sides(Tetramino tmino, int mode)
        {
            if (mode == 2)
                return false;
            int newx;
            foreach (Tile tile in tmino.tiles)
            {
                newx = tile.x + tmino.x;
                if (newx < 0 || newx >= Width)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// check for collision with the top and bottom
        /// </summary>
        /// <param name="tmino"></param>
        /// <returns>true if collision</returns>
        public bool SidesY(Tetramino tmino)
        {
            int newy;
            foreach (Tile tile in tmino.tiles)
            {
                newy = tile.y + tmino.y;
                if (newy < 0 || newy >= Height)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// check is it ok to fix tetramino here
        /// </summary>
        /// <param name="tmino"></param>
        /// <returns>true if it can be fixed</returns>
        public bool Fix(Tetramino tmino)
        {
            int newx, newy;
            foreach (Tile tile in tmino.tiles)
            {
                newx = tile.x + tmino.x;
                newy = tile.y + tmino.y;
                while (newx < 0)
                {
                    newx += Width;
                }
                if (newx >= Width)
                {
                    newx %= Width;
                }
                if (newy == Height - 1 || newy < Height - 1 && Board[newx, newy + 1] != Color.white)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
