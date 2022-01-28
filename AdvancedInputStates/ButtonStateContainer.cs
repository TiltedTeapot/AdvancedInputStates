using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdvancedInputStates
{
    /// <summary>
    /// used to get more complex button states then given by Xna by using <seealso cref="AdvancedButtonState"/>
    /// </summary>
    /// <remarks>
    /// Use <see cref="Update"/> at the begining of a frame to get acurate data <br/>
    /// grab the <see cref="State"/> to get the current <seealso cref="AdvancedButtonState"/>
    /// </remarks>
    public class ButtonStateContainer
    {
        #region class properties
        /// <summary>
        /// a delagate thats run each frame to get the current button state
        /// </summary>
        public Func<ButtonState> Button { get; private set; }

        /// <summary>
        /// The current Advanced Button State
        /// </summary>
        public AdvancedButtonState State { get; private set; }

        /// <summary>
        /// The current default button state
        /// </summary>
        public ButtonState Current { get; private set; }

        /// <summary>
        /// the default button state from the last frame
        /// </summary>
        private ButtonState Old { get; set; }
        #endregion

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
        /// new ButtonStateContainer(delegate { return Mouse.GetState().LeftButton; });
        /// </code>
        /// </example>
        /// <param name="button">the function to run each frame to get the default button state</param>
        public ButtonStateContainer(Func<ButtonState> button)
        {
            Button = button;
            Old = button();
        }
        #endregion

        #region class methods
        /// <summary>
        /// Run this at the beginning of each frame before you access the properties for acurate data
        /// </summary>
        public void Update()
        {
            Old = Current;
            Current = Button();

            if (Current == Old)
            {
                if (Current == ButtonState.Pressed)
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
                if (Current == ButtonState.Pressed)
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

        #region static properties
        /// <summary>
        /// Gets a <seealso cref="ButtonStateContainer"/> for the Left Mouse Button
        /// </summary>
        public static ButtonStateContainer LeftMouseButton
        {
            get => new ButtonStateContainer(delegate { return Mouse.GetState().LeftButton; });
        }

        /// <summary>
        /// Gets a <seealso cref="ButtonStateContainer"/> for the Right Mouse Button
        /// </summary>
        public static ButtonStateContainer RightMouseButton
        {
            get => new ButtonStateContainer(delegate { return Mouse.GetState().RightButton; });
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
        None,
        Pressed,
        Held,
        Released
    }
}
