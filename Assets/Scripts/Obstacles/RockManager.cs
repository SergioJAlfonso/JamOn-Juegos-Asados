using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ROCK_TYPE : ushort { DIAGONAL, RECTANGULAR, TRIANGLE };

public class RockManager : MonoBehaviour
{

    const int topY = 700;

    [System.Serializable]
    private struct RockPrefab
    {
        public int speed;
        public int size;
        public Vector2 pos;
        public ROCK_TYPE type;
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

        if (timer >= WAVES[actualWave].duration)
        {
            spawnWave();
            actualWave++;
            timer = 0;
        }
    }

    void spawnWave()
    {
        for (int i = 0; i < WAVES[actualWave].rocks.Length; ++i)
        {
            //Rock r = new Rock(WAVES[actualWave].rocks[i].type,
            //                  WAVES[actualWave].rocks[i].speed,
            //                  WAVES[actualWave].rocks[i].size);

            Vector2 pos = WAVES[actualWave].rocks[i].pos;

            relativePos(ref pos);

            //Instantiate(r, pos, Quaternion.identity);
        }
    }

    void relativePos(ref Vector2 pos)
    {
        pos.y += topY;
    }
}

