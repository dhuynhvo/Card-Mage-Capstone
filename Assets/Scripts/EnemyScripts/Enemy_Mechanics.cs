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
    Sprite DeadSprite;
    Enemy_Info info;
    private bool NotDead;
    public int thisID;
    public int test;
    void Start()
    {
        thisID = gameObject.GetInstanceID();
        GameEvents.current.OnEnemyDeath += DropCardOnDeath;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        info = gameObject.GetComponent<Enemy_Info>();
        NotDead = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (info.health <= 0 && NotDead)
        {
            GameEvents.current.DropCard_E(thisID);
            sprite.sprite = DeadSprite;
            Destroy(gameObject, 1f);
            NotDead = false;
        }
    }

    public void DropCardOnDeath(int ID)
    {
        float randChance = Random.Range(0f, 100f);
        if(randChance < info.DropChance && NotDead && thisID == ID)
        {
            var randPosition = new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(-3.0f, 3.0f));
            int randCard = Random.Range(0, CardPool.cards.Count);
            GameObject Card = Instantiate(CardPool.cards[randCard], gameObject.transform.position + randPosition, Quaternion.Euler(90, 0, 0));
        }
    }

    private void OnDestroy()
    {
        GameEvents.current.OnEnemyDeath -= DropCardOnDeath;
    }
}
