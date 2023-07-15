using System;

using Manager;

using UnityEngine;

public class Gold : MonoBehaviour
{
    private const string GoldCount = nameof(GoldCount);

    public static Action GoldChanged;

    private int count;

    private void Start()
    {
        if (ManagerLocalData.HaveData(GoldCount))
            count = ManagerLocalData.GetIntData(GoldCount);
        else
            Save();
    }

    public bool TryAdd(int amount)
    {
        count = ManagerLocalData.GetIntData(GoldCount);

        if (amount < 0) return false;

        count += amount;
        
        Save();
        
        return true;
    }

    public bool TrySpend(int amount)
    {
        count = ManagerLocalData.GetIntData(GoldCount);

        if (amount < 0 || count - amount < 0) return false;

        count -= amount;
        
        Save();
        
        return true;
    }

    private void Save()
    {
        ManagerLocalData.SetIntData(GoldCount, count);
        ManagerLocalData.SaveData();
        
        GoldChanged?.Invoke();
    }
}