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
        List<Chunk> chunks = new List<Chunk>();
        List<Particle> particles = new List<Particle>();

        protected override void Initialize()
        {
            player.Add(new Player());
            //enemies.Add(new Enemy(new Vector2(),13));
            //enemies.Add(new Enemy(new Vector2(100, 20),11));
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
                p.Input(projectiles);
                p.LivesUpdate();
            }

            foreach(Enemy e in enemies)
            {
                e.Update(player, projectiles);
                e.Collision(player, projectiles);
            }

            foreach(Projectile p in projectiles)
            {
                p.Update(particles);
            }

            foreach(Chunk c in chunks)
            {
                c.Update();
            }
            
            foreach(Particle p in particles)
            {
                p.Update();
            }

            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                particles.Add(new Particle(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 0,  0));
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
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            foreach (Chunk c in chunks) { c.Draw(spriteBatch, spritesheet); }
            foreach (Player p in player) { if(!p.Flash) p.DrawSprite(spriteBatch, spritesheet); }
            foreach (Enemy e in enemies) { e.DrawSprite(spriteBatch, spritesheet); }
            foreach (Particle p in particles) { p.DrawSprite(spriteBatch, spritesheet); }
            foreach (Projectile p in projectiles) { p.DrawSprite(spriteBatch, spritesheet); }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
