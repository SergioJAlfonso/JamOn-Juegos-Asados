using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField] float smoothDelay = 0.125f;
    [SerializeField] float maxReductionTime = 4;
    float reductionTime = 0;
    

    private void Update()
    {
        

        if (reductionTime > 0.2/*maxReductionTime/2*/)
        {
            reductionTime -= Time.deltaTime;
            Camera.main.fieldOfView += 0.05f;
        }
        if (reductionTime > 0.0f)
        {
            reductionTime -= Time.deltaTime;
            Camera.main.fieldOfView -= 0.005f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            Transform tr = other.GetComponent<Transform>();
            Vector3 newScale = new Vector3(tr.localScale.x, tr.localScale.y * 5f, tr.localScale.z);

            //tr.localScale = new Vector3(tr.localScale.x, Mathf.Lerp(tr.localScale.y, tr.localScale.y * 2f, smoothDelay), tr.localScale.z);
            tr.localScale = Vector3.Lerp(transform.localScale, newScale, smoothDelay);
            //tr.localScale.y = Mathf.Lerp(tr.localScale.y, tr.localScale.y * 1.25f, smoothDelay);

            GameManager.instance.parallaxMultiplier(1.75f);

            reductionTime = maxReductionTime;
            //Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, Camera.main.fieldOfView * 0.9f, smoothDelay);
            //Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x,
            //    Mathf.Lerp(Camera.main.transform.localPosition.y, Camera.main.transform.localPosition.y - 15, smoothDelay),
            //    Camera.main.transform.localPosition.z);
        }
    }
}
