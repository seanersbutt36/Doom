using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int health;

    public int maxArmor;
    private int armor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        UIManager.Instance.UpdateHealth(health);
        UIManager.Instance.UpdateArmor(armor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage)
    {
        // if the player has armor, damage it instead
        if (armor > 0)
        {
            // if the player has enough armor to absorb all the damage then only damage the armor

            // if the player only has enough armor to absorb some of the damage
            // then damage the armor first and then damage the player
            if (armor >= damage)
            {
                armor -= damage;
            }
            else if(armor < damage)
            {
                int remainingDamage;

                remainingDamage = damage - armor;

                armor = 0;

                health -= remainingDamage;
            }
        }
        else
        {
            health -= damage;
        }

        if (health <= 0)
        {
            // Player is dead
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }

        UIManager.Instance.UpdateHealth(health);
        UIManager.Instance.UpdateArmor(armor);
    }

    public void GiveHealth(int amount, GameObject pickup)
    {
        if (health < maxHealth)
        {
            health += amount;
            Destroy(pickup);
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        UIManager.Instance.UpdateHealth(health);
    }

    public void GiveArmor(int amount, GameObject pickup)
    {
        if (armor < maxArmor)
        {
            armor += amount;
            Destroy(pickup);
        }

        if (armor > maxArmor)
        {
            armor = maxArmor;
        }

        UIManager.Instance.UpdateArmor(armor);
    }
}
