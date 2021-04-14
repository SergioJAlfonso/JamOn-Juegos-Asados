using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    //ROCK_TYPE type_;
    [SerializeField]
    int speed_ = 0;

    //public Rock(ROCK_TYPE type, int speed, int size)
    //{
    //    type_ = type;
    //    speed_ = speed;
    //    size_ = size;
    //}

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(rb != null){
            rb.velocity = new Vector2(0, -speed_);
        }
    }

    void Update()
    {
        
    }
}
