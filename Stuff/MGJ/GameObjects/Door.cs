using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MGJ.GameObjects
{
    class Door : Sprite
    {
        public bool open = false;
        public String Tag;
         public Rectangle myRect;
        public Door()
        {

        }

        protected override void UpdateActive(GameTime gt)
        {

            base.UpdateActive(gt);
        }

        public void Open()
        {
            this.open = !open;
        }
    }
}
