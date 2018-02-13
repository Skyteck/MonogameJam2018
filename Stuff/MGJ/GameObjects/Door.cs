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
        public Rectangle myRect;

        public Enums.SwitchColors currentColor = Enums.SwitchColors.kColorWhite;

        public void Open()
        {
            this.open = !open;
        }

        public void ChangeColor(Enums.SwitchColors color)
        {
            currentColor = color;
            switch (color)
            {
                case Enums.SwitchColors.kColorRed:
                    _MyColor = Color.Red;
                    break;
                case Enums.SwitchColors.kColorOrange:
                    _MyColor = Color.Orange;
                    break;
                case Enums.SwitchColors.kColorYellow:
                    _MyColor = Color.Yellow;
                    break;
                case Enums.SwitchColors.kColorGreen:
                    _MyColor = Color.Green;
                    break;
                case Enums.SwitchColors.kColorBlue:
                    _MyColor = Color.Blue;
                    break;
                case Enums.SwitchColors.kColorIndigo:
                    _MyColor = new Color(136, 96, 255, 255);
                    break;
                case Enums.SwitchColors.kColorViolet:
                    _MyColor = new Color(178, 96, 255, 255);
                    break;
                default:
                    _MyColor = Color.White;
                    break;
            }
        }
    }
}
