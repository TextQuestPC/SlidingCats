using Boosters;
using Models;
using Other;
using UI;
using UnityEngine;

namespace Boosters
{
    public class BoostersController : Singleton<BoostersController>
    {
        private const string KEY_HAMMER = "Hammer", KEY_MAGNET = "Magnet";
        private const int DEFAULT_COUNT_HAMMER = 40, DEFAULT_COUNT_MAGNET = 20;

        [SerializeField] private Booster hammerBooster, magnetBooster;
        [SerializeField] private GamePlayDialog gamePlayDialog;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip hammerAudio, magnetAudio;

        private int countHammer = 0, countMagnet = 0;

        private TypeBooster activeBooster;

        protected override void AfterAwaik()
        {
            countHammer = PlayerPrefs.GetInt(KEY_HAMMER, DEFAULT_COUNT_HAMMER);
            countMagnet = PlayerPrefs.GetInt(KEY_MAGNET, DEFAULT_COUNT_MAGNET);
            SaveCountBooster();

            hammerBooster.SetData(countHammer);
            magnetBooster.SetData(countMagnet);
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

            if (typeBooster == TypeBooster.Hammer && countHammer > 0)
            {
                activeBooster = TypeBooster.Hammer;
                BlockItem.Touched += UseHammer;
                hammerBooster.SetActive(true);
            }
            else if (typeBooster == TypeBooster.Magnet && countMagnet > 0)
            {
                activeBooster = TypeBooster.Magnet;
                BlockItem.Selected += UseMagnet;
                magnetBooster.SetActive(true);
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
        }

        private void UseMagnet(Vector2 position)
        {
            PlayAudio();
            
            var itemList = gamePlayDialog.ItemList;
            var posY = position.y;

            for (var i = 0; i < itemList.Count; ++i)
            {
                if (itemList[i] != null)
                {
                    if (itemList[i].TryGetComponent(out BlockItem blockItem))
                    {
                        blockItem.SetOriginalPos();

                        var blockPos = blockItem.OriginalPos;

                        Debug.Log($"PosY: {posY}");
                        Debug.Log($"Block Y: {blockPos.y}");


                        if (posY == blockPos.y)
                        {
                            Blocks.ClearBlockDataByIndex(i);
                            gamePlayDialog.RemoveBlockItemByIndex(i, true);
                        }
                    }
                }
            }

            Blocks.UpdateMap();
            StartCoroutine(Delay.Run(() => { gamePlayDialog.MoveEnd(); }, 0.05f));
            Player.SaveGameStatusData();
            
            countMagnet--;
            magnetBooster.SetData(countMagnet);
            SaveCountBooster();

            DisableBoosters();
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