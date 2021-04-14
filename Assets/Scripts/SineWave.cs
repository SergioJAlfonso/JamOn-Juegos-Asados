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

            waveDots[i].transform.parent = target.transform;
        }
    }

    void Update()
    {
        Vector3 direction = (target.localPosition - waveDots[1].transform.localPosition);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        waveDots[1].transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // move the prefab clones as a sine wave
        for (int i = 1; i < numberOfDots; i++)
        {
            if (i > 1)
            {
                direction = (waveDots[i-1].transform.localPosition - waveDots[i].transform.localPosition);

                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                waveDots[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            }

            Vector3 position = waveDots[i].transform.localPosition; // tengo que mover esta posicion ademas con la rotacion, eso quiere decir que tengo que tener en cuenta la direccion del elemento anterior
            position.x = target.localPosition.x - (waveDots[i].transform.localScale.x * i);
            position.y = target.localPosition.y + Mathf.Sin(Time.time + i * factor) * amplitude;
            waveDots[i].transform.localPosition = position;
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
