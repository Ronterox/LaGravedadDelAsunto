using Managers;
using Plugins.Tools;
using Questing_System;
using UnityEngine;

namespace NPCs
{
    public class NPCDefault : NPC
    {
        protected override void OnCampaignCompleted() => Debug.Log(GameManager.Instance.GetCampaign(npcScriptable.campaignID).campaignResult == QuestState.Completed ? 
                                                                       $"{name}: {"Thank you so much!!!".ToColorString("green")}" 
                                                                       : $"{name}: {"Fuck you, sir".ToColorString("red")}");

        public override void Interact()
        {
            base.Interact();
            Debug.Log($"{name}: {"Morning traveller".ToColorString("cyan")}");
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log($"{name}: {$"Excuse me {other.gameObject.name}".ToColorString("gray")}");
            if(other.gameObject.CompareTag("Player")) Interact();
        }
    }
}
