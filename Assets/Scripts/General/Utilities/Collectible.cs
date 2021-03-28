using UnityEngine;
using UnityEngine.Events;

namespace General
{
    public class Collectible : MonoBehaviour
    {
       
        //This is temporal only to test the destroying event;
        [Header("Testing Can be Destroy Feedback")]
        public MeshRenderer meshRenderer;
        public Material defaultMaterial, canDestroyMaterial;

        [Header("Events")]
        public UnityEvent onPickUpEvent;
        public UnityEvent onDestroyEvent;

        private void OnMouseUpAsButton()
        {
            gameObject.SetActive(false);
            onDestroyEvent?.Invoke();
        }
        private void OnMouseEnter() => meshRenderer.material = canDestroyMaterial;

        private void OnMouseExit() => meshRenderer.material = defaultMaterial;

        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag("Player")) return;
            gameObject.SetActive(false);
            onPickUpEvent?.Invoke();
        }
    }
}
