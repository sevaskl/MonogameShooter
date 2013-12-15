#region File Description
//-----------------------------------------------------------------------------
// PlayerIndexEventArgs.cs
//
// XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
#endregion

namespace MonogameShooter
{
    /// <summary>
    /// ���������������� �������� �������, ������� �������� � ���� ������ ������, ��������������� �������. 
    /// ��� ������������ � ������ MenuEntry.Selected
    /// </summary>
    class PlayerIndexEventArgs : EventArgs
    {
        /// <summary>
        /// �����������
        /// </summary>
        public PlayerIndexEventArgs(PlayerIndex playerIndex)
        {
            this.playerIndex = playerIndex;
        }


        /// <summary>
        /// �������� ������ ������, ��������������� �������.
        /// </summary>
        public PlayerIndex PlayerIndex
        {
            get { return playerIndex; }
        }

        PlayerIndex playerIndex;
    }
}
