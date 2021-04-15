using UnityEngine;
using System.Collections;

public class SineWave : MonoBehaviour
{

    public int numberOfDots = 28;
    public float factor = 5;
    public float amplitude = 5;
    public int numberOfDotsWave = 28;
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

           // waveDots[i].transform.parent = target.transform;
        }
    }

    void Update()
    {
        Vector3 direction = Quaternion.Euler(0, 90, 0) * Vector3.forward;// (target.position - waveDots[1].transform.position);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        waveDots[1].transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        Vector3 anguloobjetivo = target.rotation * Vector3.right;

        // move the prefab clones as a sine wave
        for (int i = 1; i < numberOfDots; i++)
        {
            if (i > 1)
            {
                direction = (waveDots[i - 1].transform.position - waveDots[i].transform.position);

                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                waveDots[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            }

            Vector3 destino; //coordenada x, y destino
            float aux = i * 1.0f / numberOfDotsWave;
            destino.x = (target.position.x - (waveDots[i].transform.lossyScale.x * i));
            destino.y = target.position.y + Mathf.Sin((Time.time * speed + aux * factor)) * amplitude;
            destino.z = target.position.z;


            Vector3 position = waveDots[i].transform.localPosition; // tengo que mover esta posicion ademas con la rotacion, eso quiere decir que tengo que tener en cuenta la direccion del elemento anterior
                                                                    //position.x = destino.x;
                                                                    //position.y = destino.y;

            //Gracias leonor por tener la cosideracion de pasarte a ayudar a un idiota <3
           destino = target.transform.position + target.transform.InverseTransformPoint(destino);
            destino.y = -destino.y;
            //se le puede hacer un translation?

            // waveDots[i].transform.Translate(waveDots[i].transform.right * speed * Time.smoothDeltaTime, Space.Self);

            waveDots[i].transform.position = Vector3.Slerp(waveDots[i].transform.position, destino, 0.5f);
            waveDots[i].transform.rotation = Quaternion.Slerp(waveDots[i].transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), 0.5f);

            
            //waveDots[i].transform.position = position;
        }

    }


    Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles) {
        Debug.Log(point + " \n");
        Vector3 dir = point - pivot; // get point direction relative to pivot
           dir = Quaternion.Euler(angles) * dir; // rotate it
            point = dir + pivot; // calculate rotated point
        return point; // return it
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
