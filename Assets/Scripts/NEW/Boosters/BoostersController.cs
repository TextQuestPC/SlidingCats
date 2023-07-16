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
        private const int DEFAULT_COUNT_HAMMER = 4, DEFAULT_COUNT_MAGNET = 2;

        [SerializeField] private Booster hammerBooster, magnetBooster;
        [SerializeField] private GamePlayDialog gamePlayDialog;

        private int countHammer = 0, countMagnet = 0;

        private TypeBooster activeBooster;

        protected override void AfterAwaik()
        {
            countHammer = PlayerPrefs.GetInt(KEY_HAMMER, DEFAULT_COUNT_HAMMER);
            countMagnet = PlayerPrefs.GetInt(KEY_MAGNET, DEFAULT_COUNT_MAGNET);

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
            Blocks.ClearBlockDataByIndex(index);
            gamePlayDialog.RemoveBlockItemByIndex(index, false);
            Blocks.UpdateMap();
            StartCoroutine(Delay.Run(() => { gamePlayDialog.MoveEnd(); }, 0.05f));
            Player.SaveGameStatusData();
            
            DisableBoosters();

        }

        private void UseMagnet(Vector2 position)
        {
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
                            gamePlayDialog.RemoveBlockItemByIndex(i, false);
                        }
                    }
                }
            }

            Blocks.UpdateMap();
            StartCoroutine(Delay.Run(() => { gamePlayDialog.MoveEnd(); }, 0.05f));
            Player.SaveGameStatusData();
            
            DisableBoosters();
        }

        private void DisableBoosters()
        {
            activeBooster = TypeBooster.None;

            BlockItem.Touched -= UseHammer;
            BlockItem.Selected -= UseMagnet;
            
            magnetBooster.SetActive(false);
            hammerBooster.SetActive(false);
        }
    }

    public enum TypeBooster
    {
        None,
        Hammer,
        Magnet
    }
}