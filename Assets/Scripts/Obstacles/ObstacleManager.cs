using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{

    const int topY = 15;

    [System.Serializable]
    private struct ObstaclePrefab
    {
        public int speed;
        public float size;
        public Vector2 pos;
        public GameObject type;
    }

    [System.Serializable]
    private struct ObstacleWave
    {
        public ObstaclePrefab[] obstacles;
        public int duration;
    }

    [SerializeField]
    ObstacleWave[] WAVES;

    [SerializeField]
    short actualWave = 0;

    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (actualWave < WAVES.Length && timer >= WAVES[actualWave].duration)
        {
            spawnWave();
            actualWave++;
            timer = 0f;
        }
    }

    void spawnWave()
    {
        for (int i = 0; i < WAVES[actualWave].obstacles.Length; ++i)
        {
            Vector2 pos = WAVES[actualWave].obstacles[i].pos;

            //Sacamos la posición relativa al tope de la cámara
            relativePos(ref pos);

            //Instanciamos la roca
            GameObject obstacle = Instantiate(WAVES[actualWave].obstacles[i].type, pos, Quaternion.identity);

            //Y actualizamos sus parámetros
            obstacle.GetComponent<GoDown>().initialize(WAVES[actualWave].obstacles[i].speed, WAVES[actualWave].obstacles[i].size);
        }
    }

    void relativePos(ref Vector2 pos)
    {
        pos.y += topY;
    }
}

