using UnityEngine;

namespace BehaviorBricks.Samples.ProgrammersQuickStartGuide.Done // Programmers Quick Start Guide
{

    /// <summary>
    /// Behavior DoneDayNightCycle component. Add it to your directional light to control the brightness,colorand time to simulate day and night.
    /// </summary>
    public class DoneDayNightCycle : MonoBehaviour
	{
        /// <value>Event raised when sun rises or sets.</value>
        // Event raised when sun rises or sets.
        public event System.EventHandler OnChanged;

        /// <value>Complete day-night cycle duration (in seconds).</value>
        // Complete day-night cycle duration (in seconds).
        public float dayDuration = 10.0f;

        /// <value>Read-only property that informs if it is currently night time.</value>
        // Read-only property that informs if it is currently night time.
        public bool isNight { get; private set; }

		// Private field with the day color. It is set to the initial light color.
		private Color m_DayColor;
		private Color m_SkyColor;

		// Private field with the hard-coded night color.
		private readonly Color m_NightColor = Color.white * 0.1f;

		// Reference to the Light component
		private Light m_LightComponent;
		private Camera m_MainCamera;

        /// <summary>DoneDayNightCycle Initialization Method.</summary>
        /// <remarks>Search the light component and set color light.</remarks>
        private void Start()
        {
	        m_MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
			m_LightComponent = GetComponent<Light>();

			m_SkyColor = m_MainCamera.backgroundColor;
			m_DayColor = m_LightComponent.color;
		}

        /// <summary>DoneDayNightCycle Update Method.</summary>
        /// <remarks>Calculate the intensity of the light with the time elapsed and 
        /// depending on it do a lerp between the color of day and night, also register of EventHandler of this class.</remarks>
        private void Update()
		{
			float lightIntensity = 0.5f + Mathf.Sin(Time.time * 2.0f * Mathf.PI / dayDuration) / 2.0f;
			if (isNight != lightIntensity < 0.3)
			{
				isNight = !isNight;
				OnChanged?.Invoke(this, System.EventArgs.Empty);
			}
			
			m_LightComponent.color = Color.Lerp(m_NightColor, m_DayColor, lightIntensity);
			m_MainCamera.backgroundColor = Color.Lerp(m_NightColor, m_SkyColor, lightIntensity);
		} // Update

	} // class DoneDayNightCycle

} // namespace