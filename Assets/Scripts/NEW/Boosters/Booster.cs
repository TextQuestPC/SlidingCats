using DG.Tweening;
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

        public void SetData(int count, bool isAdd = false)
        {
            countText.text = count.ToString();

            if (isAdd)
            {
                icon.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f).OnComplete(() =>
                {
                    icon.transform.DOScale(new Vector3(1, 1, 1), 0.2f);
                });
            }
        }
        
        public void SetActive(bool isActive)
        {
            icon.sprite = isActive ? activeSrite : defaultSprite;
        }
    }
}