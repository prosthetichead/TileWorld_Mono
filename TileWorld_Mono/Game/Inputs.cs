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

        
        /// <summary>
        /// Add a map of any combo of gamepad buttons
        /// if more then 1 button provided then all buttons must be pressed for the action
        /// </summary>
        /// <param name="gamePadButtonMap"></param>
        public void AddGamePadMap(Inputs.GamePadButtons gamePadButtonMap)
        {
            gamePadButtons.Add(gamePadButtonMap);
        }

        public void AddKeyboardKeyMap(Keys keyboardKeyMap)
        {
            keyboardKeys.Add(keyboardKeyMap);
        }

        //TODO: add methods to replace maps
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

        /// <summary>
        /// Reset the action mapping to the defualts
        /// </summary>
        private static void ResetActionMaps()
        {
            int numberActions = Enum.GetNames(typeof(Action)).Length;
            actionMaps = new ActionMap[numberActions];

            //actionMap
            //move up
            actionMaps[(int)Action.MoveUp] = new ActionMap();
            actionMaps[(int)Action.MoveUp].AddKeyboardKeyMap(Keys.W);
            actionMaps[(int)Action.MoveUp].AddKeyboardKeyMap(Keys.Up);
            actionMaps[(int)Action.MoveUp].AddGamePadMap(GamePadButtons.Up);

            //move down
            actionMaps[(int)Action.MoveDown] = new ActionMap();
            actionMaps[(int)Action.MoveDown].AddKeyboardKeyMap(Keys.S);
            actionMaps[(int)Action.MoveDown].AddKeyboardKeyMap(Keys.Down);
            actionMaps[(int)Action.MoveDown].AddGamePadMap(GamePadButtons.Down);

            //move Left
            actionMaps[(int)Action.MoveLeft] = new ActionMap();
            actionMaps[(int)Action.MoveLeft].AddKeyboardKeyMap(Keys.A);
            actionMaps[(int)Action.MoveLeft].AddKeyboardKeyMap(Keys.Left);
            actionMaps[(int)Action.MoveLeft].AddGamePadMap(GamePadButtons.Left);

            //move right
            actionMaps[(int)Action.MoveRight] = new ActionMap();
            actionMaps[(int)Action.MoveRight].AddKeyboardKeyMap(Keys.D);
            actionMaps[(int)Action.MoveRight].AddKeyboardKeyMap(Keys.Right);
            actionMaps[(int)Action.MoveRight].AddGamePadMap(GamePadButtons.Right);

           

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
        

        /// <summary>
        /// Check if a key was just pressed in the most recent update.
        /// </summary>
        public static bool IsKeyTriggered(Keys key)
        {
            if ((currentKeyboardState.IsKeyDown(key)) && (!previousKeyboardState.IsKeyDown(key)))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Check if a keys are pressed.
        /// </summary>
        public static bool IsKeyPressed(Keys key)
        {
            if (currentKeyboardState.IsKeyDown(key))
                return true;
            else
                return false;

        }


        //GAMEPAD REGION
        #region GamePad Region

        public static bool IsGamePadButtonPressed(GamePadButtons gamePadKey)
        {
            switch (gamePadKey)
            {
                case GamePadButtons.Up:
                    if ((currentGamePadState.DPad.Up == ButtonState.Pressed))
                        return true;
                    else
                        return false;
                case GamePadButtons.Down:
                    if ((currentGamePadState.DPad.Down == ButtonState.Pressed))
                        return true;
                    else
                        return false;
                case GamePadButtons.Left:
                    if ((currentGamePadState.DPad.Left == ButtonState.Pressed))
                        return true;
                    else
                        return false;
                case GamePadButtons.Right:
                    if ((currentGamePadState.DPad.Right == ButtonState.Pressed))
                        return true;
                    else
                        return false;
            }
            return false;
        }
        public static bool IsGamePadButtonTriggered(GamePadButtons gamePadKey)
        {
            switch (gamePadKey)
            {
                case GamePadButtons.Up:
                    if ((currentGamePadState.DPad.Up == ButtonState.Pressed) && (previousGamePadState.DPad.Up == ButtonState.Released))
                        return true;
                    else
                        return false;
                case GamePadButtons.Down:
                    if ((currentGamePadState.DPad.Down == ButtonState.Pressed) && (previousGamePadState.DPad.Down == ButtonState.Released))
                        return true;
                    else
                        return false;
                case GamePadButtons.Left:
                    if ((currentGamePadState.DPad.Left == ButtonState.Pressed) && (previousGamePadState.DPad.Left == ButtonState.Released))
                        return true;
                    else
                        return false;
                case GamePadButtons.Right:
                    if ((currentGamePadState.DPad.Right == ButtonState.Pressed) && (previousGamePadState.DPad.Right == ButtonState.Released))
                        return true;
                    else
                        return false;

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
        public static void Update(GameTime gameTime)
        {

            previousKeyboardState = currentKeyboardState;
            previousGamePadState = currentGamePadState;
            
            //update the keyboard state
            currentKeyboardState = Keyboard.GetState();
            //update the gamepad state
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
        }

    }
}
