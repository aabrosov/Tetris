using UnityEngine;
using UnityEngine.UI;
namespace Tetris
{
    public class Tetris : MonoBehaviour
    {
        public static int GameMode;
        public GameObject myGO;
        public enum MenuType { Main, Exit }
        private MenuType menuType;
        Game game;
        void Start()
        {
            myGO = GameObject.Find("GameObject");
            var mainmenu = myGO.GetComponent<MainMenu>();
            mainmenu.Show();
            menuType = MenuType.Main;
        }
        public void ReStart()
        {
            myGO = GameObject.Find("GameObject");
            var exitmenu = myGO.GetComponent<ExitMenu>();
            exitmenu.Show();
            menuType = MenuType.Exit;
        }
        void Update()
        {
            if (GameMode == 1 || GameMode == 2)
            {
                if (menuType == MenuType.Main)
                {
                    var mainmenu = myGO.GetComponent<MainMenu>();
                    mainmenu.Hide();
                }
                else if (menuType == MenuType.Exit)
                {
                    var exitmenu = myGO.GetComponent<ExitMenu>();
                    exitmenu.Hide();
                }
                //Destroy(mainmenu);
                var game = myGO.GetComponent<Game>();
                game.Start();
                GameMode = 0;
            }
        }
        public static void SetMode1()
        {
            GameMode = 1;
        }
        public static void SetMode2()
        {
            GameMode = 2;
        }
    }
}
