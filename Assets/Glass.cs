using UnityEngine;
//using UnityEngine.UI;
namespace Tetris
{
    public class Glass : MonoBehaviour
    {
        private static int GlassWidth;
        private static int GlassHeight;
        public static int FigCount;
        private static Color[,] Board;
        private static int Scale;
        private static int ShiftX;
        private static int ShiftY;
        private static Rect rect;
        private static Texture2D texture;
        public static bool DoInit;
        private static bool GameOver;
        private static bool NewFigure;
        private static bool DoUpdate;
        private static bool DoRedraw;
        Tetris tetris;

        void OnGUI()
        {
            if (GameOver)
            {
                DoUpdate = false;
                Redraw();
                tetris.Run(true);
            }
            else if (DoRedraw)
            {
                Redraw();
            }
        }


        public Glass(int GameMode)
        {
            if (GameMode == 1 || GameMode == 2)
            {
                if (GameMode == 1)
                {
                    GlassHeight = 20;
                    GlassWidth = 10;
                    FigCount = 7;
                }
                else if (GameMode == 2)
                {
                    GlassHeight = 12;
                    GlassWidth = 20;
                    FigCount = 10;
                    Game.Figures[6].probability = 5;
                }
                int ScaleX = (Screen.width - 160) / (GlassWidth + 3);
                int ScaleY = Screen.height / (GlassHeight + 1);
                Scale = Mathf.Min(ScaleX, ScaleY);
                Board = new Color[GlassWidth, GlassHeight];
                ShiftX = Screen.width / Scale - GlassWidth - 2;
                ShiftY = (Screen.height / Scale - GlassHeight - 1) / 2;
                for (int i = 0; i < GlassWidth; i++)
                {
                    for (int j = 0; j < GlassHeight; j++)
                    {
                        Board[i, j] = Color.white;
                    }
                }
            }
        }

        public void Redraw()
        {
            Color currentcolor;
            for (int i = -1; i < GlassWidth + 1; i++)
            {
                for (int j = 0; j < GlassHeight + 1; j++)
                {
                    if (i == -1 | i == GlassWidth | j == GlassHeight)
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
    }
}
