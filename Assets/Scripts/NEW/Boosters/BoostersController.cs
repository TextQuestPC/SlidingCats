using DG.Tweening;
using UI;
using UnityEngine;

namespace Boosters
{
    public class BoostersController : Singleton<BoostersController>
    {
        private const string KEY_HAMMER = "Hammer", KEY_MAGNET = "Magnet";
        private const int DEFAULT_COUNT_HAMMER = 4, DEFAULT_COUNT_MAGNET = 2;

        [SerializeField] private Booster hammerBooster, magnetBooster;
        [SerializeField] private GamePlayDialog gamePlayDialog;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip hammerAudio, magnetAudio;

        private int countHammer = 0, countMagnet = 0;

        private TypeBooster activeBooster;

        public bool BoosterIsActive;

        protected override void AfterAwaik()
        {
            countHammer = PlayerPrefs.GetInt(KEY_HAMMER, DEFAULT_COUNT_HAMMER);
            countMagnet = PlayerPrefs.GetInt(KEY_MAGNET, DEFAULT_COUNT_MAGNET);
            SaveCountBooster();

            hammerBooster.SetData(countHammer);
            magnetBooster.SetData(countMagnet);

            BoosterIsActive = false;
        }

        public void AddBooster(TypeBooster typeBooster, int count)
        {
            if (typeBooster == TypeBooster.Hammer)
            {
                countHammer += count;
                hammerBooster.SetData(countHammer);
            }
            else if (typeBooster == TypeBooster.Magnet)
            {
                countMagnet += count;
                magnetBooster.SetData(countMagnet);
            }

            SaveCountBooster();
        }

        public void TryActivateBooster(TypeBooster typeBooster)
        {
            if (activeBooster == typeBooster)
            {
                DisableBoosters();

                return;
            }
            else
            {
                DisableBoosters();
            }

            activeBooster = typeBooster;
            BoosterIsActive = true;

            if (typeBooster == TypeBooster.Hammer)
            {
                if (countHammer > 0)
                {
                    activeBooster = TypeBooster.Hammer;
                    BlockItem.Touched += UseHammer;
                    hammerBooster.SetActive(true);
                }
                else
                {
                    BuyBoosterWindow.Instance.OpenWindow(TypeBooster.Hammer);
                    DisableBoosters();
                }
            }
            else if (typeBooster == TypeBooster.Magnet)
            {
                if (countMagnet > 0)
                {
                    activeBooster = TypeBooster.Magnet;
                    BlockItem.Selected += UseMagnet;
                    magnetBooster.SetActive(true);
                }
                else
                {
                    BuyBoosterWindow.Instance.OpenWindow(TypeBooster.Magnet);
                    DisableBoosters();
                }
            }
        }

        private void UseHammer(int index)
        {
            PlayAudio();

            gamePlayDialog.UseHammerBooster(index);

            countHammer--;
            hammerBooster.SetData(countHammer);
            SaveCountBooster();

            DisableBoosters();

            DOTween.Sequence().AppendInterval(0.1f).OnComplete(() => BoosterIsActive = false);
        }

        private void UseMagnet(Vector2 position)
        {
            PlayAudio();

            gamePlayDialog.UseMagnet(position);

            countMagnet--;
            magnetBooster.SetData(countMagnet);
            SaveCountBooster();

            DisableBoosters();

            DOTween.Sequence().AppendInterval(0.1f).OnComplete(() => BoosterIsActive = false);
        }

        private void PlayAudio()
        {
            if (activeBooster == TypeBooster.Hammer)
            {
                audioSource.clip = hammerAudio;
            }
            else if (activeBooster == TypeBooster.Magnet)
            {
                audioSource.clip = magnetAudio;
            }

            audioSource.Play();
        }

        private void DisableBoosters()
        {
            activeBooster = TypeBooster.None;

            BlockItem.Touched -= UseHammer;
            BlockItem.Selected -= UseMagnet;
            // BlockItem.OnPointerUp();

            magnetBooster.SetActive(false);
            hammerBooster.SetActive(false);
        }

        private void SaveCountBooster()
        {
            PlayerPrefs.SetInt(KEY_HAMMER, countHammer);
            PlayerPrefs.SetInt(KEY_MAGNET, countMagnet);
        }
    }

    public enum TypeBooster
    {
        None,
        Hammer,
        Magnet
    }
}