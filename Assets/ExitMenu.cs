using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    public class ExitMenu : Tetris
    {
        public Button m_Repeat1, m_Repeat2, m_Exit;
        //GameObject myGO;

        //public static int GameMode;

        public void Start()
        {
            Hide();
            Button button1 = m_Repeat1.GetComponent<Button>();
            button1.onClick.AddListener(Tetris.SetMode1);
            button1.onClick.AddListener(Game.SetMode1);
            Button button2 = m_Repeat2.GetComponent<Button>();
            button2.onClick.AddListener(Tetris.SetMode2);
            button2.onClick.AddListener(Game.SetMode2);
            Button button3 = m_Exit.GetComponent<Button>();
            button3.onClick.AddListener(Exit);
        }

        void Update()
        {
        }

        public static void Exit()
        {
            Application.Quit();
        }

        public void Hide()
        {
            myGO = GameObject.Find("GameOver");
            var label = myGO.GetComponent<Text>();
            label.transform.localScale = new Vector3(0, 0, 0);

            myGO = GameObject.Find("Repeat1");
            var button1 = myGO.GetComponent<Button>();
            button1.enabled = false;
            button1.transform.localScale = new Vector3(0, 0, 0);

            myGO = GameObject.Find("Repeat2");
            var button2 = myGO.GetComponent<Button>();
            button2.enabled = false;
            button2.transform.localScale = new Vector3(0, 0, 0);

            myGO = GameObject.Find("Exit");
            var button3 = myGO.GetComponent<Button>();
            button3.enabled = false;
            button3.transform.localScale = new Vector3(0, 0, 0);
        }

        public void Show()
        {
            myGO = GameObject.Find("GameOver");
            var label = myGO.GetComponent<Text>();
            label.transform.localScale = new Vector3(1, 1, 1);

            myGO = GameObject.Find("Repeat1");
            var button1 = myGO.GetComponent<Button>();
            button1.enabled = true;
            button1.transform.localScale = new Vector3(1, 1, 1);

            myGO = GameObject.Find("Repeat2");
            var button2 = myGO.GetComponent<Button>();
            button2.enabled = true;
            button2.transform.localScale = new Vector3(1, 1, 1);

            myGO = GameObject.Find("Exit");
            var button3 = myGO.GetComponent<Button>();
            button3.enabled = true;
            button3.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
