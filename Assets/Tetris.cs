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
    public class Tetris : MonoBehaviour
    {
        private static int ScreenWidth;
        private static int ScreenHeight;
        private static int GlassWidth;
        private static int GlassHeight;
        private static int[,] Glass;
        private static int[,] Figure;
        private static int Scale;
        private static int CurrentFig;
        private static int PosX;
        private static int PosY;
        public static Figure[] Figures = {
                new Figure( new Color(1.0f,0.0f,0.0f), new int[,] {{0,0,0,0},{0,1,1,0},{0,1,1,0},{0,0,0,0}}, 10 ),
                new Figure( new Color(0.0f,1.0f,0.0f), new int[,] {{0,0,0,0},{1,1,0,0},{0,1,1,0},{0,0,0,0}}, 15 ),
                new Figure( new Color(0.0f,0.0f,1.0f), new int[,] {{0,0,0,0},{0,1,1,0},{1,1,0,0},{0,0,0,0}}, 15 ),
                new Figure( new Color(0.0f,1.0f,1.0f), new int[,] {{0,0,0,0},{0,0,1,0},{1,1,1,0},{0,0,0,0}}, 15 ),
                new Figure( new Color(1.0f,0.0f,1.0f), new int[,] {{0,0,0,0},{1,0,0,0},{1,1,1,0},{0,0,0,0}}, 15 ),
                new Figure( new Color(1.0f,1.0f,0.0f), new int[,] {{0,0,0,0},{0,0,0,0},{1,1,1,1},{0,0,0,0}}, 10 ),
                new Figure( new Color(0.5f,1.0f,1.0f), new int[,] {{0,0,0,0},{0,1,0,0},{1,1,1,0},{0,0,0,0}}, 20 ),
                new Figure( new Color(1.0f,0.5f,1.0f), new int[,] {{0,0,0,0},{0,1,0,0},{1,1,1,0},{0,1,0,0}},  5 ),
                new Figure( new Color(1.0f,1.0f,0.5f), new int[,] {{0,0,0,0},{1,1,1,0},{1,0,1,0},{0,0,0,0}},  5 ),
                new Figure( new Color(1.0f,0.5f,0.5f), new int[,] {{0,0,0,0},{1,0,0,0},{1,1,0,0},{0,1,1,0}},  5 )
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
            Figure = new int[GlassWidth, GlassHeight];
            PosX = GlassWidth / 2 - 2;
            PosY = 0;
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
            CurrentFig = Select.Figure();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Figure[i + PosX, j + PosY] = Figures[CurrentFig].tiles[i, j];
                }
            }
            if (Input.GetKeyDown("up"))
                Rotate();
            else if (Input.GetKeyDown("down"))
                MoveDown();
            else if (Input.GetKeyDown("right"))
                MoveRight();
            else if (Input.GetKeyDown("left"))
                MoveLeft();
            System.Threading.Thread.Sleep(1000);
            //MoveDown();
        }
        void Rotate()
        {
            print("Rotate");
        }
        void MoveDown()
        {
            print("MoveDown");
        }
        void MoveRight()
        {
            print("MoveRight");
        }
        void MoveLeft()
        {
            print("MoveLeft");
        }
    }
}
