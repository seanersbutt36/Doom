using UnityEditor;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 1;
    public float fireRate = 1f;
    private float nextTimeToFire;

    public GameObject prefabBullet;
    public Transform bulletTransform;
    public float bulletSpeed = 5f;

    private void Update()
    {
        // Reset timer
        nextTimeToFire = Time.time + fireRate;
    }

    public void Fire()
    {
        if (Time.time > nextTimeToFire)
        {
            GameObject bullet = Instantiate(prefabBullet, bulletTransform.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = bulletTransform.forward * bulletSpeed;
        }
    }
}
