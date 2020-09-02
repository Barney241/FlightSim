using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlightSim
{
    public class Mission_Controller : MonoBehaviour
    {
        [Header("Missions Properties")]
        public GameObject Missions_Container;
        public IP_Track_Manager ringRaceManager;
        public Race_Track_Manager raceManager;

        [SerializeField]
        private KeyCode missionsKey = KeyCode.N;


        private bool isVysible = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
            MissionsControl();
        }

        public void MissionsControl()
        {
            if (Input.GetKeyDown(missionsKey) && isVysible == false) 
            {
                Missions_Container.SetActive(true);
                isVysible = true;
                return;
                
            }
            if (Input.GetKeyDown(missionsKey) && isVysible == true)
            {
                Missions_Container.SetActive(false);
                isVysible = false;
                return;
            }

        }
        public void StartRingRaceOnClick(int trackID)
        {
            Debug.Log(trackID);
            ringRaceManager.StartTrack(trackID);
            Missions_Container.SetActive(false);
            isVysible = false;
            return;
        }
        public void StartRaceOnClick(int trackID)
        {
            Debug.Log(trackID);
            raceManager.StartTrack(trackID);
            Missions_Container.SetActive(false);
            isVysible = false;
            return;
        }
    }
    }
