using Plugins.Tools;
using UnityEngine;

namespace General.Utilities
{
    public class WorldHelpMessage : MonoBehaviour
    {
        public Transform player;

        private void Awake()
        {
            if (!player) player = GameObject.FindWithTag("Player")?.transform;
        }

        private void LateUpdate()
        {
            if (player) transform.RotateTowards(player);
        }
    }
}
