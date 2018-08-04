using UnityEngine;

namespace Tetris
{
    public class Game : MonoBehaviour
    {
        public static int Mode;
        private static Tetramino CurrentFig;
        public static bool DoInit;
        private static bool GameOver;
        private static bool NewFigure;
        private static bool DoUpdate;
        private static bool DoRedraw;

        GameObject RootGameObject;
        Tetris tetris;
        Glass glass;
        Figures figures;

        public void Start()
        {
            RootGameObject = GameObject.Find("Root");
            tetris = RootGameObject.GetComponent<Tetris>();
            DoInit = true;
            NewFigure = true;
            DoUpdate = false;
            GameOver = false;
        }

        public void Run(int gamemode)
        {
            Mode = gamemode;
            Start();
        }

        void OnGUI()
        {
            if (DoInit)
            {
                if (Mode == 1 || Mode == 2)
                {
                    glass = new Glass(Mode);
                    DoInit = false;
                    DoUpdate = true;
                    DoRedraw = true;
                }
            }
            else if (GameOver)
            {
                DoUpdate = false;
                glass.Redraw();
                tetris.Run(true);
            }
            else if (DoRedraw)
            {
                glass.Redraw();
            }
        }

        void Update()
        {
            if (DoUpdate)
            {
                if (NewFigure)
                {
                    glass.RemoveRows();
                    figures = new Figures(Mode);
                    CurrentFig = figures.Select();
                    CurrentFig.x = glass.Width / 2;
                    CurrentFig.y = 2;
                    if (CheckOverlay())
                    {
                        GameOver = true;
                    }
                    NewFigure = false;
                }
                else
                {
                    Process();
                }
            }
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
                if (Mode == 1 && (newx < 0 || newx >= glass.Width))
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
            if ((Mode == 1 && checksides) | checktop | checkbottom | checkoverlay)
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
        void TryMove(string UserInput)
        {
            switch (UserInput)
            {
                case "RotateLeft":
                    CurrentFig.RotateLeft();
                    break;
                case "RotateRight":
                    CurrentFig.RotateRight();
                    break;
                case "MoveDown":
                    CurrentFig.MoveDown();
                    break;
                case "MoveLeft":
                    CurrentFig.MoveLeft();
                    break;
                case "MoveRight":
                    CurrentFig.MoveRight();
                    break;
            }
        }
        void Rollback(string UserInput)
        {
            switch (UserInput)
            {
                case "RotateLeft":
                    CurrentFig.RotateRight();
                    break;
                case "RotateRight":
                    CurrentFig.RotateLeft();
                    break;
                case "MoveDown":
                    CurrentFig.MoveUp();
                    break;
                case "MoveLeft":
                    CurrentFig.MoveRight();
                    break;
                case "MoveRight":
                    CurrentFig.MoveLeft();
                    break;
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
