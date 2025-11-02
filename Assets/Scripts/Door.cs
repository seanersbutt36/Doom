using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator anim;

    public bool requireKey;
    public bool reqRed, reqGreen, reqBlue;

    public GameObject areaToSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (requireKey)
            {
                if (reqRed && other.GetComponent<PlayerInventory>().hasRed)
                {
                    // Open door
                    anim.SetTrigger("Open");

                    // Spawn enemys in area
                    areaToSpawn.SetActive(true);
                }

                if (reqBlue && other.GetComponent<PlayerInventory>().hasBlue)
                {
                    // Open door
                    anim.SetTrigger("Open");

                    // Spawn enemys in area
                    areaToSpawn.SetActive(true);
                }

                if (reqGreen && other.GetComponent<PlayerInventory>().hasGreen)
                {
                    // Open door
                    anim.SetTrigger("Open");

                    // Spawn enemys in area
                    areaToSpawn.SetActive(true);
                }
            }
            else
            {
                // Open door
                anim.SetTrigger("Open");

                // Spawn enemys in area
                areaToSpawn.SetActive(true);
            }
        }
    }
}
