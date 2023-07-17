using TMPro;
using UnityEngine;

public class GoldController : Singleton<GoldController>
{
    private const string KEY_GOLD = "KEY_GOLD";
    private const int DEFAULT_GOLD = 20;
    
    [SerializeField] private TextMeshProUGUI goldText;

    private int countGold;

    protected override void AfterAwaik()
    {
        countGold = PlayerPrefs.GetInt(KEY_GOLD, DEFAULT_GOLD);
        goldText.text = countGold.ToString();
    }

    public void AddGold(int value)
    {
        countGold += value;
        goldText.text = countGold.ToString();
        PlayerPrefs.SetInt(KEY_GOLD, countGold);
    }

    public void SpendGold(int value)
    {
        countGold -= value;
        goldText.text = countGold.ToString();
        PlayerPrefs.SetInt(KEY_GOLD, countGold);
    }

    public bool CanSpendGold(int value)
    {
        return value <= countGold;
    }
}