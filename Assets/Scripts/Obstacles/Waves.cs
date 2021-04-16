using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    const int topY = 15;

    [SerializeField]
    GameObject[] gObj;
    [SerializeField]
    Vector2[] pos;
    [SerializeField]
    bool aleatorio;




    int speed_ = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawn();
    }

    void spawn()
    {
        if (gObj.Length != pos.Length)
        {
            aleatorio = true;
        }
        for (int i = 0; i < pos.Length; i++)
        {
            Debug.Log(i);
            //Instanciamos la roca
            int j;
            if (!aleatorio)
                 j = i;
            else j = Random.Range(0, gObj.Length);
            GameObject obstacle = Instantiate(gObj[j], pos[i]+ new Vector2(0, topY), Quaternion.identity);

            //Y actualizamos sus par�metros
            obstacle.GetComponent<GoDown>().initialize(speed_);
        }
    }

    public void initialize(int speed)
    {
        speed_ = speed;
    }
}
