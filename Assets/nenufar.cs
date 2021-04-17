using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nenufar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<ArrayDePecesQueTeSiguen>() || collision.GetComponent<CarpaBotFollow>() || collision.GetComponent<CoolCarpaBotFollow>()) //todos los posibles elementos que tienen las cabezas de los peces
        {//es la cabeza del jugador
            Vector2 direction = (collision.gameObject.transform.position - transform.position) * GameManager.instance.getSpeed() * 15; ;
            //direction.Normalize();

            //segun si la x es positiva o negativa giramos izquierda o derecha, con
            gameObject.GetComponent<Rigidbody2D>().angularVelocity = direction.x;
            Debug.Log("colision carpa");
        }
        else Debug.Log("colision");
    }

}
