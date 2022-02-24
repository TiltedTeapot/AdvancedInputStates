using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace AdvancedInputStates
{
    /// <summary>
    /// used to get more complex Mouse Button states then given by Xna by using <seealso cref="AdvancedButtonState"/>
    /// </summary>
    /// <remarks>
    /// Use <see cref="Update"/> at the begining of a frame to get accurate data <br/>
    /// grab the <see cref="State"/> to get the current <seealso cref="AdvancedButtonState"/>
    /// </remarks>
    public class MouseButton : ButtonStateContainer
    {

        #region constuctors
        /// <summary>
        /// basic constructor
        /// </summary>
        /// <remarks>
        /// pass a delegate that gets the current default button state, must return <seealso cref="ButtonState"/> 
        /// </remarks>
        /// <example>
        /// this demonstrates how to make a constructor
        /// <code>
        /// new MouseButton(delegate { return Mouse.GetState().LeftButton; });
        /// </code>
        /// </example>
        /// <param name="button">the function to run each frame to get the current default button state</param>
        MouseButton(Func<ButtonState> button) : base(delegate { return button() == ButtonState.Pressed; })
        {

        }
        #endregion

        #region static properties
        /// <summary>
        /// Gets a <seealso cref="MouseButton"/> for the Left Mouse Button
        /// </summary>
        public static MouseButton LeftMouseButton
        {
            get => new MouseButton(delegate { return Mouse.GetState().LeftButton; });
        }

        /// <summary>
        /// Gets a <seealso cref="MouseButton"/> for the Right Mouse Button
        /// </summary>
        public static MouseButton RightMouseButton
        {
            get => new MouseButton(delegate { return Mouse.GetState().RightButton; });
        }
        #endregion
    }
}
