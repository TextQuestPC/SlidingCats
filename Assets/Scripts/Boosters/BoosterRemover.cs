using System;
using System.Collections.Generic;
using Models;
using Other;
using UI;
using UnityEngine;

namespace Boosters
{
    public class BoosterRemover : Booster
    {
        [SerializeField] private GamePlayDialog gamePlayDialog;

        private bool _isActivated = false;

        private void OnEnable() => BlockItem.Selected += OnSelected;

        private void OnDisable() => BlockItem.Selected -= OnSelected;

        private void OnSelected(Vector2 position)
        {
            if (_isActivated)
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
                _isActivated = false;
                
                // List<BlockItem> itemsToRemove = new List<BlockItem>();
                // for (int i = 0; i < gamePlayDialog.ItemList.Count; i++)
                // {
                //     var posX = position.x;
                //
                //     if (gamePlayDialog.ItemList[i].GetComponent<BlockItem>())
                //     {
                //         
                //     }
                // }


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