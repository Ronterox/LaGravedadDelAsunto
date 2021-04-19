using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Ragdoll
{
    public class RagdollScript : MonoBehaviour
    {

        private Collider ragcollider;
        private Rigidbody rigibody;

        private Collider[] childrencolliders;
        private Rigidbody[] childrenrigibodies;

        PlayerInput input;


        private void Awake()
        {

            ragcollider = GetComponent<Collider>();
            rigibody = GetComponent<Rigidbody>();

            childrencolliders = GetComponentsInChildren<Collider>();
            childrenrigibodies = GetComponentsInChildren<Rigidbody>();
            enableRagdoll(false);

        }
        public void enableRagdoll(bool enabled)
        {
            foreach (var collider in childrencolliders)
            {
                collider.enabled = enabled;
            }
            foreach (var rigibody in childrenrigibodies)
            {
                rigibody.detectCollisions = enabled;
                rigibody.isKinematic = !enabled;
            }


            rigibody.isKinematic = enabled;
            ragcollider.enabled = !enabled;

        }


    }
}
