using UnityEngine;
//using UnityEngine.UI;
namespace Tetris
{
    public class Figure
    {
        public Color color;
        public int count;
        public int[,] tiles;
        public int probability;
        public bool allowrotate;
        public int x;
        public int y;
        public Figure() {}
        public Figure(Color color, int count, int[,] tiles, int probability, bool allowrotate)
        {
            this.color = color;
            this.count = count;
            this.tiles = tiles;
            this.probability = probability;
            this.allowrotate = allowrotate;
        }
    }
    public class Game : MonoBehaviour
    {
        public static int GameMode;
        private static int GlassWidth;
        private static int GlassHeight;
        private static int ShiftX;
        private static int ShiftY;
        private static Color[,] Glass;
        private static int Scale;
        private static Figure CurrentFig;
        public static Figure[] Figures;
        public static int FigCount;
        private static Rect rect;
        private static Texture2D texture;
        private static bool[] FilledRaw;
        public static bool DoInit;
        private static bool GameOver;
        private static bool NewFigure;
        private static bool DoUpdate;
        private static bool DoRedraw;

        GameObject RootGameObject;
        Tetris tetris;

        public void Start()
        {
            RootGameObject = GameObject.Find("Root");
            tetris = RootGameObject.GetComponent<Tetris>();
        }

        public void Run(int gamemode)
        {
            GameMode = gamemode;
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
            DoInit = true;
            NewFigure = true;
            DoUpdate = false;
            GameOver = false;
        }

        void OnGUI()
        {
            if (DoInit)
            {
                Init();
            }
            else if (GameOver)
            {
                End();
            }
            else if (DoRedraw)
            {
                Redraw();
            }
        }
        void End()
        {
            DoUpdate = false;
            Redraw();
            tetris.Run(true);
        }
        void Init()
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
                    Figures[6].probability = 5;
                }
                int ScaleX = (Screen.width - 160) / (GlassWidth + 3);
                int ScaleY = Screen.height / (GlassHeight + 1);
                Scale = Mathf.Min(ScaleX, ScaleY);
                Glass = new Color[GlassWidth, GlassHeight];
                ShiftX = Screen.width / Scale - GlassWidth - 2;
                ShiftY = (Screen.height / Scale - GlassHeight - 1) / 2;
                for (int i = 0; i < GlassWidth; i++)
                {
                    for (int j = 0; j < GlassHeight; j++)
                    {
                        Glass[i, j] = Color.white;
                    }
                }
                rect = new Rect();
                FilledRaw = new bool[GlassHeight];
                DoInit = false;
                DoUpdate = true;
                DoRedraw = true;
            }
        }
        void Redraw()
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
        void Update()
        {
            if (DoUpdate)
            {
                if (NewFigure)
                {
                    NewFig();
                }
                else
                {
                    Process();
                }
            }
        }
        void NewFig()
        {
            RemoveRows();
            CurrentFig = Random.Select();
            CurrentFig.x = GlassWidth / 2;
            CurrentFig.y = 2;
            if (CheckOverlay())
            {
                GameOver = true;
            }
            NewFigure = false;
        }
        bool CheckOverlay()
        {
            int newx, newy;
            bool checkoverlay = false;
            for (int i = 0; i < CurrentFig.count; i++)
            {
                newx = CurrentFig.tiles[i, 0] + CurrentFig.x;
                newy = CurrentFig.tiles[i, 1] + CurrentFig.y;
                if (Glass[newx, newy] != Color.white)
                {
                    checkoverlay = true;
                    break;
                }
            }
            return checkoverlay;
        }
        void Process()
        {
            PutFigure(Color.white);
            string Input = UserInput.CheckUserInput();
            TryMove(Input);
            bool checksides = false;
            bool checktop = false;
            bool checkbottom = false;
            bool checkoverlay = false;
            bool checkfix = false;
            int newx, newy;
            for (int i = 0; i < CurrentFig.count; i++)
            {
                newx = CurrentFig.tiles[i, 0] + CurrentFig.x;
                newy = CurrentFig.tiles[i, 1] + CurrentFig.y;
                if (GameMode == 1 && (newx < 0 || newx >= GlassWidth))
                {
                    checksides = true;
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
            if ((GameMode == 1 && checksides) | checktop | checkbottom | checkoverlay)
            {
                Rollback(Input);
            }
            PutFigure(CurrentFig.color);
            if (checkfix)
            {
                //PutFigure(CurrentFig.color);
                checkfix = false;
                NewFigure = true;
            }
        }
        void RemoveRows()
        {
            for (int j = 0; j < GlassHeight; j++)
            {
                FilledRaw[j] = true;
                for (int i = 0; i < GlassWidth; i++)
                    if (Glass[i, j] == Color.white)
                        FilledRaw[j] = false;
            }
            int MovedRaw = GlassHeight - 1;
            int NotFilledRaw = GlassHeight - 1;
            while (MovedRaw >= 0)
            {
                if (GameMode == 1 && NotFilledRaw >= 0)
                {
                    while (FilledRaw[NotFilledRaw])
                    {
                        NotFilledRaw--;
                    }
                }
                else if (GameMode == 2 && NotFilledRaw >= 1)
                {
                    while (FilledRaw[NotFilledRaw] && FilledRaw[NotFilledRaw - 1])
                    {
                        NotFilledRaw -= 2;
                    }
                }
                for (int i = 0; i < GlassWidth; i++)
                {
                    if (NotFilledRaw < 0)
                    {
                        Glass[i, MovedRaw] = Color.white;
                    }
                    else
                    {
                        Glass[i, MovedRaw] = Glass[i, NotFilledRaw];
                    }
                }
                MovedRaw--;
                NotFilledRaw--;
            }
        }
        void TryMove(string UserInput)
        {
            switch (UserInput)
            {
                case "RotateLeft":
                    Rotate(1);
                    break;
                case "RotateRight":
                    Rotate(-1);
                    break;
                case "MoveDown":
                    CurrentFig.y += 1;
                    break;
                case "FallDown":
                    CurrentFig.y += 1;
                    break;
                case "MoveLeft":
                    CurrentFig.x -= 1;
                    break;
                case "MoveRight":
                    CurrentFig.x += 1;
                    break;
            }
        }
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
                case "FallDown":
                    CurrentFig.y -= 1;
                    break;
                case "MoveLeft":
                    CurrentFig.x += 1;
                    break;
                case "MoveRight":
                    CurrentFig.x -= 1;
                    break;
            }
        }
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
    }
}
