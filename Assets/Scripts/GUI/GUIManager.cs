using DG.Tweening;
using Plugins.DOTween.Modules;
using UnityEngine;

namespace GUI
{
    public class GUIManager : MonoBehaviour
    {
        [Range(0.5f, 3f)] public float alphaAnimationDuration;

        public void AnimateAlpha(CanvasGroup canvasGroup, float objectiveAlpha, TweenCallback onceStartAnimation = null, TweenCallback onceFinishAnimation = null) => 
            canvasGroup.DOFade(objectiveAlpha, alphaAnimationDuration).OnStart(onceStartAnimation).OnComplete(onceFinishAnimation);
    }
}
