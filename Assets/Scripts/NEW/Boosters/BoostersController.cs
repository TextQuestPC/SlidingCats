using System;
using DG.Tweening;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Boosters
{
    public class BoostersController : Singleton<BoostersController>
    {
        private const string KEY_HAMMER = "Hammer", KEY_MAGNET = "Magnet";
        private const int DEFAULT_COUNT_HAMMER = 20, DEFAULT_COUNT_MAGNET = 200;

        private const float START_GOLD_BOOSTER_TIME = 60, START_BOOSTER_TIME = 10;
        [SerializeField] private Booster hammerBooster, magnetBooster;
        [SerializeField] private GamePlayDialog gamePlayDialog;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip hammerAudio, magnetAudio;

        private int countHammer = 0, countMagnet = 0;

        private TypeBooster activeBooster;
        private bool isMagnetSelected, isGetMagnet;
        private float goldBoosterTime, boosterTime;
        
        public bool BoosterIsActive;
        public bool GoldBoosterIsReady{get=>goldBoosterTime<=0;}
        public bool BoosterIsReady{get=>boosterTime<=0;}

        public TypeBooster GetRndBooster
        {
            get
            {
                isGetMagnet = !isGetMagnet;
                if (isGetMagnet)
                {
                    return TypeBooster.Magnet;
                }
                else
                {
                    return TypeBooster.Hammer;
                }
            }
        }

        protected override void AfterAwaik()
        {
            countHammer = PlayerPrefs.GetInt(KEY_HAMMER, DEFAULT_COUNT_HAMMER);
            countMagnet = PlayerPrefs.GetInt(KEY_MAGNET, DEFAULT_COUNT_MAGNET);
            SaveCountBooster();

            hammerBooster.SetData(countHammer);
            magnetBooster.SetData(countMagnet);
            
            goldBoosterTime = START_GOLD_BOOSTER_TIME;
            boosterTime = START_BOOSTER_TIME;

            BoosterIsActive = false;
        }

        private void Update()
        {
            if (goldBoosterTime > 0)
            {
                goldBoosterTime -= Time.deltaTime;
            }
            
            if (boosterTime > 0)
            {
                boosterTime -= Time.deltaTime;
            }
        }

        public void SpawnGetBooster(TypeBooster typeBooster)
        {
            if (typeBooster == TypeBooster.AddGold)
            {
                goldBoosterTime = START_GOLD_BOOSTER_TIME;
            }
            else
            {
                boosterTime = START_BOOSTER_TIME;
            }
        }

        public void TakeBooster(TypeBooster typeBooster)
        {
            if (typeBooster == TypeBooster.AddGold)
            {
                AddGoldWindow.Instance.OpenWindow();
            }
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
                    BlockItem.SelectedForMagnet += SelectedLineForMagnet;
                    BlockItem.UseMagnet += UseMagnet;
                    BlockItem.StopSelectedMagnet += DeselectLineForMagnet;
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

        private void SelectedLineForMagnet(float posY)
        {
            isMagnetSelected = true;
            gamePlayDialog.SelectLineForMagnet(posY);
        }

        private void DeselectLineForMagnet()
        {
            if (isMagnetSelected)
            {
                isMagnetSelected = false;
                DisableBoosters();
            }
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
            BlockItem.SelectedForMagnet -= SelectedLineForMagnet;
            BlockItem.UseMagnet -= UseMagnet;
            BlockItem.StopSelectedMagnet -= DeselectLineForMagnet;

            gamePlayDialog.UnselectLineForMagner();


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
        Magnet,
        AddGold
    }
}