using General.Utilities;
using UnityEngine;

namespace Minigames
{
    public abstract class QTEInteractable : GUIInteractable
    {
        protected override void OnEnterTrigger(Collider other) { }

        protected override void OnExitTrigger(Collider other) { }

        public override void OnInterfaceOpen(GameObject gui)
        {
            var quickTimeEvent = gui.GetComponentInChildren<QuickTimeEvent>();

            quickTimeEvent.onCorrectPressEvent.AddListener(OnCorrectPressEvent);
            quickTimeEvent.onWrongPressEvent.AddListener(OnWrongPressEvent);

            quickTimeEvent.onQTEStart.AddListener(OnQTEStart);
            quickTimeEvent.onQTEStop.AddListener(() => OnQTEStop(quickTimeEvent.totalCorrect, quickTimeEvent.totalWrong));

            quickTimeEvent.StartQuickTimeEvent();
        }

        public override void OnInterfaceClose(GameObject gui) { }

        public abstract void OnCorrectPressEvent();

        public abstract void OnWrongPressEvent();

        public abstract void OnQTEStart();

        public abstract void OnQTEStop(int correctPresses, int wrongPresses);
    }
}
