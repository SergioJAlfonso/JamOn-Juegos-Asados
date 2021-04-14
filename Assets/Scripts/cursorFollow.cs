using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorFollow : MonoBehaviour
{
    [SerializeField]
    Texture2D cursorTex;
    [SerializeField]
    float maxHideTime = 5;
    float hideTime;
    Vector2 hotspot;

    // Start is called before the first frame update
    void Start()
    {
        hideTime = maxHideTime;
        Cursor.visible = true;
        hotspot = new Vector2(cursorTex.width / 2, cursorTex.height / 2);
        Cursor.SetCursor(cursorTex, hotspot, CursorMode.ForceSoftware);
        //StartCoroutine(ShowCursorAfter(HideTime));
    }

    private void Update()
    {
        // Is true when the mouse has moved
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            Cursor.visible = true;
            hideTime = maxHideTime;
        }
        else
            //Cursor.visible = false;
            DontShowCursorAfter();
        //StartCoroutine(DontShowCursorAfter(HideTime));
    }

    /*IEnumerator*/
    private void DontShowCursorAfter()
    {
        hideTime -= Time.deltaTime;

        if (hideTime <= 0.0f)
        {
            Cursor.visible = false;
        }

    } 
}

