using Models;
using Other;
using UI;
using UnityEngine;

namespace Boosters
{
    public class BoosterBreak : Booster
    {
        [SerializeField] private GamePlayDialog gamePlayDialog;

        private bool _isActivated = false;

        private void OnEnable() => BlockItem.Touched += OnTouched;

        private void OnDisable() => BlockItem.Touched -= OnTouched;

        private void OnTouched(int index)
        {
            if (_isActivated)
            {
                Blocks.ClearBlockDataByIndex(index);
                gamePlayDialog.RemoveBlockItemByIndex(index, false);
                Blocks.UpdateMap();
                StartCoroutine(Delay.Run(() => { gamePlayDialog.MoveEnd(); }, 0.05f));
                Player.SaveGameStatusData();
                
                _isActivated = false;
            }
        }

        public override void BuyBooster()
        {
            if (_isActivated) return;

            if (TrySpendBooster())
            {
                UseBooster();
            }
            else
            {
                base.BuyBooster();
            }
        }

        private void UseBooster() => _isActivated = true;
    }   
}