using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using PathCreation;


namespace FlightSim
{
    public class Race_Track : MonoBehaviour
    {
        #region Variables
        [Header("Track Properties")]
        public Race_Track_Data trackData;
        public List<IP_Gate> gates = new List<IP_Gate>();
        public List<GameObject> planes = new List<GameObject>();
        public List<Transform> planesStartPositions = new List<Transform>();
        public Transform startPosition;
        public Transform finishPosition;
        public IP_BaseAirplane_Input input;
        public PathCreator trackPath;
        

        [Header("Track Events")]
        public UnityEvent OnCompletedTrack = new UnityEvent();

        private float startTime;
        private int currentTime;
        private int playerCount;
        #endregion


        #region Properties
        public int currentGateID = 0;
        public int CurrentGateID
        {
            get { return currentGateID; }
        }

        private int totalGates;
        public int TotalGates
        {
            get { return totalGates; }
        }

        private int currentMinutes;
        public int CurrentMinutes
        {
            get { return currentMinutes; }
        }

        private int currentSeconds;
        public int CurrentSecond
        {
            get { return currentSeconds; }
        }

        private int currentPosition;
        public int CurrentPosition
        {
            get { return currentPosition; }
        }

        private bool isComplete = false;
        public bool IsComplete
        {
            set { isComplete = value; }
        }
        private float bestPosition;
        public float BestPosition
        {
            get { return bestPosition; }
        }
        private string bestTime;
        public string BestTime
        {
            get { return bestTime; }
        }
        public int PlayerCount
        {
            get { return playerCount; }
        }
        #endregion


        #region Builtin Methods
        // Use this for initialization
        void Start()
        {
            FindGates();
            InitializeGates();

            currentGateID = 0;
            Debug.Log("start");
            StartTrack();
        }

        // Update is called once per frame
        void Update()
        {

            if (!isComplete)
            {
                UpdateStats();
                UpdatePositions();
            }
            Debug.Log(currentGateID);
        }

        private void OnDrawGizmos()
        {
            FindGates();

            if (gates.Count > 0)
            {
                for (int i = 0; i < gates.Count; i++)
                {
                    if ((i + 1) == gates.Count)
                    {
                        break;
                    }

                    Gizmos.color = new Color(1f, 1f, 0, 0.5f);
                    Gizmos.DrawLine(gates[i].transform.position, gates[i + 1].transform.position);
                }
            }
        }
        #endregion


        #region Custom Methods
        public void StartTrack()
        {
            if (gates.Count > 0)
            {

                currentGateID = 0;
                DeactivateAllGates();

                startTime = Time.time;
                currentPosition = 0;
                isComplete = false;
                gates[currentGateID].ActivateGate();
                playerCount = planes.Count();

                for(int i = 0; i < planes.Count();i++){
                    Rigidbody temp = planes[i].GetComponent<Rigidbody>();
                    planes[i].transform.position = planesStartPositions[i].position;
                    planes[i].transform.rotation = planesStartPositions[i].rotation;
                    temp.velocity = new Vector3(0, 0, 0);
                    input.stickyThrottle = 0f;
                    input.flaps = 0;
                }

            }
        }

        void SelectNextGate(float distPercentage)
        {
            currentPosition += 1; //TODO:

            currentGateID++;
            if (currentGateID == gates.Count)
            {
                if (OnCompletedTrack != null)
                {
                    OnCompletedTrack.Invoke();
                }
                return;
            }

            gates[currentGateID].ActivateGate();
        }

        void FindGates()
        {
            gates.Clear();
            gates = GetComponentsInChildren<IP_Gate>().ToList<IP_Gate>();
            totalGates = gates.Count;
        }

        void InitializeGates()
        {
            if (gates.Count > 0)
            {
                foreach (IP_Gate gate in gates)
                {
                    gate.DeactivateGate();
                    gate.OnClearedGate.AddListener(SelectNextGate);
                }
            }
        }

        void UpdateStats()
        {
            currentTime = (int)(Time.time - startTime);
            currentMinutes = (int)(currentTime / 60f);
            currentSeconds = currentTime - (currentMinutes * 60);

            bestPosition = trackData.GetBestPosition();
            bestTime = trackData.GetBestTime();
        }

        public void SaveTrackData()
        {
            if (trackData)
            {
                trackData.SetTimes(currentTime);
                trackData.SetPositions(currentPosition);
            }
        }

        void DeactivateAllGates()
        {
            foreach (IP_Gate gate in gates)
            {
                gate.DeactivateGate();
                gate.isCleared = false;
            }
        }
        void UpdatePositions()
        {
            foreach(GameObject plane in planes)
            {
                float dist = Vector3.Distance(plane.transform.position, gates[gates.Count() -1].transform.position);
            }
        }
        #endregion
    }
}
