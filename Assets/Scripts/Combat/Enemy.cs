using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(CharacterHealth))]
    public class Enemy : MonoBehaviour
    {
        public CharacterHealth myHealth;
        public bool InCombat {get; private set;}

        private void Start() => myHealth = GetComponent<CharacterHealth>();

        

    }
}
