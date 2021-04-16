using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Creas un object GameManager vacio (prefab para que sobreviva escenas) con este script.

    public static GameManager instance;
    public int actualScene = 1;

    public GameObject bg;
    public Transform playerTr;
    Parallax[] childrenParallax;
    float[] originParallaxVel;

    float originalFOV;
    float originalScaleY;
    float restoreTime = 0;
    bool hasToRestore = false;
    bool FOVRestoration = false;
    float distance = 0;

    int velChain = 0;


    // En el método Awake comprueba si hay otro GameManger
    // y si no lo hay se inicializa como GameManager. En el caso
    // que hubiera otro se autodestruye
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

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
    }
    public void parallaxMultiplier(float val)
    {
        for (int i = 0; i < bg.transform.childCount; ++i)
        {
            childrenParallax[i].parallEffectMultiplier(val);
        }
    }

    private void Update()
    {
        distance += Time.deltaTime;
        Debug.Log(distance);
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
