using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpriteDemonstrations
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Player player;
        public BoundingRectangle box;
        Texture2D pixel;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            player = new Player(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1042;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();
            // TODO: Add your initialization logic here
            box.X = 400;
            box.Y = 300;
            box.Width = 200;
            box.Height = 200;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent();
            spriteFont = Content.Load<SpriteFont>("defaultFont");
            pixel = Content.Load<Texture2D>("pixel");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update(gameTime);
//            var size = spriteFont.MeasureString("Hello World");

            if(player.Bounds.CollidesWith(box))
            {
                float delta;
                switch(player.state)
                {
                    case State.East:
                        delta = (player.Bounds.X + player.Bounds.Width) - box.X;
                        player.Position.X = box.X - player.Bounds.Width - delta;
                        break;
                    case State.North:
                        delta = (box.Y + box.Height) - player.Bounds.Y;
                        player.Position.Y = box.Y + box.Height + delta;
                        break;
                    case State.West:
                        delta = (box.X + box.Width) - player.Bounds.X;
                        player.Position.X = box.X + box.Width + delta + 1;
                        break;
                    case State.South:
                        delta = (player.Bounds.Y + player.Bounds.Height) - box.Y;
                        player.Position.Y = box.Y - player.Bounds.Height - delta;
                        break;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(pixel, box, Color.White);
            spriteBatch.DrawString(
                spriteFont,
                "Hello World",
                new Vector2(200, 200),
                Color.White
                );
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
