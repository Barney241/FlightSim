using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace FlightSim 
{
    public class SettingManager : MonoBehaviour
    {
        public Toggle fullScreenToogle;
        public Dropdown resolutionDropdown;
        public Dropdown texturequalityDropdown;
        public Dropdown antialiasingDropdown;
        public Dropdown vSyncDropdown;
        public Slider musicVolumeSlider;
        public Button applyButton;

        public AudioSource musicSource;
        public Resolution[] resolutions;
        public GameSettings gameSettings;



        void Start()
        {
            gameSettings = new GameSettings();
            fullScreenToogle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
            resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
            texturequalityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
            antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
            vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
            musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
            applyButton.onClick.AddListener(delegate { OnApllyButtonClick(); });

            resolutions = Screen.resolutions;
            foreach(Resolution resolution in resolutions)
            {
                resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
            }

            LoadSettings();
        }
        public void OnFullscreenToggle()
        {
           gameSettings.fullscreen = Screen.fullScreen = fullScreenToogle.isOn;
        }
        public void OnResolutionChange()
        {
            Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
            gameSettings.resolutionIndex = resolutionDropdown.value;
        }
        public void OnTextureQualityChange()
        {
            QualitySettings.masterTextureLimit = gameSettings.textureQuality = texturequalityDropdown.value;
        }
        public void OnAntialiasingChange()
        {
            QualitySettings.antiAliasing = gameSettings.antiAliasing = (int)Mathf.Pow(2, antialiasingDropdown.value);
        }
        public void OnVSyncChange()
        {
            QualitySettings.vSyncCount = gameSettings.vSync = vSyncDropdown.value;
        }
        public void OnMusicVolumeChange()
        {
            musicSource.volume = gameSettings.musicVolume = musicVolumeSlider.value;
        }
        public void OnApllyButtonClick()
        {
            SaveSettings();
        }
        public void SaveSettings()
        {
            string jsonData = JsonUtility.ToJson(gameSettings, true);
            File.WriteAllText(Application.persistentDataPath + "/gamesettings.json",jsonData);
        }
        public void LoadSettings()
        {
            gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

            musicVolumeSlider.value = gameSettings.musicVolume;
            antialiasingDropdown.value = gameSettings.antiAliasing;
            vSyncDropdown.value = gameSettings.vSync;
            texturequalityDropdown.value = gameSettings.textureQuality;
            resolutionDropdown.value = gameSettings.resolutionIndex;
            fullScreenToogle.isOn = gameSettings.fullscreen;
            Screen.fullScreen = gameSettings.fullscreen;

            resolutionDropdown.RefreshShownValue();
        }
    }
}

