using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pausedMenu_Controller : MonoBehaviour
{
    public Button backButton;
    public Button settingsButton;
    public Button exitButton;
    public Button backSettingsButton;
    public GameObject pausedMenuPanel;
    public GameObject SettingsPanel;

    [SerializeField]
    private KeyCode menuKey = KeyCode.Escape;

    private bool visibility = false;
    private bool settingsVisibility = false;
    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(delegate { BackToMenu(); });
        backSettingsButton.onClick.AddListener(delegate { BackToPausedMenu(); });
        settingsButton.onClick.AddListener(delegate { Settings(); });
        exitButton.onClick.AddListener(delegate { ExitGame(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(menuKey))
        {
            if(visibility == false && settingsVisibility == false)
            {
                pausedMenuPanel.SetActive(true);
                visibility = true;
                Time.timeScale = 0;
                return;
            }
            else if(visibility == true && settingsVisibility == false)
            {
                pausedMenuPanel.SetActive(false);
                visibility = false;
                Time.timeScale = 1;
                return;
            }  
        }
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    private void BackToPausedMenu()
    {
        pausedMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        settingsVisibility = false;
    }
    private void Settings()
    {
        pausedMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        settingsVisibility = true;
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
