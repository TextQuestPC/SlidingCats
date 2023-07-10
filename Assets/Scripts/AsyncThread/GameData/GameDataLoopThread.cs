/*
 * Created on 2022
 *
 * Copyright (c) 2022 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System;
using BFF.Lib;

public class GameDataLoopThread : BFFLoopThread
{
    private string TAG = "GameDataLoopThread----------";
    private static GameDataLoopThread _instance;
    private static readonly object LockInstance = new object();
        
    public static GameDataLoopThread Instance
    {
        get
        {
            lock (LockInstance)
            {
                if (_instance == null)
                {
                    _instance = new GameDataLoopThread();
                    _instance.Start("GameDataLoopThread");
                }
                return _instance;
            }
        }
    }

    public void AddTask(Action act)
    {
        Handler.Post(act);
    }
}
