using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BancoDePeces : MonoBehaviour
{
    public List<GameObject> carpas = new List<GameObject>();
    public GameObject CarpaBotPrefab;

    private void Start()
    {
        for (int i = 0; i < Random.Range(1, 6); i++) instanciarCarpa();
    }

    void instanciarCarpa()
    {
        GameObject nuevo = Instantiate(CarpaBotPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

        nuevo.GetComponent<CarpaBotFollow>().setTarget(transform, 0.0f);
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
                carpas[i].GetComponent<CarpaBotFollow>().setTarget(collision.gameObject.transform, 1.5f);
                //bodyParts[i].transform.parent = collision.transform;
                Debug.Log("cambiado " + i);

            }

            Debug.Log("collisiono");
            carpas.Clear();
        }
    }
}

