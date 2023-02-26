using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    [SerializeField] GameObject wizard;
    [SerializeField] GameObject bg;
    private float wizardY;
    private void Update()
    {
        wizardY = wizard.transform.position.y;
        float x = (wizardY) switch
        {
            (> 0) and (<= 7) => x = 0,
            (> 7) and (<= 21) => 14,
            (> 21) and (<= 35) => 28,
            (> 35) and (<= 49) => 42,
            (> 49) and (<= 63) => 56,
            (> 63) and (<= 77) => 70,

            _ => 0
        };
        transform.position = new Vector3(transform.position.x, x, transform.position.z);
        bg.transform.position = new Vector3(bg.transform.position.x, x,bg.transform.position.z);

    }
     



}
