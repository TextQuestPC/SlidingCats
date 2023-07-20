using System.Collections.Generic;
using UnityEngine;

namespace PixelGame
{
    public class AppmetricaController : Singleton<AppmetricaController>
    {
        private bool isActive = false;

        protected override void AfterAwaik()
        {
            DontDestroyOnLoad(gameObject);
        }

        public static void LogEvent(string nameEvent)
        {
            // if (Advertisements.Instance.GetUserConsent() == UserConsent.Accept)
            // {
                AppMetrica.Instance.ReportEvent(nameEvent);
            // }
        }

        public static void LogEvent(string nameEvent, Dictionary<string, object> data)
        {
            // if (Advertisements.Instance.GetUserConsent() == UserConsent.Accept)
            // {
                AppMetrica.Instance.ReportEvent(nameEvent, data);
            // }
        }
    }
}