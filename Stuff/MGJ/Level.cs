using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MGJ
{
    class Level
    {
        public List<DoorInfo> DoorList;
        public List<SwitchInfo> SwitchList;
        public List<FoodInfo> FoodList;
        public List<WallInfo> WallList;
        public Vector2 SpawnPoint = Vector2.Zero;
        public Vector2 ExitPoint = Vector2.Zero;
        public string LevelName;

        public Level()
        {
            DoorList = new List<DoorInfo>();
            SwitchList = new List<SwitchInfo>();
            FoodList = new List<FoodInfo>();
            WallList = new List<WallInfo>();
        }

        public void SaveLevel(List<GameObjects.Door> dl,  List<GameObjects.Switch> sL, List<GameObjects.Food> fL, List<GameObjects.SpriteRectangle> wL, Vector2 sp, Vector2 ep)
        {
            foreach(GameObjects.Door d in dl)
            {
                DoorInfo di = new DoorInfo();
                di.pos = d.myRect.Location.ToVector2();
                di.color = d.currentColor;
                DoorList.Add(di);
            }

            foreach(GameObjects.Switch s in sL)
            {
                SwitchInfo si = new SwitchInfo();
                si.color = s.currentColor;
                si.pos = s._Position;
                SwitchList.Add(si);
            }

            foreach(GameObjects.Food f in fL)
            {
                FoodInfo fi = new FoodInfo();
                fi.goodFood = f.goodFood;
                fi.pos = f._Position;
                FoodList.Add(fi);
            }

            foreach(GameObjects.SpriteRectangle sr in wL)
            {
                WallInfo wi = new WallInfo();
                wi.rect = sr.myRect;
                wi.pos = sr.Position;
                WallList.Add(wi);
            }

            SpawnPoint = sp;
            ExitPoint = ep;
            //SwitchList.AddRange(sL);
            //FoodList.AddRange(fL);
            //WallList.AddRange(wL);
            //SpawnPoint = sp;
        }

    }

    class DoorInfo
    {
        public Vector2 pos;
        public Enums.SwitchColors color;
    }

    class SwitchInfo
    {
        public Vector2 pos;
        public Enums.SwitchColors color;
    }

    class FoodInfo
    {
        public Vector2 pos;
        public bool goodFood;
    }

    class WallInfo
    {
        public Rectangle rect;
        public Vector2 pos;
    }
}
