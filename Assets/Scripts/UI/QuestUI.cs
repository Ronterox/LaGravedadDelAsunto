using TMPro;
using UnityEngine;

namespace UI
{
    public class QuestUI : MonoBehaviour
    {
        public TMP_Text title, description;

        public void SetInfo(string titleText, string descriptionText)
        {
            title.text = titleText;
            description.text = descriptionText;
        }
    }
}
