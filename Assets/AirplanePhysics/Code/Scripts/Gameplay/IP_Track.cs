using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;


namespace FlightSim
{
    public class IP_Track : MonoBehaviour
    {
        #region Variables
        [Header("Track Properties")]
        public IP_Track_Data trackData;
        public List<IP_Gate> gates = new List<IP_Gate>();
        public GameObject Plane;
        public Transform startPosition;
        public IP_BaseAirplane_Input input;

        [Header("Track Events")]
        public UnityEvent OnCompletedTrack = new UnityEvent();

        [Header("Score Properties")]
        public int ScoreForGate = 100;

        private float startTime;
        private int currentTime;
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

        private int currentScore;
        public int CurrentScore
        {
            get { return currentScore; }
        }

        private bool isComplete = false;
        public bool IsComplete
        {
            set { isComplete = value; }
        }
        private float bestScore;
        public float BestScore
        {
            get { return bestScore; }
        }
        private string bestTime;
        public string BestTime
        {
            get { return bestTime; }
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
            
            if(!isComplete)
            {
                UpdateStats();
            }
            Debug.Log(currentGateID);
        }

        private void OnDrawGizmos()
        {
            FindGates();

            if(gates.Count > 0)
            {
                for(int i = 0; i < gates.Count; i++)
                {
                    if((i+1) == gates.Count)
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
            if(gates.Count > 0)
            {
                
                currentGateID = 0;
                DeactivateAllGates();

                startTime = Time.time;
                currentScore = 0;
                isComplete = false;
                gates[currentGateID].ActivateGate();

                Rigidbody temp = Plane.GetComponent<Rigidbody>();
                Plane.transform.position = startPosition.position;
                Plane.transform.rotation = startPosition.rotation;
                temp.velocity = new Vector3(0,0,0);
                input.stickyThrottle = 0f;
                input.flaps = 0;

            }
        }

        void SelectNextGate(float distPercentage)
        {
            int score = Mathf.RoundToInt(ScoreForGate * (1f-distPercentage));
            score = Mathf.Clamp(score, 0, ScoreForGate);
            currentScore += score;

            currentGateID++;
            if(currentGateID == gates.Count)
            {
                //Debug.Log("Completed Track!");
                if(OnCompletedTrack != null)
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
            if(gates.Count > 0)
            {
                foreach(IP_Gate gate in gates)
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

            bestScore = trackData.GetBestScore();
            bestTime = trackData.GetBestTime();
        }

        public void SaveTrackData()
        {
            if(trackData)
            {
                trackData.SetTimes(currentTime);
                trackData.SetScores(currentScore);
            }
        }

        void DeactivateAllGates()
        {
            foreach(IP_Gate gate in gates)
            {
                gate.DeactivateGate();
                gate.isCleared = false;
            }
        } 
        #endregion
    }
}
