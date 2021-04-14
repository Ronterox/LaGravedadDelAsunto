using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class QuestHolderUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public CanvasGroup canvasGroup;

        private void Awake()
        {
            if (!canvasGroup) canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnPointerEnter(PointerEventData eventData) => GUIManager.AnimateAlpha(canvasGroup, 1f);

        public void OnPointerExit(PointerEventData eventData) => GUIManager.AnimateAlpha(canvasGroup, .5f);

        private void OnMouseEnter() => GUIManager.AnimateAlpha(canvasGroup, 1f);

        private void OnMouseExit() => GUIManager.AnimateAlpha(canvasGroup, .5f);
    }
}
