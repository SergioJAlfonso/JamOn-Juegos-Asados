using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeanText : MonoBehaviour
{
    public float tweenTime;
    // Start is called before the first frame update
    void Start()
    {
        Tween();
    }

    // Update is called once per frame
    public void Tween()
    {
        LeanTween.cancel(gameObject);
        transform.localScale = Vector3.one;
        LeanTween.scale(gameObject, Vector3.one * 1.3f, tweenTime)
            .setEasePunch();

        LeanTween.value(gameObject, 0, 1, tweenTime)
            .setEasePunch();
    }
 
}
