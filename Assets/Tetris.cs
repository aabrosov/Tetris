using UnityEditor;
using UnityEngine;
using System.Collections;

namespace Tetris
{
    public class Figure
    {
        public Color color;
        public int[,] tiles;
        public int probability;
        public bool allowrotate;
        public int positionx;
        public int positiony;
        public Figure() { }
        public Figure(Color color, int[,] tiles, int probability, bool allowrotate)
        {
            this.color = color;
            this.tiles = tiles;
            this.probability = probability;
            this.allowrotate = allowrotate;
        }
    }
    public class Tetris : MonoBehaviour
    {
        private static int ScreenWidth;
        private static int ScreenHeight;
        private static int GlassWidth;
        private static int GlassHeight;
        private static Color[,] Glass;
        private static int GameMode;
        private static int Scale;
        private static Figure CurrentFig = new Figure();
        public static Figure[] Figures = {
            new Figure(new Color(1.0f, 0.0f, 0.0f), new int[,] { { 0, 0 }, { 0, 1 }, { 1, 0 }, { 1, 1 }, { 1, 1 } }, 10, false),
            new Figure(new Color(0.0f, 1.0f, 0.0f), new int[,] { { 0, 0 }, { 0, 1 }, { 1, 0 }, {-1, 1 }, {-1, 1 } }, 15, true),
            new Figure(new Color(0.0f, 0.0f, 1.0f), new int[,] { { 0, 0 }, { 0, 1 }, { 1, 1 }, {-1, 0 }, {-1, 0 } }, 15, true),
            new Figure(new Color(0.0f, 1.0f, 1.0f), new int[,] { { 0, 0 }, { 1, 0 }, {-1, 0 }, { 1, 1 }, { 1, 1 } }, 15, true),
            new Figure(new Color(1.0f, 0.0f, 1.0f), new int[,] { { 0, 0 }, { 1, 0 }, {-1, 0 }, {-1, 1 }, {-1, 1 } }, 15, true),
            new Figure(new Color(1.0f, 1.0f, 0.0f), new int[,] { { 2, 0 }, { 1, 0 }, { 0, 0 }, {-1, 0 }, {-1, 0 } }, 10, true),
            new Figure(new Color(0.8f, 0.0f, 0.0f), new int[,] { { 0, 0 }, { 1, 0 }, { 0, 1 }, {-1, 0 }, {-1, 0 } }, 20, true),
            new Figure(new Color(0.0f, 0.8f, 0.0f), new int[,] { { 0, 0 }, { 0, 1 }, { 0,-1 }, { 1, 0 }, {-1, 0 } }, 5, false),
            new Figure(new Color(0.0f, 0.0f, 0.8f), new int[,] { { 0, 0 }, { 1, 0 }, {-1, 0 }, { 1, 1 }, {-1, 1 } }, 5, true),
            new Figure(new Color(0.0f, 0.8f, 0.8f), new int[,] { { 0, 0 }, { 1,-1 }, { 0,-1 }, {-1, 0 }, {-1, 1 } }, 5, true),
        };
        public static int FigCount;
        // Use this for initialization
        void Start()
        {
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
            CurrentFig = Select();
            CurrentFig.positionx = GlassWidth / 2;
            CurrentFig.positiony = 1;
            for (int i = 0; i < 5; i++)
            {
                Glass[CurrentFig.tiles[i, 0] + CurrentFig.positionx, CurrentFig.tiles[i, 1] + CurrentFig.positiony] = CurrentFig.color;
            }
        }
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
                        Texture2D MyTexture = new Texture2D(1, 1);
                        MyTexture.SetPixel(0, 0, currentcolor);
                        MyTexture.Apply();
                        GUI.skin.box.normal.background = MyTexture;
                        GUI.Box(new Rect((i + 1) * Scale, j * Scale, Scale, Scale), "");
                    }
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            int newx, newy;
            for (int i = 0; i < 5; i++)
            {
                newx = CurrentFig.tiles[i, 0] + CurrentFig.positionx;
                newy = CurrentFig.tiles[i, 1] + CurrentFig.positiony;
                if (newx < 0) newx += GlassWidth;
                if (newx >= GlassWidth) newx -= GlassWidth;
                Glass[newx, newy] = Color.white;
            }
            string UserInput = CheckUserInput();
            bool checkleft = false;
            bool checkright = false;
            bool checktop = false;
            bool checkbottom = false;
            bool checkoverlay = false;
            bool checkfix = false;
            for (int i = 0; i < 5; i++)
            {
                newx = CurrentFig.tiles[i, 0] + CurrentFig.positionx;
                newy = CurrentFig.tiles[i, 1] + CurrentFig.positiony;
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
            }
            if (GameMode == 1 & (checkleft | checkright))
            {
                Rollback(UserInput);
            }
            else if (checktop | checkbottom)
            {
                Rollback(UserInput);
            }
            for (int i = 0; i < 5; i++)
            {
                newx = CurrentFig.tiles[i, 0] + CurrentFig.positionx;
                if (newx < 0) newx += GlassWidth;
                if (newx >= GlassWidth) newx -= GlassWidth;
                newy = CurrentFig.tiles[i, 1] + CurrentFig.positiony;
                Glass[newx, newy] = CurrentFig.color;
            }
        }
        string CheckUserInput()
        {
            string UserInput = "";
            if (Input.GetKeyDown("up"))
            {
                CurrentFig = RotateLeft(CurrentFig);
                UserInput = "RotateLeft";
            }
            else if (Input.GetKeyDown("a"))
            {
                CurrentFig = RotateRight(CurrentFig);
                UserInput = "RotateRight";
            }
            else if (Input.GetKeyDown("d"))
            {
                CurrentFig = RotateLeft(CurrentFig);
                UserInput = "RotateLeft";
            }
            else if (Input.GetKeyDown("down"))
            {
                CurrentFig.positiony += 1;
                UserInput = "MoveDown";
            }
            else if (Input.GetKeyDown("left"))
            {
                CurrentFig.positionx -= 1;
                UserInput = "MoveLeft";
            }
            else if (Input.GetKeyDown("right"))
            {
                CurrentFig.positionx += 1;
                UserInput = "MoveRight";
            }
            return UserInput;
        }
        void Rollback(string UserInput)
        {
            switch (UserInput)
            {
                case "RotateLeft":
                    CurrentFig = RotateRight(CurrentFig);
                    break;
                case "RotateRight":
                    CurrentFig = RotateLeft(CurrentFig);
                    break;
                case "MoveDown":
                    CurrentFig.positiony -= 1;
                    break;
                case "MoveRight":
                    CurrentFig.positionx -= 1;
                    break;
                case "MoveLeft":
                    CurrentFig.positionx += 1;
                    break;
            }
        }
        Figure RotateLeft(Figure figure)
        {
            if (figure.allowrotate)
            {
                int temp;
                for (int i = 0; i < 5; i++)
                {
                    temp = -figure.tiles[i, 0];
                    figure.tiles[i, 0] = figure.tiles[i, 1];
                    figure.tiles[i, 1] = temp;
                }
            }
            return figure;
        }
        Figure RotateRight(Figure figure)
        {
            if (figure.allowrotate)
            {
                int temp;
                for (int i = 0; i < 5; i++)
                {
                    temp = figure.tiles[i, 0];
                    figure.tiles[i, 0] = -figure.tiles[i, 1];
                    figure.tiles[i, 1] = temp;
                }
            }
            return figure;
        }
        public static Figure Select()
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
