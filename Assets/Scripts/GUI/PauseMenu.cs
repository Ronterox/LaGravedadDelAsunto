using Managers;
using Plugins.Audio;
using Plugins.Properties;
using UnityEngine;

namespace GUI
{
    public class PauseMenu : MonoBehaviour
    {
        [Scene] public string menuScene, settingsScene;
        
        [Header("Buttons")]
        public SelectableButton resumeButton;
        public SelectableButton settingsButton, mainMenuButton;

        [Header("Sound Effects")]
        public AudioClip selectAudio;
        public AudioClip pressAudio;

        private void OnEnable()
        {
            void PlaySelectSound() => PlayAudio(selectAudio);
            
            resumeButton.SetActions(Resume, PlaySelectSound);
            settingsButton.SetActions(OpenSettings, PlaySelectSound);
            mainMenuButton.SetActions(LoadMainMenu, PlaySelectSound);
        }

        public void Resume()
        {
            PlayAudio(pressAudio);
            GUIManager.Instance.CloseGUIMenu();
        }

        public void LoadMainMenu() => Load(menuScene);

        public void OpenSettings() => Load(settingsScene);
        
        private void Load(string scene)
        {
            PlayAudio(pressAudio);
            LevelLoadManager.Instance.LoadScene(scene);
        }

        private void PlayAudio(AudioClip audioClip) => SoundManager.Instance.PlayNonDiegeticSound(audioClip);
    }
}
