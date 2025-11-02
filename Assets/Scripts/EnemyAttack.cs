using UnityEditor;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 1;
    [SerializeField] private float fireRate = 1f;
    private float nextTimeToFire;

    public GameObject prefabBullet;
    public Transform bulletTransform;
    [SerializeField] private float bulletSpeed = 5f;

    private void Update()
    {
        // Reset timer
        //nextTimeToFire = Time.time + fireRate;
        if (nextTimeToFire > 0f)
        {
            nextTimeToFire -= Time.deltaTime;
        }
    }

    public void Fire()
    {
        if (nextTimeToFire <= 0f)
        {
            GameObject bullet = Instantiate(prefabBullet, bulletTransform.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = bulletTransform.forward * bulletSpeed;
            nextTimeToFire = fireRate;
        }
    }
}
