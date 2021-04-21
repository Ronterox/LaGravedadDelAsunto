using System.Collections.Generic;
using Questing_System;
using UI;
using UnityEngine;

namespace Managers
{
    public class QuestManager : MonoBehaviour
    {
        [Header("Interface")]
        public GameObject questUIHolder;
        public GameObject questUITemplate;

        [Header("Quests")]
        public Quest[] allQuests;

        private readonly Dictionary<string, Quest> m_Quests = new Dictionary<string, Quest>();
        public readonly HashSet<Quest> onGoingQuests = new HashSet<Quest>();

        private readonly List<GameObject> m_QuestUIGameObjects = new List<GameObject>();
        private GameObject m_InstanceQuestHolder;

        private void Awake()
        {
            foreach (Quest quest in allQuests) m_Quests.Add(quest.questID, quest);
        }

        public void UpdateQuests()
        {
            onGoingQuests.RemoveWhere(quest => quest.IsCompleted);

            m_QuestUIGameObjects.ForEach(ui => GUIManager.Instance.RemoveUI(ui));
            foreach (Quest onGoingQuest in onGoingQuests) AddQuestUI(onGoingQuest);
        }

        public void StartNewQuest(string questID)
        {
            if (!m_Quests.TryGetValue(questID, out Quest result)) return;
            result.StartQuest();
            
            onGoingQuests.Add(result);
            AddQuestUI(result);
        }

        public void AddQuestUI(Quest quest)
        {
            if (!m_InstanceQuestHolder) m_InstanceQuestHolder = GUIManager.Instance.InstantiateUI(questUIHolder, .5f);
            GameObject inst = GUIManager.Instance.InstantiateUI(questUITemplate);

            var questUI = inst.GetComponent<QuestUI>();

            questUI.SetInfo(quest.questInfo.questName, quest.questInfo.questDescription);
            questUI.transform.parent = m_InstanceQuestHolder.transform;

            m_QuestUIGameObjects.Add(questUI.gameObject);
        }

        public Quest GetQuest(string id) => m_Quests.TryGetValue(id, out Quest value) ? value : null;
        public Quest GetQuestRandom() => allQuests[Random.Range(0, allQuests.Length)];
    }
}
