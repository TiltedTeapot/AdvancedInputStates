using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace AdvancedInputStates
{
    /// <summary>
    /// A much more efficent way to track multiple keyboard keys than <seealso cref="KeyboardButton"/>
    /// </summary>
    /// <remarks>
    /// Use <see cref="Update"/> at the begining of a frame to get accurate data <br/>
    /// Use <see cref="GetKeyState"/> to get the current for a key <seealso cref="AdvancedButtonState"/>
    /// </remarks>
    public class AdvancedKeyboardState
    {
        Dictionary<Keys, (bool Old, bool Current)> _defaultKeysState;

        /// <summary>
        /// empty constuctor.
        /// </summary>
        /// <remarks>
        /// use <see cref="Add"/> to add keys
        /// </remarks>
        public AdvancedKeyboardState()
        {
            _defaultKeysState = new Dictionary<Keys, (bool, bool)>();
        }

        /// <summary>
        /// initialize and add keys at the same time.
        /// </summary>
        /// <param name="keys">an array of starting keys</param>
        public AdvancedKeyboardState(Keys[] keys) : this()
        {
            foreach(Keys key in keys)
            {
                Add(key);
            }
        }

        /// <summary>
        /// Adds a key to asses the state of
        /// </summary>
        /// <param name="key">the key to asses</param>
        public void Add(Keys key)
        {
            if (_defaultKeysState.ContainsKey(key))
            {
                throw new ArgumentException("Can not have duplicate keys. key already exists", nameof(key));
            }
            _defaultKeysState.Add(key, (false, false));
        }

        /// <summary>
        /// Removes a key from assesment
        /// </summary>
        /// <remarks>
        /// saves a bit of time each frame if you no longer need the sate of this key
        /// </remarks>
        /// <param name="key">the key to remove</param>
        public void Remove(Keys key)
        {
            if (!_defaultKeysState.ContainsKey(key))
            {
                throw new ArgumentException("that key does not exist", nameof(key));
            }
            _defaultKeysState.Remove(key);
        }

        /// <summary>
        /// gets the state of the given key
        /// </summary>
        /// <param name="key">the key to get the state of</param>
        /// <returns><seealso cref="AdvancedButtonState"/> of the given key</returns>
        public AdvancedButtonState GetKeyState(Keys key)
        {
            if (!_defaultKeysState.ContainsKey(key))
            {
                throw new ArgumentException("that key does not exist", nameof(key));
            }
            AdvancedButtonState ret;

            if (_defaultKeysState[key].Old)
            {
                if (_defaultKeysState[key].Current)
                {
                    ret = AdvancedButtonState.Held;
                }
                else
                {
                    ret = AdvancedButtonState.Released;
                }
            }
            else
            {
                if (_defaultKeysState[key].Current)
                {
                    ret = AdvancedButtonState.Pressed;
                }
                else
                {
                    ret = AdvancedButtonState.None;
                }
            }

            return ret;
        }
        /// <summary>
        /// Run this at the beginning of each frame before you access the properties for accurate data
        /// </summary>
        public void Update()
        {
            KeyboardState state = Keyboard.GetState();

            Keys[] keys = new Keys[_defaultKeysState.Keys.Count];
            _defaultKeysState.Keys.CopyTo(keys, 0);
            foreach (Keys key in keys)
            {
                _defaultKeysState[key] = (_defaultKeysState[key].Current, state.IsKeyDown(key));
            }
        } 
    }
}
