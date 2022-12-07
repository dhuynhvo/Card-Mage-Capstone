using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mechanics : MonoBehaviour
{
    // Start is called before the first frame update

    SpriteRenderer sprite;
    [SerializeField]
    private Premade_Decks CardPool;
    [SerializeField]
    float CardDropChance; //Currently unused
    [SerializeField]
    Sprite DeadSprite;
    Enemy_Info info;
    private bool NotDead = true;
    void Start()
    {
        GameEvents.current.OnEnemyDeath += DropCardOnDeath;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        info = gameObject.GetComponent<Enemy_Info>();
    }

    // Update is called once per frame
    void Update()
    {
        if(info.health <= 0 && NotDead)
        {
            GameEvents.current.DropCard_E();
            sprite.sprite = DeadSprite;
            Destroy(gameObject, 5f);
            NotDead = false;
        }
    }

    public void DropCardOnDeath()
    {
            var randPosition = new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(-3.0f, 3.0f));
            GameObject Card = Instantiate(CardPool.cards[0], transform.position + randPosition, Quaternion.Euler(90, 0, 0));
    }
}
