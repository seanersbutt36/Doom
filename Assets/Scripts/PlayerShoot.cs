using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    // Temp Var
    public float damage = 1f;
    public float range = 1f;
    public float fireRate = 1f;
    private float nextTimeToFire;

    public Animator gunAnim;
    public InputAction attackAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
        nextTimeToFire = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackAction.IsPressed() && nextTimeToFire <= 0f)
        {
            nextTimeToFire = fireRate;
            PlayGunAnim();
        }
        else
        {
            nextTimeToFire -= Time.deltaTime;
            gunAnim.SetBool("Fire", false);
        }
    }

    public void PlayGunAnim()
    {
        gunAnim.SetBool("Fire", true);
    }
}
