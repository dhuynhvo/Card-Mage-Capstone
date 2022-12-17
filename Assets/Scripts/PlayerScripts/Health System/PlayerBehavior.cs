using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Worked on by Abida
// used outside sources for basis/research


    // connects health bar to character
public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            PlayerTakeDmg(10);
            Debug.Log(GameManager.gameManager.playerHealth.Health);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PlayerHeal(10);
            Debug.Log(GameManager.gameManager.playerHealth.Health);
        }
    }

        // manages player getting damage
    private void PlayerTakeDmg(int dmg)
    {
        GameManager.gameManager.playerHealth.DmgUnit(dmg);
        healthBar.SetHealth(GameManager.gameManager.playerHealth.Health);
    }

        // manages player getting healing
    private void PlayerHeal(int healing)
    {
        GameManager.gameManager.playerHealth.HealUnit(healing);
        healthBar.SetHealth(GameManager.gameManager.playerHealth.Health);
    }

}
