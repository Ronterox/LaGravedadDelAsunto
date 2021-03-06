using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ImageTextCard : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public TMP_Text tmpText;
        public Image image;

        public void Set(Sprite sprite, string text)
        {
            tmpText.text = text;
            image.sprite = sprite;
        }
    }
}
