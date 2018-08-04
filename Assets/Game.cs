using UnityEngine;

namespace Tetris
{
    public class Game : MonoBehaviour
    {
        public static int Mode;
        private static Tetramino CurrentFig;
        public static Tetramino[] Figures;
        public static int FigCount;
        //private static bool[] FilledRaw;
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
                    CurrentFig = Random.Select();
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
