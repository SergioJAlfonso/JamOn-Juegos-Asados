using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    [FMODUnity.EventRef] [SerializeField] string route;
    private FMOD.Studio.EventInstance eventMusic;

    private void Awake()
    {
        eventMusic = FMODUnity.RuntimeManager.CreateInstance(route);
        eventMusic.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Carpa" && this.name == "SpeedBoost(Clone)" && collision.GetComponent<Transform>().position.z == 0)
        {
            eventMusic.setParameterByName("isDragon", GameManager.instance.fishState);
            eventMusic.start();
            eventMusic.release();
        }
    }
    //else if (collision.name == "Carpa")
    //    FMODUnity.RuntimeManager.PlayOneShot(route);
}

