using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GUI
{
    [AddComponentMenu("Penguins Mafia/GUI/Selectable Option")]
    public class SelectableButton : Button
    {
        public UnityEvent onSelect;
        public UnityEvent onDeselect;

        public void SetActions(UnityAction onClickAction, UnityAction onSelectAction = null, UnityAction onDeselectAction = null)
        {
            onClick.AddListener(onClickAction);
            onSelect.AddListener(onSelectAction);
            onDeselect.AddListener(onDeselectAction);
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            onSelect?.Invoke();
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            onDeselect?.Invoke();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            onSelect?.Invoke();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            try { onDeselect?.Invoke(); }
            catch (Exception e) { print(e.Message); }
        }
    }
}
