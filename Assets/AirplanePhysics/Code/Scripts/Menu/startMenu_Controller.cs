using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace FlightSim
{
    public class startMenu_Controller : MonoBehaviour
    {
        public Button startButton;
        public Button newGameButton;
        public Button optionsButton;
        public Button exitButton;
        public GameObject SettingsPanel;

        private bool visibility = false;
        // Start is called before the first frame update
        void Start()
        {
            startButton.onClick.AddListener(delegate { StartGame(); });
            newGameButton.onClick.AddListener(delegate { NewGame(); });
            optionsButton.onClick.AddListener(delegate { Settings(); });
            exitButton.onClick.AddListener(delegate { ExitGame(); });
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void StartGame()
        {
            SceneManager.LoadScene("world");
        }
        private void NewGame()
        {
            SceneManager.LoadScene("world");
        }
        private void ExitGame()
        {
            Application.Quit();
        }
        private void Settings()
        {
            if(visibility == false)
            {
                SettingsPanel.SetActive(true);
                visibility = true;
            }
            else if(visibility == true)
            {
                SettingsPanel.SetActive(false);
                visibility = false;
            }
            
        }
    }
}