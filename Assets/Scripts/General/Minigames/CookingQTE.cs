using General.Utilities;
using Plugins.Tools;

namespace General.Minigames
{
    public class CookingQTE : QuickTimeEvent
    {
        protected override void OnWrongPress() => print("Failed Quick Time Event!!!".ToColorString("red"));

        protected override void OnCorrectPress() => print("Pressed Correctly Quick Time Event!!!".ToColorString("green"));

        protected override void OnQTEStop() { }

        protected override void OnQTEStart() { }
    }

}
