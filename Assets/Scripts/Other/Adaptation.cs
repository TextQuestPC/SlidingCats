using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneSignalSDK;

public class Adaptation : MonoBehaviour
{
    [SerializeField]
    private GameObject topUI;


     void Start()
    {
        // Replace 'YOUR_ONESIGNAL_APP_ID' with your OneSignal App ID from app.onesignal.com
       OneSignal.Default.Initialize("YOUR_ONESIGNAL_APP_ID");

        //Debug.Log("Chay vao day dau tien");
    }

    private void Awake()
    {
      

        Debug.Log("adaptation: " + Screen.safeArea.top);
        Vector3 temVector = new Vector3();
        temVector = topUI.transform.localPosition;
        temVector.y -= Screen.safeArea.top;
        topUI.transform.localPosition = temVector;
    }
}

public class SafeArea
{
    public float x;
    public float y;
    public float width;
    public float height;
}