//Dan Huynhvo
//UNR
//CS 425

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Play_Card : MonoBehaviour
{
    public Hand PlayerHand;
    public Hand EnemyHand; //Currently unused
    public Deck PlayerDeck;
    public Deck EnemyDeck; //Currently unused
    private int count;
    private bool QueueEmpty;
    private bool DeQueuePlaying;
    [SerializeField]
    public List<GameObject> CardQueue;
    [SerializeField]
    private int MaxCardsInQueue;
    public string Card1Bind, Card2Bind, Card3Bind, Card4Bind, LCMouseBind;
    public GameObject SpellSpawnArea;
    string[] binds = { "1", "2", "3", "4"};
    public GameObject newBasic;
    private bool BasicOn;
    private bool CRStarted;
    Spell_Info info;
    public bool NotSpamming;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    public float Creativity;
    [SerializeField]
    private float CreativityCounter;
    [SerializeField]
    private int MaxCreativity;
    [SerializeField]
    private string LastButton;
    [SerializeField]
    private Text CreativityText;
    [SerializeField]
    private bool IsAttacking;
    [SerializeField]
    private float AttackTimer;
    [SerializeField]
    private float AttackCooldown;
    [SerializeField]
    private Creativity_Sprites CSprites;

    public float AttackBuff;

    void Start()
    {
        NotSpamming = true;
        CardQueue = new List<GameObject>(new GameObject[MaxCardsInQueue]);
        StartCoroutine(GetOutQueue());
        InstBasic();
        AttackBuff = 1;
        Creativity = 0;
        MaxCreativity = 5;
    }

    void Update()
    {
        count = CardQueue.Count(x => x != null);
        PlayCard();
        PushQueueforward();
        //BasicBehavior();
    }

    private void FixedUpdate()
    {
        GetOutQueue();
        if (IsAttacking)
        {
            AttackTimer += Time.fixedDeltaTime;
            if (AttackTimer >= AttackCooldown)
            {
                AttackTimer = 0;
                IsAttacking = false;
                anim.SetBool("NA", true);
                //DashSphere.SetActive(false);
            }
        }
        //BasicCooldown(); //This is semihardcoded, a coroutine would work better but this is a scratchy fix
    }

    private IEnumerator SpamTimer()
    {
        yield return new WaitForSeconds(1f - Creativity);
        NotSpamming = true;
    }

    public void InstBasic() //unused
    {
        newBasic = Instantiate(PlayerHand.BasicSpell, SpellSpawnArea.transform.position, Quaternion.Euler(90,0,0));
        info = newBasic.GetComponent<Spell_Info>();
        newBasic.SetActive(false);
    }

    public void BasicBehavior()
    {
        newBasic.transform.position = SpellSpawnArea.transform.position;
        newBasic.transform.rotation = SpellSpawnArea.transform.rotation;

        if(Input.GetMouseButtonDown(0) && !BasicOn)
        {
            //Debug.Log("Basic Played");
            newBasic.SetActive(true);
            BasicOn = true;
        }
    }

    public void BasicCooldown()
    {
        if(BasicOn)
        {
            info.CooldownTimer += .002f;
            if(info.ActiveDuration <= info.CooldownTimer)
            {
                newBasic.SetActive(false);
            }
            if(info.cooldown <= info.CooldownTimer)
            {
                newBasic.GetComponent<Spell_Info>().CooldownTimer = 0;
                BasicOn = false;
            }
        }
    }

    public bool EmptyQueue()
    {
        for (int i = 0; i < MaxCardsInQueue; i++)
        {
            if (CardQueue[i] != null)
            {
                return false;
            }
        }

        return true;
    }

    //public Vector3 SpellSpawnPoint()


    public void AttackAnim()
    {
        anim.SetBool("NA", false);
        IsAttacking = true;
        float angle = SpellSpawnArea.transform.rotation.eulerAngles.y;
        if (angle > 45 && angle < 135)
        {
            anim.SetTrigger("AR");
            sprite.flipX = false;
        }

        else if(angle > 135 && angle < 225)
        {
            anim.SetTrigger("AD");
        }

        else if (angle > 225 && angle < 315)
        {
            anim.SetTrigger("AR");
            sprite.flipX = true;
        }

        else if ((angle > 315 && angle < 360) || (angle > 0 && angle < 45))
        {
            anim.SetTrigger("AU");
        }

        else if(angle == 45 || angle == 135)
        {
            anim.SetTrigger("AR");
            sprite.flipX = false;
        }

        else if (angle == 225 || angle == 315)
        {
            anim.SetTrigger("AR");
            sprite.flipX = true;
        }
    }

    public void SetAttackBoolsToFalse()
    {
        return;
    }

    public void SetCreativity(string button)
    {
        
        if(button != LastButton)
        {
            if(CreativityCounter < MaxCreativity)
            {
                CreativityCounter++;
            }
        }
        
        else
        {
            CreativityCounter = 0;
        }
        CSprites.SpriteOn(CreativityCounter);
        LastButton = button;
        Creativity = CreativityCounter / 10;
        CreativityText.text = "Creativity: " + CreativityCounter.ToString();
    }


    public void PlayCard()  //main logic for playing spells/cards
    {

        if (PlayerHand.HandEmptyCheck() && count == 0 && EmptyQueue())
        {
            NotSpamming = true;
            //PlayerDeck.GraveIndex = 0;
            //PlayerDeck.DeckReload();
            PlayerHand.FillHand();
        }

        else if (Input.GetMouseButtonDown(0) && PlayerHand.CardsInHand[0] != null && count < MaxCardsInQueue && NotSpamming && Time.timeScale != 0)
        {
            NotSpamming = false;
            AttackAnim();
            StartCoroutine(SpamTimer());
            audioDeterminer(PlayerHand.CardsInHand[0].name);
            GameObject newSpell_0 = Instantiate(PlayerHand.CardsInHand[0], transform.position, SpellSpawnArea.transform.rotation) as GameObject;
            newSpell_0.GetComponent<Spell_Info>().damage = newSpell_0.GetComponent<Spell_Info>().damage * AttackBuff * (1 + Creativity);
            SetCreativity("M1");
            PlayerDeck.GraveTheCard(newSpell_0, ref PlayerDeck.GraveIndex);
            PlayerHand.CardsInHand[0] = null;
            PlayerDeck.CheckEmptyDeck();
            if (PlayerDeck.Cards[0] != null)
            {
                PlayerHand.FillHand();
            };
        }

        else if (Input.GetKeyDown(KeyCode.Q) && PlayerHand.CardsInHand[1] != null && count < MaxCardsInQueue && NotSpamming && Time.timeScale != 0)
        {
            NotSpamming = false;
            AttackAnim();
            StartCoroutine(SpamTimer());
            audioDeterminer(PlayerHand.CardsInHand[1].name);
            GameObject newSpell_1 = Instantiate(PlayerHand.CardsInHand[1], transform.position, SpellSpawnArea.transform.rotation) as GameObject;
            newSpell_1.GetComponent<Spell_Info>().damage = newSpell_1.GetComponent<Spell_Info>().damage * AttackBuff * (1 + Creativity);
            SetCreativity("Shift");
            PlayerDeck.GraveTheCard(newSpell_1, ref PlayerDeck.GraveIndex);
            PlayerHand.CardsInHand[1] = null;
            PlayerDeck.CheckEmptyDeck();
            if (PlayerDeck.Cards[0] != null)
            {
                PlayerHand.FillHand();
            };
        }

        else if (Input.GetKeyDown(KeyCode.E) && PlayerHand.CardsInHand[2] != null && count < MaxCardsInQueue && NotSpamming && Time.timeScale != 0)
        {
            NotSpamming = false;
            AttackAnim();
            StartCoroutine(SpamTimer());
            audioDeterminer(PlayerHand.CardsInHand[2].name);
            GameObject newSpell_2 = Instantiate(PlayerHand.CardsInHand[2], transform.position, SpellSpawnArea.transform.rotation) as GameObject;
            newSpell_2.GetComponent<Spell_Info>().damage = newSpell_2.GetComponent<Spell_Info>().damage * AttackBuff * (1 + Creativity);
            SetCreativity("F");
            PlayerDeck.GraveTheCard(newSpell_2, ref PlayerDeck.GraveIndex);
            PlayerHand.CardsInHand[2] = null;
            PlayerDeck.CheckEmptyDeck();
            if (PlayerDeck.Cards[0] != null)
            {
                PlayerHand.FillHand();
            };
            //SetAttackBoolsToFalse();
        }

        else if (Input.GetMouseButtonDown(1) && PlayerHand.CardsInHand[3] != null && count < MaxCardsInQueue && NotSpamming && Time.timeScale != 0)
        {
            NotSpamming = false;
            AttackAnim();
            StartCoroutine(SpamTimer());
            GameObject newSpell_3 = Instantiate(PlayerHand.CardsInHand[3], transform.position, SpellSpawnArea.transform.rotation) as GameObject;
            newSpell_3.GetComponent<Spell_Info>().damage = newSpell_3.GetComponent<Spell_Info>().damage * AttackBuff * (1 + Creativity);
            SetCreativity("M2");
            audioDeterminer(PlayerHand.CardsInHand[3].name);
            PlayerDeck.GraveTheCard(newSpell_3, ref PlayerDeck.GraveIndex);
            PlayerHand.CardsInHand[3] = null;
            PlayerDeck.CheckEmptyDeck();
            if (PlayerDeck.Cards[0] != null)
            {
                PlayerHand.FillHand();
            };
            //SetAttackBoolsToFalse();
        }

        else if (!PlayerHand.HandEmptyCheck() && AnyKeyDown(binds))
        {
            Debug.Log("That hand slot is empty");
        }
    }
    private void audioDeterminer(string spellName)
    {
        switch (spellName)
        {
            case "Icicle":
                AudioManager.instance.Play("IciclePlayerSpell");
                break;
            case "ArcaneBolt":
                AudioManager.instance.Play("ArcaneBoltPlayerSpell");
                break;
            case "FireCube":
                AudioManager.instance.Play("FireCubePlayerSpell");
                break;
            case "WaterCube":
                AudioManager.instance.Play("WaterCubePlayerSpell");
                break;
            case "ArcaneCube":
                AudioManager.instance.Play("ArcaneCubePlayerSpell");
                break;
            case "LightningCone":
                AudioManager.instance.Play("LightningConePlayerSpell");
                break;
            case "VoidArea":
                AudioManager.instance.Play("VoidAreaPlayerSpell");
                break;
            case "EarthWall":
                AudioManager.instance.Play("EarthWallPlayerSpell");
                break;
            case "BlazeInfusion":
                AudioManager.instance.Play("BlazeInfusionPlayerSpell");
                break;
            case "AquaTonic":
                AudioManager.instance.Play("AquaTonicPlayerSpell");
                break;
            case "ManaShield":
                AudioManager.instance.Play("ManaShieldPlayerSpell");
                break;
            case "ShockBoots":
                AudioManager.instance.Play("ShockBootsPlayerSpell");
                break;
            case "SmokeForm":
                AudioManager.instance.Play("SmokeFormPlayerSpell");
                break;

            default:
                // code block
                break;
        }
    }



    public bool AnyKeyDown(IEnumerable<string> keys)
    {
        foreach (string key in keys)
        {
            if (Input.GetKeyDown(key)) return true;
        }
        return false;
    }

    private void PushQueueforward()
    {
        int j;
        if (count < MaxCardsInQueue)
        {
            if(CardQueue[MaxCardsInQueue - 1] != null)
            {
                for(int i = MaxCardsInQueue - 2; i >= 0; i--)
                {
                    j = i + 2;
                    if(CardQueue[i + 1] == null)
                    {
                        CardQueue[i + 1] = CardQueue[i];
                        CardQueue[i] = null;
                        while(CardQueue[j] == null)
                        {
                            CardQueue[j] = CardQueue[j - 1];
                            CardQueue[j - 1] = null;
                            j++;
                        }
                    }
                }
            }

            else if(CardQueue[MaxCardsInQueue - 1] == null)
            {
                for(int i = MaxCardsInQueue - 2; i >= 0; i--)
                {
                    if(CardQueue[i] != null)
                    {
                        CardQueue[MaxCardsInQueue - 1] = CardQueue[i];
                        CardQueue[i] = null;
                        break;
                    }
                }
            }    
        }
    }

    private IEnumerator GetOutQueue()
    {
        while(true)
        {
            for(int i = MaxCardsInQueue - 1; i > 0; i--)
            {
                if(count > 0 && CardQueue[i] != null)
                {
                    CardQueue[i].GetComponent<Spell_Info>().CooldownTimer++;
                    //Debug.Log(CardQueue[i].GetComponent<Spell_Info>().CooldownTimer);
                    if (CardQueue[i].GetComponent<Spell_Info>().CooldownTimer >= CardQueue[i].GetComponent<Spell_Info>().cooldown)
                    {
                        PlayerDeck.GraveTheCard(CardQueue[i], ref PlayerDeck.GraveIndex);
                        CardQueue[i] = null;
                    }

                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void ClearQueue()
    {
        for (int i = MaxCardsInQueue - 1; i > 0; i--)
        {
            CardQueue[i] = null;
        }
    }

}