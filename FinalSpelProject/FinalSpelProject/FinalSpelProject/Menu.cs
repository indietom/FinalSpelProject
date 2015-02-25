using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalSpelProject
{
    class Menu
    {      

        enum Gamestates
        {
            Start,
            Options,
            Levels
        }
        Gamestates gamestate;

        KeyboardState ks = Keyboard.GetState();
        KeyboardState prevks;        

        
        public void Update()
        {
            switch (gamestate)
            {
                case Gamestates.Start:
                    if (ks.IsKeyDown(Keys.Enter))
                    {

                    }
                break;

                case Gamestates.Options:
                if (ks.IsKeyDown(Keys.O))
                    Console.WriteLine("För att byta rörelse knapp tryck \n tryck ner orginal knappen och sedan \n en valfri knapp");
                    
                    {
                        if (ks.IsKeyDown(Keys.W))
                        {
                            Console.ReadLine();
                            
                        }
                        
                        if (ks.IsKeyDown(Keys.A))
                        {
                            Console.ReadLine();

                        }
                        
                        if (ks.IsKeyDown(Keys.S))
                        {
                            Console.ReadLine();

                        }
                        
                        if (ks.IsKeyDown(Keys.D))
                        {
                            Console.ReadLine();
                            
                        }                       
                        
                    }
                break;

                case Gamestates.Levels:
                    if(ks.IsKeyDown(Keys.L))
                    {
                        if (ks.IsKeyDown(Keys.D1))
                        {
                            
                        }
                        
                        if (ks.IsKeyDown(Keys.D2))
                        {
                                                        
                        }
                        
                        if (ks.IsKeyDown(Keys.D3))
                        {

                        }                        
                        
                    }
                break;
            }
            

            
         }
         
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            
            spriteBatch.End();
                
        }

    }
}
