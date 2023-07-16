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
        [SerializeField] private Sprite activeSrite, defaultSprite;

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
        }
        
        public void SetActive(bool isActive)
        {
            icon.sprite = isActive ? activeSrite : defaultSprite;
        }
    }
}