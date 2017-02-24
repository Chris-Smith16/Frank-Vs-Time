using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank_vs_time
{
    class Enemy
    {
        // Animation representing the enemy
        public Texture2D EnemyTexture;

        // The position of the enemy relative to the top left corner of the screen
        public Vector2 Position;

        // The state of the Enemy 
        public bool Active;

        // The hit points of the enemy, if this goes to zero the enemy dies
        public int Health;

        // The amount of damage the enemy inflicts on frank on contact
        public int Damage;

        // The amount of score the enemy will give to the player
        public int Value;

        public int Enemy_numbers;

        // Get the width of the enemy sprite
        public int Width
        {
            get { return EnemyTexture.Width; }
        }

        // Get the height of the enemy
        public int Height
        {
            get { return EnemyTexture.Height; }
        }

        // The speed at which the enemy moves
        float enemyMoveSpeed;

        public void Initialize(Texture2D texture, Vector2 position)
        {
            //Load the enemy character texture
            EnemyTexture = texture;

            //Set the position of the enemy
            Position = position;

            //Initialise enemy to be updated in game
            Active = true;

            //Enemy health 
            Health = 2;

            //Amount of damage the enemy does
            Damage = 10;

            //Setting how fast the enemy moves
            enemyMoveSpeed = 7f;

            //Setting enemy score value
            Value = 100;

            //number of enemys that occur before boss is active.
            Enemy_numbers = 20;

        }

        public void Update(GameTime gameTime)
        {
            // The enemy currently always moves to the left 
            Position.X -= enemyMoveSpeed;




            // If the enemy is past the screen or its health reaches 0 then deactivate it
            if (Position.X < -Width || Health <= 0 )
            {
                Active = false;
            }

            //enemys run out 
             if (  Enemy_numbers  <= 0)
             {
                 Active = false;
              
             }        
        }



        //draw parameters
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EnemyTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

        }

    }
}
