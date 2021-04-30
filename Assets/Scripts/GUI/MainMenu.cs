using Managers;
using Player;
using Plugins.Audio;
using Plugins.Persistence;
using Plugins.Properties;
using Plugins.Tools;
using UnityEngine;

namespace GUI
{
    public class MainMenu : MonoBehaviour
    {
        [Scene] public string playScene, testZone, settings;

        [Header("Buttons")]
        public SelectableButton playGameButton;
        public SelectableButton continueButton, testZoneButton, settingsButton, quitButton;

        [Header("Sound Effects")]
        public AudioClip selectAudio;
        public AudioClip pressAudio;

        private void OnEnable()
        {
            void PlaySelectSound() => PlayAudio(selectAudio);
            
            continueButton.gameObject.SetActive(SaveLoadManager.SaveExists("playerData"));

            playGameButton.SetActions(StartNewGame, PlaySelectSound);
            continueButton.SetActions(ContinueGame, PlaySelectSound);
            testZoneButton.SetActions(TestZone, PlaySelectSound);
            settingsButton.SetActions(OpenSettings, PlaySelectSound);
            quitButton.SetActions(QuitGame, PlaySelectSound);
        }

        public void StartNewGame() => Load(playScene);

        public void ContinueGame()
        {
            PlayerData data = DataManager.Instance.Deserialize();
            
            PersistentDataManager.Instance.Store = data.storedPersisters;
            ArchievementsManager.Instance.Deserialize(data.achievementStatuses);

            void PositionatePlayer()
            {
                PlayerController.Instance.transform.position = data.playerPosition;
                LevelLoadManager.Instance.onSceneLoadCompleted -= PositionatePlayer;
            }

            LevelLoadManager.Instance.onSceneLoadCompleted += PositionatePlayer;

            Load(data.scene);
        }

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
