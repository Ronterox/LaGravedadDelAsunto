using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Plugins.Tools
{
    [System.Serializable]
    public struct TimerEvents
    {
        public UnityEvent onTimerStart;
        public UnityEvent onTimerStop;
        public UnityEvent onTimerEnd;
    }

    public class TimerUI : MonoBehaviour
    {
        public TMP_Text m_TimerText;

        [Header("Settings")]
        public bool resetOnEnd;

        public float timerTime;
        private float m_Timer;

        [Space]
        public TimerEvents events;

        private bool m_TimerStarted;
        private void Update()
        {
            if (!m_TimerStarted) return;
            if (m_Timer <= 0)
            {
                events.onTimerEnd?.Invoke();
                if (resetOnEnd) ResetTimer();
                else StopTimer();
            }
            else m_Timer -= Time.deltaTime;

            UpdateTimerText();
        }

        private void UpdateTimerText()
        {
            if (m_TimerText) m_TimerText.text = $"{Mathf.Floor(m_Timer / 60) % 60:00}:{m_Timer % 60:00}";
        }

        public void ResetTimer() => m_Timer = timerTime;

        public void StartTimer()
        {
            ResetTimer();
            m_TimerStarted = true;
            events.onTimerStart?.Invoke();
        }

        public void StopTimer()
        {
            m_TimerStarted = false;
            events.onTimerStop?.Invoke();
        }
    }
}