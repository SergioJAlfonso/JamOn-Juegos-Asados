using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CarpaSounds : MonoBehaviour
{
    [SerializeField] string route;

    Transform tr;
    bool control = true;

    void Awake()
    {
        tr = GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        //Sale del agua
        if (tr.position.z < 0 && control)
        {
            FMODUnity.RuntimeManager.PlayOneShot(route);
            control = !control;
        }
        else if (tr.position.z > 0 && control)
        {
            FMODUnity.RuntimeManager.PlayOneShot(route);
            control = !control;
        }
        else if(tr.position.z == 0)
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
