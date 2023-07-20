using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AddGoldWindow : Singleton<AddGoldWindow>
{
    private const float OFFSET_SUBSTRTE = 1200f;
    private const int CLAIM_VALUE = 10, REWARD_VALUE = 20;

    [SerializeField] private GameObject substrate, background;
    [SerializeField] private Button claimButton, rewardButton;
    [SerializeField] private Text claimValueText, rewardValueText;

    private void Start()
    {
        claimValueText.text = CLAIM_VALUE.ToString();
        rewardValueText.text = REWARD_VALUE.ToString();

        claimButton.onClick.AddListener(() =>
        {
            CloseWindow();

            GoldController.Instance.AddGold(CLAIM_VALUE);
        });

        rewardButton.onClick.AddListener(() =>
        {
            if (AdManager.Instance.CanShowReward)
            {
                AdManager.Instance.ShowReward((bool isShow) =>
                {
                    CloseWindow();

                    GoldController.Instance.AddGold(REWARD_VALUE);
                });
            }
        });
    }

    public void OpenWindow()
    {
        rewardButton.interactable = AdManager.Instance.CanShowReward;

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