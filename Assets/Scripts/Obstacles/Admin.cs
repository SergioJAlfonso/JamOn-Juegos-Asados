using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Admin : MonoBehaviour
{
    [SerializeField]
    GameObject[] rockManagers;

    GameObject actualManager;
    int actual = 0;

    bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        actualManager = rockManagers[0];
        Instantiate(rockManagers[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.getRecovery())
        {
            if (!done)
            {
                actual++;
            }
            done = true;
        }
        else done = false;

        if (actualManager != rockManagers[actual])
        {
            Destroy(actualManager);
            Instantiate(rockManagers[actual]);
        }
    }
}
