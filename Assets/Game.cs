using UnityEngine;

namespace Tetris
{
    /// <summary>
    /// main game logic
    /// </summary>
    public class Game : MonoBehaviour
    {
        private int Mode;
        private Tetramino CurrentFig;
        private bool DoInit;
        private bool GameOver;
        private bool NewFigure;
        private bool DoUpdate;
        private bool DoRedraw;

        GameObject RootGameObject;
        Tetris tetris;
        Glass glass;
        Checker checker;
        Figures figures;
        UserInput userInput;
        string Input;

        /// <summary>
        /// init all objects
        /// </summary>
        public void Start()
        {
            RootGameObject = GameObject.Find("Root");
            tetris = RootGameObject.GetComponent<Tetris>();

            glass = new Glass(Mode);
            checker = new Checker(glass);
            DoInit = true;

            figures = new Figures(Mode);
            NewFigure = true;

            userInput = new UserInput();
            DoUpdate = false;
            GameOver = false;
        }

        /// <summary>
        /// run game with selected mode
        /// </summary>
        /// <param name="gamemode"></param>
        public void Run(int gamemode)
        {
            Mode = gamemode;
            Start();
        }

        /// <summary>
        /// while not gameover,
        /// if game mode = 1 or 2
        /// redraw glass
        /// </summary>
        void OnGUI()
        {
            if (DoInit)
            {
                if (Mode == 1 || Mode == 2)
                {
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

        /// <summary>
        /// create new figure,
        /// and move it down with reading user input
        /// and trying to do what user wants
        /// </summary>
        void Update()
        {
            if (DoUpdate)
            {
                if (NewFigure)
                {
                    glass.RemoveRows();
                    CurrentFig = figures.Select();
                    CurrentFig.x = glass.Width / 2;
                    CurrentFig.y = 2;
                    if (checker.Overlay(CurrentFig))
                    {
                        GameOver = true;
                    }
                    NewFigure = false;
                }
                else
                {
                    glass.PutFigure(CurrentFig, Color.white);
                    Input = userInput.CheckUserInput();
                    CurrentFig.TryMove(Input);
                    if (checker.Sides(CurrentFig, Mode) || checker.SidesY(CurrentFig) || checker.Overlay(CurrentFig))
                    {
                        CurrentFig.Rollback(Input);
                    }
                    if (checker.Fix(CurrentFig))
                    {
                        NewFigure = true;
                    }
                    glass.PutFigure(CurrentFig, CurrentFig.color);
                }
            }
        }
    }
}
