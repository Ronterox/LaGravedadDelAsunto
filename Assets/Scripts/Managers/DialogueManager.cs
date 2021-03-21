using System.Collections;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class DialogueManager : MonoBehaviour
    {
        public TextMeshPro textInstance;
        public float secondsBetweenTyping = 0.025f;

        private Transform m_TextTransform;
        private Coroutine m_CurrentCoroutine;
        private WaitForSeconds m_WaitForSeconds;

        private void Awake()
        {
            m_TextTransform = textInstance.transform;
            m_WaitForSeconds = new WaitForSeconds(secondsBetweenTyping);
        }

        public void Type(string text, Vector3 position)
        {
            m_TextTransform.position = position;
            TypeInto(textInstance, text);
        }

        public void TypeInto(TMP_Text textMeshPro, string text)
        {
            if (m_CurrentCoroutine != null) StopCoroutine(m_CurrentCoroutine);
            m_CurrentCoroutine = StartCoroutine(TypingCoroutine(textMeshPro, text, true));
        }

        private IEnumerator TypingCoroutine(TMP_Text textMeshPro, string text, bool disappearAfter = false, float disappearDelay = 2f)
        {
            textMeshPro.gameObject.SetActive(true);
            textMeshPro.text = "";
            foreach (char value in text)
            {
                textMeshPro.text += value;
                yield return m_WaitForSeconds;
            }
            if (!disappearAfter) yield break;
            yield return new WaitForSeconds(disappearDelay);
            textMeshPro.gameObject.SetActive(false);
        }
    }
}
