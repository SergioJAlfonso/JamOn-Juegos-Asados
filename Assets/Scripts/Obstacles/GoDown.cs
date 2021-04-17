using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDown : MonoBehaviour
{
    //ROCK_TYPE type_;
    [SerializeField]
    float speed_;
    float limit_ = -60;
    Rigidbody2D rb;

    private void Start()
    {
        speed_ = GameManager.instance.getSpeed();
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.velocity = new Vector2(0, -speed_);
    }

    void Update()
    {
        if (speed_ != GameManager.instance.getSpeed())
        {
            speed_ = GameManager.instance.getSpeed();
            rb.velocity = new Vector2(0, -speed_);
        }

        if (transform.position.y <= limit_)
        {
            Destroy(this.gameObject);
        }
    }

    //public void initialize(int speed)
    //{
    //    speed_ = speed;

    //    Rigidbody2D rb = GetComponent<Rigidbody2D>();

    //    if(rb != null)
    //        rb.velocity = new Vector2(0, -speed_);
    //}
}
