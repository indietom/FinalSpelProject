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

namespace FinalSpelProject
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        List<Player> player = new List<Player>();

        protected override void Initialize()
        {
            player.Add(new Player());
            base.Initialize();
        }

        Texture2D spritesheet;

        protected override void LoadContent()
        {
            spritesheet = Content.Load<Texture2D>("spritesheet");
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }
        int[,] map;
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            foreach(Player p in player)
            {
                p.Update();
                p.Input();
                p.livesUpdate();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            foreach (Player p in player) { p.DrawSprite(spriteBatch, spritesheet); }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
