using Plugins.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace General.Utilities
{
    public class QuickTimeEvent : MonoBehaviour
    {
        public Image timerProgressFilledImage;
        public TMP_Text qteText;

        [Header("Settings")]
        public int numberOfPresses;
        public KeyCode[] keys;

        public float timeForEach;
        private float m_Timer;

        [Header("Events")]
        public UnityEvent onQTEStart;
        public UnityEvent onQTEStop;
        [Space]
        public UnityEvent onWrongPressEvent;
        public UnityEvent onCorrectPressEvent;

        private bool m_QTEStarted;
        private KeyCode m_CorrectKeycode;

        [HideInInspector]
        public int totalWrong, totalCorrect;

        private void OnEnable()
        {
            onWrongPressEvent.AddListener(OnWrongPress);
            onCorrectPressEvent.AddListener(OnCorrectPress);
            
            onQTEStart.AddListener(OnQTEStart);
            onQTEStop.AddListener(OnQTEStop);
        }

        private void OnDisable()
        {
            onWrongPressEvent.RemoveListener(OnWrongPress);
            onCorrectPressEvent.RemoveListener(OnCorrectPress);
            
            onQTEStart.RemoveListener(OnQTEStart);
            onQTEStop.RemoveListener(OnQTEStop);
        }

        public void StartQuickTimeEvent()
        {
            m_QTEStarted = true;
            totalWrong = 0;
            totalCorrect = 0;
            onQTEStart?.Invoke();
            SetRandomPositionQTE();
        }

        public void StopQuickTimeEvent()
        {
            m_QTEStarted = false;
            onQTEStop?.Invoke();
        }

        private void SetRandomPositionQTE()
        {
            m_CorrectKeycode = keys[Random.Range(0, keys.Length)];
            qteText.text = $"[\"{m_CorrectKeycode}\"]";
            SetTimer(timeForEach);
            transform.position = UtilityMethods.GetRandomVector2(0, Screen.width, 0, Screen.height);
        }

        private void WrongPress()
        {
            totalWrong++;
            onWrongPressEvent?.Invoke();
            StopIfFinish();
        }

        private void StopIfFinish() { if(totalWrong + totalCorrect >= numberOfPresses) StopQuickTimeEvent(); }
        
        private void CorrectPress()
        {
            totalCorrect++;
            onCorrectPressEvent?.Invoke();
            StopIfFinish();
        }

        private void Update()
        {
            if (!m_QTEStarted) return;

            if (m_Timer <= 0)
            {
                SetRandomPositionQTE();
                WrongPress();
            }
            else if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(m_CorrectKeycode)) CorrectPress();
                else WrongPress();

                SetRandomPositionQTE();
            }
            else SetTimer(m_Timer - Time.deltaTime);
        }

        private void SetTimer(float time)
        {
            m_Timer = time;
            timerProgressFilledImage.fillAmount = m_Timer.GetPercentageValue(timeForEach);
        }

        protected virtual void OnWrongPress() {}

        protected virtual void OnCorrectPress() {}

        protected virtual void OnQTEStop() {}
        protected virtual void OnQTEStart() {}
    }

}
