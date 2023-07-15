using UnityEngine;

using TMPro;

namespace Boosters
{
    public class BoosterUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private TextMeshProUGUI priceText;
    
        [SerializeField] private Booster booster;

        private void OnEnable() => booster.ValueChanged += OnValueChanged;

        private void OnDisable() => booster.ValueChanged -= OnValueChanged;

        private void OnValueChanged()
        {
            priceText.text = booster.Price.ToString();
            countText.text = "x" + booster.Count;
        }
    }   
}