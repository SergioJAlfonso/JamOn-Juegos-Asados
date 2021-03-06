using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField] float smoothDelay = 0.125f;
    [SerializeField] float maxReductionTime = 4;
    float reductionTime = 0;
    bool lesserBoost = false;


    private void Update()
    {
        if (reductionTime > 0.2/*maxReductionTime/2*/)
        {
            reductionTime -= Time.deltaTime;
            if(!lesserBoost)
                Camera.main.fieldOfView += 0.05f;
            else
                Camera.main.fieldOfView += 0.02f;
        }
        if (reductionTime > 0.0f)
        {
            reductionTime -= Time.deltaTime;
            if (!lesserBoost)
                Camera.main.fieldOfView -= 0.01f;
            else
                Camera.main.fieldOfView -= 0.005f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        PlayerController plC = other.gameObject.GetComponent<PlayerController>();
        if (plC != null && plC.nextPiece == null)
        {
            Transform tr = other.GetComponent<Transform>();
            if (GameManager.instance.getVelChain() < 3)
            {

                //Vector3 newScale = new Vector3(tr.localScale.x, tr.localScale.y * 5f, tr.localScale.z);

                //tr.localScale = Vector3.Lerp(transform.localScale, newScale, smoothDelay);

                GameManager.instance.parallaxMultiplier(1.75f);
                GameManager.instance.speedMultiplier(1.5f);

                reductionTime = maxReductionTime;

                if (lesserBoost)
                    lesserBoost = false;
            }
            else
            {
                Vector3 newScale = new Vector3(tr.localScale.x, tr.localScale.y * 2f, tr.localScale.z);

                tr.localScale = Vector3.Lerp(transform.localScale, newScale, smoothDelay);

                GameManager.instance.parallaxMultiplier(1.2f);
                GameManager.instance.speedMultiplier(1.2f);

                reductionTime = maxReductionTime;

                if (!lesserBoost)
                    lesserBoost = true;
            }
            GameManager.instance.setHasToRestore(true);
        }
    }
}
