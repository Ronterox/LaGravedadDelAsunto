using Inventory_System;
using Managers;
using UnityEngine;

namespace GUI.Minigames
{
    public class FallingIngredient : FallingTrigger
    {
        public Item itemIngredient;
        private Vector3 m_StartPosition;

        private void Awake() => m_StartPosition = transform.position;

        protected override void OnEnable()
        {
            base.OnEnable();
            onTriggerEnter.AddListener(_GivePlayer);
            transform.position = m_StartPosition;
        }

        private void OnDisable() => onTriggerEnter.RemoveListener(_GivePlayer);

        private void _GivePlayer()
        {
            GameManager.Instance.inventory.Add(itemIngredient);
            gameObject.SetActive(false);
        }
    }
}
