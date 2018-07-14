using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    private int GameMode = 0;
    private static readonly int ScreenWidth = 60;
    private static readonly int ScreenHeight = 30;
    private static int GlassWidth = 15;
    private static int GlassHeight = 15;
    private readonly int[,] Field = new int[ScreenWidth, ScreenHeight];
    
	// Use this for initialization
	void Start ()
    {
        Random rnd = new Random();
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
        {
            GlassHeight = ScreenHeight - 2;
        }
        if (GlassWidth > ScreenWidth - 2)
        {
            GlassWidth = ScreenWidth - 2;
        }
        //margin to center glass
        int Margin = (ScreenWidth - GlassWidth) / 2;
        //draw glass walls
        for (int k = ScreenHeight - GlassHeight - 1; k < ScreenHeight; k++)
        {
            Field[Margin - 1, k] = 1;
            Field[GlassWidth + Margin, k] = 1;
        }
        //draw glass bottom
        for (int k = Margin - 1; k < GlassWidth + Margin; k++)
        {
            Field[k, ScreenHeight - 1] = 1;
        }
        //add palette
        for (int k = 0; k < 16; k++)
        {
            Field[k, 0] = k;
        }
    }
    void OnGUI()
    {
        //color table
        // 0 - white
        // 1 - black
        // 2 - red
        // 3 - green
        // 4 - blue
        // 5 - cyan
        // 6 - magenta
        // 7 - yellow
        // 8 - lightgray
        // 9 - darkgray
        // 10 - darkred
        // 11 - darkgreen
        // 12 - darkblue
        // 13 - darkcyan
        // 14 - darkmagenta
        // 15 - darkyellow
        Texture2D[] MyTextures = new Texture2D[16];
        Color[] MyColors = new Color[16] {
            Color.white, Color.black, Color.red, Color.green,
            Color.blue, Color.cyan, Color.magenta, Color.yellow,
            new Color(0.75f, 0.75f, 0.75f),
            new Color(0.25f, 0.25f, 0.25f),
            new Color(0.5f, 0.0f, 0.0f),
            new Color(0.0f, 0.5f, 0.0f),
            new Color(0.0f, 0.0f, 0.5f),
            new Color(0.0f, 0.5f, 0.5f),
            new Color(0.5f, 0.0f, 0.5f),
            new Color(0.5f, 0.5f, 0.0f)
        };
        //define and fill textures
        for (int i = 0; i < 16; i++)
        {
            MyTextures[i] = new Texture2D(1, 1);
            MyTextures[i].SetPixel(0, 0, MyColors[i]);
            MyTextures[i].Apply();
        }
        //draw field
        for (int i = 0; i < ScreenWidth; i++)
            for (int j = 0; j < ScreenHeight; j++)
            {
                if (Field[i, j] != 0)
                {
                    GUI.skin.box.normal.background = MyTextures[Field[i, j]];
                    GUI.Box(new Rect(i * 20, j * 20, 19, 19), "");
                }
            }
    }
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown("up"))
        {
            //print("up key was pressed");
            Field[5, 5] = 5;
        }
        else if (Input.GetKeyDown("down"))
        {
            //print("down key was pressed");
            Field[4, 4] = 4;
        }
        else if (Input.GetKeyDown("right"))
        {
            //print("right key was pressed");
            Field[3, 3] = 3;
        }
        else if (Input.GetKeyDown("left"))
        {
            //print("left key was pressed");
            Field[2, 2] = 2;
        }

    }
}
