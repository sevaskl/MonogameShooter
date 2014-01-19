﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameShooter.GameEngine
{


    class Player
    {

        public int HP, SP, Level;
        public string Name;
        public Texture2D Portrait;

        public void DrawPortrait(Texture2D Portrait)
        {
            Portrait = Content.Load<Texture2D>('portrait.png');
        }
    }
}
