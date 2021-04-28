using Inventory_System;
using Managers;
using Player;
using UnityEngine;

namespace GUI.Minigames.Pick_Ingredients
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
            if(!GameManager.Instance.inventory.Add(itemIngredient)) GameManager.Instance.inventory.SpawnItem(itemIngredient, PlayerInput.Instance.transform.position);
            gameObject.SetActive(false);
        }
    }
}
