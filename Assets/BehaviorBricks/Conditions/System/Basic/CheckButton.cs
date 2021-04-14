﻿using Pada1.BBCore;
using Pada1.BBCore.Framework;
using UnityEngine;

namespace BehaviorBricks.Conditions.System.Basic
{
    /// <summary>
    /// It is a basic condition to check if the button has been pressed.
    /// </summary>
    [Condition("Basic/CheckButton")]
    [Help("Checks whether a button is pressed")]
    public class CheckButton : ConditionBase
    {
        ///<value>Input Button Name Parameter, jump by default.</value>
        [InParam("button", DefaultValue = "Jump")]
        [Help("Button expected to be pressed")]
        public string button;
        /*
        public enum MouseAction {down, up, during}
        [InParam("mouseAction", DefaultValue = MouseAction.during)]
        public MouseAction mouseAction = MouseAction.during;*/

        /// <summary>
        /// Checks whether the button is pressed.
        /// </summary>
        /// <returns>True if the button is pressed.</returns>
		public override bool Check() =>
            /*switch (mouseAction)
            {
                case MouseAction.down:
                    return Input.GetMouseButtonDown(button);

                case MouseAction.up:
                    return Input.GetMouseButtonUp(button);

                case MouseAction.during:*/
            Input.GetButton(button);
        /*}
            return false;*/
    }
}