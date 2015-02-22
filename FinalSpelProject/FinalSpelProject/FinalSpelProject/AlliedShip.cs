using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class AlliedShip : GameObject
    {
        byte gunType;

        float maxDistance;

        public AlliedShip(Vector2 pos2)
        {
            Pos = pos2;
            SetSize(24);
            SetSpriteCoords(1, 66);
        }

        public void Update(List<Player> players, List<Projectile> projectiles)
        {

        }
    }
}
