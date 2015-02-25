using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class AlliedShip : GameObject
    {
        int lifeTime;
        int maxLifeTime;

        byte gunType;
        byte fireRate;

        float maxDistance;

        public AlliedShip(Vector2 pos2)
        {
            Random random = new Random();
            Pos = pos2;
            SetSize(24);
            SetSpriteCoords(1, 66);
            maxDistance = random.Next(64+24, 64+(24*4));
            maxLifeTime = 128 * 10;
            Speed = 0.02f;
        }

        public void Update(List<Player> players, List<Projectile> projectiles, List<Explosion> explosions)
        {
            // TODO: Should these ships emitt a sound effect when shooting? Might get too messy
            Random random = new Random();

            lifeTime += 1;

            foreach(Player p in players)
            {
                gunType = p.GetGunType();
                fireRate = p.GetFireRate();
                lifeTime = (p.Dead) ? maxLifeTime : lifeTime;
                if(gunType == 4 && lifeTime <= maxLifeTime)
                {
                    for (int i = 0; i < p.GetCurrentLaserHeigt(); i++)
                    {
                        projectiles.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 8, Pos.Y - i), 0, 0, 2, 0, false, 1));
                    }
                }
                if(DistanceTo(p.Pos) > maxDistance && !p.Dead)
                {
                    Pos = new Vector2(Lerp(Pos.X, p.GetCenter.X, Speed), Lerp(Pos.Y, p.GetCenter.Y, Speed));
                }
                if(DistanceTo(p.Pos) < maxDistance-5 && !p.Dead)
                {
                    Pos = new Vector2(Lerp(Pos.X, p.GetCenter.X, -Speed), Lerp(Pos.Y, p.GetCenter.Y, -Speed));
                }
            }

            if (fireRate == 1)
            {
                switch (gunType)
                {
                    case 0:
                        projectiles.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 3, Pos.Y + (Height / 2) - 3), -90, 9, 0, 0, false));
                        break;
                    case 2:
                        for (int i = 0; i < 3; i++)
                            projectiles.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 3, Pos.Y + (Height / 2) - 3), -80 - i * 10, 9, 0, 0, false));
                        break;
                    case 3:
                        projectiles.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 3, Pos.Y + (Height / 2) - 3), -90, -2, 1, 1, false));
                        break;
                }
            }
            if (gunType == 1 && fireRate >= 1)
            {
                if (fireRate == 8 || fireRate == 16 || fireRate == 24)
                {
                    projectiles.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 3, Pos.Y + (Height / 2) - 3), -90 + random.Next(-5 - (fireRate / 5), 5 + (fireRate / 5)), 9, 0, 0, false));
                }
            }

            if(lifeTime >= maxLifeTime)
            {
                explosions.Add(new Explosion(Pos+new Vector2(random.Next(17), random.Next(17)), 16, false));
                VelY += 0.2f;
                Pos += Vel;
                Destroy = (Pos.Y > Globals.screenH) ? true : Destroy;
            }
        }
    }
}
