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

        Ui ui = new Ui();

        List<Player> player = new List<Player>();
        List<Enemy> enemies = new List<Enemy>();
        List<Projectile> projectiles = new List<Projectile>();
        List<Chunk> chunks = new List<Chunk>();
        List<Particle> particles = new List<Particle>();
        List<Explosion> explosions = new List<Explosion>();
        List<PowerUp> powerUps = new List<PowerUp>();
        List<TextEffect> textEffects = new List<TextEffect>();

        Level level;

        protected override void Initialize()
        {
            player.Add(new Player());
            screenH = graphics.PreferredBackBufferHeight;
            screenW = graphics.PreferredBackBufferWidth;
            //chunks.Add(new Chunk(new Vector2(0, 0), @"map1"));
            level = new Level(chunks, 0, 14);
            worldSpeed = 1.5f;
            base.Initialize();
        }

        Texture2D spritesheet;
        SpriteFont font;

        protected override void LoadContent()
        {
            spritesheet = Content.Load<Texture2D>("spritesheet");
            TilesheetManager.Load(Content);
            font = Content.Load<SpriteFont>("font");
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

            ui.Update(player);

            foreach(Enemy e in enemies)
            {
                e.Update(player, projectiles, explosions, powerUps);
            }
            foreach (Player p in player)
            {
                p.Update(projectiles, enemies, explosions);
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
                p.Update(player, textEffects);
            }

            foreach(TextEffect te in textEffects)
            {
                te.Update();
            }

            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                //explosions.Add(new Explosion(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 64, false));
                powerUps.Add(new PowerUp(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 1, 0, false));
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
            for (int i = 0; i < textEffects.Count; i++)
            {
                if (textEffects[i].Destroy) textEffects.RemoveAt(i);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            foreach (Chunk c in chunks) { c.Draw(spriteBatch, TilesheetManager.TileSheets[level.CurrentLevel]); }
            foreach (Enemy e in enemies) { if (e.OnGround) { if (!e.Rotated) e.DrawSprite(spriteBatch, spritesheet); else e.DrawSprite(spriteBatch, spritesheet, e.RoateOnRad); } }
            foreach (Player p in player) { if(!p.Flash) p.DrawSprite(spriteBatch, spritesheet); }
            foreach (Enemy e in enemies) { if (!e.OnGround) { if (!e.Rotated) e.DrawSprite(spriteBatch, spritesheet); else e.DrawSprite(spriteBatch, spritesheet, e.RoateOnRad); } }
            foreach (Particle p in particles) { p.DrawSprite(spriteBatch, spritesheet); }
            foreach (Explosion e in explosions) { e.DrawSprite(spriteBatch, spritesheet);  }
            foreach (Projectile p in projectiles) { p.DrawSprite(spriteBatch, spritesheet); }
            foreach (PowerUp p in powerUps) { p.Draw(spriteBatch, spritesheet); }
            UpdateScreenFlash();
            ui.Draw(spriteBatch, spritesheet, font);
            foreach (TextEffect te in textEffects) { te.Draw(spriteBatch, font); }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
