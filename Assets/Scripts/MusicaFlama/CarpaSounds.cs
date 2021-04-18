using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CarpaSounds : MonoBehaviour
{
    [FMODUnity.EventRef] [SerializeField] string route;
    private FMOD.Studio.EventInstance eventMusic;

    Transform tr;
    bool control = true;

    void Awake()
    {
        tr = GetComponent<Transform>();
        eventMusic = FMODUnity.RuntimeManager.CreateInstance(route);
        eventMusic.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
    }
    void FixedUpdate()
    {
        
        eventMusic.setParameterByName("isDragon", GameManager.instance.fishState);

        //Sale del agua
        if (tr.position.z < 3 && control)
        {
            eventMusic.start();
            control = !control;
        }
        else if (tr.position.z > 2 && control)
        {
            eventMusic.start();
            control = !control;
        }
        else if (tr.position.z == 2)
            control = true;
    }







    ////##! SONIDOS
    //private FMOD.Studio.EventInstance instance;
    //[FMODUnity.EventRef]
    //[SerializeField] string fmodEvent;

    //Transform tr;
    //void Awake()
    //{
    //    tr = GetComponent<Transform>();

    //    //##! SONIDOS
    //    instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
    //    instance.set3DAttributes(RuntimeUtils.To3DAttributes(tr));
    //    instance.start();
    //    instance.release();
    //}

    //void FixedUpdate()
    //{
    //    //##! SONIDOS
    //    instance.setParameterByName("HD", tr.position.z);
    //}
}
