using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    [SerializeField] private float secondsToDestroy = 1f;
    [SerializeField] private bool DestroyAfterAnimation = false;
    void Update()
    {
        if (!DestroyAfterAnimation)
        {
            Destroy(gameObject, secondsToDestroy);
        }

    }
    void EndAnimation()
    {
        if (DestroyAfterAnimation)
        {

            Destroy(gameObject);
        }

    }
}
