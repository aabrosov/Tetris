using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    public class Tetris : MonoBehaviour
    {
        static int Mode;
        bool repeat;
        GameObject RootGameObject;
        MainMenu mainmenu;
        Game game;

        //
        public void Start()
        {
            RootGameObject = GameObject.Find("Root");
            mainmenu = RootGameObject.GetComponent<MainMenu>();
            game = RootGameObject.GetComponent<Game>();
            Run(false);
        }

        //
        public void Run(bool repetition)
        {
            repeat = repetition;
        }

        //
        void Update()
        {
            if (repeat == true)
            {
                mainmenu.label1.text = "Game Over. Repeat?";
                mainmenu.Show();
            }
            if (Mode == 1 || Mode == 2)
            {
                mainmenu.Hide();
                game.Run(Mode);
                Mode = 0;
                repeat = false;
            }
        }
        
        //
        public static void SetMode(int mod)
        {
            Mode = mod;
        }
    }
}
