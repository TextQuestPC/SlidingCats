using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Boosters
{
    public class BuyBoosterWindow : Singleton<BuyBoosterWindow>
    {
        private const float OFFSET_SUBSTRTE = 1200f;

        [SerializeField] private Sprite hammerSprite, magnetSprite;
        [Space] [SerializeField] private GameObject background, substrate;
        [SerializeField] private Button closeButton;
        [SerializeField] private BuyComponent[] buyComponents;

        private List<BuyData> hammerBuyData, magnetBuyData;
        private bool isBucket;

        private void Start()
        {
            hammerBuyData = new List<BuyData>();
            magnetBuyData = new List<BuyData>();

            hammerBuyData.Add(new BuyData(TypeBooster.Hammer, hammerSprite, true, 0, 4));
            hammerBuyData.Add(new BuyData(TypeBooster.Hammer, hammerSprite, false, 20, 4));

            magnetBuyData.Add(new BuyData(TypeBooster.Magnet, magnetSprite, true, 0, 2));
            magnetBuyData.Add(new BuyData(TypeBooster.Magnet, magnetSprite, false, 20, 4));

            closeButton.onClick.AddListener(() => { CloseWindow(); });
        }

        public void ClickBuyButton(BuyData data)
        {
            CloseWindow();

            if (data.IsAd)
            {
                if (AdManager.Instance.CanShowReward)
                {
                    if (data.TypeBooster == TypeBooster.Hammer)
                    {
                        // FirebaseManager.LogEvent($"booster_hammer_add_reward");
                    }
                    else if (data.TypeBooster == TypeBooster.Magnet)
                    {
                        // FirebaseManager.LogEvent($"booster_magnet_add_reward");
                    }

                    AdManager.Instance.ShowReward((bool isShow) =>
                    {
                        BoostersController.Instance.AddBooster(data.TypeBooster, data.CountBoosters);
                    });
                }
            }
            else
            {
                GoldController.Instance.SpendGold(data.Price);

                if (data.TypeBooster == TypeBooster.Hammer)
                {
                    // FirebaseManager.LogEvent($"booster_hammer_add_reward");
                }
                else if (data.TypeBooster == TypeBooster.Magnet)
                {
                    // FirebaseManager.LogEvent($"booster_magnet_add_reward");
                }

                BoostersController.Instance.AddBooster(data.TypeBooster, data.CountBoosters);
            }
        }

        public void OpenWindow(TypeBooster typeBooster)
        {
            if (typeBooster == TypeBooster.Hammer)
            {
                for (int i = 0; i < buyComponents.Length; i++)
                {
                    buyComponents[i].SetData(hammerBuyData[i]);
                }
            }
            else if (typeBooster == TypeBooster.Magnet)
            {
                for (int i = 0; i < buyComponents.Length; i++)
                {
                    buyComponents[i].SetData(magnetBuyData[i]);
                }
            }

            background.SetActive(true);
            Vector3 position = background.transform.position;

            substrate.transform.DOLocalMove(position, 0.3f);
        }

        private void CloseWindow()
        {
            Vector3 position = substrate.transform.position;
            position.x += OFFSET_SUBSTRTE;
            substrate.transform.DOLocalMove(position, 0.3f);
            DOTween.Sequence().AppendInterval(0.3f).OnComplete(() => { background.SetActive(false); });
        }
    }

    public class BuyData
    {
        public TypeBooster TypeBooster;
        public Sprite BoosterSprite;
        public bool IsAd;
        public int Price;
        public int CountBoosters;

        public BuyData(TypeBooster typeBooster, Sprite boosterSprite, bool isAd, int price, int countBoosters)
        {
            TypeBooster = typeBooster;
            BoosterSprite = boosterSprite;
            IsAd = isAd;
            Price = price;
            CountBoosters = countBoosters;
        }
    }
}