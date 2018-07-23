using UnityEditor;
using UnityEngine;
using System.Collections;

namespace Tetris
{
	//
    public class Figure
    {
        public Color color;
        public int count;
        public int[,] tiles;
        public int probability;
        public bool allowrotate;
        public int x;
        public int y;
        public Figure()
        {

        }
        public Figure(Color color, int count, int[,] tiles, int probability, bool allowrotate)
        {
            this.color = color;
            this.count = count;
            this.tiles = tiles;
            this.probability = probability;
            this.allowrotate = allowrotate;
        }
    }

    //
    public class Tetris : MonoBehaviour
    {
        private static int ScreenWidth;
        private static int ScreenHeight;
        private static int GlassWidth;
        private static int GlassHeight;
        private static int ShiftX;
        private static int ShiftY;
        private static Color[,] Glass;
        private static int GameMode;
        private static int Scale;
        private static Figure CurrentFig;
        private static Figure[] Figures;
        private static int FigCount;
        private static Rect rect;
        private static Texture2D texture;
        private static float currenttime = 0;
        private static float fallspeed = 1;

        // Use this for initialization
        void Start()
        {
            CurrentFig = new Figure();
            texture = new Texture2D(1, 1);
            Figures = new Figure[10];
            Figures[0] = new Figure(Color.red, 4, new int[,] { { 0, 0 }, { 0, -1 }, { -1, 0 }, { -1, -1 } }, 10, false);
            Figures[1] = new Figure(Color.green, 4, new int[,] { { 0, 0 }, { 0, -1 }, { 1, 0 }, { -1, -1 } }, 15, true);
            Figures[2] = new Figure(Color.blue, 4, new int[,] { { 0, 0 }, { 0, -1 }, { 1, -1 }, { -1, 0 } }, 15, true);
            Figures[3] = new Figure(Color.cyan, 4, new int[,] { { 0, 0 }, { 1, 0 }, { -1, 0 }, { 1, -1 } }, 15, true);
            Figures[4] = new Figure(Color.magenta, 4, new int[,] { { 0, 0 }, { 1, 0 }, { -1, 0 }, { -1, -1 } }, 15, true);
            Figures[5] = new Figure(Color.yellow, 4, new int[,] { { 1, 0 }, { 0, 0 }, { -1, 0 }, { -2, 0 } }, 10, true);
            Figures[6] = new Figure(Color.red, 4, new int[,] { { 0, 0 }, { 1, 0 }, { 0, -1 }, { -1, 0 } }, 20, true);
            Figures[7] = new Figure(Color.green, 5, new int[,] { { 0, 0 }, { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } }, 5, false);
            Figures[8] = new Figure(Color.blue, 5, new int[,] { { 0, 0 }, { 1, 0 }, { -1, 0 }, { 1, 1 }, { -1, 1 } }, 5, true);
            Figures[9] = new Figure(Color.cyan, 5, new int[,] { { 0, 0 }, { 1, 1 }, { 0, 1 }, { -1, 0 }, { -1, -1 } }, 5, true);
            ScreenWidth = Screen.width;
            ScreenHeight = Screen.height;
            if (EditorUtility.DisplayDialog("Game mode selection", "Please, select game mode", "Mode 1", "Mode 2"))
            {
                GameMode = 1;
            }
            else
            {
                GameMode = 2;
            }
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
                Figures[6].probability = 5;
            }
            int ScaleX = ScreenWidth / (GlassWidth + 2);
            int ScaleY = ScreenHeight / (GlassHeight + 1);
            Scale = Mathf.Min(ScaleX, ScaleY);
            Glass = new Color[GlassWidth, GlassHeight];
            ShiftX = (ScreenWidth / Scale - GlassWidth - 2) / 2;
            ShiftY = (ScreenHeight / Scale - GlassHeight - 1) / 2;
            for (int i = 0; i < GlassWidth; i++)
            {
                for (int j = 0; j < GlassHeight; j++)
                {
                    Glass[i, j] = Color.white;
                }
            }
            CurrentFig = Select();
            CurrentFig.x = GlassWidth / 2;
            CurrentFig.y = 2;
            PutFigure(CurrentFig.color);
            rect = new Rect();
        }

