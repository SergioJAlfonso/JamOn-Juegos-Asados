using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockParallax : MonoBehaviour
{
    
    Transform player;
    [SerializeField]
    float parallaxVerticalFactor;
    [SerializeField]
    float parallaxHorizontalFactor;
    
    Rigidbody2D playerRb;

    float startPos;


    // Start is called before the first frame update

    void Start()
    {
        player = GameManager.instance.playerTr;
        playerRb = GameManager.instance.playerRb;
        startPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento de capas en vertical
        float difY = Math.Abs(player.position.y - transform.position.y);        

        if(difY < 15)
        {
            transform.Translate(Vector2.down * parallaxVerticalFactor * -difY * 0.02f * Time.smoothDeltaTime);
        }
        //Movimiento en capas horizontal
        if(playerRb != null)
        {
            //Diferencia entre el jugador y la roca 
            float dif = player.position.x - GetComponent<Transform>().position.x;
            //COMPROBAMOS QUE LA POSICION DEL PADRE Y DE LA DEL PLAYER SEAN CERCANAS
            if(dif > -7 && dif < 7)
            {
                if (transform.position.x < startPos + 1 && transform.position.x > startPos - 1)
                {
                    //Derecha
                    if (playerRb.velocity.x > 0)
                    {
                        transform.Translate(Vector2.left * 0.2f * parallaxHorizontalFactor * Time.smoothDeltaTime);
                    }
                    //Izquierda
                    else
                    {
                        transform.Translate(Vector2.left * 0.2f * -parallaxHorizontalFactor * Time.smoothDeltaTime);
                    }
                }
                //demasiado derecha
                else if (transform.position.x > startPos + 0.5)
                {
                    //izquierda
                    if (parallaxHorizontalFactor > 0)
                        transform.Translate(Vector2.left * 0.2f * parallaxHorizontalFactor * Time.smoothDeltaTime);
                    else
                        transform.Translate(Vector2.left * 0.2f * -parallaxHorizontalFactor * Time.smoothDeltaTime);


                }
                //demasiado izquierda
                else if (transform.position.x < startPos - 0.5)
                {
                    //transform.Translate(Vector2.left * 0.5f * -parallaxHorizontalFactor * Time.smoothDeltaTime);

                    //izquierda
                    if (parallaxHorizontalFactor > 0)
                        transform.Translate(Vector2.left * 0.2f * -parallaxHorizontalFactor * Time.smoothDeltaTime);
                    else
                        transform.Translate(Vector2.left * 0.2f * parallaxHorizontalFactor * Time.smoothDeltaTime);

                }
            }
            

        }

    }
}
