using System;
using UnityEngine;
using UnityEngine.Events;
using Firebase.Analytics;

namespace Core
{
    public class FirebaseManager : Singleton<FirebaseManager>
    {
        [HideInInspector] public UnityEvent OnInit;

        private bool isActive = false;
        private Firebase.FirebaseApp app;

        protected override void AfterAwaik()
        {
            DontDestroyOnLoad(gameObject);

            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    app = Firebase.FirebaseApp.DefaultInstance;
                    Debug.Log($"Firebase: {dependencyStatus} :)");
                }
                else
                {
                    Debug.LogError(String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                }

                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

                isActive = true;

                OnInit?.Invoke();
            });
        }

        public static void LogEvent(string message)
        {
            if (Instance && Instance.isActive)
            {
                FirebaseAnalytics.LogEvent(message);
                Debug.Log($"Firebase {message}");
            }
        }

        public static void LogEvent(string nameEvent, Parameter[] parameters)
        {
            if (Instance && Instance.isActive)
            {
                FirebaseAnalytics.LogEvent(nameEvent, parameters);
                Debug.Log($"Firebase {nameEvent}");
            }
        }
    }
}