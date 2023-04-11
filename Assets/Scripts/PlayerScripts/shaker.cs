using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaker : MonoBehaviour
{
    public bool enableds = true;
    public bool start = false;
    public AnimationCurve curve;
    public float duration = 1f;
    [SerializeField]
    private GameObject player;
    // Update is called once per frame
    void Update()
    {
        if (enableds == true)
        {
            if (start)
            {
                start = false;
                StartCoroutine(Shaking());
            }
        }
        
    }
    IEnumerator Shaking()
    {
        
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime+=Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            Vector3 startPosition = player.transform.position + new Vector3(0, 35, 0);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        Vector3 startPosition2 = player.transform.position + new Vector3(0, 35, 0);
        transform.position = startPosition2;
    }
}
