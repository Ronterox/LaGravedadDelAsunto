#if UNITY_EDITOR
using UnityEditor;

#endif

namespace BehaviorBricks
{
    /// <summary>
    /// Behavior executor component. Add it to your game objects
    /// to execute BehaviorBrick's behaviors.
    /// </summary>
    [UnityEngine.AddComponentMenu("Behavior Bricks/Behavior executor component")]
    public class BehaviorExecutor : BBUnity.InternalBehaviorExecutor
    {
        /// <summary>
        /// Method to put into operation the behavior and initiation the debug option of the behavior.
        /// </summary>
#if UNITY_EDITOR
        protected override void Start()
        {
            setDebugMode();
            base.Start();
        }
#endif

#if UNITY_EDITOR
        /// <summary>
        /// In editor mode, 
        /// </summary>
        private new void Update()
        {
            bool prev = requestTickExecution;
            base.Update();
            if (prev != requestTickExecution)
                // Force inspector repaint in editor mode to reactivate
                // Tick button.
                EditorUtility.SetDirty(this);
        }
#endif

    }
}
