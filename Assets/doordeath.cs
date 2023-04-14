//Dan Huynhvo
//UNR
//CS 425

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doordeath : MonoBehaviour
{
    // Start is called before the first frame update

    SpriteRenderer sprite;
    [SerializeField]
    doorscript info;
    public bool NotDead;
    public int thisID;
    [SerializeField]
    public float fadeDuration = 2f;

    void Start()
    {
        // Get the NavMeshAgent component from the parent game object
        
        thisID = gameObject.GetInstanceID();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        NotDead = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (info.health <= 0 && NotDead)
        {
            //Disable RigidBody
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            //Disable Collision
            Collider col = GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false;
            }
            //FadeOut and Die
            FadeOut();
        }
    }
    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        Renderer enemyRenderer = GetComponent<Renderer>();
        Material enemyMaterial = enemyRenderer.material;
        Color initialColor = enemyMaterial.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0); // Target color with 0 alpha (transparent)

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            enemyMaterial.color = Color.Lerp(initialColor, targetColor, t);
            yield return null;
        }

        // Optional: Destroy the enemy GameObject after the fade-out
        Destroy(gameObject);
    }
}
