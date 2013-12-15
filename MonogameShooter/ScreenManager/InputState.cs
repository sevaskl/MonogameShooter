#region File Description
//-----------------------------------------------------------------------------
// InputState.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
#endregion

namespace MonogameShooter
{
    /// <summary>
    /// ��������������� ����� ��� ������ ��������� � �������� ��� ���������, 
    /// ������������� ���������� � ��������� ��������� ����� ���������, � ��� �� ������������ �
    /// ����������� ������ ��� ��������������� ��������, ����� ��� "������������� ����� �� ����"
    /// ��� "��������� ���� �� �����".
    /// </summary>
    public class InputState
    {
        #region Fields

        public const int MaxInputs = 4;

        public readonly KeyboardState[] CurrentKeyboardStates;
        public readonly GamePadState[] CurrentGamePadStates;

        public readonly KeyboardState[] LastKeyboardStates;
        public readonly GamePadState[] LastGamePadStates;

        public readonly bool[] GamePadWasConnected;

        public TouchCollection TouchState;

        public readonly List<GestureSample> Gestures = new List<GestureSample>();

        #endregion

        #region Initialization


        /// <summary>
        /// ����������� ����-��������� ��������
        /// </summary>
        public InputState()
        {
            CurrentKeyboardStates = new KeyboardState[MaxInputs];
            CurrentGamePadStates = new GamePadState[MaxInputs];

            LastKeyboardStates = new KeyboardState[MaxInputs];
            LastGamePadStates = new GamePadState[MaxInputs];

            GamePadWasConnected = new bool[MaxInputs];//��������: ���� �� �������
        }


        #endregion

        #region Public Methods


        /// <summary>
        /// ��������� ��������� ��������� ���������� �  ��������
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < MaxInputs; i++)
            {
                LastKeyboardStates[i] = CurrentKeyboardStates[i];
                LastGamePadStates[i] = CurrentGamePadStates[i];

                CurrentKeyboardStates[i] = Keyboard.GetState((PlayerIndex)i);
                CurrentGamePadStates[i] = GamePad.GetState((PlayerIndex)i);

                // ���������, ��� �� ������� ����������
                // ��� �� ����� ����������, ��� ��� ���
                if (CurrentGamePadStates[i].IsConnected)
                {
                    GamePadWasConnected[i] = true;
                }
            }

            TouchState = TouchPanel.GetState(); //���������� 1: ������ � ��������

            Gestures.Clear();
            while (TouchPanel.IsGestureAvailable)
            {
                Gestures.Add(TouchPanel.ReadGesture());
            }
        }


        /// <summary>
        /// �������� ��� ������� ������� �������, ��� ���� ����������. �������
		/// ���������� �������(controllingPlayer), ���������: ��������� ������ ������ ���������.
        /// ��� �������� ����� ��������� null ������ ���������� �� ������ ������. ����� ��������
        /// ������� �������, ��������� ����������� playerIndex ���������� �������� ������, ��������� �� �������.
		///���������� 2: ��� ������������
        /// </summary>
        public bool IsNewKeyPress(Keys key, PlayerIndex? controllingPlayer,
                                            out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // ��������� ���� � ������������ ������.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentKeyboardStates[i].IsKeyDown(key) &&
                        LastKeyboardStates[i].IsKeyUp(key));
            }
            else
            {
                // ��������� ���� � ������ ������.
                return (IsNewKeyPress(key, PlayerIndex.One, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Two, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Three, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Four, out playerIndex));
            }
        }


        /// <summary>
        /// �������� ��� ������� ������� �������, ��� ���� ����������. �������
        /// ���������� �������(controllingPlayer), ���������: ��������� ������ ������ ���������.
        /// ��� �������� ����� ��������� null ������ ���������� �� ������ ������. ����� ��������
        /// ������� �������, ��������� ����������� playerIndex ���������� �������� ������, ��������� �� �������.
        ///���������� 2: ��� ������������
        /// </summary>
        public bool IsNewButtonPress(Buttons button, PlayerIndex? controllingPlayer,
                                                     out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // ��������� ���� � ������������ ������.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentGamePadStates[i].IsButtonDown(button) &&
                        LastGamePadStates[i].IsButtonUp(button));
            }
            else
            {
                // ��������� ���� � ������ ������.
                return (IsNewButtonPress(button, PlayerIndex.One, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Two, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Three, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Four, out playerIndex));
            }
        }


        /// <summary>
        /// �������� �������� ������� "����� ����".�������
        /// ���������� �������(controllingPlayer), ���������: ��������� ������ ������ ���������
        /// ��� �������� ����� ��������� null ������ ���������� �� ������ ������. ����� ��������
        /// ������� �������, ��������� ����������� playerIndex ���������� �������� ������, ��������� �� �������..
        /// </summary>
        public bool IsMenuSelect(PlayerIndex? controllingPlayer,
                                 out PlayerIndex playerIndex)
        {
            return IsNewKeyPress(Keys.Space, controllingPlayer, out playerIndex) ||
                   IsNewKeyPress(Keys.Enter, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.A, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.Start, controllingPlayer, out playerIndex);
        }


        /// <summary>
        /// �������� �������� ������� "�������� ����".�������
        /// ���������� �������(controllingPlayer), ���������: ��������� ������ ������ ���������
        /// ��� �������� ����� ��������� null ������ ���������� �� ������ ������. ����� ��������
        /// ������� �������, ��������� ����������� playerIndex ���������� �������� ������, ��������� �� �������..
        /// </summary>
        public bool IsMenuCancel(PlayerIndex? controllingPlayer,
                                 out PlayerIndex playerIndex)
        {
            return IsNewKeyPress(Keys.Escape, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.B, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.Back, controllingPlayer, out playerIndex);
        }


        /// <summary>
        /// �������� �������� ������� "���� �����".�������
        /// ���������� �������(controllingPlayer), ���������: ��������� ������ ������ ���������
        /// ��� �������� ����� ��������� null ������ ���������� �� ������ ������.
        /// </summary>
        public bool IsMenuUp(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(Keys.Up, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.DPadUp, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.LeftThumbstickUp, controllingPlayer, out playerIndex);
        }


        /// <summary>
        /// �������� �������� ������� "���� ����".�������
        /// ���������� �������(controllingPlayer), ���������: ��������� ������ ������ ���������
        /// ��� �������� ����� ��������� null ������ ���������� �� ������ ������.
        /// </summary>
        public bool IsMenuDown(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(Keys.Down, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.DPadDown, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.LeftThumbstickDown, controllingPlayer, out playerIndex);
        }


        /// <summary>
        /// �������� �������� ������� "����� ���� ����".�������
        /// ���������� �������(controllingPlayer), ���������: ��������� ������ ������ ���������
        /// ��� �������� ����� ��������� null ������ ���������� �� ������ ������.
        /// </summary>
        public bool IsPauseGame(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(Keys.Escape, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.Back, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.Start, controllingPlayer, out playerIndex);
        }


        #endregion
    }
}
