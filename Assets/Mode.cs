using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    public class Mode : MonoBehaviour
    {
        public static GameObject buttonPrefab;
        public static GameObject panelToAttachButtonsTo;
        public static int GameMode;

        public static void SetMode1()
        {
            GameMode = 1;
        }

        public static void SetMode2()
        {
            GameMode = 2;
        }

        public static int Select()
        {
            GameObject myGO;
            GameObject myButton;
            GameObject myText;
            Canvas myCanvas;
            Text text;
            RectTransform rectTransform;
            // Canvas
            myGO = new GameObject();
            myGO.name = "Canvas";
            myCanvas = myGO.AddComponent<Canvas>();
            myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

            // Button
            myButton = new GameObject();
            myButton.name = "Button";
            myButton.transform.parent = myGO.transform;
            Button button = myButton.AddComponent<Button>();
            button.onClick.AddListener(SetMode1);
            var buttonRT = myButton.AddComponent<RectTransform>();
            //buttonRT.anchoredPosition = new Vector3(0, 0, 0);
            //buttonRT.sizeDelta = new Vector2(100, 30);

            // Text
            myText = new GameObject();
            myText.transform.parent = myButton.transform;
            myText.name = "Text";

            text = myText.AddComponent<Text>();
            text.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            text.text = "Mode 1";
            text.fontSize = 20;
            text.alignment = TextAnchor.MiddleCenter;

            // Text position
            rectTransform = text.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(100, 100, 0);
            rectTransform.sizeDelta = new Vector2(100, 30);
            return GameMode;
        }
    }
}
