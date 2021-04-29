using System.Collections.Generic;
using Player;
using Plugins.Persistence;
using Plugins.Tools;

namespace Managers
{
    public struct PlayerData
    {
        public string scene;
        public SerializableVector3 playerPosition;
        public Dictionary<ushort, Data> storedPersisters;
        public List<ArchievementsManager.AchievementStatus> achievementStatuses;
    }

    public class DataManager : PersistentSingleton<DataManager>
    {
        private void OnDestroy()
        {
            if (Instance == this) Serialize();
        }

        public void Serialize()
        {
            var data = new PlayerData
            {
                scene = LevelLoadManager.Instance.GetCurrentSceneName(),
                playerPosition = PlayerController.Instance.transform.position,
                storedPersisters = PersistentDataManager.Instance.Store,
                achievementStatuses = ArchievementsManager.Instance.Serialize()
            };
            SaveLoadManager.Save(data, "playerData");
        }

        public PlayerData Deserialize() => SaveLoadManager.Load<PlayerData>("playerData");
    }
}
