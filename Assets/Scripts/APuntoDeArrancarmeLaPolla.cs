using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APuntoDeArrancarmeLaPolla : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float mi_Puta_Madre;
    [SerializeField]
    float mi_Puta_Madre_2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * mi_Puta_Madre * Time.smoothDeltaTime);

        float pollaDeGuille = player.position.x - transform.position.x;

        if (player != null)
            transform.Translate(Vector2.left * pollaDeGuille * 0.01f * mi_Puta_Madre_2* Time.smoothDeltaTime);


    }
}
