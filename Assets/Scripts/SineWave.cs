using UnityEngine;
using System.Collections;

public class SineWave : MonoBehaviour
{

    public int numberOfDots = 28;
    public float factor = 5;
    public float amplitude = 5;
    public GameObject[] waveDots;
    public GameObject waveDotPrefab;

    public Transform target;
    public float speed = 1.0f;
    public float minDistance = 0.3f;
    Vector3 DistancefromTarget = new Vector3(0, 0, 0);
    Vector3 direction;
    // Animator m_Animator;

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

            Vector3 position = waveDots[i].transform.localPosition;
            position.y = target.position.y - (waveDots[i].transform.lossyScale.y * i);
            position.x = target.position.x + Mathf.Sin(Time.time + i * factor) * amplitude;
            waveDots[i].transform.position = position;
        }

    }


    void Follow()
    {
        //a ver mamerto, si la distancia es no muy chikita te apegas al bicho, se mueve en el eje z
        // transform.LookAt(player);
        if (DistancefromTarget.magnitude > minDistance)
        {
            transform.Translate(speed * Time.deltaTime * -direction.x,
                                speed * Time.deltaTime * -direction.y,
                                0.0f);
        }
    }

    private void FixedUpdate()
    {
        DistancefromTarget = transform.position - target.position;
        direction = DistancefromTarget.normalized;
    }

    void LateUpdate()
    {
        Follow();

    }

}
