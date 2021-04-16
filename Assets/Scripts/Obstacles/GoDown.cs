using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDown : MonoBehaviour
{
    //ROCK_TYPE type_;
    [SerializeField]
    int speed_ = 0, limit_ = -10;

    void Update()
    {
        if (transform.position.y <= limit_)
            Destroy(this.gameObject);
    }

    public void initialize(int speed)
    {
        speed_ = speed;


        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if(rb != null)
            rb.velocity = new Vector2(0, -speed_);
    }
}
