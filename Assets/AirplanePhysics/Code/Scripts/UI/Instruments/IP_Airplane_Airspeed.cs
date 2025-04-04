﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FlightSim
{
    public class IP_Airplane_Airspeed : MonoBehaviour, IAirplaneUI
    {
        #region Variables
        [Header("Airspeed Indicator Properties")]
        public IP_Airplane_Characteristics characteristics;
        public RectTransform pointer;
        public float maxIndicatedKnots = 160f;
        #endregion


        public const float mphToKnts = 0.868976f;


        #region Interface Methods
        public void HandleAirplaneUI()
        {
            if(characteristics && pointer)
            {
                float currentKnots = characteristics.MPH * mphToKnts;
                //Debug.Log(currentKnots);

                float normalizedKnots = Mathf.InverseLerp(0f, maxIndicatedKnots, currentKnots);
                float wantedRotation = 360f * normalizedKnots;
                pointer.rotation = Quaternion.Euler(0f, 0f, -wantedRotation);
            }
        }
        #endregion
    }
}
