using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mechanics : MonoBehaviour
{
    // Start is called before the first frame update

    SpriteRenderer sprite;
    [SerializeField]
    Sprite DeadSprite;
    Enemy_Info info;
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        info = gameObject.GetComponent<Enemy_Info>();
    }

    // Update is called once per frame
    void Update()
    {
        if(info.health <= 0)
        {
            sprite.sprite = DeadSprite;
            Destroy(gameObject, 5f);
        }
    }
}
