using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    /// <summary>
    /// class to show main menu
    /// wait for user mode selection
    /// and run game with defined mode
    /// </summary>
    public class Tetris : MonoBehaviour
    {
        static int Mode;
        bool repeat;
        GameObject RootGameObject;
        MainMenu mainmenu;
        Game game;

        /// <summary>
        /// init objects and starting game in the first time
        /// </summary>
        public void Start()
        {
            RootGameObject = GameObject.Find("Root");
            mainmenu = RootGameObject.GetComponent<MainMenu>();
            game = RootGameObject.GetComponent<Game>();
            Run(false);
        }

        /// <summary>
        /// if user selects run another game
        /// it will be run with repetition=true
        /// </summary>
        /// <param name="repetition"></param>
        public void Run(bool repetition)
        {
            repeat = repetition;
        }

        /// <summary>
        /// hide/show main menu, run game, change message for user
        /// </summary>
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
        
        /// <summary>
        /// set gamemode from mainmenu buttons
        /// </summary>
        /// <param name="mode"></param>
        public static void SetMode(int mode)
        {
            Mode = mode;
        }
    }
}
