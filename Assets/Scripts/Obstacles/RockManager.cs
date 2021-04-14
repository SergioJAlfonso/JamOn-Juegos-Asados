using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{

    const int topY = 15;

    [System.Serializable]
    private struct RockPrefab
    {
        public int speed;
        public float size;
        public Vector2 pos;
        public GameObject type;
    }

    [System.Serializable]
    private struct RockWave
    {
        public RockPrefab[] rocks;
        public int duration;
    }

    [SerializeField]
    RockWave[] WAVES;

    [SerializeField]
    short actualWave = 0;

    float timer = 0f;

    void Start()
    {
        
    }

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
        for (int i = 0; i < WAVES[actualWave].rocks.Length; ++i)
        {
            Vector2 pos = WAVES[actualWave].rocks[i].pos;

            //Sacamos la posición relativa al tope de la cámara
            relativePos(ref pos);

            //Instanciamos la roca
            GameObject rock = Instantiate(WAVES[actualWave].rocks[i].type, pos, Quaternion.identity);

            //Y actualizamos sus parámetros
            rock.GetComponent<Rock>().initialize(WAVES[actualWave].rocks[i].speed, WAVES[actualWave].rocks[i].size);
        }
    }

    void relativePos(ref Vector2 pos)
    {
        pos.y += topY;
    }
}

