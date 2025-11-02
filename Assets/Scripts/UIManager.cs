using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI armor;
    public TextMeshProUGUI ammo;

    public GameObject redKey;
    public GameObject blueKey;
    public GameObject greenKey;

    private static UIManager _instance;
    public static UIManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void UpdateHealth(int healthValue)
    {
        health.text = healthValue.ToString() + "%";
    }

    public void UpdateArmor(int armorValue)
    {
        armor.text = armorValue.ToString() + "%";
    }

    public void UpdateAmmo(int ammoValue)
    {
        ammo.text = ammoValue.ToString();
    }

    public void UpdateKeys(string keyColor)
    {
        if (keyColor == "red")
        {
            redKey.SetActive(true);
        }

        if (keyColor == "blue")
        {
            blueKey.SetActive(true);
        }

        if (keyColor == "green")
        {
            greenKey.SetActive(true);
        }
    }

    public void ClearKeys()
    {
        redKey.SetActive(false);
        blueKey.SetActive(false);
        greenKey.SetActive(false);
    }
}
