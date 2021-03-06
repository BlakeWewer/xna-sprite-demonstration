﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpriteDemonstrations
{
    /// <summary>
    /// An enum representing the states the player can be in
    /// </summary>
    public enum State
    {
        South = 0,
        East = 1, 
        West = 2,
        North = 3,
        Idle = 4,
    }

    /// <summary>
    /// A class representing a player
    /// </summary>
    public class Player
    {
        /// <summary>
        /// How quickly the animation should advance frames (1/8 second as milliseconds)
        /// </summary>
        const int ANIMATION_FRAME_RATE = 124;

        /// <summary>
        /// How quickly the player should move
        /// </summary>
        const float PLAYER_SPEED = 100;

        /// <summary>
        /// The width of the animation frames
        /// </summary>
        const int FRAME_WIDTH = 49;

        /// <summary>
        /// The hieght of the animation frames
        /// </summary>
        const int FRAME_HEIGHT = 64;

        // Private variables
        Game1 game;
        Texture2D texture;
        public State state;
        TimeSpan timer;
        int frame;
        public BoundingRectangle Bounds;
        public Vector2 Position;
        SpriteFont font;

        /// <summary>
        /// Creates a new player object
        /// </summary>
        /// <param name="game"></param>
        public Player(Game1 game)
        {
            this.game = game;
            timer = new TimeSpan(0);
            Position = new Vector2(600, 200);
            Bounds.X = Position.X;
            Bounds.Y = Position.Y;
            Bounds.Height = FRAME_HEIGHT;
            Bounds.Width = FRAME_WIDTH;
            state = State.Idle;
        }

        /// <summary>
        /// Loads the sprite's content
        /// </summary>
        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("spritesheet");
            font = game.Content.Load<SpriteFont>("defaultFont");
        }

        /// <summary>
        /// Update the sprite, moving and animating it
        /// </summary>
        /// <param name="gameTime">The GameTime object</param>
        public void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update the player state based on input
            if (keyboard.IsKeyDown(Keys.Up))
            {
                state = State.North;
                Position.Y -= delta * PLAYER_SPEED;
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                state = State.West;
                Position.X -= delta * PLAYER_SPEED;
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                state = State.East;
                Position.X += delta * PLAYER_SPEED;
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                state = State.South;
                Position.Y += delta * PLAYER_SPEED;
            }
            else state = State.Idle;

            // Update the player animation timer when the player is moving
            if(state != State.Idle) timer += gameTime.ElapsedGameTime;
            
            // Determine the frame should increase.  Using a while 
            // loop will accomodate the possiblity the animation should 
            // advance more than one frame.
            while(timer.TotalMilliseconds > ANIMATION_FRAME_RATE) {
                // increase by one frame
                frame++;
                // reduce the timer by one frame duration
                timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }
            Bounds.X = Position.X;
            Bounds.Y = Position.Y;
            // Keep the frame within bounds (there are four frames)
            frame %= 4;
        }

        /// <summary>
        /// Renders the sprite on-screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // determine the source rectagle of the sprite's current frame
            var source = new Rectangle(
                frame * FRAME_WIDTH, // X value 
                (int)state % 4 * FRAME_HEIGHT, // Y value
                FRAME_WIDTH, // Width 
                FRAME_HEIGHT // Height
                );

            // render the sprite
            spriteBatch.Draw(texture, Bounds, source, Color.White);

            // render the sprite's coordinates in the upper-right-hand corner of the screen
            spriteBatch.DrawString(font, $"X:{Position.X} Y:{Position.Y}", Vector2.Zero, Color.White);
            spriteBatch.DrawString(font, $"X:400 Y:300", new Vector2(0, 30), Color.White);
        }

    }
}
