using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _coinText;

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance ==null)
            {
                Debug.LogError("UIManager is NULL:");
            }
            return _instance;
        }
    }

    void Start()
    {
        _instance = this;
        _coinText.text = "Coins: " + 0;
    }

    public void UpdateCoin(int amount)
    {
        _coinText.text = "Coins: " + amount;
    }
}
