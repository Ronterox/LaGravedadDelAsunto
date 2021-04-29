using Managers;
using UnityEngine;

namespace General
{
    public enum Stat { Health, Speed, Damage, KarmaGaining }
    
    [CreateAssetMenu(fileName = "New Status Effect", menuName = "Penguins Mafia/Status Effect")]
    public class StatusEffect : ScriptableObject
    {
        [Header("Required")]
        public Stat statAffected;
        public float valueOfAffection;

        [Header("Optional")]
        public bool isTemporal;
        public float secondsAffected;

        [Header("Visual Feedback")]
        public Sprite sprite;
        public string effectName;

        public void ApplyStatus()
        {
            switch (statAffected)
            {
                case Stat.Health:
                    //StatusEffectManager.Instance.ReduceHealthBy(valueOfAffection, secs);
                    Debug.LogWarning("Not implemented health status");
                    break;
                case Stat.Speed:
                    StatusEffectManager.Instance.LimitSpeedBy(this);
                    break;
                case Stat.Damage:
                    StatusEffectManager.Instance.ReduceDamageBy(this);
                    break;
                case Stat.KarmaGaining:
                    StatusEffectManager.Instance.LimitKarmaBy(this);
                    break;
                default:
                    Debug.Log("Status Effect is not set");
                    break;
            }
        }
    }
}
