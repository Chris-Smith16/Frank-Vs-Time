﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank_vs_time
{
    class Bullet
    {
        // Image representing the bullet
        public Texture2D Texture;

        // Position of the bullet relative to the upper left side of the screen
        public Vector2 Position;

        // State of the bullet
        public bool Active;

        // The amount of damage the bullet can inflict to an enemy
        public int Damage;

        // Represents the viewable boundary of the game
        Viewport viewport;

        // Get the width of the bullet
        public int Width
        {
            get { return Texture.Width; }
        }

        // Get the height of the bullet
        public int Height
        {
            get { return Texture.Height; }
        }

        // Determines how fast the bullet moves
        float bulletMoveSpeed;


        public void Initialize(Viewport viewport, Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            this.viewport = viewport;

            Active = true;

            Damage = 2;

            bulletMoveSpeed = 50f;
        }

        public void Update()
        {
            // bullets always move to the right
            Position.X += bulletMoveSpeed;

            // Deactivate the bullet if it goes out of screen
            if (Position.X + Texture.Width / 2 > viewport.Width)
                Active = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f,
            new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);
        }

    }
}
