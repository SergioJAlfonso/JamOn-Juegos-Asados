using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;

public class GameManager : MonoBehaviour
{
    //Creas un object GameManager vacio (prefab para que sobreviva escenas) con este script.

    public static GameManager instance;
    public int actualScene = 1;

    public GameObject bg;
    public Transform playerTr;
    public Rigidbody2D playerRb;
    Parallax[] childrenParallax;
    float[] originParallaxVel;

    float originalFOV;
    float originalScaleY;
    float restoreTime = 0;
    bool hasToRestore = false;
    bool FOVRestoration = false;

    //DISTANCIA QUE LLEVA EL PEZ RECORRIDA
    float distance = 0;
    public float getDistance() { return distance; }

    int velChain = 0;

    [SerializeField] float minObstSpeed = 7;
    float obstSpeed;

    [SerializeField] float[] sectionTimeStamps = new float[5];
    int sectionId = 0;

    float gameTime = 0;

    float perspectiveRecovery;
    bool hasToRecover = false;

    [SerializeField] float smoothDelay = 0.125f;
    public enum States { Playing, Menu };
    States GameStates;

    //////##! SONIDOS
    private FMOD.Studio.EventInstance instanceMusic;
    [FMODUnity.EventRef]
    [SerializeField] string fmodEvent;

    public GameObject cascada;
    bool cascadaEspauneada = false;
    // En el método Awake comprueba si hay otro GameManger
    // y si no lo hay se inicializa como GameManager. En el caso
    // que hubiera otro se autodestruye
    void Awake()
    {
        //GameStates = States.Menu;

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
        instanceMusic = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instanceMusic.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
        instanceMusic.start();
        instanceMusic.release();
    }
    public void parallaxMultiplier(float val)
    {
        if (GameStates == States.Playing)
        {
            for (int i = 0; i < bg.transform.childCount; ++i)
            {
                childrenParallax[i].parallEffectMultiplier(val);
            }
        }
    }

    public void changePlayState(int n)
    {
        if (n == 0)
            GameStates = States.Playing;
        else if (n == 1)
            GameStates = States.Menu;
    }


    private void Update()
    {
        //LA DISTANCIA SOLO AVANZA SI EL JUGADOR ESTA JUGANDO
        if (GameStates == States.Playing)
        {
            distance += Time.deltaTime;
            //instanceMusic.setParameterByName("Distance", distance);
        }

        gameTime += Time.deltaTime;

        if (!cascadaEspauneada && sectionId < sectionTimeStamps.Length && gameTime > sectionTimeStamps[sectionId] - 4)
        {
            GameObject cascadita = Instantiate(cascada, new Vector3(0, 0, 0), Quaternion.identity);
            cascadaEspauneada = true;
        }

        if (sectionId < sectionTimeStamps.Length && gameTime > sectionTimeStamps[sectionId])
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