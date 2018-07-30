using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    public class MainMenu : MonoBehaviour
    {
        public Button m_Mode1, m_Mode2;
        GameObject myGO;
        Tetris tetris;

        public static int GameMode;

        public void Start()
        {
            Button button1 = m_Mode1.GetComponent<Button>();
            button1.onClick.AddListener(SetMode1);
            Button button2 = m_Mode2.GetComponent<Button>();
            button2.onClick.AddListener(SetMode2);
            myGO = GameObject.Find("GameObject");
            tetris = myGO.GetComponent<Tetris>();
        }

        void Update()
        {
            //print("Hello from main menu");
            if (GameMode == 1 || GameMode == 2)
            {
                //print("gamemode = 1 or 2");
                tetris.GameStart(GameMode);
            }
        }

        public static void SetMode1()
        {
            //print("mode1");
            GameMode = 1;
        }

        public static void SetMode2()
        {
            //print("mode2");
            GameMode = 2;
        }
    }
}
