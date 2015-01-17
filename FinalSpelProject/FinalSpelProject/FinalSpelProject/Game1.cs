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

        public static int screenW;
        public static int screenH;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 640;
            Content.RootDirectory = "Content";
        }

        List<Player> player = new List<Player>();
        List<Enemy> enemies = new List<Enemy>();
        List<Projectile> projectiles = new List<Projectile>();
        List<Chunk> chunks = new List<Chunk>();
        List<Particle> particles = new List<Particle>();
        List<Explosion> explosions = new List<Explosion>();
        List<PowerUp> powerUps = new List<PowerUp>();

        protected override void Initialize()
        {
            player.Add(new Player());
            //enemies.Add(new Enemy(new Vector2(),13));
            //enemies.Add(new Enemy(new Vector2(100, 20),11));
            screenH = graphics.PreferredBackBufferHeight;
            screenW = graphics.PreferredBackBufferWidth;
            enemies.Add(new Enemy(new Vector2(200,10), 14));
            chunks.Add(new Chunk(new Vector2(0, 0), @"map"));
            worldSpeed = 0.1f;
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
                p.Update(projectiles);
            }

            foreach(Enemy e in enemies)
            {
                e.Update(player, projectiles);
            }

            foreach(Projectile p in projectiles)
            {
                p.Update(particles);
            }

            foreach(Chunk c in chunks)
            {
                c.Update(enemies);
            }
            
            foreach(Particle p in particles)
            {
                p.Update();
            }

            foreach(Explosion e in explosions)
            {
                e.Update();
            }

            foreach(PowerUp p in powerUps)
            {
                p.Update(player);
            }

            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                powerUps.Add(new PowerUp(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 1, 0));
            }

            for (int i = 0; i < enemies.Count();i++)
            {
                if (enemies[i].Destroy) enemies.RemoveAt(i);
            }
            for (int i = 0; i < projectiles.Count(); i++)
            {
                if (projectiles[i].Destroy) projectiles.RemoveAt(i);
            }
            for (int i = 0; i < chunks.Count; i++)
            {
                if (chunks[i].Destroy) chunks.RemoveAt(i);
            }
            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i].Destroy) particles.RemoveAt(i);
            }
            for (int i = 0; i < explosions.Count; i++)
            {
                if (explosions[i].Destroy) explosions.RemoveAt(i);
            }
            for (int i = 0; i < powerUps.Count; i++)
            {
                if (powerUps[i].Destroy) powerUps.RemoveAt(i);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            foreach (Chunk c in chunks) { c.Draw(spriteBatch, spritesheet); }
            foreach (Player p in player) { if(!p.Flash) p.DrawSprite(spriteBatch, spritesheet); }
            foreach (Enemy e in enemies) { if (!e.Rotated) e.DrawSprite(spriteBatch, spritesheet); else e.DrawSprite(spriteBatch, spritesheet, e.RoateOnRad); }
            foreach (Particle p in particles) { p.DrawSprite(spriteBatch, spritesheet); }
            foreach (Explosion e in explosions) { e.DrawSprite(spriteBatch, spritesheet);  }
            foreach (Projectile p in projectiles) { p.DrawSprite(spriteBatch, spritesheet); }
            foreach (PowerUp p in powerUps) { p.DrawSprite(spriteBatch, spritesheet); }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
