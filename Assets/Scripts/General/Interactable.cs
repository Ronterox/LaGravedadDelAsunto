
using UnityEngine;
using Player;

public class Interactable : MonoBehaviour
{
    private bool m_PlayerOnRange;
   
    
    public virtual void Interact()
    {
        Debug.Log("xd");

    }
    private void Update()
    {
        if (m_PlayerOnRange && PlayerInput.Instance.Interact) Interact();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        m_PlayerOnRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        m_PlayerOnRange = false;
    }
}
