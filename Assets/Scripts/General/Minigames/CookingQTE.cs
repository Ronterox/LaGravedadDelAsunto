using Plugins.Tools;

namespace General.Minigames
{
    public class CookingQTE : QuickTimeEvent
    {
        private void Awake() => StartQuickTimeEvent();

        protected override void OnWrongPress() => print("Failed Quick Time Event!!!".ToColorString("red"));

        protected override void OnCorrectPress() => print("Pressed Correctly Quick Time Event!!!".ToColorString("green"));
    }

}
