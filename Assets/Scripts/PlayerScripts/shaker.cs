using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaker : MonoBehaviour
{
    public bool start = false;
    public AnimationCurve curve;
    public float duration = 1f;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject myCamera;
    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }
    IEnumerator Shaking()
    {
        Vector3 startPosition = myCamera.transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime+=Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            startPosition = myCamera.transform.position;
                //player.transform.position + new Vector3(0, 35, 0);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        //Vector3 startPosition2 = player.transform.position + new Vector3(0, 35, 0);
        transform.position = startPosition;
    }
}
