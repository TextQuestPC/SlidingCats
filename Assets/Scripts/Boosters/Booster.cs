using UnityEngine;

using System;
using Manager;

namespace Boosters
{
    public class Booster : MonoBehaviour
    {
        public Action ValueChanged;
        
        [SerializeField] private int price;
        public int Price => price;
    
        public int Count { get; private set; }

        [SerializeField] private Gold gold;
        
        [SerializeField] private BoosterType boosterType;
        
        protected enum BoosterType
        {
            Remove,
            Break
        }
        
        private void Start()
        {
            if (PlayerPrefs.HasKey(boosterType.ToString()))
                Count = PlayerPrefs.GetInt(boosterType.ToString());
            else
                Save(Count);
            
            ValueChanged?.Invoke();
        }

        public virtual void BuyBooster() => TryBuyBooster();

        protected bool TryBuyBooster()
        {
            if (gold.TrySpend(price))
            {
                Count++;
                Save(Count);
                return true;
            }
    
            return false;
        }
    
        protected bool TrySpendBooster()
        {
            ValueChanged?.Invoke();

            if (!ManagerLocalData.HaveData(boosterType.ToString())) return false;

            Count = ManagerLocalData.GetIntData(boosterType.ToString());
            if (Count <= 0) return false;
            
            Count--;
            Save(Count);
            
            return true;
        }
    
        private void Save(int count)
        {
            ManagerLocalData.SetIntData(boosterType.ToString(), count);
            ValueChanged?.Invoke();
        }
    }
}