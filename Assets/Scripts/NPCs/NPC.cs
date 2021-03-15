using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace NPCs
{
    public abstract class NPC : MonoBehaviour
    {
        [Space] public ScriptableNPC npcScriptable;

        [Space] public UnityEvent onCampaignCompleted;

        protected abstract void OnCampaignCompleted();

        public virtual void Interact()
        {
            if (GameManager.Instance.GetCampaign(npcScriptable.campaignID).IsCompleted)
            {
                OnCampaignCompleted();
                onCampaignCompleted.Invoke();
            }
            else if (GameManager.Instance.GetCampaign(npcScriptable.campaignID).started) GameManager.Instance.StartNewCampaign(npcScriptable.campaignID);
        }
    }
}
