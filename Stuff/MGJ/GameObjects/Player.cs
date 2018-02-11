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

    }
}
