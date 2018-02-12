using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MGJ.GameObjects
{
    class Switch : Sprite
    {
        List<Door> doorList;

        public Switch()
        {
            doorList = new List<Door>();
        }

        protected override void UpdateActive(GameTime gt)
        {

            base.UpdateActive(gt);
        }

        public void AddDoor(Door d)
        {
            doorList.Add(d);
        }

        public void ChangeDoors()
        {
            foreach(Door d in doorList)
            {
                d.Open();
            }
        }
    }
}
