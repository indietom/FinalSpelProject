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
    class GameObject
    {
        public Vector2 Pos { get; set; }

        public Vector2 GetCenter { get { return new Vector2(Pos.X + Width / 2, Pos.Y + Height / 2); } }

        public Vector2 Vel { get { return new Vector2(VelX, VelY); } }

        public Rectangle HitBox {get; set;}

        public Rectangle FullHitBox { get { return new Rectangle((int)Pos.X, (int)Pos.Y, Width, Height); } }
        public Rectangle FullHitBoxMiddle { get { return new Rectangle((int)Pos.X-Width/2, (int)Pos.Y-Height/2, Width, Height); } }

        public short CurrentFrame { get; set; }
        public short MaxFrame { get; set; }
        public short MinFrame { get; set; }

        public short AnimationCount { get; set; }
        public short MaxAnimationCount { get; set; }
        
        public bool AnimationActive { get; set; }
        public bool Rotated { get; set; }
        public bool RoateOnRad { get; set; }

        public short Imx { get; set; }
        public short Imy {get; set;}

        public short Width { get; set; }
        public short Height { get; set; }

        public float Rotation { get; set; }

        public float Angle { get; set; }
        public float Angle2 { get; set; }
        public float VelX { get; set; }
        public float VelY { get; set; }
        public float ScaleX { get; set; }
        public float ScaleY { get; set; }
        public float Speed { get; set; }

        public bool Destroy { get; set; }

        public void AngleMath(bool rad)
        {
            if(!rad) Angle2 = (Angle * (float)Math.PI / 180);
            if (rad) Angle2 = Angle;
            ScaleX = (float)Math.Cos(Angle2);
            ScaleY = (float)Math.Sin(Angle2);
            VelX = (ScaleX * Speed);
            VelY = (ScaleY * Speed);
        }

        public short Frame(short frame)
        {
            if (frame < 0) frame = 0;
            return (short)(frame * 32 + 1 + frame);
        }
        public short FrameX(short frame)
        {
            if(frame < 0) frame = 0;
            return (short)(frame * Width + 1 + frame);
        }
        public short FrameY(short frame)
        {
            if (frame < 0) frame = 0;
            return (short)(frame * Height + 1 + frame);
        }

        public float AimAt(Vector2 pos2)
        {
            return (float)Math.Atan2(pos2.Y - Pos.Y, pos2.X - Pos.X);
        }

        public float DistanceTo(Vector2 pos2)
        {
            return (float)Math.Sqrt((Pos.X - pos2.X) * (Pos.X - pos2.X) + (Pos.Y - pos2.Y) * (Pos.X - pos2.Y));
        }

        public float SimpleDistanceTo(Vector2 pos2)
        {
            return (float)Math.Abs(Pos.Y - pos2.Y);
        }

        public float Lerp(float s, float e, float t)
        {
            return s + t * (e - s);
        }

        public int Dubblan(int x)
        {
            return x * 2;
        }

        /*
         * Must set minFrame(the first frame in an animation) and maxFrame(lastFrame+1 of animation) 
         * Must set MaxAnimationCount is the amount of datacycels that has to pass for the animation to update 
         * imx = frameX(currentFrame) for horizntel animatios, Imy = frameY(currentFrame) for vertical
        */
        public void Animate()
        {
            if(AnimationCount >= MaxAnimationCount)
            {
                CurrentFrame += 1;
                AnimationCount = 0;
            }
            if (CurrentFrame >= MaxFrame) CurrentFrame = MinFrame;
        }

        public void SetSpriteCoords(short imx2, short imy2)
        {
            Imx = imx2;
            Imy = imy2;
        }
        public void SetSize(short w2, short h2)
        {
            Width = w2;
            Height = h2;
        }
        public void SetSize(short size)
        {
            Width = size;
            Height = size;
        }
        public void DrawSprite(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            Rectangle srcRect = new Rectangle((short)Imx, (short)Imy, (short)Width, (short)Height);
            spriteBatch.Draw(spritesheet, Pos, srcRect, Color.White);
        }
        public void DrawSprite(SpriteBatch spriteBatch, Texture2D spritesheet, Color color)
        {
            Rectangle srcRect = new Rectangle((short)Imx, (short)Imy, (short)Width, (short)Height);
            spriteBatch.Draw(spritesheet, Pos, srcRect, color);
        }
        public void DrawSprite(SpriteBatch spriteBatch, Texture2D spritesheet, bool rad)
        {
            Rectangle srcRect = new Rectangle((short)Imx, (short)Imy, (short)Width, (short)Height);
            if (rad) spriteBatch.Draw(spritesheet, Pos, srcRect, Color.White, Rotation, new Vector2(Width / 2, Height / 2), 1.0f, SpriteEffects.None, 1.0f);
            if (!rad) spriteBatch.Draw(spritesheet, Pos, srcRect, Color.White, (Rotation*((float)Math.PI/180)), new Vector2(Width / 2, Height / 2), 1.0f, SpriteEffects.None, 1.0f);
        }
    }
}
