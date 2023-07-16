using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Boosters
{
    public class Booster : MonoBehaviour
    {
        [SerializeField] private TypeBooster typeBooster;
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private Image icon;
        [SerializeField] private Sprite activeSrite, blockSprite;

        private bool isActive;
        
        public TypeBooster GetTypeBooster => typeBooster;
        
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                BoostersController.Instance.TryActivateBooster(typeBooster);
            });
        }

        public void SetData(int count)
        {
            countText.text = count.ToString();
            isActive = count > 0;
            icon.sprite = isActive ? activeSrite : blockSprite;
        }
    }
}