using System.Collections;
using Managers;
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

        private void Awake()
        {
            m_WaitForSeconds = new WaitForSeconds(secondsBetweenTyping);
            TypeInto(helpMessage, "Hello you, come here!");
        }

        protected override void OnCampaignCompleted() => TypeInto(helpMessage, GameManager.Instance.GetCampaign(npcScriptable.campaignID).campaignResult == QuestState.Completed ? 
                                                                      "Thank you so much!!!" 
                                                                      : "Fuck you, sir");

        protected override void OnInteractionRangeEnter() => TypeInto(helpMessage, "You can press \"E\" to interact!");

        protected override void OnInteractionRangeExit() => TypeInto( helpMessage,"Well, see you and good luck...");

        public override void Interact()
        {
            TypeInto(helpMessage,"Quest started sir, look behind you!");
            base.Interact();
        }

        private void TypeInto(TMP_Text textMeshPro, string text)
        {
            if(m_CurrentCoroutine != null) StopCoroutine(m_CurrentCoroutine);
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
