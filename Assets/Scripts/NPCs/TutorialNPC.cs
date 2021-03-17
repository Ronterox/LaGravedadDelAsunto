using System.Collections;
using Questing_System;
using TMPro;
using UnityEngine;

namespace NPCs
{
    public class TutorialNPC : NPC
    {
        public TextMeshPro helpMessage;
        public float secondsBetweenTyping = 0.025f;

        private Coroutine m_CurrentCoroutine;
        private WaitForSeconds m_WaitForSeconds;

        private const string GATHERING_QUEST_ID = "gathering";

        private void Awake()
        {
            m_WaitForSeconds = new WaitForSeconds(secondsBetweenTyping);
            TypeInto(helpMessage, "Hello you, come here!");
        }

        protected override void OnCampaignCompletedInteraction(Campaign campaign)
        {
            if(campaign.campaignResult == QuestState.NeutralEnding) TypeInto(helpMessage, "Thank you, I guess...?");
            else TypeInto(helpMessage, campaign.campaignResult == QuestState.Completed ? "Thank you so much!!!, Sir" : "Fuck you, Sir");
        }

        protected override void OnInteractionRangeEnter() => TypeInto(helpMessage, "You can press \"E\" to interact!");

        protected override void OnInteractionRangeExit() => TypeInto(helpMessage, "Well, see you and good luck...");

        protected override void OnInteraction(Campaign campaign)
        {
            Quest currentQuest = campaign.GetCurrentQuest();
            
            if (!currentQuest.questID.Equals(GATHERING_QUEST_ID)) return;
            
            if (currentQuest.IsJustStarted()) TypeInto(helpMessage, "Quest started sir, look around!");
            else if(currentQuest.IsOnGoing) TypeInto(helpMessage, "Come talk to me once you collect everything!");
        }

        private void TypeInto(TMP_Text textMeshPro, string text)
        {
            if (m_CurrentCoroutine != null) StopCoroutine(m_CurrentCoroutine);
            m_CurrentCoroutine = StartCoroutine(TypingCoroutine(textMeshPro, text));
        }

        private IEnumerator TypingCoroutine(TMP_Text textMeshPro, string text)
        {
            textMeshPro.text = "";
            foreach (char value in text)
            {
                textMeshPro.text += value;
                yield return m_WaitForSeconds;
            }
        }

    }
}
