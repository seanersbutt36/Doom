using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 20f;
    public float verticalRange = 20f;
    public float gunShotRadius = 20f;

    public float smallDamage = 1f;
    public float bigDamage = 2f;

    public float fireRate = 1f;
    private float nextTimeToFire;

    public int maxAmmo;
    private int ammo;

    public LayerMask raycastLayerMask;
    public LayerMask enemyLayerMask;

    private BoxCollider gunTrigger;
    public EnemyManager enemyManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range * 0.5f);

        UIManager.Instance.UpdateAmmo(ammo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire && ammo > 0)
        {
            Fire();
        }
    }

    private void Fire()
    {
        // simulate gun shot radius

        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);

        // alert any enemy in earshot
        foreach (var enemyCollider in enemyColliders)
        {
            enemyCollider.GetComponent<EnemyAwareness>().isAggro = true;
        }

        // Play test audio
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();

        // Damage enemies
        foreach(var enemy in enemyManager.enemiesInTrigger)
        {
            var dir = enemy.transform.position - transform.position;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, range * 1.5f, raycastLayerMask))
            {
                if (hit.transform == enemy.transform)
                {
                    // Range check
                    float dist = Vector3.Distance(enemy.transform.position, transform.position);

                    if (dist > range * 0.5f)
                    {
                        // Damage enemy
                        enemy.TakeDamage(smallDamage);
                    }
                    else
                    {
                        // Damage enemy
                        enemy.TakeDamage(bigDamage);

                    }
                }
            }
        }

        // Reset timer
        nextTimeToFire = Time.time + fireRate;

        // Deduct ammo
        ammo--;
        UIManager.Instance.UpdateAmmo(ammo);
    }

    public void GiveAmmo(int amount, GameObject pickup)
    {
        if (ammo < maxAmmo)
        {
            ammo += amount;
            Destroy(pickup);
        }

        if (ammo > maxAmmo)
        {
            ammo = maxAmmo;
        }

        UIManager.Instance.UpdateAmmo(ammo);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Add enemy to shoot
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            enemyManager.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Remove enemy to shoot
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            enemyManager.RemoveEnemy(enemy);
        }

    }
}
