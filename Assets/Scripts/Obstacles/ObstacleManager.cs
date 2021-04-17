using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{

    [System.Serializable]
    private struct ObstacleWave
    {
        public GameObject type;
        public int duration;
    }

    [SerializeField]
    ObstacleWave[] WAVES;

    [SerializeField]
    short actualWave = 0;

    double timer = 0f;

    void Update()
    {
        int velChain = 1;
        if (GameManager.instance != null)
        {
            velChain = GameManager.instance.getVelChain();
            velChain++;
        }
        timer += (Time.deltaTime* (1.5 * velChain));

        if (actualWave < WAVES.Length && timer >= WAVES[actualWave].duration)
        {
            spawnWave();
            actualWave++;
            timer = 0f;
        }
    }

    void spawnWave()
    {

        //speed = GameManager.instance.getSpeed();
        //Instanciamos la roca
        GameObject obstacle = Instantiate(WAVES[actualWave].type, new Vector3(0 ,0 ,0), Quaternion.identity);
    }

    public int getActWave()
    {
        return actualWave;
    } 
}

