using System;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank_vs_time
{
    class Pickup
    {
    // Animation representing the enemy
        public Texture2D PickupTexture;

        // The position of the enemy relative to the top left corner of the screen
        public Vector2 Position;

        // The state of the Enemy 
        public bool Active;

  

        // The amount of damage the enemy inflicts on frank on contact
        public int Damage;

     

        public int pickupnumbers;

        // Get the width of the enemy sprite
        public int Width
        {
            get { return PickupTexture.Width; }
        }

        // Get the height of the enemy
        public int Height
        {
            get { return PickupTexture.Height; }
        }

        // The speed at which the enemy moves
        float PickupMoveSpeed;

        public void Initialize(Texture2D texture, Vector2 position)
        {
            //Load the enemy character texture
            PickupTexture = texture;

            //Set the position of the enemy
            Position = position;

            //Initialise enemy to be updated in game
            Active = true;

            //Amount of damage the enemy does
            Damage = 20;

            //Setting how fast the enemy moves
            PickupMoveSpeed = 20f;

        

            pickupnumbers = 5;

       

        }

        public void Update(GameTime gameTime)
        {
            // The enemy currently always moves to the left 
            Position.X -= PickupMoveSpeed;

           



    
            if (Position.X < -Width  )
            {
                Active = false;
            }

       
        }



        //draw parameters
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PickupTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

        }

    }
}
