using UnityEditor;
using UnityEngine;
namespace Tetris
{
    public class Select
    {
        public static int Figure()
        {
            float rnd = Random.Range(0.0f, 100.0f);
            float LimitLeft = 0.0f;
            float LimitRight = 0.0f;
            for (int i = 0; i < Tetris.FigCount; i++)
            {
                LimitLeft = LimitRight;
                LimitRight = LimitLeft + Tetris.Figures[i].probability;
                if (rnd > LimitLeft & rnd < LimitRight)
                {
                    return i;
                }
            }
            return -1;
        }
    }
    public class Figure
    {
        public Color color;
        public int[,] tiles;
        public int probability;
        public Figure(Color color, int[,] tiles, int probability)
        {
            this.color = color;
            this.tiles = tiles;
            this.probability = probability;
        }
    }
    public class Position
    {
        public int x;
        public int y;
        public int r;
        public Position() { }
        public Position(int x, int y, int r)
        {
            this.x = x;
            this.y = y;
            this.r = r;
        }
    }
    public class Tetris : MonoBehaviour
    {
        private static int ScreenWidth;
        private static int ScreenHeight;
        private static int GlassWidth;
        private static int GlassHeight;
        private static int[,] Glass;
        private static int[,] Figure;
        private static int[,] Moved;
        private static int Scale;
        private static int CurrentFig;
        //private static int PosX;
        //private static int PosY;
        private static Position before = new Position();
        public static Figure[] Figures = {
                new Figure( new Color(1.0f,0.0f,0.0f), new int[,] {
                    {0,0,0,0},
                    {0,1,1,0},
                    {0,1,1,0},
                    {0,0,0,0}
                }, 10 ),
                new Figure( new Color(0.0f,1.0f,0.0f), new int[,] {{0,0,0,0},{0,0,1,0},{0,1,1,0},{0,1,0,0}}, 15 ),
                new Figure( new Color(0.0f,0.0f,1.0f), new int[,] {{0,0,0,0},{0,1,0,0},{0,1,1,0},{0,0,1,0}}, 15 ),
                new Figure( new Color(0.0f,1.0f,1.0f), new int[,] {{0,0,0,0},{0,1,1,0},{0,0,1,0},{0,0,1,0}}, 15 ),
                new Figure( new Color(1.0f,0.0f,1.0f), new int[,] {{0,0,0,0},{0,0,1,0},{0,0,1,0},{0,1,1,0}}, 15 ),
                new Figure( new Color(1.0f,1.0f,0.0f), new int[,] {{0,0,1,0},{0,0,1,0},{0,0,1,0},{0,0,1,0}}, 10 ),
                new Figure( new Color(0.5f,1.0f,1.0f), new int[,] {{0,0,0,0},{0,0,1,0},{0,1,1,0},{0,0,1,0}}, 20 ),
                new Figure( new Color(1.0f,0.5f,1.0f), new int[,] {{0,0,0,0},{0,1,0,0},{1,1,1,0},{0,1,0,0}},  5 ),
                new Figure( new Color(1.0f,1.0f,0.5f), new int[,] {{0,0,0,0},{0,1,1,0},{0,1,0,0},{0,1,1,0}},  5 ),
                new Figure( new Color(1.0f,0.5f,0.5f), new int[,] {{0,0,0,0},{0,0,0,1},{0,0,1,1},{0,1,1,0}},  5 )
            };
        public static int FigCount;
        public Texture2D[] MyTextures;
        // Use this for initialization
        void Start()
        {
            ScreenWidth = Screen.width;
            ScreenHeight = Screen.height;
            if (EditorUtility.DisplayDialog("Game mode selection", "Please, select game mode", "Mode 1", "Mode 2"))
            {
                //GameMode 1
                GlassHeight = 20;
                GlassWidth = 10;
                FigCount = 7;
            }
            else
            {
                //GameMode 2
                GlassHeight = 12;
                GlassWidth = 20;
                FigCount = 10;
                Figures[6].probability = 5;
            }
            //define and fill textures
            MyTextures = new Texture2D[FigCount];
            for (int i = 0; i < FigCount; i++)
            {
                MyTextures[i] = new Texture2D(1, 1);
                MyTextures[i].SetPixel(0, 0, Figures[i].color);
                MyTextures[i].Apply();
            }
            int ScaleX = ScreenWidth / GlassWidth;
            int ScaleY = ScreenHeight / GlassHeight;
            Scale = Mathf.Min(ScaleX, ScaleY);
            Glass = new int[GlassWidth, GlassHeight];
            Position position = new Position(GlassWidth / 2 - 2, 0, 0);
            CurrentFig = Select.Figure();
            InitFigure(position, CurrentFig);
        }
        void OnGUI()
        {
            //draw Glass and Figure
            for (int i = 0; i < GlassWidth; i++)
            {
                for (int j = 0; j < GlassHeight; j++)
                {
                    if (Glass[i, j] != 0)
                    {
                        GUI.skin.box.normal.background = MyTextures[Glass[i, j]];
                        GUI.Box(new Rect(i * Scale, j * Scale, Scale, Scale), "");
                    }
                    if (Figure[i, j] != 0)
                    {
                        GUI.skin.box.normal.background = MyTextures[CurrentFig];
                        GUI.Box(new Rect(i * Scale, j * Scale, Scale, Scale), "");
                    }
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            //Moved = new int[GlassWidth, GlassHeight];
            Position after = new Position();
            after = before;
            if (Input.GetKeyDown("up"))
                after.r += 90;
            else if (Input.GetKeyDown("a"))
                after.r -= 90;
            else if (Input.GetKeyDown("d"))
                after.r += 90;
            else if (Input.GetKeyDown("down"))
                after.y += 1;
            else if (Input.GetKeyDown("left"))
                after.x -= 1;
            else if (Input.GetKeyDown("right"))
                after.x += 1;
            InitFigure(after, CurrentFig);
        }
        void InitFigure(Position pos, int CurrentFig)
        {
            Figure = new int[GlassWidth, GlassHeight];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Figure[i + pos.x, j + pos.y] = Rotated(pos.r, GetFigure(CurrentFig))[i,j];
                }
            }
        }
        int[,] GetFigure(int CurrentFig)
        {
            int[,] Figure = new int[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Figure[i, j] = Figures[CurrentFig].tiles[i, j];
                }
            }
            return Figure;
        }
        int[,] Rotated(int angle, int[,] figure)
        {
            angle = angle % 360;
            if (angle == 0)
                return figure;
            int [,] Temp = new int[4, 4];
            if (angle == 90)
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        Temp[i, j] = figure[j, 3 - i];
            if (angle == 180)
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        Temp[i, j] = figure[3 - i, 3 - j];
            if (angle == 270)
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        Temp[i, j] = figure[3 - j, i];
            return Temp;
        }
    }
}
