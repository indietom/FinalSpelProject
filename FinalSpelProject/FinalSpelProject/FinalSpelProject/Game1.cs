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

        public static bool flashScreen;
        public static byte flashScreenCount;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Globals.Load();
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 640;
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
        List<Gib> gibs = new List<Gib>();

        Level level;

        protected override void Initialize()
        {
            player.Add(new Player());
            Globals.screenH = graphics.PreferredBackBufferHeight;
            Globals.screenW = graphics.PreferredBackBufferWidth;
            //chunks.Add(new Chunk(new Vector2(0, 0), @"map1"));
            level = new Level(chunks, 0, 14);
            Globals.worldSpeed = 1.5f;
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
                spriteBatch.Draw(spritesheet, new Rectangle(0, 0, Globals.screenW, Globals.screenH), new Rectangle(1, 1496, 64, 64), Color.White);
                // lite kod > läslighet amrite
                flashScreenCount = (flashScreenCount >= 32) ? flashScreenCount = 0 : flashScreenCount = (byte)(flashScreenCount + 1);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            Random random = new Random();

            ui.Update(player);

            foreach(Gib g in gibs)
            {
                g.Update();
            }

            foreach(Enemy e in enemies)
            {
                e.Update(player, projectiles, explosions, powerUps, gibs);
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
                //if(explosions.Count == 0) explosions.Add(new Explosion(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 16, false));
                //powerUps.Add(new PowerUp(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 1, 0, false));
                gibs.Add(new Gib(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), (short)random.Next(5), 140, random.Next(6, 12), random.Next(360)));   
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
            for (int i = 0; i < gibs.Count; i++)
            {
                if (gibs[i].Destroy) gibs.RemoveAt(i);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            foreach (Chunk c in chunks) { c.Draw(spriteBatch, TilesheetManager.TileSheets[level.CurrentLevel]); }
            foreach (Enemy e in enemies) { if (e.OnGround) { if (!e.Rotated) e.DrawSprite(spriteBatch, spritesheet); else e.DrawSprite(spriteBatch, spritesheet, e.RoateOnRad); } }
            foreach (Gib g in gibs) { g.DrawSprite(spriteBatch, spritesheet, false); }
            foreach (Particle p in particles) { p.DrawSprite(spriteBatch, spritesheet); }
            foreach (Projectile p in projectiles) { p.DrawSprite(spriteBatch, spritesheet); }
            foreach (Player p in player) { if(!p.Flash) p.Draw(spriteBatch, spritesheet); }
            foreach (Enemy e in enemies) { if (!e.OnGround) { if (!e.Rotated) e.DrawSprite(spriteBatch, spritesheet); else e.DrawSprite(spriteBatch, spritesheet, e.RoateOnRad); } }
            foreach (Explosion e in explosions) { e.DrawSprite(spriteBatch, spritesheet);  }
            foreach (PowerUp p in powerUps) { p.Draw(spriteBatch, spritesheet); }
            UpdateScreenFlash();
            ui.Draw(spriteBatch, spritesheet, font);
            foreach (TextEffect te in textEffects) { te.Draw(spriteBatch, font); }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
