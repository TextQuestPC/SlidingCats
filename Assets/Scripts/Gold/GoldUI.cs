using Manager;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;

    [SerializeField] private Gold _gold;

    private void Awake()
    {
        _gold.TryAdd(1000);
        
        Gold.GoldChanged += OnGoldChanged;
    } 
    
    private void Start() => OnGoldChanged();

    private void OnDisable() => Gold.GoldChanged -= OnGoldChanged;

    private void OnGoldChanged() => goldText.text = ManagerLocalData.GetIntData("GoldCount").ToString();
}