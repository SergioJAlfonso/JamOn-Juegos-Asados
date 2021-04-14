using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    //ROCK_TYPE type_;
    [SerializeField]
    int speed_ = 0, limit_ = -10;

    //public Rock(ROCK_TYPE type, int speed, int size)
    //{
    //    type_ = type;
    //    speed_ = speed;
    //  
    //}

    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.y <= limit_)
            Destroy(this.gameObject);
    }

    public void initialize(int speed, float size)
    {
        speed_ = speed;


        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if(rb != null)
            rb.velocity = new Vector2(0, -speed_);
        

        transform.localScale.Set(size, size, 0);
    }
}
