using UnityEngine;

namespace Animations
{
    public class RagdollScript : MonoBehaviour
    {
        private Collider m_RagCollider;
        private Rigidbody m_Rigidbody;

        private Collider[] m_ChildrenColliders;
        private Rigidbody[] m_ChildrenRigibodies;

        private void Awake()
        {
            m_RagCollider = GetComponent<Collider>();
            m_Rigidbody = GetComponent<Rigidbody>();

            m_ChildrenColliders = GetComponentsInChildren<Collider>();
            m_ChildrenRigibodies = GetComponentsInChildren<Rigidbody>();
            
            EnableRagdoll(false);
        }
        public void EnableRagdoll(bool enable = true)
        {
            foreach (Collider coll in m_ChildrenColliders) coll.enabled = enable;
            
            foreach (Rigidbody rigibdy in m_ChildrenRigibodies)
            {
                rigibdy.detectCollisions = enable;
                rigibdy.isKinematic = !enable;
            }
            m_Rigidbody.isKinematic = enable;
            m_RagCollider.enabled = !enable;
        }
    }
}
