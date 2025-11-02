using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public bool isBlueKey, isGreenKey, isRedKey;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isRedKey)
            {
                other.GetComponent<PlayerInventory>().hasRed = true;
                UIManager.Instance.UpdateKeys("red");
            }

            if (isGreenKey)
            {
                other.GetComponent<PlayerInventory>().hasGreen = true;
                UIManager.Instance.UpdateKeys("green");
            }

            if (isBlueKey)
            {
                other.GetComponent<PlayerInventory>().hasBlue = true;
                UIManager.Instance.UpdateKeys("blue");
            }

            Destroy(gameObject);
        }

    }
}
