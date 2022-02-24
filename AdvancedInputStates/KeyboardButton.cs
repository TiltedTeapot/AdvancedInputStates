using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace AdvancedInputStates
{
    /// <summary>
    /// used to get more complex Keyboard Button states then given by Xna by using <seealso cref="AdvancedButtonState"/>
    /// </summary>
    /// <remarks>
    /// Use <see cref="Update"/> at the begining of a frame to get accurate data <br/>
    /// grab the <see cref="State"/> to get the current <seealso cref="AdvancedButtonState"/>
    /// </remarks>
    public class KeyboardButton : ButtonStateContainer
    {
        #region constuctors
        /// <summary>
        /// advanced constructor
        /// </summary>
        /// <remarks>
        /// pass a delegate that returns true if the button is currently down
        /// </remarks>
        /// <example>
        /// this demonstrates how to make a constructor
        /// <code>
        /// new KeyboardButton(delegate { return Keyboard.GetState().IsKeyDown(Keys.Space); });
        /// </code>
        /// </example>
        /// <param name="button">the function to run each frame to get the current default button state</param>
        KeyboardButton(Func<bool> button) : base(button)
        {

        }

        /// <summary>
        /// basic constructor
        /// </summary>
        /// <example>
        /// this demonstrates how to make a constructor
        /// <code>
        /// new KeyboardButton(Keys.Space);
        /// </code>
        /// </example>
        /// <param name="key">the Key you want to asses</param>
        KeyboardButton(Keys key) : this(delegate { return Keyboard.GetState().IsKeyDown(key); })
        {

        }
        #endregion
    }
}
