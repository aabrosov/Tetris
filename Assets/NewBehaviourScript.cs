using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    private int GameMode = 0;
    private static int ScreenWidth = 60;
    private static int ScreenHeight = 30;
    private static int GlassWidth = 15;
    private static int GlassHeight = 15;
    private int[,] Field = new int[ScreenWidth, ScreenHeight];
    
	// Use this for initialization
	void Start () {
        if (EditorUtility.DisplayDialog("Game mode selection", "Please, select game mode", "Mode 1", "Mode 2"))
        {
            GameMode = 1;
            GlassHeight = 20;
            GlassWidth = 10;
        }
        else
        {
            GameMode = 2;
            GlassHeight = 12;
            GlassWidth = 20;
        }
        //fix glass size if it's too much
        if (GlassHeight > ScreenHeight - 2)
            GlassHeight = ScreenHeight - 2;
        if (GlassWidth > ScreenWidth - 2)
            GlassWidth = ScreenWidth - 2;
        //margin to center glass
        int Margin = (ScreenWidth - GlassWidth) / 2;
        //draw glass walls
        for (int k = ScreenHeight - GlassHeight - 1; k < ScreenHeight; k++)
        {
            Field[Margin - 1, k] = 2;
            Field[GlassWidth + Margin, k] = 2;
        }
        //draw glass bottom
        for (int k = Margin - 1; k < GlassWidth + Margin; k++)
            Field[k, ScreenHeight - 1] = 2;
        Field[30, 15] = 1;
    }
    void OnGUI()
    {
        Color MyColor = Color.green;
        if (GameMode == 1)
            MyColor = Color.red;
        else if (GameMode == 2)
            MyColor = Color.yellow;
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.black);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        for (int i = 0; i < ScreenWidth; i++)
            for (int j = 0; j < ScreenHeight; j++)
            {
                if (Field[i, j] == 2)
                {
                    GUI.Box(new Rect(i * 20, j * 20, 18, 18), "");
                }
            }
        texture.SetPixel(0, 0, Color.green);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        for (int i = 0; i < ScreenWidth; i++)
            for (int j = 0; j < ScreenHeight; j++)
            {
                if (Field[i, j] == 1)
                {
                    GUI.Box(new Rect(i * 20, j * 20, 18, 18), "");
                }
            }
    }
    // Update is called once per frame
    void Update () {
    }
}
