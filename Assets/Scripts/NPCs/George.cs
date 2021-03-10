using UnityEngine;

namespace NPCs
{
    public class George : NPC
    {
        protected override void OnCampaignCompleted() => Debug.Log("<color=cyan>Thank you so much!!!<color>");

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.CompareTag("Player")) Interact();
        }
    }
}
