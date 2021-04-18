using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDown : MonoBehaviour
{


    //ROCK_TYPE type_;
    [SerializeField]
    float speed_;
    [SerializeField]
    bool destroyable = true;
    [SerializeField]
    bool particle = false;
    float limit_ = -60;
    Rigidbody2D rb;


    [FMODUnity.EventRef] string musicManagerEvent = "event:/Waterfall";
    private FMOD.Studio.EventInstance corrienteMusic;




    private void Start()
    {
        corrienteMusic = FMODUnity.RuntimeManager.CreateInstance(musicManagerEvent);
        corrienteMusic.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
        corrienteMusic.setParameterByName("Distance", GameManager.instance.getDistance());
        if (this.name == "Cascada" || this.name == "Cascada (1)")
        {
            corrienteMusic.start();
            corrienteMusic.release();
        }
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            if (destroyable)
            {
                speed_ = GameManager.instance.getSpeed();
                rb.velocity = new Vector2(0, -speed_);
            }
            else
            {
                speed_ = GameManager.instance.getMinSpeed();
                rb.velocity = new Vector2(0, -speed_);

            }
        }

    }

    void Update()
    {
        corrienteMusic.setParameterByName("Distance", GameManager.instance.getDistance());
        if (speed_ != GameManager.instance.getSpeed() && destroyable)
        {
            speed_ = GameManager.instance.getSpeed();
            rb.velocity = new Vector2(0, -speed_);
        }

        if (GameManager.instance.getRecovery() && destroyable)
        {
            Destroy(this.gameObject);
        }

        if (transform.position.y < limit_)
        {
            if (!particle)
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
