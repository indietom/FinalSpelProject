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

        public static float worldSpeed;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 640;
            Content.RootDirectory = "Content";
        }

        List<Player> player = new List<Player>();
        List<Enemy> enemies = new List<Enemy>();
        List<Projectile> projectiles = new List<Projectile>();

        protected override void Initialize()
        {
            player.Add(new Player());
            enemies.Add(new Enemy(new Vector2(),13));
            
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

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            
            foreach(Player p in player)
            {
                p.Update();
                p.Input(projectiles);
                p.LivesUpdate();
            }
            foreach(Enemy e in enemies)
            {
                e.Update(player);
                e.Collision(player, enemies);
            }
            foreach(Projectile p in projectiles)
            {
                p.Update();
            }

            for (int i = 0; i < enemies.Count();i++)
            {
                if (enemies[i].Destroy) enemies.RemoveAt(i);
            }
            for (int i = 0; i < projectiles.Count(); i++)
            {
                if (projectiles[i].Destroy) projectiles.RemoveAt(i);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            foreach (Player p in player) { if(!p.Flash) p.DrawSprite(spriteBatch, spritesheet); }
            foreach (Enemy e in enemies) { e.DrawSprite(spriteBatch, spritesheet); }
            foreach (Projectile p in projectiles) { p.DrawSprite(spriteBatch, spritesheet); }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
