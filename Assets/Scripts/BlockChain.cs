using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChain : MonoBehaviour
{
    public Transform target;
    public float speed = 1.0f;
    public float minDistance = 0.3f;
    Vector3 DistancefromTarget = new Vector3(0,0,0);
    Vector3 direction;
    // Animator m_Animator;
    void Start()
    {
        //followers[0] = player;
        //m_Animator = GetComponent<Animator>();
    }



    void Follow()
    {
        //a ver mamerto, si la distancia es no muy chikita te apegas al bicho, se mueve en el eje z
       // transform.LookAt(player);
        if (DistancefromTarget.magnitude > minDistance)
        {
            transform.Translate(speed * Time.deltaTime * -direction.x,
                                speed * Time.deltaTime * -direction.y,
                                0.0f);
        }
    }

    private void FixedUpdate()
    {
        DistancefromTarget = transform.position - target.position;
        direction = DistancefromTarget.normalized;
    }

    void LateUpdate()
    {
        Follow();
        
    }
}