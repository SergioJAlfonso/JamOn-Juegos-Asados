using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolCarpaBotFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float factorX = 1.0f;
    public float amplitudeX = 1.0f;
    public float factorY = 1.0f;
    public float amplitudeY = 1.0f;
    public float speed = 1.0f;

    public float speedX = 1.0f;
    public float speedY = 1.0f;

    public float distYOffSet = 2.0f;
    public Vector3 destino;


    void Start()
    {

        factorX = Random.Range(-2.0f, 2.0f);
        if (Mathf.Abs(factorX) < 0.5) factorX = factorX * 0.5f;
        amplitudeX = Random.Range(-2.0f, 2.0f);
        if (Mathf.Abs(amplitudeX) < 0.5) amplitudeX = amplitudeX * 0.5f;

        factorY = Random.Range(-2.0f, 2.0f);
        if (Mathf.Abs(factorY) < 0.5) factorY = factorY * 0.5f;
        amplitudeY = Random.Range(-2.0f, 2.0f);
        if (Mathf.Abs(amplitudeY) < 0.5) amplitudeY = amplitudeY * 0.5f;

        speed = Random.Range(1.0f, 2.0f);
        /*
        speedX = Random.Range(1.0f, 2.0f);
        speedY = Random.Range(1.0f, 2.0f);
         */



    }

    // Update is called once per frame
    void Update()
    {
        //coordenada x, y destino
        destino.y = (target.position.y + Mathf.Sin((Time.time * speedY + factorY)) * amplitudeY) - distYOffSet;
        destino.x = target.position.x + Mathf.Cos((Time.time * speedX + factorX)) * amplitudeX;
        destino.z = target.position.z;

        destino = target.transform.position + target.transform.InverseTransformPoint(destino);
        //destino.y = -destino.y;


        /*
        float dis = Vector3.Distance(destino, transform.position);
        float T = Time.deltaTime * dis / speed;

        if (T > 0.5f) T = 0.5f;

        transform.position = Vector3.Slerp(transform.position, destino, T);
         */
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), 0.5f);
    }

    public void setTarget(Transform tg, float distY)
    {
        target = tg;
        distYOffSet = distY;
    }

}
