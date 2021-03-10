using UnityEngine;

namespace Plugins
{
    [AddComponentMenu("Penguins Mafia/Tools/FPS Counter")]
    public class FPSCounter : MonoBehaviour
    {
        public float updateInterval = 0.5f;
        private float timeLeft; // Left time for current interval

        public int fontSize = 24;
        private int safeZone;
        
        private float fps;
        
        private int frames;        // Frames drawn over the interval
        private float accumulated; // FPS accumulated over the interval


        /// <summary>
        /// Start
        /// </summary>
        public void Start() => safeZone = (int)(Screen.width * 0.05f);

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            timeLeft -= Time.deltaTime;
            accumulated += Time.timeScale / Time.deltaTime;
            ++frames;

            // Interval ended - update GUI text and start new interval
            if (timeLeft > 0) return;
            // display two fractional digits (f2 format)
            fps = accumulated / frames;
            timeLeft = updateInterval;
            accumulated = 0f;
            frames = 0;
        }

        /// <summary>
        /// On GUI
        /// </summary>
        private void OnGUI()
        {
            GUIStyle style = GUI.skin.GetStyle("Label");
            style.fontSize = fontSize;
            style.alignment = TextAnchor.LowerLeft;
            style.wordWrap = false;

            GUIStyle labeleStyle = GUI.skin.GetStyle("Box");
            labeleStyle.alignment = TextAnchor.UpperRight;

            float height = style.lineHeight + 16;
            var frameBox = new Rect(Screen.width - 150, 30, 200 - safeZone, height);
            GUI.Box(frameBox, "FPS", labeleStyle);
            GUI.Label(frameBox, $"{fps:F2}");
        }
    }
}
