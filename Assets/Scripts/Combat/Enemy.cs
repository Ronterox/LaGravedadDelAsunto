using General.Utilities;
using Managers;
using Player;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(CharacterHealth))]
    public class Enemy : MonoBehaviour
    {
        public CharacterHealth myHealth;

        private void Start() => myHealth = GetComponent<CharacterHealth>();

  

    }
}
