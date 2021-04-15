using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            Transform tr = other.GetComponent<Transform>();
            tr.localScale = new Vector3(tr.localScale.x, tr.localScale.y * 1.5f, tr.localScale.z);

            GameManager.instance.parallaxMultiplier(5f);

            Camera.main.fieldOfView -= 15;
        }
    }
}
