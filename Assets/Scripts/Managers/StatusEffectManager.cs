using System;
using General;
using Plugins.Tools;
using UI;
using UnityEngine;

namespace Managers
{
    public class StatusEffectManager : Singleton<StatusEffectManager>
    {
        public float speedAffection, damageAffection, karmaAffection;

        [Header("Visual Feedback")]
        public GameObject statusHolder;
        private GameObject m_HolderInstance;
        
        public GameObject statusTemplate;

        private void Start() => m_HolderInstance = GUIManager.Instance.InstantiateUI(statusHolder);

        public void LimitSpeedBy(StatusEffect statusEffect)
        {
            speedAffection += statusEffect.valueOfAffection;
            
            GameObject status = ShowStatus(statusEffect.effectName, statusEffect.sprite);
           
            if(statusEffect.isTemporal) GenerateTimer(statusEffect.secondsAffected, () =>
            {
                speedAffection += statusEffect.valueOfAffection.ContraryValue();
                GUIManager.Instance.RemoveUI(status);
            });
        }

        public void ReduceDamageBy(StatusEffect statusEffect)
        {
            damageAffection += statusEffect.valueOfAffection;
            
            GameObject status = ShowStatus(statusEffect.effectName, statusEffect.sprite);
            
            if(statusEffect.isTemporal) GenerateTimer(statusEffect.secondsAffected, () =>
            {
                damageAffection += statusEffect.valueOfAffection.ContraryValue();
                GUIManager.Instance.RemoveUI(status);
            });
        }

        public void LimitKarmaBy(StatusEffect statusEffect)
        {
            karmaAffection += statusEffect.valueOfAffection;
            
            GameObject status = ShowStatus(statusEffect.effectName, statusEffect.sprite);
            
            if(statusEffect.isTemporal) GenerateTimer(statusEffect.secondsAffected, () =>
            {
                karmaAffection += statusEffect.valueOfAffection.ContraryValue();
                GUIManager.Instance.RemoveUI(status);
            });
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
            GameObject insta = GUIManager.Instance.InstantiateUI(statusTemplate, false);
            insta.transform.parent = m_HolderInstance.transform;

            var card = insta.GetComponent<ImageTextCard>();
            card.Set(img, status);
            
            return insta;
        }
    }
}
