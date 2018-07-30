using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    public class ExitMenu : MonoBehaviour
    {
        public Button m_Repeat, m_Exit;
        GameObject myGO;
        MainMenu mainmenu;

        public static int GameMode;

        public void Start()
        {
            Button button1 = m_Repeat.GetComponent<Button>();
            button1.onClick.AddListener(Repeat);
            Button button2 = m_Exit.GetComponent<Button>();
            button2.onClick.AddListener(Exit);
            myGO = GameObject.Find("GameObject");
            mainmenu = myGO.GetComponent<MainMenu>();
        }

        void Update()
        {
        }

        public void Repeat()
        {
            mainmenu.Start();
        }

        public static void Exit()
        {
            //print("mode2");
            Application.Quit();
        }
    }
}
