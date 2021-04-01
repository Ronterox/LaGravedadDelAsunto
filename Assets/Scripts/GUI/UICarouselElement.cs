using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GUI
{
    public class UICarouselElement : Button
    {
        [System.Serializable]
        public struct ElementAnim
        {
            public float Scale;
            public float Duration;
            public Ease EaseType;
        }

        public int ID;
        public ElementAnim SelectAnim;
        public ElementAnim DeselectAnim;

        public delegate void UICarouselElementEvent(UICarouselElement element);

        public event UICarouselElementEvent OnSelectElement;
        public event UICarouselElementEvent OnDeselectElement;

        public RectTransform RectTransform { get; private set; }

        protected override void OnEnable()
        {
            base.OnEnable();
            RectTransform = transform as RectTransform;
            //PlayDeselectAnim();
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            OnSelectElement?.Invoke(this);
            PlaySelectAnim();
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            OnDeselectElement?.Invoke(this);
            PlayDeselectAnim();
        }

        public virtual void PlaySelectAnim() =>
            transform.DOScale(SelectAnim.Scale, SelectAnim.Duration).SetEase(SelectAnim.EaseType).OnUpdate(() =>
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent as RectTransform);
            });

        public virtual void PlayDeselectAnim() => transform.DOScale(DeselectAnim.Scale, DeselectAnim.Duration).SetEase(DeselectAnim.EaseType);
    }
}
