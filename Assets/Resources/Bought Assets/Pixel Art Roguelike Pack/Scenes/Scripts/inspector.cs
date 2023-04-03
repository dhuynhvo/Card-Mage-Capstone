using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inspector : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private int KeyInspect;                  //don't use for anything else
    [SerializeField] private int MoneyInspect;
    [SerializeField] private bool PlayerIsMovingInspect;
    [SerializeField] private bool PlayerIsActingInspect;
    [SerializeField] private bool PlayerTurnInspect;
    [SerializeField] private bool EnemyMove;
    [SerializeField] private bool PlayerWait;
    [SerializeField] private int PlayerLife;
    [Header("Change")]
    [SerializeField] private bool OVERRITE = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(OVERRITE){
            PlayerController.PlayerWait = PlayerWait;
            //OVERRITE = false;
        }else{
            PlayerIsMovingInspect = PlayerController.PlayerIsmoving;
            KeyInspect = PlayerController.Keys;
            MoneyInspect = PlayerController.Money;
            PlayerIsActingInspect = PlayerController.PlayerAction;
            PlayerTurnInspect = PlayerController.PlayerTurn;
            EnemyMove = Enemy.moveEnemy;
            PlayerLife = PlayerController.life;
            PlayerWait = PlayerController.PlayerWait;
        }
        
    }
}
