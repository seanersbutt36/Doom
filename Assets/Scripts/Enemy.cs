using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyManager enemyManager;
    private EnemyAI enemyAI;
    private Animator spriteAnim;
    private AngleToPlayer angleToPlayer;

    private float enemyHealth = 2f;

    public GameObject hitEffect;

    private EnemyAwareness enemyAwareness;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteAnim = GetComponentInChildren<Animator>();
        angleToPlayer = GetComponent<AngleToPlayer>();
        enemyAI = GetComponent<EnemyAI>();

        enemyManager = FindAnyObjectByType<EnemyManager>();

        enemyAwareness = GetComponent<EnemyAwareness>();
    }

    // Update is called once per frame
    void Update()
    {
        // Beginning of update set the animations rotational index
        spriteAnim.SetFloat("spriteRot", angleToPlayer.lastIndex);
        Debug.Log(enemyAwareness.isAggro);
        if (enemyAwareness.isAggro)
        {
            spriteAnim.SetBool("Moving", true);
        }
        else
        {
            spriteAnim.SetBool("Moving", false);
        }

        if (enemyHealth <= 0)
        {
            enemyManager.RemoveEnemy(this);
            Destroy(gameObject);
        }

        // Any animations we call will have updated index
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(damage);
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        enemyHealth -= damage;
    }
}
