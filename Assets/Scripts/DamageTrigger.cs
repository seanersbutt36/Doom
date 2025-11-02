using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    private bool damagingPlayer;
    private PlayerHealth playerHealth;

    public int damageAmount = 10;
    public float timeBetweenDamage = 1.5f;

    private float damageCounter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damageCounter = timeBetweenDamage;
        playerHealth = FindAnyObjectByType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (damagingPlayer)
        {
            // Damage player every (timeBetweenDamage)
            if(damageCounter > timeBetweenDamage)
            {
                // Damage player
                playerHealth.DamagePlayer(damageAmount);

                // Restart counter
                damageCounter = 0f;
            }

            damageCounter += Time.deltaTime;
        }
        else
        {
            // Keep damage counter reset
            damageCounter = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damagingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damagingPlayer = false;
        }
    }
}
