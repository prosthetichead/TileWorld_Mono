using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TileWorld_Mono
{
    /// <summary>
    /// A combination of gamepad and keyboard keys mapped to a particular action.
    /// </summary>
    public class ActionMap
    {
        public List<Inputs.GamePadButtons> gamePadButtons = new List<Inputs.GamePadButtons>();
        public List<Keys> keyboardKeys = new List<Keys>();
    }

    public static class Inputs
    {
        private static KeyboardState currentKeyboardState;
        private static KeyboardState previousKeyboardState;
        private static GamePadState currentGamePadState;
        private static GamePadState previousGamePadState;
        private static ActionMap[] actionMaps;
        /// The value of an analog control that reads as a "pressed button"
        const float analogLimit = 0.5f;

        /// <summary>
        /// Actions
        /// </summary>
        public enum Action { MoveUp, MoveDown, MoveLeft, MoveRight };
        private static readonly string[] actionNames = { "Move Up", "Move Down", "Move Left", "Move Right" };


        /// <summary>
        /// GamePad controls expressed as one type, unified with button semantics.
        /// </summary>
        public enum GamePadButtons
        {
            // Center
            Home,
            Start,
            Back,
            // D-Pad
            Up,
            Left,
            Down,
            Right,
            // Face buttons
            FaceButtonUp,
            FaceButtonLeft,
            FaceButtonDown,
            FaceButtonRight,
            // Shoulder buttons
            LeftShoulder,
            RightShoulder,
            // Triggers
            LeftTrigger,
            RightTrigger,
            // Left Stick
            LeftStick,
            LeftStickUp,
            LeftStickLeft,
            LeftStickDown,
            LeftStickRight,
            // Right Stick
            RightStick,
            RightStickUp,
            RightStickLeft,
            RightStickDown,
            RightStickRight
        }


        //ACTION REGION
        #region Action Region

        /// <summary>
        /// Reset the action mapping to the defualts
        /// </summary>
        private static void ResetActionMaps()
        {
            int numberActions = Enum.GetNames(typeof(Action)).Length;
            actionMaps = new ActionMap[numberActions];
            //move up
            actionMaps[(int)Action.MoveUp] = new ActionMap();
            actionMaps[(int)Action.MoveUp].keyboardKeys.Add(Keys.W);
            actionMaps[(int)Action.MoveUp].keyboardKeys.Add(Keys.Up);
            actionMaps[(int)Action.MoveUp].gamePadButtons.Add(GamePadButtons.Up);

            //move down
            actionMaps[(int)Action.MoveDown] = new ActionMap();
            actionMaps[(int)Action.MoveDown].keyboardKeys.Add(Keys.S);
            actionMaps[(int)Action.MoveDown].keyboardKeys.Add(Keys.Down);
            actionMaps[(int)Action.MoveDown].gamePadButtons.Add(GamePadButtons.Down);

            //move Left
            actionMaps[(int)Action.MoveLeft] = new ActionMap();
            actionMaps[(int)Action.MoveLeft].keyboardKeys.Add(Keys.A);
            actionMaps[(int)Action.MoveLeft].keyboardKeys.Add(Keys.Left);
            actionMaps[(int)Action.MoveLeft].gamePadButtons.Add(GamePadButtons.Left);

            //move right
            actionMaps[(int)Action.MoveRight] = new ActionMap();
            actionMaps[(int)Action.MoveRight].keyboardKeys.Add(Keys.D);
            actionMaps[(int)Action.MoveRight].keyboardKeys.Add(Keys.Right);
            actionMaps[(int)Action.MoveRight].gamePadButtons.Add(GamePadButtons.Right);

        }

        /// <summary>
        /// Returns the readable name of the given action.
        /// </summary>
        public static string GetActionName(Action action)
        {
            int index = (int)action;
            return actionNames[index];
        }
        
        /// <summary>
        /// Check if an action has been pressed.
        /// </summary>
        public static bool IsActionPressed(Action action)
        {
            return IsActionMapPressed(actionMaps[(int)action]);
        }
        private static bool IsActionMapPressed(ActionMap actionMap) {
            for (int i = 0; i < actionMap.keyboardKeys.Count; i++) {
                if (IsKeyPressed(actionMap.keyboardKeys[i]))
                    return true;
            }
            //Is a Gamepad pugged in?
            if (currentGamePadState.IsConnected) {
                for (int i = 0; i < actionMap.gamePadButtons.Count; i++) {
                    if (IsGamePadButtonPressed(actionMap.gamePadButtons[i]))
                        return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Check if an action was just performed in the most recent update.
        /// </summary>
        public static bool IsActionTriggered(Action action)
        {
            return IsActionMapTriggered(actionMaps[(int)action]);
        }
        private static bool IsActionMapTriggered(ActionMap actionMap)
        {
            for (int i = 0; i < actionMap.keyboardKeys.Count; i++)
            {
                if (IsKeyTriggered(actionMap.keyboardKeys[i]))
                {
                    return true;
                }
            }
            if (currentGamePadState.IsConnected)
            {
                for (int i = 0; i < actionMap.gamePadButtons.Count; i++)
                {
                    if (IsGamePadButtonTriggered(actionMap.gamePadButtons[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

#endregion

        //KEYBOARD REGION
        #region Keyboard Region

        /// <summary>
        /// Check if a key was just pressed in the most recent update.
        /// </summary>
        public static bool IsKeyTriggered(Keys key)
        {
            return (currentKeyboardState.IsKeyDown(key)) &&
                (!previousKeyboardState.IsKeyDown(key));
        }
        /// <summary>
        /// Check if a key is pressed.
        /// </summary>
        public static bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        #endregion

        //GAMEPAD REGION
        #region GamePad Region
            
        private static bool IsGamePadButtonPressed(GamePadButtons gamePadKey)
        {
            switch (gamePadKey)
            {
                case GamePadButtons.Up:
                    return (currentGamePadState.DPad.Up == ButtonState.Pressed);
                case GamePadButtons.Down:
                    return (currentGamePadState.DPad.Down == ButtonState.Pressed);
                case GamePadButtons.Left:
                    return (currentGamePadState.DPad.Left == ButtonState.Pressed);
                case GamePadButtons.Right:
                    return (currentGamePadState.DPad.Right == ButtonState.Pressed);
            }
            return false;
        }
        private static bool IsGamePadButtonTriggered(GamePadButtons gamePadKey)
        {
            switch (gamePadKey)
            {
                case GamePadButtons.Up:
                    return ((currentGamePadState.DPad.Up == ButtonState.Pressed) && (previousGamePadState.DPad.Up == ButtonState.Released));
                case GamePadButtons.Down:
                    return ((currentGamePadState.DPad.Down == ButtonState.Pressed) && (previousGamePadState.DPad.Down == ButtonState.Released));
                case GamePadButtons.Left:
                    return ((currentGamePadState.DPad.Left == ButtonState.Pressed) && (previousGamePadState.DPad.Left == ButtonState.Released));
                case GamePadButtons.Right:
                    return ((currentGamePadState.DPad.Right == ButtonState.Pressed) && (previousGamePadState.DPad.Right == ButtonState.Released));
            }
            return false;
        }

        #endregion


        public static void Initialize()
        {
            ResetActionMaps();
        }

        ///<summary>
        ///Updates the keyboard and gamepad control states.
        ///</summary>
        public static void Update()
        {
            //update the keyboard state
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            //update the gamepad state
            previousGamePadState = currentGamePadState;
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
        }

    }
}
