using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MGJ.GameObjects
{
    class Player : Sprite
    {
        public float speed = 100f;

        public Enums.SwitchColors currentColor = Enums.SwitchColors.kColorGreen;

        protected override void UpdateActive(GameTime gt)
        {
            ProcessInput(gt);
            base.UpdateActive(gt);
        }

        private void ProcessInput(GameTime gt)
        {
            float scaleSpeed = 0.1f;

            if (InputHelper.IsKeyDown(Keys.A))
            {
                this._Position.X -= (float)(speed * gt.ElapsedGameTime.TotalSeconds);
            }
            if (InputHelper.IsKeyDown(Keys.D))
            {
                this._Position.X += (float)(speed * gt.ElapsedGameTime.TotalSeconds);
            }
            if (InputHelper.IsKeyDown(Keys.W))
            {
                this._Position.Y -= (float)(speed * gt.ElapsedGameTime.TotalSeconds);
            }
            if (InputHelper.IsKeyDown(Keys.S))
            {
                this._Position.Y += (float)(speed * gt.ElapsedGameTime.TotalSeconds);
            }

            //if (InputHelper.IsKeyDown(Keys.R))
            //{
            //    this._Rotation += scaleSpeed;
            //}
            if (InputHelper.IsKeyDown(Keys.Left))
            {
                this._Scale.X -= 0.1f;
                if (this._Scale.X < 0.1f)
                {
                    this._Scale.X = scaleSpeed;
                }
            }
            else if (InputHelper.IsKeyDown(Keys.Right))
            {
                this._Scale.X += scaleSpeed;
            }
            //if (InputHelper.IsKeyDown(Keys.Up))
            //{
            //    this._Scale.Y -= scaleSpeed;
            //    if (this._Scale.Y < scaleSpeed)
            //    {
            //        this._Scale.Y = scaleSpeed;
            //    }
            //}
            //else if (InputHelper.IsKeyDown(Keys.Down))
            //{
            //    this._Scale.Y += scaleSpeed;
            //}

            if (InputHelper.IsKeyDown(Keys.E))
            {
                this._Scale = Vector2.One;
            }
        }

        public void ChangeSize(int i)
        {
            if(i > 0)
            {
                this._Scale.X += 0.25f;
                if(this._Scale.X >= 1.75f)
                {
                    this._Scale.X = 1.75f;
                }
                this._Scale.Y = this._Scale.X;
                if((int)this.currentColor < (int)Enums.SwitchColors.kColorViolet)
                {

                    this.ChangeColor(this.currentColor + 1);
                }

            }
            else if(i < 0)
            {
                this._Scale.X -= 0.25f;
                if (this._Scale.X <= 0.25f)
                {
                    this._Scale.X = 0.25f;
                }


                if ((int)this.currentColor > (int)Enums.SwitchColors.kColorRed)
                {
                    
                    this.ChangeColor(this.currentColor - 1);
                }

            }
            this._Scale.Y = this._Scale.X;
            


        }

        public void SetPlayerSize(int i)
        {
                this._Scale.X = 0.25f * i;
                this._Scale.Y = 0.25f * i;
                ChangeColor((Enums.SwitchColors)i);
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
