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
        byte pauseDelay;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Globals.Load();
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 640;
            Content.RootDirectory = "Content";
        }

        Ui ui = new Ui();
        Menu menu = new Menu();
        StartScreen startScreen = new StartScreen();

        List<Player> player = new List<Player>();
        List<Enemy> enemies = new List<Enemy>();
        List<Boss> bosses = new List<Boss>();
        List<Projectile> projectiles = new List<Projectile>();
        List<Chunk> chunks = new List<Chunk>();
        List<Particle> particles = new List<Particle>();
        List<Explosion> explosions = new List<Explosion>();
        List<PowerUp> powerUps = new List<PowerUp>();
        List<TextEffect> textEffects = new List<TextEffect>();
        List<Gib> gibs = new List<Gib>();
        List<Tile> tiles = new List<Tile>();
        List<AlliedShip> alliedShips = new List<AlliedShip>();
        List<TextBox> textBoxes = new List<TextBox>();
        List<Loot> loots = new List<Loot>();

        Level level;
        LevelTransitionScreen LevelTransitionScreen = new LevelTransitionScreen();

        SpawnManager spawnManager = new SpawnManager();
        LevelManager levelManager = new LevelManager();
        FileManager fileManager = new FileManager();
        ProceduralGenerationManager proceduralGenerationManager = new ProceduralGenerationManager();

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        protected override void Initialize()
        {
            fileManager.LoadConfig();
            player.Add(new Player());
            //bosses.Add(new Boss(new Vector2(GraphicsDevice.Viewport.Width / 2 - 32, 0), 4));
            Globals.screenH = graphics.PreferredBackBufferHeight;
            Globals.screenW = graphics.PreferredBackBufferWidth;
            //chunks.Add(new Chunk(new Vector2(0, 0), @"map1"));
            fileManager.LoadPlayer("save.sav", player);
            level = new Level(chunks, LevelManager.currentLevel, Globals.LevelHeight());
            base.Initialize();
        }

        Texture2D spritesheet;
        Texture2D finalBossSheet;
        SpriteFont font;
        
        protected override void LoadContent()
        {
            spritesheet = Content.Load<Texture2D>("spritesheet");
            finalBossSheet = Content.Load<Texture2D>("finalBossSheet");
            TilesheetManager.Load(Content);
            SoundManager.Load(Content);
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
                // nä
                flashScreenCount = (flashScreenCount >= 32) ? flashScreenCount = 0 : flashScreenCount = (byte)(flashScreenCount + 1);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            Random random = new Random();

            switch (Globals.gameState)
            {
                case GameStates.Credits:
                    // Logic in game1.cs because I can't bother with classes at this point, thursday 20:58
                    Globals.startedGame = false;
                    bosses.Clear();
                    LevelManager.currentLevel = 0;
                    if(keyboard.IsKeyDown(Keys.X) && prevKeyboard.IsKeyUp(Keys.X) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
                    {
                        LevelManager.currentLevel = 0;
                        Globals.gameState = GameStates.Menu;
                        menu.SetDelay(1);
                        player[0] = new Player();
                    }
                    levelManager.StartLevel(0, chunks, enemies, projectiles, player, ref level, tiles);
                    break;
                case GameStates.Menu:
                    menu.Update(levelManager, chunks, enemies, projectiles, player, level, tiles);
                    break;
                case GameStates.LevelTransition:
                    LevelTransitionScreen.Update(ref level, bosses);
                    break;
                case GameStates.Game:
                    if(pauseDelay >= 10 && keyboard.IsKeyDown(Keys.Escape) && prevKeyboard.IsKeyUp(Keys.Escape))
                    {
                        Globals.gameState = GameStates.Menu;
                        pauseDelay = 0;
                    }

                    if (pauseDelay < 10) pauseDelay += 1;

                    ui.Update(player, bosses);

                    level.Update(tiles, chunks, proceduralGenerationManager, spawnManager, enemies, powerUps, bosses, levelManager);
                    levelManager.Update(chunks, enemies, projectiles, player, bosses, level, textEffects, tiles);

                    foreach (Gib g in gibs)
                    {
                        g.Update();
                    }

                    foreach (AlliedShip a in alliedShips)
                    {
                        a.Update(player, projectiles, explosions, levelManager);
                    }

                    foreach (Enemy e in enemies)
                    {
                        e.Update(player, projectiles, explosions, powerUps, gibs, levelManager);
                    }

                    foreach (Player p in player)
                    {
                        p.Update(projectiles, enemies, explosions, textEffects);
                    }

                    foreach (Projectile p in projectiles)
                    {
                        p.Update(particles, explosions, player[0]);
                    }

                    foreach (Chunk c in chunks)
                    {
                        c.Update(enemies);
                    }

                    foreach (Boss b in bosses)
                    {
                        b.Update(player, projectiles, explosions, enemies);
                    }
                    
                    foreach (Particle p in particles)
                    {
                        p.Update(projectiles);
                    }

                    foreach (Explosion e in explosions)
                    {
                        e.Update();
                    }

                    foreach (PowerUp p in powerUps)
                    {
                        p.Update(player, textEffects, alliedShips);
                    }

                    foreach (TextEffect te in textEffects)
                    {
                        te.Update();
                    }

                    foreach (Tile t in tiles)
                    {
                        t.Update();
                    }

                    foreach (TextBox t in textBoxes)
                    {
                        t.Update();
                    }

                    foreach (Loot l in loots)
                    {
                        l.Update(player, textEffects);
                    }

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        //levelManager.StartLevel(1, chunks, enemies, projectiles, player, ref level, tiles);
                    }

                    if (Mouse.GetState().RightButton == ButtonState.Pressed)
                    {
                        Globals.worldSpeed = 64;
                        // fileManager.LoadPlayer("save.sav", player);
                    }
                    else
                    {
                        Globals.worldSpeed = 1;
                    }
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        //fileManager.SavePlayer("save.sav", player);
                        //textEffects.Add(new TextEffect(new Vector2(0, 0), "", 1, Color.White, new Vector2(Globals.screenW / 2 - 200, Globals.screenH / 2), 0.05f, 64*3, 4, 1, "LEVEL COMPLETED"));
                        //enemies.Add(new Enemy(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 12, random));
                        //levelManager.ResetLevel(chunks, enemies, projectiles, player, level);
                        //loots.Add(new Loot(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 2, 0));
                        //if (textBoxes.Count == 0) textBoxes.Add(new TextBox(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), "The textbox works.\nThis is great news I think", Color.White, 4));
                        if (alliedShips.Count == 0) alliedShips.Add(new AlliedShip(new Vector2(Mouse.GetState().X, Mouse.GetState().Y)));
                        //proceduralGenerationManager.SpawnTree(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 1, tiles);
                        //tiles.Add(new Tile(new Vector2(Mouse.GetState().X/16, Mouse.GetState().Y/16), 2));
                        //tiles.Add(new Tile(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 1));
                        //if(true) explosions.Add(new Explosion(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 16, false));
                        powerUps.Add(new PowerUp(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 4, 0, false));
                        //projectiles.Add(new Projectile(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), -90+random.Next(-8, 9), 8, 10, 0, false, true));
                        //particles.Add(new Particle(new Vector2(800 / 2, 640 / 2), new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 0.05f, 1, 1, Color.White));
                        //gibs.Add(new Gib(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), (short)random.Next(5), 140, random.Next(6, 12), random.Next(360)));   
                    }
                    break;
            }
            for (int i = 0; i < loots.Count(); i++)
            {
                if (loots[i].Destroy) loots.RemoveAt(i);
            }
            for (int i = 0; i < enemies.Count(); i++)
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
            for (int i = 0; i < tiles.Count; i++)
            {
                if (tiles[i].Destroy) tiles.RemoveAt(i);
            }
            for (int i = 0; i < alliedShips.Count; i++)
            {
                if (alliedShips[i].Destroy) alliedShips.RemoveAt(i);
            }
            for (int i = 0; i < bosses.Count; i++)
            {
                if (bosses[i].Destroy) bosses.RemoveAt(i);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            switch (Globals.gameState)
            {
                case GameStates.Credits:
                    spriteBatch.DrawString(font, "THANKS FOR PLAYING!", new Vector2(300, 10), Color.Gold);
                    spriteBatch.DrawString(font, "TOM - PROGRAMMING", new Vector2(303, 10+30*4), Color.LightBlue);
                    spriteBatch.DrawString(font, "HANNES - PROGRAMMING", new Vector2(303, 10 + 30 * 5), Color.LightBlue);
                    spriteBatch.DrawString(font, "EMIL - PROGRAMMING", new Vector2(303, 10 + 30 * 6), Color.LightBlue);

                    spriteBatch.DrawString(font, "ELINA - GRAPHICS", new Vector2(303, 10 + 30 * 8), Color.LightSalmon);
                    spriteBatch.DrawString(font, "TYRA - GRAPHICS", new Vector2(303, 10 + 30 * 9), Color.LightSalmon);

                    if(!GamePad.GetState(PlayerIndex.One).IsConnected) spriteBatch.DrawString(font, "PRESS X TO GO BACK TO THE MENU", new Vector2(303, 10 + 30 * 11), Color.LightGreen);
                    else spriteBatch.DrawString(font, "PRESS A TO GO BACK TO THE MENU", new Vector2(303, 10 + 30 * 11), Color.LightGreen);
                    break;
                case GameStates.StartScreen:
                    startScreen.Draw(spriteBatch, spritesheet, font);
                    break;
                case GameStates.Menu:
                    menu.Draw(spriteBatch, font);
                    break;
                case GameStates.LevelTransition:
                    LevelTransitionScreen.Draw(spriteBatch, font, spritesheet);
                    break;
                case GameStates.Game:
                    foreach (Chunk c in chunks) { c.Draw(spriteBatch, TilesheetManager.TileSheets[LevelManager.currentLevel]); }
                    foreach (Tile t in tiles)
                    {
                        if (t.GetType() == 1)
                            t.DrawSprite(spriteBatch, TilesheetManager.TileSheets[LevelManager.currentLevel]);
                    }
                    foreach (Tile t in tiles)
                    {
                        if (t.GetType() != 1)
                            t.DrawSprite(spriteBatch, TilesheetManager.TileSheets[LevelManager.currentLevel]);
                    }

                    foreach (Enemy e in enemies) { if (e.OnGround) { if (!e.Rotated) e.DrawSprite(spriteBatch, spritesheet); else e.DrawSprite(spriteBatch, spritesheet, e.RoateOnRad); } }
                    foreach (Gib g in gibs) { g.DrawSprite(spriteBatch, spritesheet, false); }
                    foreach (Particle p in particles) { if (!p.Rotated) p.DrawSprite(spriteBatch, spritesheet); else p.DrawSprite(spriteBatch, spritesheet, p.RoateOnRad); }
                    foreach (Projectile p in projectiles) { if (p.Rotated) p.DrawSprite(spriteBatch, spritesheet, p.RoateOnRad); else p.DrawSprite(spriteBatch, spritesheet); }
                    foreach (Player p in player) { if (!p.Flash) p.Draw(spriteBatch, spritesheet); }
                    foreach (AlliedShip a in alliedShips) { a.DrawSprite(spriteBatch, spritesheet); }
                    foreach (Enemy e in enemies) { if (!e.OnGround) { if (!e.Rotated) e.DrawSprite(spriteBatch, spritesheet); else e.DrawSprite(spriteBatch, spritesheet, e.RoateOnRad); } }
                    foreach (Boss b in bosses)
                    {
                        if(b.GetType() != 4) b.Draw(spriteBatch, spritesheet);
                        else b.Draw(spriteBatch, finalBossSheet);
                    }
                    foreach (Explosion e in explosions) { e.DrawSprite(spriteBatch, spritesheet); }
                    foreach (PowerUp p in powerUps) { p.Draw(spriteBatch, spritesheet); }
                    foreach (Loot l in loots) { l.DrawSprite(spriteBatch, spritesheet); }
                    UpdateScreenFlash();
                    foreach (TextEffect te in textEffects) { te.Draw(spriteBatch, font); }
                    ui.Draw(spriteBatch, spritesheet, font);
                    foreach (TextBox t in textBoxes) { t.Draw(spriteBatch, spritesheet, font); }
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
