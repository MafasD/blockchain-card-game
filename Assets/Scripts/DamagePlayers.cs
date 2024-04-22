using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DamagePlayers : MonoBehaviour
{
    public Image playerHealthBar;
    public Image opponentHealthBar;
    public float damagePercentage = 0.2f;

    public void LowerPlayerHealth()
    {
        int damage = Mathf.RoundToInt(playerHealthBar.fillAmount * damagePercentage);
        playerHealthBar.fillAmount -= damagePercentage;
        CheckEndGame(playerHealthBar);
    }

    public void LowerOpponentHealth()
    {
        int damage = Mathf.RoundToInt(opponentHealthBar.fillAmount * damagePercentage);
        opponentHealthBar.fillAmount -= damagePercentage;
        CheckEndGame(opponentHealthBar);
    }

    void CheckEndGame(Image healthBar)
    {
        if (healthBar.fillAmount <= 0.1) //If either healthbar value is lower than or equal to 0.1, match ends and player will be directed to a match end scene.
        {
            if (healthBar == playerHealthBar)
            {
                SceneManager.LoadScene("LoseScene");
            }
            else if (healthBar == opponentHealthBar)
            {
                SceneManager.LoadScene("WinScene");
            }
        }
    }
}