using System;
using UnityEngine;

namespace GUI.Minigames
{
    public class FallingIngredient : FallingTrigger
    {
        private Vector3 m_StartPosition;
        //public Item itemIngredient;

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
            //Called inventory a give the ingredient
            //GameManager.Instance.inventory.Add(itemIngredient);
            gameObject.SetActive(false);
        }
    }
}
