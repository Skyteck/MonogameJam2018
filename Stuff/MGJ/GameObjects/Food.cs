using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MGJ.GameObjects
{
    class Food : Sprite
    {
        public bool goodFood = true;

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(goodFood)
            {
                _MyColor = Color.White;
            }
            else
            {
                _MyColor = Color.Red;
            }
            base.Draw(spriteBatch);
        }
    }
}
