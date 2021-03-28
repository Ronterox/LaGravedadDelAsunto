using TMPro;
using UnityEngine;

namespace General.Minigames
{
    public abstract class QuickTimeEvent : MonoBehaviour
    {
        public TMP_Text qteText;
        public KeyCode[] keys;

        public float timeForEach;
        private float m_Timer;

        private bool m_QTEStarted;
        private KeyCode m_CorrectKeycode;

        public void StartQuickTimeEvent()
        {
            m_QTEStarted = true;
            SetRandomCookQTE();
        }

        public void StopQuickTimeEvent() => m_QTEStarted = false;

        private void SetRandomCookQTE()
        {
            m_CorrectKeycode = keys[Random.Range(0, keys.Length)];
            qteText.text = $"[\"{m_CorrectKeycode}\"]";
            m_Timer = 0;
        }

        private void Update()
        {
            if(!m_QTEStarted) return;
            
            if (m_Timer >= timeForEach)
            {
                SetRandomCookQTE();
                OnWrongPress();
            }
            else if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(m_CorrectKeycode)) OnCorrectPress();
                else OnWrongPress();
            
                SetRandomCookQTE();
            }
            else m_Timer += Time.deltaTime;
        }

        protected abstract void OnWrongPress();

        protected abstract void OnCorrectPress();
    }

}
