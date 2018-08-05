using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    /// <summary>
    /// class to draw message and three buttons
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        GameObject m_Label1;
        public Button m_Button1, m_Button2, m_Button3;
        public Text label1;
        Button button1, button2, button3;

        /// <summary>
        /// getting components and adding listeners
        /// </summary>
        public void Start()
        {
            m_Label1 = GameObject.Find("Label");
            label1 = m_Label1.GetComponent<Text>();
            button1 = m_Button1.GetComponent<Button>();
            button1.onClick.AddListener(delegate { Tetris.SetMode(1); });
            button2 = m_Button2.GetComponent<Button>();
            button2.onClick.AddListener(delegate { Tetris.SetMode(2); });
            button3 = m_Button3.GetComponent<Button>();
            button3.onClick.AddListener(Exit);
        }

        /// <summary>
        /// quit game
        /// </summary>
        public static void Exit()
        {
            Application.Quit();
        }

        /// <summary>
        /// hide all ui elements
        /// </summary>
        public void Hide()
        {
            label1.transform.localScale = Vector3.zero;
            button1.enabled = false;
            button1.transform.localScale = Vector3.zero;
            button2.enabled = false;
            button2.transform.localScale = Vector3.zero;
            button3.enabled = false;
            button3.transform.localScale = Vector3.zero;
        }

        /// <summary>
        /// show all ui elements
        /// </summary>
        public void Show()
        {
            label1.transform.localScale = Vector3.one;
            button1.enabled = true;
            button1.transform.localScale = Vector3.one;
            button2.enabled = true;
            button2.transform.localScale = Vector3.one;
            button3.enabled = true;
            button3.transform.localScale = Vector3.one;
        }
    }
}
