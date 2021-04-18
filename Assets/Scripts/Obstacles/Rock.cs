using DitzeGames.Effects;
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
        CoolBotController boot = other.GetComponent<CoolBotController>();

        if (boot != null && plC.nextPiece == null)
        {
            //other.GetComponent<CoolCarpaBotFollow>().setTarget(GameManager.instance.dumpingObjectTr, 0.0f);
            Transform oTr = other.GetComponent<Transform>();

            if (oTr.position.z >= 0)
            {
                boot.enabled = false;
                Rigidbody2D oRb = other.GetComponent<Rigidbody2D>();
                float distToCenter = oTr.position.x - transform.position.x;
                if (distToCenter > 0)
                {
                    //oTr.position = new Vector3(oTr.position.x + 0.1f, oTr.position.y, oTr.position.y);
                    oRb.velocity = new Vector2(15.0f - distToCenter, 0);
                    /*
                    if (plC.sombra != null)
                    {
                        Rigidbody2D sRb = plC.sombra.GetComponent<Rigidbody2D>();
                        sRb.velocity = new Vector2(15.0f - distToCenter, 0);
                    }
                    */
                }
                else
                {
                    oRb.velocity = new Vector2(-15.0f - distToCenter, 0);
                    /*
                    if (plC.sombra != null)
                    {
                        Rigidbody2D sRb = plC.sombra.GetComponent<Rigidbody2D>();
                        sRb.velocity = new Vector2(-15.0f - distToCenter, 0);
                    }
                    */
                }

                //CameraEffects.ShakeOnce(0.5f, 1.5f, new Vector3(2, 1, 0));
            }
        }
        else if (plC != null && plC.nextPiece == null)
        {
            Transform oTr = other.GetComponent<Transform>();

            if (oTr.position.z >= 0 && !plC.ascending)
            {
                plC.enabled = false;
                Rigidbody2D oRb = other.GetComponent<Rigidbody2D>();
                float distToCenter = oTr.position.x - tr.position.x;
                if (distToCenter > 0)
                {
                    //oTr.position = new Vector3(oTr.position.x + 0.1f, oTr.position.y, oTr.position.y);
                    oRb.velocity = new Vector2(15.0f - distToCenter, 0);
                    if (plC.sombra != null)
                    {
                        Rigidbody2D sRb = plC.sombra.GetComponent<Rigidbody2D>();
                        sRb.velocity = new Vector2(15.0f - distToCenter, 0);
                    }
                }
                else
                {
                    oRb.velocity = new Vector2(-15.0f - distToCenter, 0);
                    if (plC.sombra != null)
                    {
                        Rigidbody2D sRb = plC.sombra.GetComponent<Rigidbody2D>();
                        sRb.velocity = new Vector2(-15.0f - distToCenter, 0);
                    }
                }

                CameraEffects.ShakeOnce(0.5f, 1.5f, new Vector3(2, 1, 0));
            }
        }
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    PlayerController plC = other.GetComponent<PlayerController>();

    //    if (plC != null /*&& plC.nextPiece == null*/)
    //    {
    //        Transform oTr = other.GetComponent<Transform>();

    //        if (oTr.position.z >= 0)
    //        {
    //            plC.enabled = false;
    //            Rigidbody2D oRb = other.GetComponent<Rigidbody2D>();
    //            if (oTr.position.x > tr.position.x)
    //            {
    //                //oTr.position = new Vector3(oTr.position.x + 0.1f, oTr.position.y, oTr.position.y);
    //                oRb.velocity += new Vector2(2.0f, 0);
    //                if (plC.sombra != null)
    //                {
    //                    Rigidbody2D sRb = plC.sombra.GetComponent<Rigidbody2D>();
    //                    sRb.velocity += new Vector2(2.0f, 0);
    //                }
    //            }
    //            else
    //            {
    //                oRb.velocity -= new Vector2(2.0f, 0);
    //                if (plC.sombra != null)
    //                {
    //                    Rigidbody2D sRb = plC.sombra.GetComponent<Rigidbody2D>();
    //                    sRb.velocity -= new Vector2(2.0f, 0);
    //                }
    //            }
    //        }
    //    }
    //}

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController plC = other.GetComponent<PlayerController>();
        CoolBotController boot = other.GetComponent<CoolBotController>();

        if (plC != null)
        {
            plC.enabled = true;
            plC.timeToActive = 0.075f;
        }
        if (boot != null)
            boot.enabled = true;
    }
}
