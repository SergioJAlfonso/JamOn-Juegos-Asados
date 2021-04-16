using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] float z = -10;
    [SerializeField] float y = -10;


    void FixedUpdate()
    {
        if (player != null)
        {
            // Coge la posici�n del jugador
            Vector3 playerPos = new Vector3(player.transform.position.x, y, player.transform.position.z + z);

            // Si el jugador est� activo (vivo), la c�mara le sigue en un intervalo de tiempo dado por delay
            if (player.gameObject.activeSelf)
                transform.position = playerPos;
        }
    }
}
