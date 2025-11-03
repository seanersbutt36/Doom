using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public InputAction attackAction;

    public Animator gunAnim;

    public float range = 20f;
    public float verticalRange = 20f;
    public float gunShotRadius = 20f;

    public float smallDamage = 1f;
    public float bigDamage = 2f;

    public float fireRate = 1f;
    private float nextTimeToFire;

    public int maxAmmo;
    [SerializeField] private int ammo;

    public LayerMask raycastLayerMask;
    public LayerMask enemyLayerMask;

    private BoxCollider gunTrigger;
    public EnemyManager enemyManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackAction = InputSystem.actions.FindAction("Attack");

        nextTimeToFire = 0f;

        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range * 0.5f);

        UIManager.Instance.UpdateAmmo(ammo);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackAction.IsPressed() && nextTimeToFire <= 0f && ammo > 0)
        {
            PlayGunAnim();
            Fire();
        }
        else if(nextTimeToFire > 0f)
        {
            gunAnim.SetBool("Fire", false);
            nextTimeToFire -= Time.deltaTime;
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
                Debug.Log("Hit: " + hit.collider.name);
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
        nextTimeToFire = fireRate;

        // Deduct ammo
        ammo--;
        UIManager.Instance.UpdateAmmo(ammo);
    }

    public void PlayGunAnim()
    {
        gunAnim.SetBool("Fire", true);
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
