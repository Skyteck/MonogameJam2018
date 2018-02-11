using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MGJ.GameObjects
{
    class Pillar : Sprite
    {

        protected override void UpdateActive(GameTime gt)
        {
            float speed = 200f;
            this._Position.Y += speed * (float)gt.ElapsedGameTime.TotalSeconds;

            base.UpdateActive(gt);
        }
    }
}
