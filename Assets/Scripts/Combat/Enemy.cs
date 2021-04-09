using General.Utilities;
using Managers;
using Player;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(CharacterHealth))]
    public class Enemy : Interactable
    {
        public CharacterHealth myHealth;

        private void Start() => myHealth = GetComponent<CharacterHealth>();

        public override void Interact() => GameManager.Instance.characterCombat.Attack(myHealth);

        protected override void Update()
        {
            if (m_PlayerOnRange && PlayerInput.Instance.Attack) Interact();
        }

        protected override void OnEnterTrigger(Collider other) { }

        protected override void OnExitTrigger(Collider other) { }


    }
}
