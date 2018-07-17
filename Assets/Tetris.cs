using UnityEditor;
using UnityEngine;
namespace Tetris
{
    public class NewBehaviourScript : MonoBehaviour
    {
        private readonly Texture2D[] MyTextures = new Texture2D[16];
        public static int FigCount;
        private static int ScreenWidth;
        private static int ScreenHeight;
        private static int GlassWidth;
        private static int GlassHeight;
        private static int[,] Glass;
        private static int Scale;
        private static int CurrentFig;
        private static int PosX;
        private static int PosY;
        private static int Rot;
        // Use this for initialization
        void Start()
        {
            ScreenWidth = Screen.width;
            ScreenHeight = Screen.height;
            DefineTextures();
            if (EditorUtility.DisplayDialog("Game mode selection", "Please, select game mode", "Mode 1", "Mode 2"))
            {
                //GameMode = 1;
                GlassHeight = 20;
                GlassWidth = 10;
                FigCount = 7;
            }
            else
            {
                //GameMode = 2;
                GlassHeight = 12;
                GlassWidth = 20;
                FigCount = 10;
                Tiles.Probabilities[6] = 5.0f;
            }
            int ScaleX = ScreenWidth / GlassWidth;
            int ScaleY = ScreenHeight / GlassHeight;
            Scale = Mathf.Min(ScaleX, ScaleY);
            Glass = new int[GlassWidth, GlassHeight];
            PosX = GlassWidth / 2 - 2;
            PosY = 0;
            Rot = 0;
        }
        void OnGUI()
        {
            //draw glass
            for (int i = 0; i < GlassWidth; i++)
            {
                for (int j = 0; j < GlassHeight; j++)
                {
                    GUI.skin.box.normal.background = MyTextures[Glass[i, j]];
                    GUI.Box(new Rect(i * Scale, j * Scale, Scale, Scale), "");
                }
            }
            //draw tile
            GUI.skin.box.normal.background = MyTextures[CurrentFig + 2];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Tiles.Figures[CurrentFig, j, i] != 0)
                    {
                        GUI.Box(new Rect((PosX + i) * Scale, (PosY + j) * Scale, Scale, Scale), "");
                    }
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            CurrentFig = RandomGenerator.Generate();
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
            //for (int j = 0; j < 4; j++)
            //{
            //    for (int k = 0; k < 4; k++)
            //    {
            //        Field[k + posx, j + posy] -= Tiles.Figures[CurrentFig, j, k];
            //        posy++;
            //        Field[k + posx, j + posy] += Tiles.Figures[CurrentFig, j, k];
            //    }
            //}
        }
        void MoveRight()
        {
            print("MoveRight");
        }
        void MoveLeft()
        {
            print("MoveLeft");
        }
        void DefineTextures()
        {
            //define color palette
            Color[] MyColors = new Color[16]
            {
                Color.white,
                Color.black,
                Color.red,
                Color.green,
                Color.blue,
                Color.cyan,
                Color.magenta,
                Color.yellow,
                new Color(0.75f, 0.75f, 0.75f),
                new Color(0.25f, 0.25f, 0.25f),
                new Color(0.5f, 0.0f, 0.0f),
                new Color(0.0f, 0.5f, 0.0f),
                new Color(0.0f, 0.0f, 0.5f),
                new Color(0.0f, 0.5f, 0.5f),
                new Color(0.5f, 0.0f, 0.5f),
                new Color(0.5f, 0.5f, 0.0f)
            };
            //define and fill textures
            for (int i = 0; i < 16; i++)
            {
                MyTextures[i] = new Texture2D(1, 1);
                MyTextures[i].SetPixel(0, 0, MyColors[i]);
                MyTextures[i].Apply();
            }
        }
    }
}
