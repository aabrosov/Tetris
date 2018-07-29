//using UnityEditor;
using UnityEngine;

namespace Tetris
{
    /// <summary>
    /// class for figure:
    /// color, tiles count, tiles array {x,y}, probability in %, bool allowrotate?, and x&y shift
    /// </summary>
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

    /// <summary>
    /// main class, all code is here
    /// </summary>
    public class Tetris : MonoBehaviour
    {
        private static int GlassWidth;
        private static int GlassHeight;
        private static int ShiftX;
        private static int ShiftY;
        private static Color[,] Glass;
        private static int GameMode;
        private static int Scale;
        private static Figure CurrentFig;
        public static Figure[] Figures;
        public static int FigCount;
        private static Rect rect;
        private static Texture2D texture;
        private static bool[] FilledRaw;
        private static bool DoInit;
        private static bool GameOver;
        private static bool NewFigure;
        private static bool DoUpdate;

        /// <summary>
        /// start application
        /// init figures array
        /// allow init
        /// allow new figure
        /// stop updates
        /// game is not over
        /// </summary>
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
            DoInit = true;
            //Init();
            NewFigure = true;
            DoUpdate = false;
            GameOver = false;
        }

        /// <summary>
        /// we redraw glass only if it's not init or end screen
        /// </summary>
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
            else
            {
                Redraw();
            }
        }

        /// <summary>
        /// end screen
        /// stop updating
        /// last glass redraw
        /// and add buttons
        /// </summary>
        void End()
        {
            DoUpdate = false;
            Redraw();
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200));
            GUILayout.Label("Game Over");
            if (GUILayout.Button("Repeat Game"))
            {
                Start();
            }
            if (GUILayout.Button("Exit"))
            {
                Application.Quit();
            }
            GUILayout.EndArea();
        }

        /// <summary>
        /// add buttons
        /// and if game mode selected
        /// calculate and init some values
        /// stop init (allow glass redraw)
        /// allow update
        /// </summary>
        void Init()
        {
            GameMode = 0;
            //var button = Instantiate(Button, Vector3.zero, Quaternion.identity) as Button;
            //var rectTransform = button.GetComponent<RectTransform>();
            //rectTransform.SetParent(Canvas.transform);
            //rectTransform.offsetMin = Vector2.zero;
            //rectTransform.offsetMax = Vector2.zero;
            //button.onClick.AddListener(SpawnPlayer);
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 70, Screen.height / 2 - 70, 140, 140));
            GUILayout.Label("Select Game Mode");
            if (GUILayout.Button("Mode 1"))
                GameMode = 1;
            if (GUILayout.Button("Mode 2"))
                GameMode = 2;
            GUILayout.EndArea();

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
                int ScaleX = Screen.width / (GlassWidth + 2);
                int ScaleY = Screen.height / (GlassHeight + 1);
                Scale = Mathf.Min(ScaleX, ScaleY);
                Glass = new Color[GlassWidth, GlassHeight];
                ShiftX = (Screen.width / Scale - GlassWidth - 2) / 2;
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
            }
        }

        /// <summary>
        /// redraw Glass with walls and bottom
        /// </summary>
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

        /// <summary>
        /// if updates not allowed do nothing
        /// else
        /// drop new figure to glass
        /// or move existing
        /// </summary>
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

        /// <summary>
        /// remove filled rows
        /// select new figure
        /// put it to the top of glass
        /// if it overlay with glass content it's gameover
        /// goto process mode
        /// </summary>
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

        /// <summary>
        /// check if CurrentFig is overlay with glass content
        /// </summary>
        /// <returns>
        /// if something under figure return true
        /// </returns>
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

        /// <summary>
        /// main processing
        /// erase current figure
        /// get user input
        /// try to transform figure
        /// if something goes wrong
        /// rolling back
        /// put figure back
        /// if it can be fixed, goto creation of the new figure
        /// </summary>
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

        /// <summary>
        /// this function will remove filled raws
        /// 1) scan Glass for filled raws
        /// 2) go throw Glass from bottom to top
        /// 3) copy all NotFilledRaws to MovedRaws
        /// 4) add empty lines at the top
        /// </summary>
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

        /// <summary>
        /// this function will transform CurrentFig
        /// with the user input
        /// </summary>
        /// <param name="UserInput">
        /// string with recieved command
        /// </param>
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

        /// <summary>
        /// this function will rollback transformation for CurrentFig
        /// if it cannot be placed after transformation
        /// </summary>
        /// <param name="UserInput">
        /// string with recieved command
        /// </param>
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

        /// <summary>
        /// if direction = -1 it's right rotation
        /// overwise it's left rotation
        /// </summary>
        /// <param name="direction">
        /// if it's = -1 it's right rotation
        /// overwise it's left rotation
        /// </param>
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

        /// <summary>
        /// put figure into glass with color
        /// </summary>
        /// <param name="color">
        /// white color used to erasing figures
        /// </param>
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
