using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //Controla el cambio de escenas
public class PlayMenu : MonoBehaviour
{
    public GameObject playMenu;
    public static bool isPlaying;  //Al hacerla static es accesible en todos los sitios
    // Start is called before the first frame update
    void Start()
    {
        playMenu.SetActive(true);
    }


    public void PlayGame()
    {
        playMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
