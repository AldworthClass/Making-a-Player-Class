using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Making_a_Player_Class
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Random generator = new Random();

        KeyboardState keyboardState;

        Texture2D amoebaTexture;
        Texture2D wallTexture;
        Texture2D foodTexture;

        Player amoeba;

        List<Rectangle> barriers;
        List<Rectangle> food;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
         
            // TODO: Add your initialization logic here
            barriers = new List<Rectangle>();
            
            barriers.Add(new Rectangle(100, 100, 10, 200));
            barriers.Add(new Rectangle(400, 400, 100, 10));

            Rectangle temp;
            food = new List<Rectangle>();
            bool safe;

            // Adds 50 food items that do not intersect with walls
            while (food.Count < 50)
            {
                safe = true;
                temp = new Rectangle(generator.Next(_graphics.PreferredBackBufferWidth - 10), generator.Next(_graphics.PreferredBackBufferHeight - 10), 10, 10);
                foreach (Rectangle barrier in barriers)
                    if (barrier.Intersects(temp))
                        safe = false;
                
                if (safe)
                    food.Add(temp);                 
            }
                
            base.Initialize();
            amoeba = new Player(amoebaTexture, 10, 10);

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            amoebaTexture = Content.Load<Texture2D>("amoeba");
            wallTexture = Content.Load<Texture2D>("rectangle");
            foodTexture = Content.Load<Texture2D>("circle");
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            amoeba.HSpeed = 0;
            amoeba.VSpeed = 0;

            if (keyboardState.IsKeyDown(Keys.D))
                amoeba.HSpeed = 3;
            else if (keyboardState.IsKeyDown(Keys.A))
                amoeba.HSpeed = -3;

            if (keyboardState.IsKeyDown(Keys.W))
                amoeba.VSpeed = -3;
            else if (keyboardState.IsKeyDown(Keys.S))
                amoeba.VSpeed = 3;

            amoeba.Update();

            // Look for wall collisions
            foreach (Rectangle barrier in barriers)
                if (amoeba.Collide(barrier))
                    amoeba.UndoMove();

            // Look for food consumption
            for (int i = 0; i < food.Count; i++) 
                if (amoeba.Collide(food[i]))
                {
                    amoeba.Grow();
                    food.RemoveAt(i);
                    i--;
                }
                    

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            amoeba.Draw(_spriteBatch);

            foreach (Rectangle barrier in barriers)
                _spriteBatch.Draw(wallTexture, barrier, Color.White);

            foreach (Rectangle bit in food)
                _spriteBatch.Draw(foodTexture, bit, Color.Green);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
