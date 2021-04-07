using General.Utilities;
using Managers;
using Plugins.Tools;

namespace General.Minigames
{
    public class CookingQTE : QuickTimeEvent
    {
        protected override void OnWrongPress() => print("Failed Quick Time Event!!!".ToColorString("red"));

        protected override void OnCorrectPress() => print("Pressed Correctly Quick Time Event!!!".ToColorString("green"));

        protected override void OnQTEStop() => GUIManager.Instance.CloseGUIMenu();

        protected override void OnQTEStart() { }
    }

}
