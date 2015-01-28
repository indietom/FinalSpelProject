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

        public static bool flashScreen;
        public static byte flashScreenCount;

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
            screenH = graphics.PreferredBackBufferHeight;
            screenW = graphics.PreferredBackBufferWidth;
            chunks.Add(new Chunk(new Vector2(0, 0), @"map"));
            worldSpeed = 0f;
            base.Initialize();
        }

        Texture2D spritesheet;

        protected override void LoadContent()
        {
            spritesheet = Content.Load<Texture2D>("spritesheet");
            TilesheetManager.Load(Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }

        public void UpdateScreenFlash()
        {
            if(flashScreenCount >= 1)
            {
                spriteBatch.Draw(spritesheet, new Rectangle(0, 0, screenW, screenH), new Rectangle(1, 562, 32, 32), Color.White);
                // lite kod > läslighet amrite
                flashScreenCount = (flashScreenCount >= 32) ? flashScreenCount = 0 : flashScreenCount = (byte)(flashScreenCount + 1);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            foreach(Enemy e in enemies)
            {
                e.Update(player, projectiles, explosions);
            }
            foreach (Player p in player)
            {
                p.Update(projectiles, enemies);
            }
            foreach(Projectile p in projectiles)
            {
                p.Update(particles, player[0]);
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
                powerUps.Add(new PowerUp(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 1, 0, false));
                powerUps.Add(new PowerUp(new Vector2(Mouse.GetState().X+100, Mouse.GetState().Y), 2, 0, false));
                powerUps.Add(new PowerUp(new Vector2(Mouse.GetState().X+300, Mouse.GetState().Y), 3, 0, false));
                //flashScreenCount = 1;
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
            foreach (Chunk c in chunks) { c.Draw(spriteBatch, TilesheetManager.TileSheets[0]); }
            foreach (Player p in player) { if(!p.Flash) p.DrawSprite(spriteBatch, spritesheet); }
            foreach (Enemy e in enemies) { if (!e.Rotated) e.DrawSprite(spriteBatch, spritesheet); else e.DrawSprite(spriteBatch, spritesheet, e.RoateOnRad); }
            foreach (Particle p in particles) { p.DrawSprite(spriteBatch, spritesheet); }
            foreach (Explosion e in explosions) { e.DrawSprite(spriteBatch, spritesheet);  }
            foreach (Projectile p in projectiles) { p.DrawSprite(spriteBatch, spritesheet); }
            foreach (PowerUp p in powerUps) { p.DrawSprite(spriteBatch, spritesheet); }
            UpdateScreenFlash();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
