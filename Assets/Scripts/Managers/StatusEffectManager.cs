using System;
using General;
using Plugins.Tools;
using UI;
using UnityEngine;

namespace Managers
{
    public class StatusEffectManager : PersistentSingleton<StatusEffectManager>
    {
        public float speedAffection, damageAffection, karmaAffection;

        [Header("Visual Feedback")]
        public GameObject statusHolder;
        private GameObject m_HolderInstance;

        public GameObject statusTemplate;

        public void LimitSpeedBy(StatusEffect statusEffect)
        {
            speedAffection += statusEffect.valueOfAffection;

            GameObject status = ShowStatus(statusEffect.effectName, statusEffect.sprite);

            Action deactiveEffect = () =>
            {
                speedAffection += statusEffect.valueOfAffection.ContraryValue();
                GUIManager.Instance.RemoveUI(status);
            };

            if (statusEffect.isTemporal) deactiveEffect.DelayAction(statusEffect.secondsAffected);
        }

        public void ReduceDamageBy(StatusEffect statusEffect)
        {
            damageAffection += statusEffect.valueOfAffection;

            GameObject status = ShowStatus(statusEffect.effectName, statusEffect.sprite);

            Action deactiveEffect = () =>
            {
                damageAffection += statusEffect.valueOfAffection.ContraryValue();
                GUIManager.Instance.RemoveUI(status);
            };

            if (statusEffect.isTemporal) deactiveEffect.DelayAction(statusEffect.secondsAffected);
        }

        public void LimitKarmaBy(StatusEffect statusEffect)
        {
            karmaAffection += statusEffect.valueOfAffection;

            GameObject status = ShowStatus(statusEffect.effectName, statusEffect.sprite);

            Action deactiveEffect = () =>
            {
                karmaAffection += statusEffect.valueOfAffection.ContraryValue();
                GUIManager.Instance.RemoveUI(status);
            };

            if (statusEffect.isTemporal) deactiveEffect.DelayAction(statusEffect.secondsAffected);
        }

        private void GenerateTimer(float secs, Action action)
        {
            Timer timer = gameObject.CreateTimerInstance();

            timer.AddListeners(null, null, action.Invoke);
            timer.SetTimer(new TimerOptions(secs, TimerType.Progressive, false));

            timer.StartTimer();
        }

        public GameObject ShowStatus(string status, Sprite img)
        {
            if (!m_HolderInstance) m_HolderInstance = GUIManager.Instance.InstantiateUI(statusHolder);

            GameObject insta = GUIManager.Instance.InstantiateUI(statusTemplate, false, .8f);
            insta.transform.SetParent(m_HolderInstance.transform);

            var card = insta.GetComponent<ImageTextCard>();
            card.Set(img, status);

            return insta;
        }
    }
}
