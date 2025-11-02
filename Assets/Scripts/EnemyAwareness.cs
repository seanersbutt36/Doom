using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    public float awarenessRadius = 8f;
    public bool isAggro;
    private Transform playerTransform;

    public LayerMask playerMask;
    private RaycastHit hit;
    private EnemyAttack enemyAttack;

    private void Start()
    {
        playerTransform = FindAnyObjectByType<PlayerMove>().transform;
        enemyAttack = GetComponent<EnemyAttack>();
    }

    private void Update()
    {
        var dist = Vector3.Distance(transform.position, playerTransform.position);

        if (dist < awarenessRadius)
        {
            isAggro = true;
        }

        if (isAggro)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit,
                Mathf.Infinity, playerMask))
            {
                enemyAttack.Fire();
            }
        }
    }
}
