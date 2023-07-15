/*
 * Created on 2022
 *
 * Copyright (c) 2022 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */


using Manager;
using UI;
using UnityEngine;

namespace Models
{
    public static class Constant
    {
       
        public static float DragDelta = 1f;
        public static float ScreenWidth = Screen.width;
        public static float ScreenHeight = Screen.height;
        
        
        public const int FrameRate = 60;
        public const float FrameTime = 1.0f / 60;
        
        public const int Hang = 10;
        public const int Lie = 8;

        public static int BlockWidth = 120;
        public static int BlockHeight = 120;

        public static float WRatio = 1f;
        public static float HRatio = 1f;

        public static int BlockGroupEdgeLeft = -Lie * BlockWidth / 2;
        public static int BlockGroupEdgeBottom = -Hang * BlockHeight / 2;
        
       
        public const string Prop1 = "prop1";
        public const string Prop2 = "prop2";
        public const string Prop3 = "prop3";
        public const string Prop4 = "prop4";
        
        
        public static bool SpecialRainbowSwitch = false;
        public static int BlockSpecialRate = 20;
        public static int BlockSpecialHangCountMin = 5;
        public static int BlockSpecialHangCountMax = 8;
        public static float SecondChanceClearEffTime = 0.7f;
        
       
        public static bool SpecialBronzeSwitch = true;
        public static bool SpecialGoldSwitch = true;
        public static string SpecialGoldEffWeight = "1:0:0";
        public static int SpecialGoldEffNoNewBlocksTime = 10;
        public static string SpecialBronzeRandom = "50,100,50";
        public static string SpecialGoldRandom = "100,150,200";
        public static string PreviousBlocks = "";
        public static bool SpecialGoldAdInterstitialSwitch = false;
        public static bool SpecialGoldAdRvAndInterSwitch = false;
        public static bool SpecialGoldCountDownSwitch = false;
        public static int SpecialGoldCountDownNum = 15;
        public static bool SpecialGoldAdClear = false;
        
     
        public static bool StoneSwitch = false;
        public static int StoneScoreTimes = 4;
        public static int StoneScoreGenerate = 6000;
        public static int StoneGenerateHang = 10;
        public static int StoneMaxCount = 2;

     
        public static bool VibratorSwitch = false;
        public static int VibratorTime = 300;
        public static int VibratorAmplitude = 200;//0-255
        
        
        public static bool IceBlockSwitch = true;
        
      
        public static bool LevelToStageSwitch = false;
        public static string LevelText = "Level ";
        public static string LevelUpText = "LEVEL UP ";
        public static bool LevelRewardSwitch = false;
        public static bool LevelUpOtherEffSwitch = false;

        public static string SceneVersion = "3";
        public static string LevelUpEffVersion = "";
        public static string LevelProgressVersion = "1";
        
       
        public static bool IsDeviceSoHeight = false;

        
        public static float UpAnimTime = 0.2f;
        public static float DownAnimTime = 0.16f;
        public static float BlockRemoveTime = 0.3f;
        public static float SpecialClearTime = 0.7f;
        public static float SpecialGoldClearTime = 0.3f;
        public static float SpecialGoldClearIntervalTime = 0.1f;
        public static float SpecialEdgeClearTime = 0.5f;
        public static float ClearWaitTime = 0.25f;
        public static float IceTime = 0.2f;
        public static float ScoreUpdateDelayTime = 0.5f;
        public static float ScoreEffDelayTime = 0.6f;
        public static float ClearEffDelayTime = 0.12f;
        public static float ClearTipTimeMax = 7.0f;
        public static float GuideOneBlockTime = 0.75f;
        public static float GuideEdgeWaitTime = 0.1f;
        public static float DownEndWaitTime = 0.02f;
        public static float UpEndWaitTime = 0.03f;
        public static string GuideVersion = "";
        public static bool B421Switch = false;
        public static int B421NotAddBlockStep = 2;
        public static bool B43221Switch = false;
        public static bool B43221And1HangSwitch = false;
        public static int B43221NotAddBlockStep = 2;
        public static int B43221RemoveSlideNumber = 3;
        public static string B43221BlockPro = "2:1";
        public static bool SecondChanceEnabled = true;
        public static bool AchievementSwitch = false;
        public static string ScoreToStar = "0,4000,12000";
        public static int SecondChanceScore = 0;

        public static bool ESLogUseNative = false;

        public static bool ShowRemoveAd = false;

        public static bool ShowSpecialGoldDialogAnim = true;
        
        //adChance
        public static string RewardVideoType = "";
        //bannerClicked
        public static string BannerClickedData = "";
        
        public static int DifficultyLevel = 6;
        
        public static bool PropSwitch = false;
        public static int PropUsedCount = 1;

       
        public static int AF_PLAY_ROUND = 8;
        public static int AF_PLAY_DAY = 2;
        public static int AF_ROUND_OVER_SCORE = 10000;
        public static AFData AFData;

    
        public static int SecondChanceHangStart = 3;
        public static int SecondChanceHangNum = 4;
        
       
        public static readonly int[][] EmptyBlockRate = new int[3][];
        
      
        public static GameStatus GameStatusData = null;
        
        
        public static int RateRound = 5;
        public static int RateDeltaRound = 10;
        public static int RateMaxCount = 3;

        
        public static GamePlayDialog GamePlayScript;
        public static EffectController EffCtrlScript;
        public static AchievementTips AchievementScript;
        
      
        public static string TopicID = "topic-8a21ixizu";
        
        public static void InitData()
        {
            AFData = ManagerLocalData.GetTableData<AFData>(ManagerLocalData.AF) ?? new AFData();
            GameStatusData = ManagerLocalData.GetTableData<GameStatus>(ManagerLocalData.GAME_STATUS) ?? new GameStatus();

            var gamePlayObj = GameObject.Find("GamePlayDialog");
            GamePlayScript = gamePlayObj.GetComponent<GamePlayDialog>();
            EffCtrlScript = gamePlayObj.GetComponent<EffectController>();
            AchievementScript = gamePlayObj.GetComponent<AchievementTips>();
        }

        public static void UpdateBlockSize(int sizeWidth, int sizeHeight = 0)
        {
            if (sizeHeight == 0)
            {
                sizeHeight = sizeWidth;
            }

            WRatio = sizeWidth * 1f / BlockWidth;
            HRatio = sizeHeight * 1f / BlockHeight;
            
            BlockWidth = sizeWidth;
            BlockHeight = sizeHeight;
            BlockGroupEdgeLeft = -Lie * BlockWidth / 2;
            BlockGroupEdgeBottom = -Hang * BlockHeight / 2;
        }
    }
}
