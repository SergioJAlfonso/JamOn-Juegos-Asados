using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorFollow : MonoBehaviour
{
    [SerializeField]
    Texture2D cursorTex;
    [SerializeField]
    float FadeOutTime = 1f;
    Vector2 hotspot;

    // Start is called before the first frame update
    void Start()
    {
        hotspot = new Vector2(cursorTex.width / 2, cursorTex.height/2);
        Cursor.SetCursor(cursorTex, hotspot, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        Color fillColor = Color.clear;
        Color[] fillPixels = new Color[cursorTex.width * cursorTex.height];

        for (int i = 0; i < fillPixels.Length; i++)
        {
            fillPixels[i] = fillColor;
        }

        cursorTex.SetPixels(fillPixels);
        Cursor.SetCursor(cursorTex, hotspot, CursorMode.ForceSoftware);
    }
}
