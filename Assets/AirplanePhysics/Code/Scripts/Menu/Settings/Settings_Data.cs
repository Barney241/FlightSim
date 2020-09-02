
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FlightSim
{
    [CreateAssetMenu(fileName = "SettingsData", menuName = "Indie-Pixel/New Tsssrack Data", order = 1)]
    public class Settings_Data : ScriptableObject
    {
        #region Variables
        public int textureQuality;
        public int antiAliasing;
        public int vSync;
        public float musicVolume;
        public bool fullscreen;
        #endregion


    }
}
