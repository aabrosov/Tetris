using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    public class MainMenu : Tetris
    {
        public Button m_Mode1, m_Mode2;
        //GameObject myGO;

        //public static int GameMode;

        public void Start()
        {
            Hide();
            Button button1 = m_Mode1.GetComponent<Button>();
            button1.onClick.AddListener(Tetris.SetMode1);
            button1.onClick.AddListener(Game.SetMode1);
            Button button2 = m_Mode2.GetComponent<Button>();
            button2.onClick.AddListener(Tetris.SetMode2);
            button2.onClick.AddListener(Game.SetMode2);
        }

        void Update()
        {
        }

        public void Hide()
        {
            myGO = GameObject.Find("GameMode");
            var label1 = myGO.GetComponent<Text>();
            label1.transform.localScale = new Vector3(0, 0, 0);

            myGO = GameObject.Find("Mode1");
            var button1 = myGO.GetComponent<Button>();
            button1.enabled = false;
            button1.transform.localScale = new Vector3(0, 0, 0);

            myGO = GameObject.Find("Mode2");
            var button2 = myGO.GetComponent<Button>();
            button2.enabled = false;
            button2.transform.localScale = new Vector3(0, 0, 0);
        }

        public void Show()
        {
            myGO = GameObject.Find("GameMode");
            var label1 = myGO.GetComponent<Text>();
            label1.transform.localScale = new Vector3(1, 1, 1);

            myGO = GameObject.Find("Mode1");
            var button1 = myGO.GetComponent<Button>();
            button1.enabled = true;
            button1.transform.localScale = new Vector3(1, 1, 1);

            myGO = GameObject.Find("Mode2");
            var button2 = myGO.GetComponent<Button>();
            button2.enabled = true;
            button2.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
