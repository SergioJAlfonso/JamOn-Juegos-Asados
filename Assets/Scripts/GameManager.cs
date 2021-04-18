using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine.SceneManagement;  //Controla el cambio de escenas
using FMOD;

public class GameManager : MonoBehaviour
{
    //Creas un object GameManager vacio (prefab para que sobreviva escenas) con este script.

    public static GameManager instance;
    public int actualScene = 1;

    public GameObject bg;
    public Transform playerTr;
    public Rigidbody2D playerRb;
    public Transform dumpingObjectTr;

    Parallax[] childrenParallax;
    float[] originParallaxVel;

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

    [SerializeField] float minObstSpeed = 7;
    float obstSpeed;

    [SerializeField] float[] sectionTimeStamps = new float[5];
    int sectionId = 0;

    //float gameTime = 0;

    float perspectiveRecovery;
    bool hasToRecover = false;

    [SerializeField] float smoothDelay = 0.125f;

    bool gameStates = false;

    //////##! SONIDOS
    private FMOD.Studio.EventInstance musicMusic;
    private FMOD.Studio.EventInstance backgroundMusic;

    [FMODUnity.EventRef] [SerializeField] string musicManagerEvent;
    [FMODUnity.EventRef] [SerializeField] string backgroundManagerEvent;


    public GameObject Menu;

    public GameObject cascada;
    bool cascadaEspauneada = false;

    [SerializeField] Admin elAdmin;

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
        for (int i = 0; i < numChildren; i++)
        {
            childrenParallax[i] = bg.transform.GetChild(i).gameObject.GetComponent<Parallax>();
            originParallaxVel[i] = childrenParallax[i].parallaxEffect;
        }

        //Pilla
        originalFOV = Camera.main.fieldOfView;

        //playerTR
        originalScaleY = playerTr.localScale.y;

        obstSpeed = minObstSpeed;

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
    }
    public void parallaxMultiplier(float val)
    {
        if (gameStates)
        {
            for (int i = 0; i < bg.transform.childCount; ++i)
            {
                childrenParallax[i].parallEffectMultiplier(val);
            }
        }
    }

    public void PlayGame()
    {
        Vector2 a = new Vector2(0, 0);
        if (InitDistance < 8)
        {
            distance = 8;
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
                timeRemain = TiempoBucle - InitDistance % TiempoBucle / 2;
        }
        else
            timeRemain = TiempoBucle - InitDistance;

        LeanTween.scale(Menu, a, timeRemain).setEaseInCirc();
        Invoke("gameStart", timeRemain);
        musicMusic.setParameterByName("Distance", distance);

    }
    void gameStart()
    {
        gameStates = !gameStates;
        Menu.SetActive(false);

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
            instanceMusic.setParameterByName("Distance", distance);
        }
        musicMusic.setParameterByName("Distance", distance);

        if (!cascadaEspauneada && sectionId < sectionTimeStamps.Length && distance > sectionTimeStamps[sectionId] - 4)
        {
            GameObject cascadita = Instantiate(cascada, new Vector3(0, 0, 0), Quaternion.identity);
            cascadaEspauneada = true;
        }

        if (sectionId < sectionTimeStamps.Length && distance > sectionTimeStamps[sectionId])
        {
            sectionId++;
            hasToRecover = true;
            perspectiveRecovery = 5;
            cascadaEspauneada = false;
        }

        if (hasToRecover)
        {
            perspectiveRecovery -= Time.deltaTime;
            if (perspectiveRecovery <= 0)
                hasToRecover = false;
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
    }

    private void restoreOriginalStats()
    {
        //Parallax
        for (int i = 0; i < bg.transform.childCount; ++i)
        {
            childrenParallax[i].parallaxEffect = originParallaxVel[i];
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