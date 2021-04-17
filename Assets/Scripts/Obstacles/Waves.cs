using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    const int topY = 15;
    [System.Serializable]
    struct Position {
        public Vector2 pos;
        public float randRangeX;
        public float randRangeY;
    }
    [SerializeField]
    GameObject[] gObj;
    [SerializeField]
    Position[] position;
    [SerializeField]
    bool randObstacle;


    // Start is called before the first frame update
    void Start()
    {
        spawn();
        Destroy(this.gameObject);
    }

    void spawn()
    {
        if (gObj.Length != position.Length)
        {
            randObstacle = true;
        }
        for (int i = 0; i < position.Length; i++)
        {
            Debug.Log(i);
            //Instanciamos la roca
            int j;
            if (!randObstacle)
                j = i;
            else j = Random.Range(0, gObj.Length);

            Vector2 randPos;
            randPos.x = Random.Range(position[i].pos.x - (position[i].randRangeX / 2), position[i].pos.x + (position[i].randRangeX / 2));
            randPos.y = Random.Range(position[i].pos.y - (position[i].randRangeY / 2), position[i].pos.y + (position[i].randRangeY / 2));
            Instantiate(gObj[j], randPos + new Vector2(0, topY), Quaternion.identity);
        }
    }
}
