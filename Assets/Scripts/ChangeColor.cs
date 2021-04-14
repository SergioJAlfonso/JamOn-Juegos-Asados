using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public SpriteRenderer sprite_;
    public Transform transform_;
    int spaceBetweenLayer;
    // Start is called before the first frame update
    void Start()
    {
        sprite_ = GetComponent<SpriteRenderer>();
        transform_ = GetComponent<Transform>();
        spaceBetweenLayer = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform_.position.z >= 0)
            sprite_.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        else if (transform_.position.z < 0 && transform_.position.z > -spaceBetweenLayer)
        {
            sprite_.color = new Color(79.0f/255, 98.0f / 255, 142.0f / 255, 1.0f);
        }
        else if (transform_.position.z < -spaceBetweenLayer && transform_.position.z > -spaceBetweenLayer * 2)
        {
            sprite_.color = new Color(22.0f / 255, 41.0f / 255, 85.0f / 255, 1.0f);
        }
        else if (transform_.position.z < -spaceBetweenLayer * 2 && transform_.position.z > -spaceBetweenLayer * 3)
        {
            sprite_.color = new Color(6.0f / 255, 21.0f / 255, 57.0f / 255, 1.0f);
        }
    }
}
