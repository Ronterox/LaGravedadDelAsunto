using Managers;
using UnityEngine;

namespace Karma_System
{
    public class KarmaController : MonoBehaviour
    {
        public GameObject karmaBarGameObject;

        public int maxKarmaValue = 50;
        public int karma;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.K)) ChangeKarma(10);
            if(Input.GetKeyDown(KeyCode.L)) ChangeKarma(-10);
        }

        public void ChangeKarma(int increment)
        {
            int oldKarmaQuantity = karma;

            if ((karma += increment + (int)StatusEffectManager.Instance.karmaAffection) > maxKarmaValue) karma = maxKarmaValue;
            else if (karma < -maxKarmaValue) karma = -maxKarmaValue;

            var m_KarmaBarInstance = GUIManager.Instance.InstantiateUI(karmaBarGameObject).GetComponent<Karmabar>();

            m_KarmaBarInstance.SetKarmaBar(-maxKarmaValue, maxKarmaValue);

            m_KarmaBarInstance.AnimateKarma(oldKarmaQuantity, karma);
        }
    }
}
