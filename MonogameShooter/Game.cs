#region File Description
//-----------------------------------------------------------------------------
// Game.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace MonogameShooter
{
    /// <summary>
    /// ��� ������, ������� ���������� ��� ��������� ����������� ����
    /// � ������������� ����� �������� ����, ������ ���: ���� ��������, ����� ����,
    /// ���� ����� �.�.�. �������� ����� ������ �����, �� ����� ���������� ���������� �
    /// ScreenManager component.
    /// </summary>
    public class GameStateManagementGame : Game
    {
        #region Fields

        GraphicsDeviceManager graphics;
        ScreenManager screenManager;

#if ZUNE
        int BufferWidth = 272;
        int BufferHeight = 480;
#elif IPHONE
        int BufferWidth = 320;
        int BufferHeight = 480;
#else
        int BufferWidth = 272;
        int BufferHeight = 480;
#endif
        #endregion

        #region Initialization


        /// <summary>
        /// �������� ����������� ����
        /// </summary>
        public GameStateManagementGame()
        {
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = BufferWidth;
            graphics.PreferredBackBufferHeight = BufferHeight;

            // �������� screen manager component.
            screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            // ������������ ��������� ������.
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
        }


        #endregion

        #region Draw


        /// <summary>
        /// ����������, ����� ���� ���������� ����������� ���� ����
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // ��������� ���������� ���������� ������ screen manager
            base.Draw(gameTime);
        }


        #endregion
    }
}