        // OnGUI
        void OnGUI()
        {
            Color currentcolor;
            //redraw Glass with walls and bottom
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
                        currentcolor = Glass[i, j];
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

        // Update is called once per frame
        void Update()
        {
            PutFigure(Color.white);
            string UserInput = CheckUserInput();
            bool checkleft = false;
            bool checkright = false;
            bool checktop = false;
            bool checkbottom = false;
            bool checkoverlay = false;
            bool checkfix = false;
            int newx, newy;
            for (int i = 0; i < CurrentFig.count; i++)
            {
                newx = CurrentFig.tiles[i, 0] + CurrentFig.x;
                newy = CurrentFig.tiles[i, 1] + CurrentFig.y;
                if (GameMode == 1 & newx < 0)
                {
                    checkleft = true;
                    break;
                }
                if (GameMode == 1 & newx >= GlassWidth)
                {
                    checkright = true;
                    break;
                }
                while (newx < 0)
                {
                    newx += GlassWidth;
                }
                if (newx >= GlassWidth)
                {
                    newx %= GlassWidth;
                }
                if (newy < 0)
                {
                    checktop = true;
                    break;
                }
                if (newy >= GlassHeight)
                {
                    checkbottom = true;
                    break;
                }
                if (Glass[newx, newy] != Color.white)
                {
                    checkoverlay = true;
                    break;
                }
                if (newy == GlassHeight - 1)
                {
                    checkfix = true;
                    break;
                }
                if (newy < GlassHeight - 1 & Glass[newx, newy + 1] != Color.white)
                {
                    checkfix = true;
                    break;
                }
            }
            if (checkfix)
            {
                PutFigure(CurrentFig.color);
                RemoveRows();
                CurrentFig = Select();
                CurrentFig.x = GlassWidth / 2;
                CurrentFig.y = 2;
                checkfix = false;
            }
            if ((GameMode == 1 & (checkleft | checkright)) | checktop | checkbottom | checkoverlay)
            {
                Rollback(UserInput);
            }
            PutFigure(CurrentFig.color);
        }

        //
        void RemoveRows()
        {
            //
        }

        //
        string CheckUserInput()
        {
            string UserInput = "";
            if (Input.GetKeyDown("up"))
            {
                Rotate(1);
                UserInput = "RotateLeft";
            }
            else if (Input.GetKeyDown("a"))
            {
                Rotate(-1);
                UserInput = "RotateRight";
            }
            else if (Input.GetKeyDown("d"))
            {
                Rotate(1);
                UserInput = "RotateLeft";
            }
            else if (Input.GetKeyDown("down") || Time.time - currenttime >= fallspeed)
            {
                CurrentFig.y += 1;
                UserInput = "MoveDown";
                currenttime = Time.time;
            }
            else if (Input.GetKeyDown("left"))
            {
                CurrentFig.x -= 1;
                UserInput = "MoveLeft";
            }
            else if (Input.GetKeyDown("right"))
            {
                CurrentFig.x += 1;
                UserInput = "MoveRight";
            }
            return UserInput;
        }

        //
        void Rollback(string UserInput)
        {
            switch (UserInput)
            {
                case "RotateLeft":
                    Rotate(-1);
                    break;
                case "RotateRight":
                    Rotate(1);
                    break;
                case "MoveDown":
                    CurrentFig.y -= 1;
                    break;
                case "MoveRight":
                    CurrentFig.x -= 1;
                    break;
                case "MoveLeft":
                    CurrentFig.x += 1;
                    break;
            }
        }

        //
        void Rotate(int direction)
        {
            if (direction != -1)
            {
                direction = 1;
            }
            if (CurrentFig.allowrotate)
            {
                int temp;
                for (int i = 0; i < CurrentFig.count; i++)
                {
                    temp = -CurrentFig.tiles[i, 0];
                    CurrentFig.tiles[i, 0] = direction * CurrentFig.tiles[i, 1];
                    CurrentFig.tiles[i, 1] = direction * temp;
                }
            }
        }

        //
        void PutFigure(Color color)
        {
            int newx, newy;
            for (int i = 0; i < CurrentFig.count; i++)
            {
                newx = CurrentFig.tiles[i, 0] + CurrentFig.x;
                newy = CurrentFig.tiles[i, 1] + CurrentFig.y;
                while (newx < 0)
                {
                    newx += GlassWidth;
                }
                if (newx >= GlassWidth)
                {
                    newx %= GlassWidth;
                }
                Glass[newx, newy] = color;
            }
        }

        //
        Figure Select()
        {
            float rnd = Random.Range(0.0f, 100.0f);
            float LimitLeft = 0.0f;
            float LimitRight = 0.0f;
            for (int i = 0; i < FigCount; i++)
            {
                LimitLeft = LimitRight;
                LimitRight = LimitLeft + Figures[i].probability;
                if (rnd >= LimitLeft & rnd <= LimitRight)
                {
                    return Figures[i];
                }
            }
            return null;
        }
    }
}
