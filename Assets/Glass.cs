using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    /// <summary>
    /// this glass is a gameboard
    /// </summary>
    public class Glass
    {
        public int Width { set; get; }
        public int Height { set; get; }
        public Color[,] Board;
        private Texture2D texture;
        private int ShiftX;
        private int ShiftY;
        private int Scale;
        private Rect rect;
        private bool[] FilledRaw;
        private int Mode;

        /// <summary>
        /// construct a glass with selected mode
        /// </summary>
        /// <param name="Mode"></param>
        public Glass(int Mode)
        {
            this.Mode = Mode;
            if (Mode == 1)
            {
                this.Width = 10;
                this.Height = 20;
            }
            else if (Mode == 2)
            {
                this.Width = 20;
                this.Height = 12;
            }
            FilledRaw = new bool[Height];

            this.Board = new Color[Width, Height];

            int ScaleX = (Screen.width - 160) / (Width + 3);
            int ScaleY = Screen.height / (Height + 1);
            Scale = Mathf.Min(ScaleX, ScaleY);
            ShiftX = Screen.width / Scale - Width - 2;
            ShiftY = (Screen.height / Scale - Height - 1) / 2;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Board[i, j] = Color.white;
                }
            }
            rect = new Rect();
            texture = new Texture2D(1, 1);
        }

        /// <summary>
        /// redraw glass content
        /// </summary>
        public void Redraw()
        {
            Color currentcolor;
            for (int i = -1; i < Width + 1; i++)
            {
                for (int j = 0; j < Height + 1; j++)
                {
                    if (i == -1 | i == Width | j == Height)
                    {
                        currentcolor = Color.black;
                    }
                    else
                    {
                        currentcolor = Board[i, j];
                    }
                    if (currentcolor != Color.white)
                    {
                        texture.SetPixel(0, 0, currentcolor);
                        texture.Apply();
                        GUI.skin.box.normal.background = texture;
                        rect.x = (i + 1 + ShiftX) * Scale;
                        rect.y = (j + ShiftY) * Scale;
                        rect.width = Scale;
                        rect.height = Scale;
                        GUI.Box(rect, "");
                    }
                }
            }
        }

        /// <summary>
        /// remove all filled raws in mode 1
        /// remove pairs of filled raws in mode 2
        /// </summary>
        public void RemoveRows()
        {
            for (int j = 0; j < Height; j++)
            {
                FilledRaw[j] = true;
                for (int i = 0; i < Width; i++)
                    if (Board[i, j] == Color.white)
                        FilledRaw[j] = false;
            }
            int MovedRaw = Height - 1;
            int NotFilledRaw = Height - 1;
            while (MovedRaw >= 0)
            {
                if (Mode == 1 && NotFilledRaw >= 0)
                {
                    while (FilledRaw[NotFilledRaw])
                    {
                        NotFilledRaw--;
                    }
                }
                else if (Mode == 2 && NotFilledRaw >= 1)
                {
                    while (FilledRaw[NotFilledRaw] && FilledRaw[NotFilledRaw - 1])
                    {
                        NotFilledRaw -= 2;
                    }
                }
                for (int i = 0; i < Width; i++)
                {
                    if (NotFilledRaw < 0)
                    {
                        Board[i, MovedRaw] = Color.white;
                    }
                    else
                    {
                        Board[i, MovedRaw] = Board[i, NotFilledRaw];
                    }
                }
                MovedRaw--;
                NotFilledRaw--;
            }
        }

        /// <summary>
        /// put provided tetramino to glass with specified color
        /// </summary>
        /// <param name="tmino"></param>
        /// <param name="color"></param>
        public void PutFigure(Tetramino tmino, Color color)
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
                Board[newx, newy] = color;
            }
        }
    }
}
