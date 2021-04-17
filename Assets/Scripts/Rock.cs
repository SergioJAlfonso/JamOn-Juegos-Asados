using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    Transform tr;

    private void Awake()
    {
        tr = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController plC = other.GetComponent<PlayerController>();

        if(plC != null && plC.nextPiece == null)
        {
            Transform oTr = other.GetComponent<Transform>();

            if(oTr.position.x > tr.position.x)
            {
                oTr.position = new Vector3(oTr.position.x + 0.1f, oTr.position.y, oTr.position.y);
            }
            else
                oTr.position = new Vector3(oTr.position.x - 0.1f, oTr.position.y, oTr.position.y);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerController plC = other.GetComponent<PlayerController>();

        if (plC != null && plC.nextPiece == null)
        {
            Transform oTr = other.GetComponent<Transform>();

            if (oTr.position.x > tr.position.x)
            {
                oTr.position = new Vector3(oTr.position.x + 0.1f, oTr.position.y, oTr.position.y);
            }
            else
                oTr.position = new Vector3(oTr.position.x - 0.1f, oTr.position.y, oTr.position.y);
        }
    }
}
