using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    [FMODUnity.EventRef] string winEvent = "event:/winMusic";
    private FMOD.Studio.EventInstance winMusic;

    [FMODUnity.EventRef] string musicManagerEvent = "event:/corrientes";
    private FMOD.Studio.EventInstance corrienteMusic;

    private void Awake()
    {
        winMusic = FMODUnity.RuntimeManager.CreateInstance(winEvent);
        winMusic.set3DAttributes(RuntimeUtils.To3DAttributes(transform));

        corrienteMusic = FMODUnity.RuntimeManager.CreateInstance(musicManagerEvent);
        corrienteMusic.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
        corrienteMusic.setParameterByName("Distance", GameManager.instance.getDistance());
        corrienteMusic.start();
        corrienteMusic.release();
    }
    private void FixedUpdate()
    {
        corrienteMusic.setParameterByName("Distance", GameManager.instance.getDistance());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Carpa" && this.name == "SpeedBoost(Clone)" && collision.GetComponent<Transform>().position.z == 0)
        {
            winMusic.start();
            winMusic.release();
        }
    }
    //else if (collision.name == "Carpa")
    //    FMODUnity.RuntimeManager.PlayOneShot(route);
}

