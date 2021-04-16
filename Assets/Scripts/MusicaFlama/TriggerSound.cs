using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    [SerializeField] string route;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Carpa" && this.name =="SpeedBoost(Clone)" && collision.GetComponent<Transform>().position.z == 0)
            FMODUnity.RuntimeManager.PlayOneShot(route);
        //else if (collision.name == "Carpa")
        //    FMODUnity.RuntimeManager.PlayOneShot(route);
    }
}
