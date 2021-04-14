using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    //public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startPos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        float temp = (transform.position.y * (1 - parallaxEffect));

        //Si está en el menu de Play, el parallax se mueve la mitad de rapido
        if (PlayMenu.isPlaying)
            transform.Translate(Vector2.down * parallaxEffect * Time.smoothDeltaTime);
        else
            transform.Translate(Vector2.down * (parallaxEffect / 2) * Time.smoothDeltaTime);


        if (transform.position.y < startPos - length) transform.position = new Vector2(0, startPos);
    }
}
