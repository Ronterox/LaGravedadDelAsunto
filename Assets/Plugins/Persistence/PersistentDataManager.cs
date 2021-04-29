using System.Collections.Generic;
using Plugins.Tools;
using UnityEngine;

namespace Plugins.Persistence
{
    [AddComponentMenu("Penguins Mafia/Persistence/Persistent Data Manager")]
    public class PersistentDataManager : PersistentSingleton<PersistentDataManager>
    {
        /// <summary>
        /// Support variables
        /// </summary>
        private static bool m_Quitting;

        public Dictionary<ushort, Data> Store { get; set; }

        /// <summary>
        /// Awake
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            Store = new Dictionary<ushort, Data>();
        }

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            if (Instance == this) m_Quitting = true;
        }

        /// <summary>
        /// Register persister
        /// </summary>
        /// <param name="persister"></param>
        public static void LoadPersistedData(IDataPersister persister) => Instance.LoadData(persister);

        /// <summary>
        /// Unregister persister
        /// </summary>
        /// <param name="persister"></param>
        public static void SavePersistedData(IDataPersister persister)
        {
            if (!m_Quitting) Instance.SaveData(persister);
        }

        /// <summary>
        /// Load data of single persister
        /// </summary>
        /// <param name="persister"></param>
        private void LoadData(IDataPersister persister)
        {
            DataSettings dataSettings = persister.GetDataSettings();
            if (dataSettings.type == DataSettings.PersistenceType.WriteOnly || dataSettings.type == DataSettings.PersistenceType.DoNotPersist)
                return;

            if (Store.ContainsKey(dataSettings.dataId)) persister.LoadData(Store[dataSettings.dataId]);
        }

        /// <summary>
        /// Save data of single persister
        /// </summary>
        /// <param name="persister"></param>
        private void SaveData(IDataPersister persister)
        {
            DataSettings dataSettings = persister.GetDataSettings();
            if (dataSettings.type == DataSettings.PersistenceType.ReadOnly || dataSettings.type == DataSettings.PersistenceType.DoNotPersist)
                return;

            Store[dataSettings.dataId] = persister.SaveData();
        }

        /// <summary>
        /// Remove saved data
        /// </summary>
        public static void RemoveAllData() => Instance.Store.Clear();
    }
}
