using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GDPR : MonoBehaviour
{
    public Button ShowPP;

    public Button Accept;

    public Button Cancle;
    // Start is called before the first frame update
    void Start()
    {
        ShowPP.onClick.RemoveAllListeners();
        ShowPP.onClick.AddListener(() =>
        {
            //ManagerAudio.PlaySound("sound_ButtonDown");
            AdManager.Instance.OnUserClickPrivacyPolicy();
        });
        
        Accept.onClick.RemoveAllListeners();
        Accept.onClick.AddListener(() =>
        {
            AdManager.Instance.OnUserClickAccept();
        });
        
        Cancle.onClick.RemoveAllListeners();
        Cancle.onClick.AddListener(() =>
        {
            AdManager.Instance.OnUserClickCancel();
        });
    }

}
