using UnityEngine;

namespace NPCs
{
    public class George : NPC
    {
        protected override void OnCampaignCompleted() => Debug.Log($"<color=cyan>{name}: Thank you so much!!!</color>");

        public override void Interact()
        {
            base.Interact();
            Debug.Log($"<color=cyan>{name}: Morning traveller</color>");
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log($"<color=red>{name}: Excuse me</color>");
            if(other.gameObject.CompareTag("Player")) Interact();
        }
    }
}
