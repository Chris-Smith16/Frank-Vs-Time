using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;





namespace Frank_vs_time
{
    #region state variables
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

         //represents the player
        Player player;


        // Keyboard states used to determine key presses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        // A movement speed for the player
        float playerMoveSpeed;

        // Enemies
        Texture2D enemyTexture;
        Texture2D enemyTexturelvl2;
        Texture2D enemyTexturelvl3;
        Texture2D enemyTexturelvl4;
        Texture2D enemyTexturelvl5;

        List<Enemy> enemies;
        List<Pickup> pickups;

        // The rate at which the enemies appear
        TimeSpan enemySpawnTime;
        TimeSpan pickupSpawnTime;
        TimeSpan previouspickupSpawnTime;
        TimeSpan previousSpawnTime;
        int Enemy_numbers;

        

        // A random number generator
        Random random;

   

        Texture2D projectileTexture;

        Texture2D EnprojectileTexture;

        List<Bullet> projectiles;

        List<EnemyBullet> EnProjectiles;

        // The rate of fire of the player weapon
        TimeSpan fireTime;
        TimeSpan previousFireTime;

        Texture2D explosionTexture;
        List<Animation> explosions;

        Texture2D PowerupTexture;
        List<Animation> powerupeffects;

        //Number that holds score
        int score;

        // The font used to display UI elements
        SpriteFont font;

        //background
        private Texture2D backgroundTexture;

        //background
        Texture2D Jungle;

        //background
        Texture2D Roman;


        //background
        Texture2D ww1;

        //background
        Texture2D western;

        //background
        Texture2D future;


        //BGM
        Song gameplayMusic;
        Song gameplayMusic2;
        Song gameplayMusic3;
        Song gameplayMusic4;
        Song gameplayMusic5;
        Song gameplayMusic6;
        Song Mainmenumusic;
        Song comicstripMusic;
        Song comicstripMusic2;
        Song comicstripMusic3;
        Song GAMEOVER;


        //Enemy death sound

        static int deathsoundsExtreme = 7;

        private static SoundEffect[] DeathSound = new SoundEffect[deathsoundsExtreme];

    

        SoundEffect Gunfire;

        SoundEffect Splat;

        SoundEffect pickup;

        int GameStarted = 0;

        Texture2D t2dTitleScreen;

        Texture2D comicstrip;

        Texture2D comicstrip2;

        Texture2D comicstrip3;

        Texture2D comicstrip4;

        Texture2D comicstrip5;

        Texture2D comicstrip6;

        Texture2D comicstrip7;

        Texture2D comicstrip8;

        Texture2D Menu;

        Texture2D MuzzleFlash;

        Texture2D pickupTexture;

       

        Texture2D Gameover;


        public readonly Rectangle ScreenRectangle;


    #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

       

