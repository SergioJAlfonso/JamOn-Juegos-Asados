using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BancoDePecesCool : MonoBehaviour
{
    public List<GameObject> carpas = new List<GameObject>();
    public GameObject CarpaBotPrefab;

    private void Start()
    {
        for (int i = 0; i < Random.Range(1, 4); i++) instanciarCarpa();
    }

    void instanciarCarpa()
    {
        GameObject nuevo = Instantiate(CarpaBotPrefab, transform.position, Quaternion.identity) as GameObject;

        nuevo.GetComponentInChildren<CoolCarpaBotFollow>().setTarget(transform, 0.0f);
        //nuevo.GetComponentInChildren.<CoolCarpaBotFollow>().setTarget(transform, 0.0f);
        carpas.Add(nuevo);
    }

    /*
        private void Update()
        {
            if (Input.GetKey(KeyCode.Q))
                instanciarCarpa();
        }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ArrayDePecesQueTeSiguen>() != null)
        {

            List<GameObject> aux = collision.GetComponent<ArrayDePecesQueTeSiguen>().carpas;

            for (int i = 0; i < carpas.Count; i++)
            {
                aux.Add(carpas[i]); //meros punteros a las carpas

                carpas[i].GetComponentInChildren<CoolCarpaBotFollow>().setTarget(collision.gameObject.transform, 1.5f);
                //carpas[i].GetComponent<CoolCarpaBotFollow>().setTarget(collision.gameObject.transform, 1.5f);
                //bodyParts[i].transform.parent = collision.transform;
                Debug.Log("cambiado " + i);

            }

            Debug.Log("collisiono");
            carpas.Clear();
        }
    }
}

