using Plugins.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace General.Utilities
{
    public abstract class QuickTimeEvent : MonoBehaviour
    {
        public Image timerProgressFilledImage;

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
            SetTimer(timeForEach);
            transform.position = UtilityMethods.GetRandomVector2(0, Screen.width, 0, Screen.height);
        }

        private void Update()
        {
            if (!m_QTEStarted) return;

            if (m_Timer <= 0)
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
            else SetTimer(m_Timer - Time.deltaTime);
        }

        private void SetTimer(float time)
        {
            m_Timer = time;
            timerProgressFilledImage.fillAmount = m_Timer.GetPercentageValue(timeForEach);
        }

        protected abstract void OnWrongPress();

        protected abstract void OnCorrectPress();
    }

}
