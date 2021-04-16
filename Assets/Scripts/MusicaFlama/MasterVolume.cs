using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolume : MonoBehaviour
{
    public Slider mySlider;
    public void volumeChange() {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MasterVolume", mySlider.value);
    }
}
