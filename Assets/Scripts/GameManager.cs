//using UnityEditorInternal;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using FMOD;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Creas un object GameManager vacio (prefab para que sobreviva escenas) con este script.

    public static GameManager instance;
    public int actualScene = 1;

    public GameObject bg;
    public GameObject tierraBg; 
    [SerializeField]
    GameObject carpa;
    [HideInInspector] public Transform playerTr;
    [HideInInspector] public Rigidbody2D playerRb;
    public GameObject dragon;

    public Transform dumpingObjectTr;

    [SerializeField]
    GameObject buttons;

    Parallax[] childrenParallax;
    float[] originParallaxVel;
    [SerializeField]
    Parallax[] childrenTierraParallax;
    float[] originTierraParallaxVel;

    [SerializeField]
    Camera cam;

    float originalFOV;
    float originalScaleY;
    float restoreTime = 0;
    bool hasToRestore = false;
    bool FOVRestoration = false;

    //DISTANCIA QUE LLEVA EL PEZ RECORRIDA
    float distance = 0;
    float InitDistance = 0;
    float TiempoBucle;
    float timeRemain;
    public float getDistance() { return distance; }

    int velChain = 0;
    bool chupala = true;

    [SerializeField] float minObstSpeed = 7;
    float obstSpeed;

    [SerializeField] float[] sectionTimeStamps = new float[5];
    int sectionId = 0;

    //float gameTime = 0;

    float perspectiveRecovery;
    bool hasToRecover = false;

    [SerializeField] float smoothDelay = 0.125f;

    bool gameStates = false;
    bool botonesMenuHabilitados = true;
    //////##! SONIDOS
    private FMOD.Studio.EventInstance musicMusic;
    private FMOD.Studio.EventInstance backgroundMusic;
    private FMOD.Studio.EventInstance birdsMusic;

    [FMODUnity.EventRef] string musicManagerEvent = "event:/MusicManager";
    [FMODUnity.EventRef] string backgroundManagerEvent = "event:/BackgroundMusic";
    [FMODUnity.EventRef] string birdsEvent = "event:/RamdomBirds";


    public GameObject Menu;

    public GameObject cascada;
   
    bool cascadaEspauneada = false;

    [SerializeField] Admin elAdmin;

    public Image colorPanel;
    Color newColor;

    public int fishState = 0;
    [SerializeField] GameObject rainEffect;

    // En el método Awake comprueba si hay otro GameManger
    // y si no lo hay se inicializa como GameManager. En el caso
    // que hubiera otro se autodestruye
    void Awake()
    {
        Menu.SetActive(true);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        //me mama la pinga
        //Pilla movidas del parallax
        int numChildren = bg.transform.childCount;
        childrenParallax = new Parallax[numChildren];
        originParallaxVel = new float[numChildren];
        
        int numTierraChildren = childrenTierraParallax.Length;
        //childrenTierraParallax = new Parallax[numTierraChildren];
        originTierraParallaxVel = new float[numTierraChildren];

        playerTr = carpa.transform.GetChild(0);
        playerRb = playerTr.GetComponent<Rigidbody2D>();

        for (int i = 0; i < numChildren; i++)
        {
            childrenParallax[i] = bg.transform.GetChild(i).gameObject.GetComponent<Parallax>();
            originParallaxVel[i] = childrenParallax[i].parallaxEffect;
        }

        for (int i = 0; i < numTierraChildren; i++)
        {
            //childrenTierraParallax[i] = tierraBg.transform.GetChild(i).gameObject.GetComponent<Parallax>();
            originTierraParallaxVel[i] = childrenTierraParallax[i].parallaxEffect;
        }

        //Pilla
        originalFOV = Camera.main.fieldOfView;

        //playerTR
        originalScaleY = playerTr.localScale.y;

        obstSpeed = minObstSpeed;

        //newColor = new Color(255, 0, 0, 125);
        //colorPanel.CrossFadeColor(newColor, 5f, true, true);

        //##! SONIDOS
        //musicMusic
        musicMusic = FMODUnity.RuntimeManager.CreateInstance(musicManagerEvent);
        musicMusic.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
        musicMusic.start();
        musicMusic.release();
        //musicMusic
        backgroundMusic = FMODUnity.RuntimeManager.CreateInstance(backgroundManagerEvent);
        backgroundMusic.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
        backgroundMusic.start();
        backgroundMusic.release();
        //birdsMusic
        birdsMusic = FMODUnity.RuntimeManager.CreateInstance(birdsEvent);
        birdsMusic.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
        birdsMusic.start();
        birdsMusic.release();
    }
    public void parallaxMultiplier(float val)
    {
        if (gameStates)
        {
            for (int i = 0; i < bg.transform.childCount; ++i)
            {
                childrenParallax[i].parallEffectMultiplier(val);
            }
            for (int i = 0; i < childrenTierraParallax.Length; ++i)
            {
                childrenTierraParallax[i].parallEffectMultiplier(val);
            }
        }
    }

    public void PlayGame()
    {
        if (botonesMenuHabilitados)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/winMusic");

            buttons.GetComponent<GoDown>().enabled = true;
            Vector2 a = new Vector2(0, 0);
            if (InitDistance < 8)
            {
                distance = 16;
                TiempoBucle = 8;
            }
            else
            {
                distance = 16;
                TiempoBucle = 16;
            }

            if (InitDistance > TiempoBucle)
            {
                if (InitDistance % TiempoBucle > TiempoBucle / 2)
                    timeRemain = TiempoBucle - InitDistance % TiempoBucle;
                else
                    timeRemain = TiempoBucle / 2 - InitDistance % TiempoBucle;
            }
            else
                timeRemain = TiempoBucle - InitDistance;

            LeanTween.scale(Menu, a, timeRemain).setEaseInCirc();
            Invoke("gameStart", timeRemain);
            musicMusic.setParameterByName("Distance", distance);
            botonesMenuHabilitados = false;
        }
    }
    void gameStart()
    {
        gameStates = true;
        Menu.SetActive(false);

    }

    public void volumeChange()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicMasterVolume", Menu.transform.GetChild(2).gameObject.GetComponent<Slider>().value);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    private void Update()
    {
        //Vamos guardando tiempo desde el inicial
        InitDistance += Time.deltaTime;
        //LA DISTANCIA SOLO AVANZA SI EL JUGADOR ESTA JUGANDO
        if (gameStates)
        {
            distance += Time.deltaTime;
            elAdmin.enabled = true;
        }
        musicMusic.setParameterByName("Distance", distance);
        backgroundMusic.setParameterByName("isDragon", distance);

        if (!cascadaEspauneada && sectionId < sectionTimeStamps.Length && distance > sectionTimeStamps[sectionId] - 4)
        {
            GameObject cascadita;
            if (sectionId != 3)
                cascadita = Instantiate(cascada, new Vector3(0, 0, 0), Quaternion.identity);
            cascadaEspauneada = true;
        }

        if (sectionId < sectionTimeStamps.Length && distance > sectionTimeStamps[sectionId])
        {
            sectionId++;
            hasToRecover = true;
            perspectiveRecovery = 10;
            cascadaEspauneada = false;
        }

        if (hasToRecover)
        {
            perspectiveRecovery -= Time.deltaTime;
            if (perspectiveRecovery <= 0)
            {
                hasToRecover = false;
                waterfallEvent();
            }

        }

        if (hasToRestore)
        {
            restoreTime -= Time.deltaTime;

            if (restoreTime <= 0)
            {
                restoreOriginalStats();
            }
        }
        if (FOVRestoration)
        {
            if (Camera.main.fieldOfView > originalFOV)
                Camera.main.fieldOfView -= 0.1f;
            else
            {
                FOVRestoration = false;
                Camera.main.fieldOfView = originalFOV;
            }
        }
        if(distance > 368)
        {
            if (chupala)
            {
                newColor = new Color(255, 255, 255, 255);
                colorPanel.CrossFadeColor(newColor, 0.7f, true, true);
            }
            chupala = false;
        }
        if(distance > 377)
        {
            Application.Quit();
        }
    }

    private void restoreOriginalStats()
    {
        //Parallax
        for (int i = 0; i < bg.transform.childCount; ++i)
        {
            childrenParallax[i].parallaxEffect = originParallaxVel[i];
        }

        for (int i = 0; i < childrenTierraParallax.Length; ++i)
        {
            childrenTierraParallax[i].parallaxEffect = originTierraParallaxVel[i];
        }
        
        //Scale
        playerTr.localScale = new Vector3(playerTr.localScale.x, originalScaleY, playerTr.localScale.z);

        //FOV
        FOVRestoration = true;

        //Speed
        obstSpeed = minObstSpeed;

        hasToRestore = false;
        velChain = 0;
    }

    private void waterfallEvent()
    {
        switch (sectionId)
        {
            case 1:
                newColor = new Color(0, 255, 207, 60);
                //colorPanel.CrossFadeColor(new Color(255, 255, 255, 1), 0f, true, true);
                colorPanel.CrossFadeColor(newColor, 3f, true, true);  
                break;
            case 2:
                //newColor = new Color(255, 67, 0, 90);
                //colorPanel.CrossFadeColor(new Color(255, 255, 255, 1), 0f, true, true);
                //colorPanel.color();
                //colorPanel.CrossFadeColor(new Color(255, 255, 255, 1), 0.2f, true, true);
                //colorPanel.CrossFadeColor(newColor, 7f, true, true);
                rainEffect.SetActive(true);
                break;
            case 3:
                rainEffect.SetActive(false);
                newColor = new Color(255, 255, 255, 1);
                colorPanel.CrossFadeColor(newColor, 0.2f, true, true);
                dragonTransition();
                break;
            case 4:

            default:
                break;
        }
    }

    private void dragonTransition()
    {
        childrenTierraParallax = new Parallax[0];
        for(int i=6; i < bg.transform.childCount; ++i)
        {
            bg.transform.GetChild(i).gameObject.SetActive(true);
        }
        for(int i=0; i<tierraBg.transform.childCount-2; ++i)
        {
            tierraBg.transform.GetChild(i).gameObject.SetActive(false);
        }
        dragon.SetActive(true);
        cam.GetComponent<CameraFollow>().ChangePlayer(dragon.transform.GetChild(0).gameObject);
        carpa.SetActive(false);
    }

    public void addVelChain()
    {
        velChain++;
    }
    public int getVelChain()
    {
        return velChain;
    }
    public float getSpeed()
    {
        return obstSpeed;
    }
    public float getMinSpeed()
    {
        return minObstSpeed;
    }
    public bool getRecovery()
    {
        return hasToRecover;
    }
    public void setSpeed(float s)
    {
        obstSpeed = s;
    }
    public void speedMultiplier(float m)
    {
        obstSpeed *= m;
    }
    public void setHasToRestore(bool b)
    {
        hasToRestore = b;
        if (hasToRestore)
        {
            restoreTime = 5;
            velChain++;
        }
    }
    public void setHasToRestoreWaterfall(bool b)
    {
        hasToRestore = b;
        if (hasToRestore)
        {
            restoreTime = 10;
            velChain++;
        }
    }
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