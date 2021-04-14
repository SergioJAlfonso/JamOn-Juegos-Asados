using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //Controla el cambio de escenas
public class PlayMenu : MonoBehaviour
{
    public GameObject playMenu;
    public GameObject musicSlider;
    public static bool isPlaying;  //Al hacerla static es accesible en todos los sitios
    bool showSettings;
    // Start is called before the first frame update
    void Start()
    {
        playMenu.SetActive(true);
        musicSlider.SetActive(false);
        isPlaying = false;
        showSettings = false;
    }


    public void PlayGame()
    {
        playMenu.SetActive(false);
        musicSlider.SetActive(false);
        isPlaying = true;
        showSettings = false;
    }

    public void ShowSettings()
    {
        showSettings = !showSettings;
        musicSlider.SetActive(showSettings);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
