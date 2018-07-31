using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    public class MainMenu : MonoBehaviour
    {
        GameObject m_Label1;
        public Button m_Button1, m_Button2, m_Button3;
        public Text label1;
        Button button1, button2, button3;
        Vector3 zeros, ones;

        //
        public void Start()
        {
            m_Label1 = GameObject.Find("Label");
            label1 = m_Label1.GetComponent<Text>();
            button1 = m_Button1.GetComponent<Button>();
            button1.onClick.AddListener(Tetris.SetMode1);
            button2 = m_Button2.GetComponent<Button>();
            button2.onClick.AddListener(Tetris.SetMode2);
            button3 = m_Button3.GetComponent<Button>();
            button3.onClick.AddListener(Exit);
            zeros = new Vector3(0, 0, 0);
            ones = new Vector3(1, 1, 1);
        }

        //
        public static void Exit()
        {
            Application.Quit();
        }

        //
        public void Hide()
        {
            label1.transform.localScale = zeros;
            button1.enabled = false;
            button1.transform.localScale = zeros;
            button2.enabled = false;
            button2.transform.localScale = zeros;
            button3.enabled = false;
            button3.transform.localScale = zeros;
        }

        //
        public void Show()
        {
            label1.transform.localScale = ones;
            button1.enabled = true;
            button1.transform.localScale = ones;
            button2.enabled = true;
            button2.transform.localScale = ones;
            button3.enabled = true;
            button3.transform.localScale = ones;
        }
    }
}
