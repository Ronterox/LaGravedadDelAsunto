using UnityEngine;

namespace General.ObjectPooling
{
    [AddComponentMenu("Penguins Mafia/Auto Object Pooler")]
    public class RandomAutoObjectPooler : ObjectPooler
    {
        public float InitialDelayTime;
        public float minTimeBtwSpawns, maxTimeBtwSpawns;

        private void OnEnable() => InitializeAutoRepeat();

        private void OnDisable() => StopRepeating();

        private void InitializeAutoRepeat() => InvokeRepeating(nameof(TriggerPoolObject), InitialDelayTime, Random.Range(minTimeBtwSpawns, maxTimeBtwSpawns));

        private void StopRepeating() => CancelInvoke(nameof(TriggerPoolObject));

        private void TriggerPoolObject() => GetPooledObject(Random.Range(0, ItemsToPool.Count)).SetActive(true);
    }
}
