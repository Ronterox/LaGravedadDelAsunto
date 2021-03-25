using UnityEngine;

namespace General.ObjectPooling
{
    [AddComponentMenu("Penguins Mafia/Auto Object Pooler")]
    public class AutoObjectPooler : ObjectPooler
    {
        public float InitialDelayTime;
        public float Time;

        private void Start() => InvokeRepeating(nameof(TriggerPoolObject), InitialDelayTime, Time);

        private void TriggerPoolObject()
        {
            GameObject obj = GetPooledObject(0);
            obj.SetActive(true);
        }
    }
}

