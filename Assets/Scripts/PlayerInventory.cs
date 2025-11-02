using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasRed, hasGreen, hasBlue;

    private void Start()
    {
        UIManager.Instance.ClearKeys();
    }
}
