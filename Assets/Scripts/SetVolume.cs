using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;    //Sirve para controlar el audio
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLevel(float sliderValue)
    {   //Nombre del parametro y valor
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);    //Logaritmo que coarta del 0 al 20 el nivel de volumen
    }
}
