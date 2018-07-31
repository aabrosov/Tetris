using UnityEngine;
using UnityEngine.UI;
namespace Tetris
{
    public class Tetris : MonoBehaviour
    {
        public static int Mode;
        public int MenuType;
        GameObject RootGameObject;
        MainMenu mainmenu;
        Game game;

        //
        public void Start()
        {
            RootGameObject = GameObject.Find("Root");
            mainmenu = RootGameObject.GetComponent<MainMenu>();
            MenuType = 1;
        }

        //
        public void ReStart()
        {
            MenuType = 2;
        }

        //
        void Update()
        {
            if (MenuType == 1)
            {
                mainmenu.Show();
            }
            else if (MenuType == 2)
            {
                mainmenu.Show();
                mainmenu.label1.text = "Game Over. Repeat?";
            }
            if (Mode == 1 || Mode == 2)
            {
                mainmenu.Hide();
                Game.GameStart(Mode);
                Mode = 0;
                MenuType = 0;
            }
        }
        
        //
        public static void SetMode1()
        {
            Mode = 1;
        }
        
        //
        public static void SetMode2()
        {
            Mode = 2;
        }
    }
}
