﻿/*
 * Created on 2022
 *
 * Copyright (c) 2022 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using UnityEngine;


namespace BFF
{
    /// <summary>
    /// Utility Class
    /// </summary>
    public static class Utils
	{
		static GameObject utilGameObject = null;
        /// <summary>
        /// Get or create unique BFF GameObject
        /// </summary>
        /// <returns>GameObject</returns>
        public static GameObject UtilityGameObject()
		{
            if (utilGameObject == null)
			{
				utilGameObject = new GameObject("BFFUtilityGameObject");
                utilGameObject.hideFlags = HideFlags.HideAndDontSave;

                if (Application.isPlaying)
                {
	                GameObject.DontDestroyOnLoad(utilGameObject);
                }
            }

            return utilGameObject;
		}
	}
}
