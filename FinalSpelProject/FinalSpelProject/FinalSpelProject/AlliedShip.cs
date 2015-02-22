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
            maxDistance = random.Next(74, 128 + 32);
            maxLifeTime = 128 * 10;
        }

        public void Update(List<Player> players, List<Projectile> projectiles)
        {
            // TODO: Should these ships emitt a sound effect when shooting? Might get to messy
            Random random = new Random();

            foreach(Player p in players)
            {
                gunType = p.GetGunType();
                fireRate = p.GetFireRate();
                if(p.DistanceTo(Pos) > maxDistance)
                {
                    Pos = new Vector2(Lerp(Pos.X, p.Pos.X, 0.06f), Lerp(Pos.Y, p.Pos.Y, 0.06f));
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

            Destroy = (lifeTime >= maxLifeTime) ? true : Destroy;
        }
    }
}
