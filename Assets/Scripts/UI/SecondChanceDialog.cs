/*
 * Created on 2022
 *
 * Copyright (c) 2022 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System.Collections;
using Manager;
using Models;
using Other;
using Spine;
using Spine.Unity;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SecondChanceDialog : MonoBehaviour
    {
        public GameObject content;
        public GameObject btnNoThx;
        public Sprite[] countDownSpriteFrames;
        public TextMeshProUGUI curScore;

        private void OnDestroy()
        {
            if (Constant.SceneVersion == "3")
            {
                //                var topMask = GameObject.Find("topMask");
                //                if (topMask != null)
                //                {
                //                    topMask.GetComponent<TopMask>().ShowLight();
                //                }
            }
        }

        private void PlayCountDownSound()
        {
            ManagerAudio.PlaySound("countDown");
            StartCoroutine(Delay.Run(() =>
                {
                    PlayCountDownSound();
                }, 80 / 60f));
        }

        // Start is called before the first frame update
        void Start()
        {
            curScore.text = Player.GetCurScore().ToString();
            Constant.GameStatusData.continueShow = true;

            if (Constant.SceneVersion == "3")
            {
                //                GameObject.Find("topMask").GetComponent<TopMask>().HideLight();

                var scoreGroup = curScore.transform.parent;
                var oriPos = scoreGroup.transform.localPosition;
                scoreGroup.transform.localPosition =
                    new Vector2(Player.GetCurScore().ToString().Length * -129 / 6f, oriPos.y);
                StartCoroutine(Delay.Run(() =>
                {
                    var countDownObj = content.transform.Find("countDownShadow2");
                    countDownObj.gameObject.SetActive(true);
                    countDownObj.GetComponent<SkeletonGraphic>().AnimationState.Complete += delegate (TrackEntry entry)
                    {
                        if (entry.ToString() == "animation_2")
                        {
                            OnBtnClk("noThx");
                            Debug.Log("Chay vao day");

                            
                        }
                    };
                    countDownObj.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "animation_2", false);
                    StartCoroutine(Delay.Run(() => { PlayCountDownSound(); }, 25 / 60f));

                    //                    StartCountDown();
                }, 0.5f));

            }
        }

        public void OnBtnClk(string btnType)
        {
            DebugEx.Log(btnType);


            ManagerAudio.PlaySound("sound_ButtonDown");
            switch (btnType)
            {
                case "playOn":
                    Constant.GameStatusData.continueClk = true;
                    Constant.GameStatusData.videoShow = true;
                    Constant.RewardVideoType = "secondChance";

                
                    Advertisements.Instance.ShowRewardedVideo(CompleteMethod);

                    break;
                case "noThx":
                    ManagerDialog.DestroyDialog("SecondChanceDialog");
                    Constant.GamePlayScript.ShowGameOverEff();
                    break;
            }
        }


        private void CompleteMethod(bool completed, string advertiser)
        {
            Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
            if (completed == true)
            {
                Player.UseSecondChance();
                var result = new Hashtable();
                result.Add("success", true);
                Constant.GamePlayScript.SecondChanceResult(result);
                ManagerDialog.DestroyDialog("SecondChanceDialog");

            }
            else
            {
#if UNITY_EDITOR
                Player.UseSecondChance();
                var result = new Hashtable();
                result.Add("success", true);
                Constant.GamePlayScript.SecondChanceResult(result);
                ManagerDialog.DestroyDialog("SecondChanceDialog");
#else
                Debug.Log("NO Reward");

#endif
            }


        }
    }
}
