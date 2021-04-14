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

        public void ApplyStatus()
        {
            float secs = isTemporal ? secondsAffected : 0;
            switch (statAffected)
            {
                case Stat.Health:
                    //StatusEffectManager.Instance.ReduceHealthBy(valueOfAffection, secs);
                    break;
                case Stat.Speed:
                    //StatusEffectManager.Instance.LimitSpeedBy(valueOfAffection, secs);
                    break;
                case Stat.Damage:
                    //StatusEffectManager.Instance.ReduceDamageBy(valueOfAffection, secs);
                    break;
                case Stat.KarmaGaining:
                    //StatusEffectManager.Instance.LimitKarmaBy(valueOfAffection, secs);
                    break;
                default:
                    Debug.Log("Status Effect is not set");
                    break;
            }
        }
    }
}
