using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace AdvancedInputStates
{
    /// <summary>
    /// used to get more complex button states then given by Xna by using <seealso cref="AdvancedButtonState"/>
    /// </summary>
    /// <remarks>
    /// Use <see cref="Update"/> at the begining of a frame to get accurate data <br/>
    /// grab the <see cref="State"/> to get the current <seealso cref="AdvancedButtonState"/>
    /// </remarks>
    public class ButtonStateContainer
    {
        #region class properties
        /// <summary>
        /// a delagate thats run each frame to get the current button state
        /// </summary>
        public Func<bool> Button { get; private set; }

        /// <summary>
        /// The current Advanced Button State
        /// </summary>
        public AdvancedButtonState State { get; private set; }

        /// <summary>
        /// The current default button state
        /// </summary>
        public bool CurrentlyPressed { get; private set; }

        /// <summary>
        /// the default button state from the last frame
        /// </summary>
        private bool PressedLastFrame { get; set; }
        #endregion

        #region constuctors
        /// <summary>
        /// basic constructor
        /// </summary>
        /// <remarks>
        /// pass a delegate that returns true if the button is currently down
        /// </remarks>
        /// <example>
        /// this demonstrates how to make a constructor
        /// <code>
        /// new ButtonStateContainer(delegate { return Keyboard.GetState().IsKeyDown(Keys.Space); });
        /// </code>
        /// </example>
        /// <param name="button">the function to run each frame to get the current default button state</param>
        internal ButtonStateContainer(Func<bool> button)
        {
            Button = button;
            PressedLastFrame = button();
        }
        #endregion

        #region class methods
        /// <summary>
        /// Run this at the beginning of each frame before you access the properties for accurate data
        /// </summary>
        public void Update()
        {
            PressedLastFrame = CurrentlyPressed;
            CurrentlyPressed = Button();

            if (CurrentlyPressed == PressedLastFrame)
            {
                if (CurrentlyPressed)
                {
                    State = AdvancedButtonState.Held;
                }
                else
                {
                    State = AdvancedButtonState.None;
                }
            }
            else
            {
                if (CurrentlyPressed)
                {
                    State = AdvancedButtonState.Pressed;
                }
                else
                {
                    State = AdvancedButtonState.Released;
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// holds 4 button states
    /// <list type="bullet">
    ///     <listheader>
    ///         <term>state</term> <description>description</description>
    ///     </listheader>
    ///     <item>
    ///         <term><see cref="None"/></term> <description>Button is up but did not switch to up this frame</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="Pressed"/></term> <description>Button switched to down this frame</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="Held"/></term> <description>Button is down but did not switch to down this frame</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="Released"/></term> <description>Button switched to up this frame</description>
    ///     </item>
    /// </list>
    /// </summary>
    public enum AdvancedButtonState
    {
        /// <summary>
        /// Button is up but did not switch to up this frame
        /// </summary>
        None,
        /// <summary>
        /// Button switched to down this frame
        /// </summary>
        Pressed,
        /// <summary>
        /// Button is down but did not switch to down this frame
        /// </summary>
        Held,
        /// <summary>
        /// Button switched to up this frame
        /// </summary>
        Released
    }
}
