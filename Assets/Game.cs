using UnityEngine;

namespace Tetris
{
    public class Game : MonoBehaviour
    {
        public static int GameMode;
        private static int ShiftX;
        private static int ShiftY;
        private static int Scale;
        private static Tetramino CurrentFig;
        public static Tetramino[] Figures;
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
        Glass glass;

        public void Start()
        {
            RootGameObject = GameObject.Find("Root");
            tetris = RootGameObject.GetComponent<Tetris>();
            texture = new Texture2D(1, 1);
            Figures = new Tetramino[10];
            Figures[0] = new TetraminoO();
            Figures[1] = new TetraminoL();
            Figures[2] = new TetraminoJ();
            Figures[3] = new TetraminoS();
            Figures[4] = new TetraminoZ();
            Figures[5] = new TetraminoI();
            Figures[6] = new TetraminoT();
            Figures[7] = new TetraminoX();
            Figures[8] = new TetraminoU();
            Figures[9] = new TetraminoW();
            DoInit = true;
            NewFigure = true;
            DoUpdate = false;
            GameOver = false;
        }

        public void Run(int gamemode)
        {
            GameMode = gamemode;
            Start();
        }

        void OnGUI()
        {
            if (DoInit)
            {
                Init();
                
            }
            else if (GameOver)
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
        void Init()
        {
            if (GameMode == 1 || GameMode == 2)
            {
                if (GameMode == 1)
                {
                    glass = new Glass(10, 20);
                    FigCount = 7;
                }
                else if (GameMode == 2)
                {
                    glass = new Glass(20, 12);
                    FigCount = 10;
                    Figures[6].probability = 5;
                }
                int ScaleX = (Screen.width - 160) / (glass.Width + 3);
                int ScaleY = Screen.height / (glass.Height + 1);
                Scale = Mathf.Min(ScaleX, ScaleY);
                ShiftX = Screen.width / Scale - glass.Width - 2;
                ShiftY = (Screen.height / Scale - glass.Height - 1) / 2;
                for (int i = 0; i < glass.Width; i++)
                {
                    for (int j = 0; j < glass.Height; j++)
                    {
                        glass.Board[i, j] = Color.white;
                    }
                }
                rect = new Rect();
                FilledRaw = new bool[glass.Height];
                DoInit = false;
                DoUpdate = true;
                DoRedraw = true;
            }
        }
        void Redraw()
        {
            Color currentcolor;
            for (int i = -1; i < glass.Width + 1; i++)
            {
                for (int j = 0; j < glass.Height + 1; j++)
                {
                    if (i == -1 | i == glass.Width | j == glass.Height)
                    {
                        currentcolor = Color.black;
                    }
                    else
                    {
                        currentcolor = glass.Board[i, j];
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
            CurrentFig.x = glass.Width / 2;
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
                if (glass.Board[newx, newy] != Color.white)
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
                if (GameMode == 1 && (newx < 0 || newx >= glass.Width))
                {
                    checksides = true;
                    break;
                }
                while (newx < 0)
                {
                    newx += glass.Width;
                }
                if (newx >= glass.Width)
                {
                    newx %= glass.Width;
                }
                if (newy < 0)
                {
                    checktop = true;
                    break;
                }
                if (newy >= glass.Height)
                {
                    checkbottom = true;
                    break;
                }
                if (glass.Board[newx, newy] != Color.white)
                {
                    checkoverlay = true;
                    break;
                }
                if (newy == glass.Height - 1)
                {
                    checkfix = true;
                    break;
                }
                if (newy < glass.Height - 1 & glass.Board[newx, newy + 1] != Color.white)
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
                checkfix = false;
                NewFigure = true;
            }
        }
        void RemoveRows()
        {
            for (int j = 0; j < glass.Height; j++)
            {
                FilledRaw[j] = true;
                for (int i = 0; i < glass.Width; i++)
                    if (glass.Board[i, j] == Color.white)
                        FilledRaw[j] = false;
            }
            int MovedRaw = glass.Height - 1;
            int NotFilledRaw = glass.Height - 1;
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
                for (int i = 0; i < glass.Width; i++)
                {
                    if (NotFilledRaw < 0)
                    {
                        glass.Board[i, MovedRaw] = Color.white;
                    }
                    else
                    {
                        glass.Board[i, MovedRaw] = glass.Board[i, NotFilledRaw];
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
                    newx += glass.Width;
                }
                if (newx >= glass.Width)
                {
                    newx %= glass.Width;
                }
                glass.Board[newx, newy] = color;
            }
        }
    }
}
