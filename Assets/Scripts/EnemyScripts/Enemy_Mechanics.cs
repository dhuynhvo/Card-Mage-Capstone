//Dan Huynhvo
//UNR
//CS 425

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
    public bool NotDead;
    public int thisID;
    [SerializeField]
    private float CopperMoneyChance;
    [SerializeField]
    private float SilverMoneyChance;
    [SerializeField]
    private float GoldMoneyChance;
    [SerializeField]
    private float SteveMoneyChance; ///Steve is a currency lol//
    [SerializeField]
    private GameObject[] MoneyRefArray;
    void Start()
    {
        
        thisID = gameObject.GetInstanceID();
        GameEvents.current.OnEnemyDeath += DropCardOnDeath;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        info = gameObject.GetComponent<Enemy_Info>();
        NotDead = true;
        MoneyRefArray = new GameObject[4];
        MoneyRefArray[0] = Resources.Load<GameObject>("Prefabs/TeefCopper");
        MoneyRefArray[1] = Resources.Load<GameObject>("Prefabs/TeefSilver");
        MoneyRefArray[2] = Resources.Load<GameObject>("Prefabs/TeefGold");
        MoneyRefArray[3] = Resources.Load<GameObject>("Prefabs/TeefSteve");
    }

    // Update is called once per frame
    void Update()
    {
        if (info.health <= 0 && NotDead)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 90), Space.Self);
            GameEvents.current.DropCard_E(thisID);
            DropMoneyOnDeath();
            sprite.sprite = DeadSprite;
            Destroy(transform.parent.gameObject, 1f);
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

    public void DropMoneyOnDeath()
    {
        float randChanceC;
        float randChanceS;
        float randChanceG;
        float randChanceSteve;
        float range = 2;
        Vector3 offset;
        for(int i = 0; i < 10; i++)
        {
            randChanceC = Random.Range(0, 100);
            randChanceS = Random.Range(0, 100);
            randChanceG = Random.Range(0, 100);
            randChanceSteve = Random.Range(0, 100);
            if (randChanceC < CopperMoneyChance)
            {
                offset = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
                GameObject C = Instantiate(MoneyRefArray[0], transform.position + offset, Quaternion.Euler(90, 0, 0));
            }

            if (randChanceS < SilverMoneyChance)
            {
                offset = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
                GameObject S = Instantiate(MoneyRefArray[1], transform.position + offset, Quaternion.Euler(90, 0, 0));
            }

            if (randChanceG < GoldMoneyChance)
            {
                offset = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
                GameObject G = Instantiate(MoneyRefArray[2], transform.position+offset, Quaternion.Euler(90, 0, 0));
            }

            if (randChanceSteve < SteveMoneyChance)
            {
                offset = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
                GameObject Steve = Instantiate(MoneyRefArray[3], transform.position + offset, Quaternion.Euler(90, 0, 0));
            }
        }
    }

    private void OnDestroy()
    {
        GameEvents.current.OnEnemyDeath -= DropCardOnDeath;
    }
}
