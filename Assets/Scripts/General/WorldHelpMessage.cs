using System;
using UnityEngine;

namespace General
{
    public class WorldHelpMessage : MonoBehaviour
    {
        public Transform player;

        private void Awake()
        {
            if (!player) player = GameObject.FindWithTag("Player").transform;
        }

        private void LateUpdate() => transform.LookAt(player);
    }
}
