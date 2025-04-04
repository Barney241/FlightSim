﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FlightSim
{
    public class IP_Airplane_Attitude : MonoBehaviour, IAirplaneUI
    {
        #region Variables
        [Header("Attitude Indicator Properties")]
        public IP_Airplane_Controller airplane;
        public RectTransform bgRect;
        public RectTransform arrowRect;
        #endregion


        #region Interface Methods
        public void HandleAirplaneUI()
        {
            if(airplane)
            {
                //Create Angles
                float bankAngle = Vector3.Dot(airplane.transform.right, Vector3.up) * Mathf.Rad2Deg;
                float pitchAngle = Vector3.Dot(airplane.transform.forward, Vector3.up) * Mathf.Rad2Deg;

                //Handle UI Elements
                if(bgRect)
                {
                    Quaternion bankRot = Quaternion.Euler(0f, 0f, bankAngle);
                    bgRect.transform.rotation = bankRot;

                    Vector3 wantedPosition = new Vector3(0f, -pitchAngle, 0f);
                    bgRect.anchoredPosition = wantedPosition;

                    if(arrowRect)
                    {
                        arrowRect.transform.rotation = bankRot;
                    }
                }

            }
        }
        #endregion
    }
}
