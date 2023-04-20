using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Mechanics : MonoBehaviour
{
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
    [SerializeField]
    private bool hasDroppedMoney = false;
    private bool hasDroppedCard = false;

    [SerializeField] //Time it takes for fade effect to occur
    public float fadeDuration = 2f;

    void Start()
    {   
        thisID = gameObject.GetInstanceID();
        GameEvents.current.OnEnemyDeath += DropCardOnDeath;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        info = gameObject.GetComponent<Enemy_Info>();
        NotDead = true;
        MoneyRefArray = new GameObject[4];
        MoneyRefArray[0] = Resources.Load<GameObject>("Prefabs/Player and Collectibles/TeefCopper");
        MoneyRefArray[1] = Resources.Load<GameObject>("Prefabs/Player and Collectibles/TeefSilver");
        MoneyRefArray[2] = Resources.Load<GameObject>("Prefabs/Player and Collectibles/TeefGold");
        MoneyRefArray[3] = Resources.Load<GameObject>("Prefabs/Player and Collectibles/TeefSteve");
        transform.parent.transform.position = new Vector3(transform.parent.transform.position.x, 1f, transform.parent.transform.position.z);
    }
    void Update()
    {
        
        if (info.health <= 0 && NotDead)
        {   
            //OnDeath Events for Dead Enemies---------------
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
            //----------------------------------------------

            //Drop Card
            if (!hasDroppedCard)
            {
                GameEvents.current.DropCard_E(thisID);
                DropMoneyOnDeath();
                hasDroppedCard = true;
            }
            //Drop Money
            if (!hasDroppedMoney)
            {
                DropMoneyOnDeath();
                hasDroppedMoney = true;
            }
        }
    }

    public void DropCardOnDeath(int ID)
    {
        float randChance = Random.Range(0f, 100f);
        if(randChance < info.DropChance && NotDead && thisID == ID)
        {
            Vector3 m_NewForce = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
            int randCard = Random.Range(0, CardPool.cards.Count);
            GameObject Card = Instantiate(CardPool.cards[randCard], gameObject.transform.position, Quaternion.Euler(90, 0, 0));
            Card.GetComponent<Rigidbody>().AddForce(m_NewForce, ForceMode.Impulse);
        }
    }

    public void DropMoneyOnDeath()
    {
        float randChanceC;
        float randChanceS;
        float randChanceG;
        float randChanceSteve;
        for(int i = 0; i < 10; i++)
        {
            randChanceC = Random.Range(0, 100);
            randChanceS = Random.Range(0, 100);
            randChanceG = Random.Range(0, 100);
            randChanceSteve = Random.Range(0, 100);
            if (randChanceC < CopperMoneyChance)
            {
                //offset = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
                Vector3 m_NewForce = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
                GameObject C = Instantiate(MoneyRefArray[0], transform.position, Quaternion.Euler(90, 0, 0));
                C.GetComponent<Rigidbody>().AddForce(m_NewForce, ForceMode.Impulse);
            }

            if (randChanceS < SilverMoneyChance)
            {
                //offset = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
                Vector3 m_NewForce = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
                GameObject S = Instantiate(MoneyRefArray[1], transform.position, Quaternion.Euler(90, 0, 0));
                S.GetComponent<Rigidbody>().AddForce(m_NewForce, ForceMode.Impulse);
            }

            if (randChanceG < GoldMoneyChance)
            {
                //offset = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
                Vector3 m_NewForce = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
                GameObject G = Instantiate(MoneyRefArray[2], transform.position, Quaternion.Euler(90, 0, 0));
                G.GetComponent<Rigidbody>().AddForce(m_NewForce, ForceMode.Impulse);
            }

            //if (randChanceSteve < SteveMoneyChance)
            //{
             //   //offset = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
            //    Vector3 m_NewForce = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
            //    GameObject Steve = Instantiate(MoneyRefArray[3], transform.position, Quaternion.Euler(90, 0, 0));
            //    Steve.GetComponent<Rigidbody>().AddForce(m_NewForce, ForceMode.Impulse);
            //}
        }
    }

    private void OnDestroy()
    {
        GameEvents.current.OnEnemyDeath -= DropCardOnDeath;
    }

    //FadeOut calls Coroutine -Grant Davis
    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    //Change color of enemy and fade out effect, enemy object is destroy here -Grant Davis
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
