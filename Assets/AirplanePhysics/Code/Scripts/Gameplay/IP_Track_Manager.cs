using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;


namespace FlightSim
{
    public class IP_Track_Manager : MonoBehaviour
    {
        #region Variables
        [Header("Manager Properties")]
        public List<IP_Track> tracks = new List<IP_Track>();
        public IP_Airplane_Controller airplaneController;
        public Mission_Controller missionsController;
        public GameObject Plane;

        [Header("Manager UI")]
        public Text gateText;
        public Text timeText;
        public Text scoreText;
        public GameObject statsPanel;
        public GameObject trackManager;


        [Header("Missions UI")]
        public GameObject itemsContainer;
        public GameObject item;

        [Header("Complete UI")]
        public Text CtimeText;
        public Text CscoreText;
        public Text BestCtimeText;
        public Text BestCscoreText;

        [Header("Manager Events")]
        public UnityEvent OnCompletedRace = new UnityEvent();

        private IP_Track currentTrack;
        #endregion



        #region Builtin Methods
        // Use this for initialization
        private void Start()
        {
            FindTracks();
            InitializeTracks();
            
        }

        private void Update()
        {

            if(currentTrack)
            {
                UpdateUI();
            }
        }
        #endregion



        #region Custom Methods
        public void StartTrack(int trackID)
        {
            if (trackID >= 0 && trackID < tracks.Count)
            {
                for(int i = 0; i < tracks.Count; i++)
                {
                    if(i != trackID)
                    {
                        tracks[i].gameObject.SetActive(false);
                    }

                    tracks[trackID].gameObject.SetActive(true);
                    tracks[trackID].StartTrack();
                    currentTrack = tracks[trackID];
                }
            }
            statsPanel.SetActive(true);
        }

        void FindTracks()
        {
            tracks.Clear();
            tracks = GetComponentsInChildren<IP_Track>(true).ToList<IP_Track>();

        }

        void InitializeTracks()
        {
            if(tracks.Count > 0)
            {
                foreach (IP_Track track in tracks)
                {
                    track.OnCompletedTrack.AddListener(CompletedTrack);
                    GameObject duplicate = Instantiate(item);
                    duplicate.transform.parent = itemsContainer.transform;
                    duplicate.name = "Item";
                    duplicate.transform.localScale = new Vector3(1, 1, 1);
                    duplicate.SetActive(true);

                    GameObject nameText = duplicate.transform.GetChild(0).gameObject;
                    Text txt = nameText.GetComponent<Text>();
                    txt.text = track.name;

                    GameObject scoreText = duplicate.transform.GetChild(3).gameObject;
                    txt = scoreText.GetComponent<Text>();
                    txt.text = "Score: " + track.trackData.GetBestScore(); ;

                    GameObject timeText = duplicate.transform.GetChild(2).gameObject;
                    txt = timeText.GetComponent<Text>();
                    txt.text = "Time: " + track.trackData.GetBestTime(); ;

                    GameObject buttonArg = duplicate.transform.GetChild(4).gameObject;
                    Button but = buttonArg.GetComponent<Button>();

                    string name = track.name;
                    int found = name.IndexOf("#") + 1;
                    name = name.Substring(found);
                    int idTrack = int.Parse(name) - 1;

                    but.onClick.RemoveAllListeners();
                    but.onClick.AddListener(() => missionsController.StartOnClick(idTrack));
                    string test = but.onClick.GetPersistentMethodName(0);
                }

            }
        }

        void CompletedTrack()
        {
            Debug.Log("Completed Track!");

            if(airplaneController)
            {
                StartCoroutine("WaitForLanding");
            }
        }

        IEnumerator WaitForLanding()
        {
            while(airplaneController.State != AirplaneState.LANDED)
            {
                yield return null;
            }

            Debug.Log("Completed Race!");
            if (OnCompletedRace != null)
            {
                OnCompletedRace.Invoke();
                CancelInvoke();
            }

            if (currentTrack)
            {
                currentTrack.IsComplete = true;
                currentTrack.SaveTrackData();
                SetCompleteWindowUI();
                foreach (IP_Gate gate in currentTrack.gates)
                {
                    gate.isCleared = false;
                }
                foreach(IP_Track track in tracks)
                {
                    track.currentGateID = 0;
                }
            }
            statsPanel.SetActive(false);
        }

        void UpdateUI()
        {
            if(gateText)
            {
                gateText.text = "Gates: " + currentTrack.CurrentGateID.ToString() + "/" + currentTrack.TotalGates.ToString();
            }

            if(timeText)
            {
                string minutes = currentTrack.CurrentMinutes.ToString("00");
                string seconds = currentTrack.CurrentSecond.ToString("00");
                timeText.text = minutes + ":" + seconds;
            }

            if(scoreText)
            {
                scoreText.text = "Score: " + currentTrack.CurrentScore.ToString("0");
            }
            
        }
        void SetCompleteWindowUI()
        {
            if (CtimeText)
            {
                string minutes = currentTrack.CurrentMinutes.ToString("00");
                string seconds = currentTrack.CurrentSecond.ToString("00");
                CtimeText.text = "Time: " + minutes + ":" + seconds;
            }
            if (CscoreText)
            {
                CscoreText.text = "Score: " + currentTrack.CurrentScore.ToString("0");
            }
            if (BestCtimeText)
            {
                string bestTime = currentTrack.trackData.GetBestTime();
                BestCtimeText.text = "Best Time: " + bestTime;
            }
            if (BestCscoreText)
            {
                float bestScore = currentTrack.trackData.GetBestScore();
                BestCscoreText.text = "Score: " + bestScore.ToString("00");
            }
        }

        #endregion
    }
}
