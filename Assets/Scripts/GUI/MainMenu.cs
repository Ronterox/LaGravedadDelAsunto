using Managers;
using Plugins.Audio;
using Plugins.Properties;
using UnityEngine;

namespace GUI
{
    public class MainMenu : MonoBehaviour
    {
        [Scene] public string playScene, testZone, settings;

        [Header("Buttons")]
        public SelectableButton playGameButton;
        public SelectableButton testZoneButton, settingsButton, quitButton;

        [Header("Sound Effects")]
        public AudioClip selectAudio;
        public AudioClip pressAudio;

        private void OnEnable()
        {
            void PlaySelectSound() => PlayAudio(selectAudio);
            
            playGameButton.SetActions(PlayGame, PlaySelectSound);
            testZoneButton.SetActions(TestZone, PlaySelectSound);
            settingsButton.SetActions(OpenSettings, PlaySelectSound);
            quitButton.SetActions(QuitGame, PlaySelectSound);
        }

        public void PlayGame() => Load(playScene);

        public void TestZone() => Load(testZone);

        public void OpenSettings() => Load(settings);

        public void QuitGame()
        {
            PlayAudio(pressAudio);
            Application.Quit();
        }

        private void Load(string scene)
        {
            PlayAudio(pressAudio);
            LevelLoadManager.Instance.LoadScene(scene);
        }

        private void PlayAudio(AudioClip audioClip) => SoundManager.Instance.PlayNonDiegeticSound(audioClip);
    }
}
