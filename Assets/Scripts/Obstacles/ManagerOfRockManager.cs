using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerOfRockManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] rockManagers;

    GameObject actualManager;
    int actual = 0;
    // Start is called before the first frame update
    void Start()
    {
        actualManager = rockManagers[0];
        Instantiate(rockManagers[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (actualManager != rockManagers[actual])
        {
            Destroy(actualManager);
            Instantiate(rockManagers[actual]);
        }
    }
}
