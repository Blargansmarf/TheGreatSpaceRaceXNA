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

namespace TheGreatSpaceRaceXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MouseState prevMouseState, currMouseState;
        KeyboardState prevKeyState, currKeyState;
        GamePadState prevGamePadState, currGamePadState;

        Vector2 translation;
        Vector2 startLoc;
        Vector2 playerLoc;

        Rectangle tempShipShape;

        Texture2D playerTexture;

        float screenWidth, screenHeight;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            translation = new Vector2();

            screenHeight = graphics.GraphicsDevice.Viewport.Height;
            screenWidth = graphics.GraphicsDevice.Viewport.Width;
            tempShipShape = new Rectangle((int)(screenWidth / 2) - 200, (int)(screenHeight / 2) - 200, 400, 400);
            
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

            playerTexture = Content.Load<Texture2D>("WhiteSquare");

            startLoc = new Vector2(screenWidth / 2 - playerTexture.Width / 2, screenHeight / 2 - playerTexture.Height - 2);
            playerLoc = new Vector2(startLoc.X, startLoc.Y);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            currMouseState = Mouse.GetState();
            currKeyState = Keyboard.GetState();
            currGamePadState = GamePad.GetState(PlayerIndex.One);

            ProcessMouse();
            ProcessKeyboard();
            ProcessGamePad();

            prevMouseState = currMouseState;
            prevKeyState = currKeyState;
            prevGamePadState = currGamePadState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //translated
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Matrix.CreateTranslation(-translation.X, -translation.Y, 0));

            spriteBatch.Draw(playerTexture, tempShipShape, Color.White);

            spriteBatch.End();

            //Not translated
            spriteBatch.Begin();

            spriteBatch.Draw(playerTexture, startLoc, Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ProcessMouse()
        {

        }

        private void ProcessKeyboard()
        {
            if (currKeyState.IsKeyDown(Keys.W) &&
                playerLoc.Y > tempShipShape.Top)
            {
                translation.Y--;
                playerLoc.Y--;
            }
            if (currKeyState.IsKeyDown(Keys.S) &&
                playerLoc.Y + playerTexture.Height < tempShipShape.Bottom)
            {
                translation.Y++;
                playerLoc.Y++;
            }
            if (currKeyState.IsKeyDown(Keys.A) &&
                playerLoc.X > tempShipShape.Left)
            {
                translation.X--;
                playerLoc.X--;
            }
            if (currKeyState.IsKeyDown(Keys.D) &&
                playerLoc.X + playerTexture.Width < tempShipShape.Right)
            {
                translation.X++;
                playerLoc.X++;
            }
        }

        private void ProcessGamePad()
        {
            if (currGamePadState.ThumbSticks.Left.Y > 0 &&
                playerLoc.Y > tempShipShape.Top)
            {
                translation.Y--;
                playerLoc.Y--;
            }
            if (currGamePadState.ThumbSticks.Left.Y < 0 &&
                playerLoc.Y + playerTexture.Height < tempShipShape.Bottom)
            {
                translation.Y++;
                playerLoc.Y++;
            }
            if (currGamePadState.ThumbSticks.Left.X < 0 &&
                playerLoc.X > tempShipShape.Left)
            {
                translation.X--;
                playerLoc.X--;
            }
            if (currGamePadState.ThumbSticks.Left.X > 0 &&
                playerLoc.X + playerTexture.Width < tempShipShape.Right)
            {
                translation.X++;
                playerLoc.X++;
            }
        }
    }
}
