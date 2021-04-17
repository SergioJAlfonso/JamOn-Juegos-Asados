using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotClearColision : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        CoolBotController boot = other.GetComponent<CoolBotController>();

        if (boot != null /*&& plC.nextPiece == null*/)
        {
            other.GetComponent<CoolCarpaBotFollow>().setTarget(GameManager.instance.dumpingObjectTr, 0.0f);
            
        }
    }
}
