using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FlightSim
{
    [CreateAssetMenu(fileName = "NewTrackData", menuName = "Indie-Pixel/Race Track Data/New Track Data", order = 1)]
    public class Race_Track_Data : ScriptableObject
    {
        #region Variables
        public float lastTrackTime;
        public float bestTrackTime;
        public int lastTrackPosition;
        public int bestTrackPosition;
        #endregion



        #region Custom Methods
        public void SetTimes(float aTime)
        {
            lastTrackTime = aTime;
            if (bestTrackTime == 0)
            {
                bestTrackTime = lastTrackTime;
            }
            else if (lastTrackTime < bestTrackTime)
            {
                bestTrackTime = lastTrackTime;
            }
        }

        public void SetPositions(int aPosition)
        {
            lastTrackPosition = aPosition;
            if (lastTrackPosition < bestTrackPosition)
            {
                bestTrackPosition = lastTrackPosition;
            }
        }
        public int GetBestPosition()
        {
            return this.bestTrackPosition;
        }
        public string GetBestTime()
        {
            float bestTime = this.bestTrackTime;
            int currentMinutes = (int)(bestTime / 60f);
            float currentSeconds = bestTime - (currentMinutes * 60);

            string minutes = currentMinutes.ToString("00");
            string seconds = currentSeconds.ToString("00");
            string vypis = minutes + ":" + seconds;
            return vypis;

        }
        #endregion
    }
}
