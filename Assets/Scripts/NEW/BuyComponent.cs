using UnityEngine;
using UnityEngine.UI;

namespace Boosters
{
    public class BuyComponent : MonoBehaviour
    {
        [SerializeField] private Image boosterImage;
        [SerializeField] private Button buyButton;
        [SerializeField] private GameObject adImage;
        [SerializeField] private Text countBoosterText, priceText;

        private BuyData data;

        private void Start()
        {
            buyButton.onClick.AddListener(() =>
            {
                BuyBoosterWindow.Instance.ClickBuyButton(data);  
            });
        }

        public void SetData(BuyData buyData)
        {
            data = buyData;

            adImage.SetActive(data.IsAd);
            boosterImage.sprite = data.BoosterSprite;

            priceText.text = data.Price.ToString();
            countBoosterText.text = $"+{data.CountBoosters.ToString()}";

            if (data.IsAd)
            {
                buyButton.interactable = AdManager.Instance.CanShowReward;
            }
            else
            {
                buyButton.interactable = GoldController.Instance.CanSpendGold(data.Price);
            }
        }
    }
}