            Content.RootDirectory = "Content";

        
        }

        #region  initialize

        protected override void Initialize()
        {
            //Initalise the player class
            player = new Player();

            // Set a constant player move speed
            playerMoveSpeed = 8.0f;

            // Initialize the enemies list
            enemies = new List<Enemy>();

        

            //Initialise pickups
            pickups = new List<Pickup>();

            // Set the time keepers to zero
            previousSpawnTime = TimeSpan.Zero;

            // Used to determine how fast enemy respawns
            enemySpawnTime = TimeSpan.FromSeconds(1.0f);

            previouspickupSpawnTime = TimeSpan.Zero;

            // Used to determine how fast enemy respawns
            pickupSpawnTime = TimeSpan.FromSeconds(5.0f);

            // Initialize our random number generator
            random = new Random();

            projectiles = new List<Bullet>();

            EnProjectiles = new List<EnemyBullet>();

            // Time constraint for gun
            fireTime = TimeSpan.FromSeconds(.5f);

            explosions = new List<Animation>();

            powerupeffects = new List<Animation> ();

            //Set player's score to zero
            score = 0;

            Enemy_numbers = 20;


            base.Initialize();
        }

        #endregion

   
        #region load textures & content


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the player resources 
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            backgroundTexture = Content.Load<Texture2D>("Backgrounds/lab_floor");

            player.Initialize(Content.Load<Texture2D>("Sprites/newfrank"), playerPosition);

            enemyTexture = Content.Load<Texture2D>("Sprites/NewNinja");

            enemyTexturelvl2 = Content.Load<Texture2D>("Sprites/newcaveman");

            enemyTexturelvl3 = Content.Load<Texture2D>("Sprites/NewGladiator");

            enemyTexturelvl4 = Content.Load<Texture2D>("Sprites/Newcowboy");

            enemyTexturelvl5 = Content.Load<Texture2D>("Sprites/newgerman");

            projectileTexture = Content.Load<Texture2D>("Sprites/laser (3)");

            EnprojectileTexture = Content.Load<Texture2D>("Sprites/laser (3)");

            explosionTexture = Content.Load<Texture2D>("Sprites/splatter");

            PowerupTexture = Content.Load<Texture2D>("Effects/newpowerupeffectalt2");

            gameplayMusic = Content.Load<Song>("Music/5. Lab");

            gameplayMusic2 = Content.Load<Song>("Music/6. Neanderthal");

            gameplayMusic3 = Content.Load<Song>("Music/7. Roman");

            gameplayMusic4 = Content.Load<Song>("Music/8. Cowboy");

            gameplayMusic5 = Content.Load<Song>("Music/9. Army");

            gameplayMusic6 = Content.Load<Song>("Music/10. Future");

            Mainmenumusic = Content.Load<Song>("Music/1. FVT Menu Title");

            comicstripMusic = Content.Load<Song>("Music/2. FVT Comics");

            comicstripMusic2 = Content.Load<Song>("Music/2. FVT Comics");

            comicstripMusic3 = Content.Load<Song>("Music/2. FVT Comics");

            GAMEOVER = Content.Load<Song>("Music/4. Gameover");

            DeathSound[0] = Content.Load<SoundEffect>("Sound/Death1");

            DeathSound[1] = Content.Load<SoundEffect>("Sound/Death2");

            DeathSound[2] = Content.Load<SoundEffect>("Sound/Death3");

            DeathSound[3] = Content.Load<SoundEffect>("Sound/Death4");

            DeathSound[4] = Content.Load<SoundEffect>("Sound/Death5");

            DeathSound[5] = Content.Load<SoundEffect>("Sound/Death6");

            DeathSound[6] = Content.Load<SoundEffect>("Sound/Death7");

            Splat = Content.Load<SoundEffect>("Sound/splat");

            Gunfire = Content.Load<SoundEffect>("Sound/gunsound");

            pickup = Content.Load<SoundEffect>("Sound/Powerup");

            font = Content.Load<SpriteFont>("Fonts/gameFont");

            t2dTitleScreen = Content.Load<Texture2D>("Backgrounds/background");

            Menu = Content.Load<Texture2D>("Backgrounds/background");

            comicstrip = Content.Load<Texture2D>("Backgrounds/scene 1");

            Gameover = Content.Load<Texture2D>("Backgrounds/GameOver");

            Jungle = Content.Load<Texture2D>("Backgrounds/new jungle");

            Roman = Content.Load<Texture2D>("Backgrounds/sand");

            ww1 = Content.Load<Texture2D>("Backgrounds/cowboy map");

            western = Content.Load<Texture2D>("Backgrounds/battlefield");

            future = Content.Load<Texture2D>("Backgrounds/Future 1");

            comicstrip2 = Content.Load<Texture2D>("Backgrounds/caveman begin");

            comicstrip3 = Content.Load<Texture2D>("Backgrounds/roman opening");

            comicstrip4 = Content.Load<Texture2D>("Backgrounds/world war level opening");

            comicstrip5 = Content.Load<Texture2D>("Backgrounds/wild west opening");

            comicstrip6 = Content.Load<Texture2D>("Backgrounds/future opening");

            comicstrip7 = Content.Load<Texture2D>("Backgrounds/Finale");

            comicstrip8 = Content.Load<Texture2D>("Backgrounds/CREDITS");

            MuzzleFlash = Content.Load<Texture2D>("Effects/muzzleflash");

            pickupTexture = Content.Load<Texture2D>("Sprites/healthcrate1");

           



   

            PlayMusic(Mainmenumusic);



        }

        #endregion

        #region music manager

        private void PlayMusic(Song song)
        {
            
            try
            {
                // Play
                MediaPlayer.Play(song);

                // music loop
                MediaPlayer.IsRepeating = true;
            }
            catch { }
        }

        #endregion

        protected override void UnloadContent()
        {

        }

        #region add explosion animation

        private void AddExplosion(Vector2 position)
        {
            Animation explosion = new Animation();
            explosion.Initialize(explosionTexture, position, 134, 134, 12, 45, Color.White, 1f, false);
            explosions.Add(explosion);
        }


        private void Addpowerupeffect(Vector2 position)
        {
            Animation powerup = new Animation();
            powerup.Initialize(PowerupTexture, position, 134, 134, 12, 45, Color.White, 1f, false);
            explosions.Add(powerup);
        }

        #endregion

 

        #region update methods etc

        protected void menu()
        {
            GameStarted = 16;


            PlayMusic(Mainmenumusic);

        }

           protected void StartNewGame()
        {
            GameStarted = 2;
                player.Health = 100;
                score = 0;

           
            PlayMusic(gameplayMusic);
           
        }

           protected void ComicStrip()
           {
               GameStarted = 1;

               PlayMusic(comicstripMusic);

           }

           protected void ComicStrip2()
           {
               GameStarted = 4;

               PlayMusic(comicstripMusic2);

           }

           protected void ComicStrip3()
           {
               GameStarted = 6;

               PlayMusic(comicstripMusic3);

           }


           protected void ComicStrip4()
           {
               GameStarted = 10;

               PlayMusic(comicstripMusic);

           }

           protected void ComicStrip5()
           {
               GameStarted = 8;

               PlayMusic(comicstripMusic);

           }

           protected void ComicStrip6()
           {
               GameStarted = 12;

               PlayMusic(comicstripMusic3);

           }

           protected void ComicStrip7()
           {
               GameStarted = 14;

               PlayMusic(comicstripMusic3);

           }

           protected void Comicstrip8()
           {
               GameStarted = 15;

               PlayMusic(Mainmenumusic);

           }



           protected void Level2()
           {
               GameStarted = 5;

            

               PlayMusic(gameplayMusic2);


           }

           protected void Level3()
           {
               GameStarted = 7;



               PlayMusic(gameplayMusic3);


           }

           protected void Level4()
           {
               GameStarted = 9;



               PlayMusic(gameplayMusic4);


           }

           protected void Level5()
           {
               GameStarted = 11;



               PlayMusic(gameplayMusic5);


           }

           protected void Level6()
           {
               GameStarted = 13;



               PlayMusic(gameplayMusic6);


           }



           protected void GameOver()
           {
         

                GameStarted = 3;

                PlayMusic(GAMEOVER);

           


           }

         
     

           protected override void Update(GameTime gameTime)
           {

               KeyboardState keystate = Keyboard.GetState();

               // Allows the game to exit
               if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape) == true)
                   this.Exit();

               // Get elapsed game time since last call to Update
               float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

               if (GameStarted == 0)
               {
                   if ((keystate.IsKeyDown(Keys.Enter)))
                   {
                       ComicStrip();


                   }
               }

               else

                      if (GameStarted == 1)
                        {
                              if ((keystate.IsKeyDown(Keys.C)))
                                {
                                    StartNewGame();
                                }
                        }
                     
                      else

                            
               if (GameStarted == 2 || GameStarted == 5 || GameStarted == 7|| GameStarted == 9|| GameStarted == 11|| GameStarted == 13)
               {

                   previousKeyboardState = currentKeyboardState;

                   // Read the current state of the keyboard  and store it
                   currentKeyboardState = Keyboard.GetState();

                   //Update the player
                   UpdatePlayer(gameTime);

                   // Update the enemies
                   UpdateEnemies(gameTime);

                   // Update the enemies
                   Updatepickups(gameTime);
              
                   // Update the collision
                   UpdateCollision();

                   // Update the projectiles
                   UpdateProjectiles();

                   //Update Enemy projectiles 
                   UpdateEnProjectiles();

                   // Update the explosions
                   UpdateExplosions(gameTime);

                   // Update the powerup explosions
                   UpdatePowerupEffetcs(gameTime);

                   

               }

                      else

                      if (GameStarted == 4)
                        {
                              if ((keystate.IsKeyDown(Keys.C)))
                                {
                                    Level2();
                                }
                        }


                      else

                          if (GameStarted == 6)
                          {
                              if ((keystate.IsKeyDown(Keys.C)))
                              {
                                  Level3();
                              }
                          }

                               else

                          if (GameStarted == 8)
                          {
                              if ((keystate.IsKeyDown(Keys.C)))
                              {
                                  Level5();
                              }
                          }

                          else

               if (GameStarted == 10)
               {
                   if ((keystate.IsKeyDown(Keys.C)))
                   {
                       Level4();
                   }
               }

               if (GameStarted == 12)
               {
                   if ((keystate.IsKeyDown(Keys.C)))
                   {
                       Level6();
                   }
               }

               if (GameStarted == 14)
               {
                   if ((keystate.IsKeyDown(Keys.C)))
                   {
                       Comicstrip8();
                   }
               }

               if (GameStarted == 15)
               {
                   if ((keystate.IsKeyDown(Keys.N)))
                   {
                       
                 menu();
                
                   }
               }

               if (GameStarted == 16)
               {
                   if ((keystate.IsKeyDown(Keys.Enter)))
                   {

                       StartNewGame();

                   }
               }


   
             

                      else

                   if (GameStarted == 3)

                       if ((keystate.IsKeyDown(Keys.Enter)))
                       {

                           menu();

                       }
                       

                   
                       base.Update(gameTime);
               }

           

        #endregion

   


        #region collision update

           private void UpdateCollision()
        {
            // Use the Rectangle's built-in intersect function to 
            // determine if two objects are overlapping

            Rectangle rectangle1;
            Rectangle rectangle2;

            // Only create the rectangle once for the player
            rectangle1 = new Rectangle((int)player.Position.X,
            (int)player.Position.Y,
            player.Width,
            player.Height);

            // Do the collision between the player and the enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                rectangle2 = new Rectangle((int)enemies[i].Position.X,
                (int)enemies[i].Position.Y,
                enemies[i].Width,
                enemies[i].Height);

                // Determine if the two objects collided with each
                // other
                if (rectangle1.Intersects(rectangle2))
                {
                    // Subtract the health from the player based on
                    // the enemy damage
                    player.Health -= enemies[i].Damage;

                    // Since the enemy collided with the player
                    // destroy it
                    enemies[i].Health = 0;

                    // If the player health is less than zero we died
                    if (player.Health <= 0)
                        player.Active = false;
                }


            }


            //pickup vs player collision

            for (int i = 0; i < pickups.Count; i++)
            {
                rectangle2 = new Rectangle((int)pickups[i].Position.X,
                (int)pickups[i].Position.Y,
                pickups[i].Width,
                pickups[i].Height);

                // Determine if the two objects collided with each
                // other
                if (rectangle2.Intersects(rectangle1))
                {
                    // Subtract the health from the player based on
                    // the enemy damage
                    player.Health += pickups[i].Damage;
                    pickup.Play();

                  


                    pickups[i].Active = false;

                   
                  
                }


            }
               //Enemy bullet vs player collision

            for (int i = 0; i < EnProjectiles.Count; i++)
            {
                rectangle2 = new Rectangle((int)EnProjectiles[i].Position.X,
                (int)EnProjectiles[i].Position.Y,
                EnProjectiles[i].Width,
                EnProjectiles[i].Height);

                // Determine if the two objects collided with each
                // other
                if (rectangle2.Intersects(rectangle1))
                {
                    // Subtract the health from the player based on
                    // the enemy damage
                    player.Health -= EnProjectiles[i].Damage;

                    
                   EnProjectiles[i].Active = false;

                    // If the player health is less than zero we died
                    if (player.Health <= 0)
                        player.Active = false;
                }


            }

   
            // Projectile vs Enemy Collision
            for (int i = 0; i < projectiles.Count; i++)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    // Create the rectangles we need to determine if we collided with each other
                    rectangle1 = new Rectangle((int)projectiles[i].Position.X -
                    projectiles[i].Width , (int)projectiles[i].Position.Y -
                    projectiles[i].Height -20, projectiles[i].Width, projectiles[i].Height);

                    rectangle2 = new Rectangle((int)enemies[j].Position.X - enemies[j].Width / 2,
                    (int)enemies[j].Position.Y - enemies[j].Height / 2 ,
                    enemies[j].Width, enemies[j].Height);

                    // Determine if the two objects collided with each other
                    if (rectangle1.Intersects(rectangle2))
                    {
                        enemies[j].Health -= projectiles[i].Damage;
                        projectiles[i].Active = false;
                    }
                }
            }


        }


        #endregion

        #region addenemy

        private void AddEnemy()
        {

            // Randomly generate the position of the enemy
            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + enemyTexture.Width / 2, random.Next(100, GraphicsDevice.Viewport.Height - 100));

            // Create an enemy
            Enemy enemy = new Enemy();

            if (GameStarted == 2)
            {
                // Initialize the enemy
                enemy.Initialize(enemyTexture, position);
            }

            if (GameStarted == 5)
            {
               
                // Initialize the enemy
                enemy.Initialize(enemyTexturelvl2, position);

              
            }


            if (GameStarted == 7)
            {
                // Initialize the enemy
                enemy.Initialize(enemyTexturelvl3, position);


            }

            if (GameStarted == 9)
            {
                // Initialize the enemy
                enemy.Initialize(enemyTexturelvl4, position);


            }

            if (GameStarted == 11)
            {
                // Initialize the enemy
                enemy.Initialize(enemyTexturelvl5, position);


            }

            if (GameStarted == 13)
            {
                // Initialize the enemy
                enemy.Initialize(enemyTexture, position);
              

            }

            enemies.Add(enemy);
        }

        private void AddPickup()
        {

            // Randomly generate the position of the pickup
            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + pickupTexture.Width / 2, random.Next(100, GraphicsDevice.Viewport.Height - 100));

            // Create an enemy
            Pickup pickup = new Pickup();

            if (GameStarted == 2)
            {
                // Initialize the enemy
                pickup.Initialize(pickupTexture, position);
            }

            if (GameStarted == 5)
            {
                // Initialize the enemy
                pickup.Initialize(pickupTexture, position);


            }


            if (GameStarted == 7)
            {
                // Initialize the enemy
                pickup.Initialize(pickupTexture, position);


            }

            if (GameStarted == 9)
            {
                // Initialize the enemy
                pickup.Initialize(pickupTexture, position);


            }

            if (GameStarted == 11)
            {
                // Initialize the enemy
                pickup.Initialize(pickupTexture, position);


            }

            if (GameStarted == 13)
            {
                // Initialize the enemy
                pickup.Initialize(pickupTexture, position);


            }

            pickups.Add(pickup);
        }


        #endregion

        #region update explosions

        private void UpdateExplosions(GameTime gameTime)
        {
            for (int i = explosions.Count - 1; i >= 0; i--)
            {
                explosions[i].Update(gameTime);
                if (explosions[i].Active == false)
                {
                    explosions.RemoveAt(i);
                }
            }
        }


        private void UpdatePowerupEffetcs(GameTime gameTime)
        {
            for (int i = powerupeffects.Count - 1; i >= 0; i--)
            {
                powerupeffects[i].Update(gameTime);
                if (powerupeffects[i].Active == false)
                {
                    powerupeffects.RemoveAt(i);
                }
            }
        }
        #endregion

        #region update enemy & pickup

        private void UpdateEnemies(GameTime gameTime)
        {
            if (GameStarted == 2 || GameStarted == 5|| GameStarted ==7|| GameStarted == 9|| GameStarted == 11|| GameStarted == 13)
            {
                // Spawn a new enemy enemy every 1.5 seconds
                if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
                {
                    previousSpawnTime = gameTime.TotalGameTime;


                    // Add an Enemy
                    AddEnemy();

                  
                }

                // Update the Enemies
                for (int i = enemies.Count - 3; i >= 0; i--)
                {


                    if (gameTime.TotalGameTime - previousFireTime > fireTime)
                    {
                        // Reset our current time
                        previousFireTime = gameTime.TotalGameTime;

                        

                        
                        // Add the projectile, but add it to the front and center of the player
                        AddEnProjectiles(enemies[i].Position + new Vector2(100, 85));
                    }


                    // If not active and health <= 0
                    if (enemies[i].Health <= 0)
                    {
                        Splat.Play();
                        DeathSound[random.Next(deathsoundsExtreme)].Play();

                        // Add an explosion
                        AddExplosion(enemies[i].Position);

                        //Add to the player's score
                        score += enemies[i].Value;
                    }




                    if (enemies[i].Active == false)
                    // If not active and health <= 0
                    {
                        Enemy_numbers -= 1;
                        enemies.RemoveAt(i);

                    }

                    if (GameStarted == 2)
                    {
                        if (Enemy_numbers <= 0)
                        {

                            AddExplosion(enemies[i].Position);
                            
                            ComicStrip2();
                            enemies.RemoveAt(i);

                            Enemy_numbers = 20;


                        }
                    }



                    if (GameStarted == 5)
                    {
                      

                        if (Enemy_numbers == 0)
                        {

                            AddExplosion(enemies[i].Position);
                            enemies.RemoveAt(i);
                            ComicStrip3();

                            Enemy_numbers = 25;


                        }
                    }
                       

                    if (GameStarted == 7)
                    {
                         

                         if (Enemy_numbers <= 0)
                         {

                             AddExplosion(enemies[i].Position);
                             enemies.RemoveAt(i);
                             ComicStrip4();

                             Enemy_numbers = 30;
                         }

                        }

                    if (GameStarted == 9)

                        if (Enemy_numbers <= 0)
                        {

                            AddExplosion(enemies[i].Position);
                            enemies.RemoveAt(i);
                            ComicStrip5();

                            Enemy_numbers = 35;


                        }


                    if (GameStarted == 11)

                        if (Enemy_numbers <= 0)
                        {

                            AddExplosion(enemies[i].Position);
                            enemies.RemoveAt(i);
                            ComicStrip6();
                            

                            Enemy_numbers = 35;


                        }

                    if (GameStarted == 13)

                        if (Enemy_numbers <= 0)
                        {
                            
                            AddExplosion(enemies[i].Position);
                            enemies.RemoveAt(i);
                            ComicStrip7();

                            Enemy_numbers = 40;


                        }





                    enemies[i].Update(gameTime);

                   
                }

            } 
        }
            

        private void Updatepickups(GameTime gameTime)
        {
            if (GameStarted == 2 || GameStarted == 5 || GameStarted == 7 || GameStarted == 9 || GameStarted == 11 || GameStarted == 13)
            {
               

                // Spawn a new enemy enemy every 1.5 seconds
                if (gameTime.TotalGameTime - previouspickupSpawnTime > pickupSpawnTime)
                {
                    previouspickupSpawnTime = gameTime.TotalGameTime;


                    // Add a pickup
                    AddPickup();
                }
            }

                    for (int i = pickups.Count - 3; i >= 0; i--)
                    {
                        if (player.Health == 100)
                        {
                            pickups[i].Damage = 0;
                        }

                        if (pickups[i].Active == false)
                        // If not active and health <= 0
                        {

                            Addpowerupeffect(pickups[i].Position);
                            pickups.RemoveAt(i);

                        }
                   




                        pickups[i].Update(gameTime);
                       

                    }
                }
            
        
        #endregion

        #region addprojectile & update

        private void AddProjectile(Vector2 position)
     
        {
            Bullet bullet = new Bullet();
            bullet.Initialize(GraphicsDevice.Viewport, projectileTexture, position);
            projectiles.Add(bullet);
        }

        private void AddEnProjectiles(Vector2 position)
        {
            if (GameStarted == 2 || GameStarted == 9 || GameStarted == 11 || GameStarted == 13)
            {
                EnemyBullet Enbullet = new EnemyBullet();
                Enbullet.Initialize(GraphicsDevice.Viewport, EnprojectileTexture, position);
                EnProjectiles.Add(Enbullet);
            }
        }


        private void UpdateProjectiles()
        {
            // Update the Projectiles
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {

                projectiles[i].Update();

                if (projectiles[i].Active == false)
                {
                    projectiles.RemoveAt(i);
                }

            }


        }

        private void UpdateEnProjectiles()
        {
          if (GameStarted == 2 || GameStarted == 9 || GameStarted == 11 || GameStarted == 13)
            // Update the Projectiles
            for (int i = EnProjectiles.Count - 1; i >= 0; i--)
            {

                EnProjectiles[i].Update();

                if (EnProjectiles[i].Active == false)
                {
                   EnProjectiles.RemoveAt(i);
                }

            }


        }

        #endregion

        #region updatePlayer

        private void UpdatePlayer(GameTime gameTime)
        {


            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                player.Position.X -= playerMoveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                player.Position.X += playerMoveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.W))
            {
                player.Position.Y -= playerMoveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.S))
            {
                player.Position.Y += playerMoveSpeed;
            }

            // Make sure that the player does not go out of bounds
            player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);
            player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height);

            // Fire only every interval we set as the fireTime
            if (currentKeyboardState.IsKeyDown(Keys.Space))
            {
                if (gameTime.TotalGameTime - previousFireTime > fireTime )
                {
                    Gunfire.Play();
                    previousFireTime = gameTime.TotalGameTime;


                 
                    // Add the projectile correct location of the player
                    AddProjectile(player.Position + new Vector2(100, 85));
                }

               

            }

            if (player.Health == 0)
            {

                AddExplosion(player.Position);
             
                GameOver();
            }

        }

        #endregion

        #region draw

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

          //  vidTexture = vidPlayer.GetTexture();

            // Start drawing
            spriteBatch.Begin();

            if (GameStarted == 0)
            {
                spriteBatch.Draw(t2dTitleScreen, new Rectangle(0, 0, 1280, 720), Color.White);
            }

            else


                if (GameStarted == 1)
                {
                    spriteBatch.Draw(comicstrip, new Rectangle(0, 0, 1280, 720), Color.White);
                }

                else

            if (GameStarted == 2)
            {

                //Draw the background
                spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.LightGray);

                // Draw the Player
                player.Draw(spriteBatch);


                // Draw the Enemies
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Draw(spriteBatch);
                }

                //Draw the pickups
              
                for (int i = 0; i < pickups.Count; i++)
                {
                    pickups[i].Draw(spriteBatch);
                }


                // Draw the Projectiles
                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Draw(spriteBatch);
                }

                for (int i = 0; i < EnProjectiles.Count; i++)
                {
                    EnProjectiles[i].Draw(spriteBatch);
                }

                // Draw the explosions
                for (int i = 0; i < explosions.Count; i++)
                {
                    explosions[i].Draw(spriteBatch);
                }

                //powerupeffect
                for (int i = 0; i < powerupeffects.Count; i++)
                {
                    powerupeffects[i].Draw(spriteBatch);
                }

                // Draw the score
                spriteBatch.DrawString(font, "Score: " + score, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 1000, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);

                // Draw the player health
                spriteBatch.DrawString(font, "health: " + player.Health, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 1000, GraphicsDevice.Viewport.TitleSafeArea.Y + 30), Color.White);
             
              
            }

            else

                    if (GameStarted == 4)
                {
                    spriteBatch.Draw(comicstrip2, new Rectangle(0, 0, 1280, 720), Color.White);
                }

                    else

            if (GameStarted == 6)
            {
                spriteBatch.Draw(comicstrip3, new Rectangle(0, 0, 1280, 720), Color.White);
            }

            else

                if (GameStarted == 8)
                {
                    spriteBatch.Draw(comicstrip4, new Rectangle(0, 0, 1280, 720), Color.White);
                } 

            else

                    if (GameStarted == 10)
                    {
                        spriteBatch.Draw(comicstrip5, new Rectangle(0, 0, 1280, 720), Color.White);
                    }

                    else

                        if (GameStarted == 12)
                        {
                            spriteBatch.Draw(comicstrip6, new Rectangle(0, 0, 1280, 720), Color.White);
                        }

                        else

                            if (GameStarted == 14)
                            {
                                spriteBatch.Draw(comicstrip7, new Rectangle(0, 0, 1280, 720), Color.White);
                            }

                            else

                                if (GameStarted == 15)
                                {
                                    spriteBatch.Draw(comicstrip8, new Rectangle(0, 0, 1280, 720), Color.White);
                                }

                                else


                                    if (GameStarted == 16)
                                    {
                                        spriteBatch.Draw(Menu, new Rectangle(0, 0, 1280, 720), Color.White);
                                    }

                                    else


                        if (GameStarted == 5)
                        {

                            //Draw the background
                            spriteBatch.Draw(Jungle, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.LightGray);

                            // Draw the Player
                            player.Draw(spriteBatch);


                            // Draw the Enemies
                            for (int i = 0; i < enemies.Count; i++)
                            {
                                enemies[i].Draw(spriteBatch);
                            }

                            // Draw the Projectiles
                            for (int i = 0; i < projectiles.Count; i++)
                            {
                                projectiles[i].Draw(spriteBatch);
                            }

                            //Draw the pickups

                            for (int i = 0; i < pickups.Count; i++)
                            {
                                pickups[i].Draw(spriteBatch);
                            }

                            for (int i = 0; i < EnProjectiles.Count; i++)
                            {
                                EnProjectiles[i].Draw(spriteBatch);
                            }


                            // Draw the explosions
                            for (int i = 0; i < explosions.Count; i++)
                            {
                                explosions[i].Draw(spriteBatch);
                            }

                            //powerupeffect
                            for (int i = 0; i < powerupeffects.Count; i++)
                            {
                                powerupeffects[i].Draw(spriteBatch);
                            }

                            // Draw the score
                            spriteBatch.DrawString(font, "Score: " + score, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 1000, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);

                            // Draw the player health
                            spriteBatch.DrawString(font, "health: " + player.Health, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 1000, GraphicsDevice.Viewport.TitleSafeArea.Y + 30), Color.White);


                        }

                    else

            if (GameStarted == 7)
            {

                //Draw the background
                spriteBatch.Draw(Roman, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.LightGray);

                // Draw the Player
                player.Draw(spriteBatch);


                // Draw the Enemies
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Draw(spriteBatch);
                }

                //Draw the pickups

                for (int i = 0; i < pickups.Count; i++)
                {
                    pickups[i].Draw(spriteBatch);
                }

                // Draw the Projectiles
                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Draw(spriteBatch);
                }

                // Draw the explosions
                for (int i = 0; i < explosions.Count; i++)
                {
                    explosions[i].Draw(spriteBatch);
                }

                //powerupeffect
                for (int i = 0; i < powerupeffects.Count; i++)
                {
                    powerupeffects[i].Draw(spriteBatch);
                }

                for (int i = 0; i < EnProjectiles.Count; i++)
                {
                    EnProjectiles[i].Draw(spriteBatch);
                }


                // Draw the score
                spriteBatch.DrawString(font, "Score: " + score, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 1000, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);

                // Draw the player health
                spriteBatch.DrawString(font, "health: " + player.Health, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 1000, GraphicsDevice.Viewport.TitleSafeArea.Y + 30), Color.White);


            }

            else

            if (GameStarted == 9)
            {

                //Draw the background
                spriteBatch.Draw(ww1, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.LightGray);

                // Draw the Player
                player.Draw(spriteBatch);


                // Draw the Enemies
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Draw(spriteBatch);
                }

                // Draw the Projectiles
                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Draw(spriteBatch);
                }

                //Draw the pickups

                for (int i = 0; i < pickups.Count; i++)
                {
                    pickups[i].Draw(spriteBatch);
                }

                // Draw the explosions
                for (int i = 0; i < explosions.Count; i++)
                {
                    explosions[i].Draw(spriteBatch);
                }

                //powerupeffect
                for (int i = 0; i < powerupeffects.Count; i++)
                {
                    powerupeffects[i].Draw(spriteBatch);
                }

                for (int i = 0; i < EnProjectiles.Count; i++)
                {
                    EnProjectiles[i].Draw(spriteBatch);
                }


                // Draw the score
                spriteBatch.DrawString(font, "Score: " + score, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 1000, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);

                // Draw the player health
                spriteBatch.DrawString(font, "health: " + player.Health, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 1000, GraphicsDevice.Viewport.TitleSafeArea.Y + 30), Color.White);


            }

            else

            if (GameStarted == 11)
            {

                //Draw the background
                spriteBatch.Draw(western, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.LightGray);

                // Draw the Player
                player.Draw(spriteBatch);


                // Draw the Enemies
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Draw(spriteBatch);
                }

                //Draw the pickups

                for (int i = 0; i < pickups.Count; i++)
                {
                    pickups[i].Draw(spriteBatch);
                }

                // Draw the Projectiles
                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Draw(spriteBatch);
                }

                // Draw the explosions
                for (int i = 0; i < explosions.Count; i++)
                {
                    explosions[i].Draw(spriteBatch);
                }

                //powerupeffect
                for (int i = 0; i < powerupeffects.Count; i++)
                {
                    powerupeffects[i].Draw(spriteBatch);
                }

                for (int i = 0; i < EnProjectiles.Count; i++)
                {
                    EnProjectiles[i].Draw(spriteBatch);
                }


                // Draw the score
                spriteBatch.DrawString(font, "Score: " + score, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 1000, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);

                // Draw the player health
                spriteBatch.DrawString(font, "health: " + player.Health, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 1000, GraphicsDevice.Viewport.TitleSafeArea.Y + 30), Color.White);


            }

            else

                if (GameStarted == 13)
                {

                    //Draw the background
                    spriteBatch.Draw(future, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.LightGray);

                    // Draw the Player
                    player.Draw(spriteBatch);


                    // Draw the Enemies
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].Draw(spriteBatch);
                    }

                    // Draw the Projectiles
                    for (int i = 0; i < projectiles.Count; i++)
                    {
                        projectiles[i].Draw(spriteBatch);
                    }

                    //Draw the pickups

                    for (int i = 0; i < pickups.Count; i++)
                    {
                        pickups[i].Draw(spriteBatch);
                    }

                    // Draw the explosions
                    for (int i = 0; i < explosions.Count; i++)
                    {
                        explosions[i].Draw(spriteBatch);
                    }

                    //powerupeffect
                    for (int i = 0; i < powerupeffects.Count; i++)
                    {
                        powerupeffects[i].Draw(spriteBatch);
                    }

                    for (int i = 0; i < EnProjectiles.Count; i++)
                    {
                        EnProjectiles[i].Draw(spriteBatch);
                    }


                    // Draw the score
                    spriteBatch.DrawString(font, "Score: " + score, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 1000, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);

                    // Draw the player health
                    spriteBatch.DrawString(font, "health: " + player.Health, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 1000, GraphicsDevice.Viewport.TitleSafeArea.Y + 30), Color.White);


                }
                if (GameStarted == 3)
                {
                    spriteBatch.Draw(Gameover, new Rectangle(0, 0, 1280, 720), Color.White);
                }

            // Close the SpriteBatch
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
        #endregion