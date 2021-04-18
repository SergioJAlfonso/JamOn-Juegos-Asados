using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSound : MonoBehaviour
{
    private FMOD.Studio.EventInstance evento;
    [FMODUnity.EventRef] [SerializeField] string ruta;
    public bool playOnAwake;
    void Awake()
    {
        evento = FMODUnity.RuntimeManager.CreateInstance(ruta);
        evento.set3DAttributes(RuntimeUtils.To3DAttributes(this.gameObject.transform));
        evento.start();
        evento.release();

        //if (playOnAwake)
        //    playSound();
    }
 
    //public void playSound()
    //{
       
    //}
    //public void disableSound()
    //{
    //    //musicMusic.setVolume(0);
    //}
}
