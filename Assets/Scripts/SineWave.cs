using UnityEngine;
using System.Collections;

public class SineWave : MonoBehaviour
{

    public int numberOfDots = 28;
    public float factor = 5;
    public float amplitude = 5;
    public GameObject[] waveDots;
    public GameObject waveDotPrefab;

    private object sineWave;

    void Start()
    {
        // re-initilaze array to get correct size
        waveDots = new GameObject[numberOfDots];

        // instantiate all prefab clones
        for (int i = 1; i < numberOfDots; i++)
        {
            waveDots[i] = Instantiate(waveDotPrefab, new Vector3(0, 0, i), Quaternion.identity) as GameObject;
        }
    }

    void Update()
    {
        // move the prefab clones as a sine wave
        for (int i = 1; i < numberOfDots; i++)
        {
            Vector3 position = waveDots[i].transform.position;
            position.x = Mathf.Sin(Time.time + i * factor) * amplitude;
            waveDots[i].transform.position = position;
        }

    }

}
