using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using MGJ.GameObjects;
using System.Collections.Generic;

namespace MGJ
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch; 

        MGJ.GameObjects.Player player;

        //Pillar lPillar;
        //Pillar rPillar;
        //Target lTarget;
        //Target rTarget;

        List<Food> foodList;
        List<Switch> SwitchList;
        List<Door> DoorList;
        Random ran;

        float foodTimer = 1.0f;

        Vector2 mouseClickPos = Vector2.Zero;

        Texture2D SelectTex;

        bool SelectMode = false;
        Switch SelectedSwitch;

        Rectangle SelectRect
        {
            get
            {
                Vector2 currentPos = InputHelper.MouseScreenPos;
                //one corner is always where click was
                //figure out which corner it is by checking where mouse currently is
                //if current mouse is left and up from click then top left of rect is current mouse pos. bottom right is clicked pos
                if (currentPos.X < mouseClickPos.X && currentPos.Y < mouseClickPos.Y)
                {
                    return new Rectangle((int)currentPos.X, (int)currentPos.Y, (int)(mouseClickPos.X - currentPos.X), (int)(mouseClickPos.Y - currentPos.Y));
                }
                //if current mouse is left and down from click top left is mouse click spot y with current mouse pos X
                else if (currentPos.X < mouseClickPos.X && currentPos.Y >= mouseClickPos.Y)
                {
                    return new Rectangle((int)currentPos.X, (int)mouseClickPos.Y, (int)(mouseClickPos.X - currentPos.X), (int)(currentPos.Y - mouseClickPos.Y));
                }
                //if current mouse is right and up then top left is clicked X and currentY                
                else if (currentPos.X > mouseClickPos.X && currentPos.Y < mouseClickPos.Y)
                {
                    return new Rectangle((int)mouseClickPos.X, (int)currentPos.Y, (int)(currentPos.X - mouseClickPos.X), (int)(mouseClickPos.Y - currentPos.Y));
                }
                else
                {

                    return new Rectangle((int)mouseClickPos.X, (int)mouseClickPos.Y, (int)(currentPos.X - mouseClickPos.X), (int)(currentPos.Y - mouseClickPos.Y));

                }
            }
        }

        List<SpriteRectangle> rectList;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            ran = new Random();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new GameObjects.Player();
            player.LoadContent(@"Art/Player", Content);
            player._Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            foodList = new List<Food>();
            rectList = new List<SpriteRectangle>();
            SwitchList = new List<Switch>();
            DoorList = new List<Door>();
            SelectTex = Content.Load<Texture2D>(@"Art/RectTex");

            


            //rPillar = new Pillar();
            //rPillar.LoadContent(@"Art/Pillar", Content);
            //rPillar._Position.X = GraphicsDevice.Viewport.Width;
            //rPillar._Position.Y = -100;
            //rPillar._Scale.X = 6.0f;

            //lPillar = new Pillar();
            //lPillar.LoadContent(@"Art/Pillar", Content);
            //lPillar._Position.X = 0;
            //lPillar._Position.Y = -100;
            //lPillar._Scale.X = 6.0f;

            //rTarget = new Target();
            //rTarget.LoadContent(@"Art/Target", Content);
            //rTarget._Position.X = rPillar._BoundingBox.Left;
            //rTarget._Position.Y = -100;

            //lTarget = new Target();
            //lTarget.LoadContent(@"Art/Target", Content);
            //lTarget._Position.X = lPillar._BoundingBox.Right;
            //lTarget._Position.Y = -100;


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            InputHelper.Update();
            // TODO: Add your update logic here

            Vector2 currentPlayerPos = player._Position;
            player.Update(gameTime);


            foreach(SpriteRectangle rect in rectList)
            {
                if(rect.myRect.Intersects(player._BoundingBox))
                {
                    player._Position = currentPlayerPos;
                }
            }

            foreach (Door rect in DoorList)
            {
                if (rect.myRect.Intersects(player._BoundingBox) && rect.open == false)
                {
                    player._Position = currentPlayerPos;
                }
            }

            if(InputHelper.IsKeyPressed(Keys.Space))
            {
                foreach(Switch s in SwitchList)
                {
                    if(s._BoundingBox.Contains(InputHelper.MouseScreenPos))
                    {
                        SelectMode = true;
                        SelectedSwitch = s;
                        break;
                    }
                }
            }

            if(SelectMode == true && InputHelper.IsKeyDown(Keys.Space))
            {
                foreach(Door d in DoorList)
                {
                    if(d.myRect.Contains(InputHelper.MouseScreenPos))
                    {
                        SelectedSwitch.AddDoor(d);
                        SelectMode = false;
                    }
                }
            }

            foreach(Switch s in SwitchList)
            {
                if(s._BoundingBox.Intersects(player._BoundingBox))
                {
                    s.ChangeDoors();
                }
            }

            if (InputHelper.IsKeyPressed(Keys.G))
            {
                CreateFood(true, InputHelper.MouseScreenPos);
                foodTimer = 1.0f;
            }

            if (InputHelper.IsKeyPressed(Keys.B))
            {
                CreateFood(false, InputHelper.MouseScreenPos);
                foodTimer = 1.0f;
            }

            foreach (Food f in foodList.FindAll(x=>x._CurrentState == Sprite.SpriteState.kStateActive))
            {

                if (player._BoundingBox.Intersects(f._BoundingBox))
                {
                    if(f.goodFood)
                    {

                        player._Scale.X += 0.05f;
                        player._Scale.Y += 0.05f;
                        //player.speed += 3f;
                    }
                    else
                    {

                        player._Scale.X -= 0.05f;
                        player._Scale.Y -= 0.05f;
                        //player.speed -= 3f;
                    }
                    f.Deactivate();

                }
            }
            if(InputHelper.LeftButtonClicked)
            {
                mouseClickPos = InputHelper.MouseScreenPos;
            }

            if(InputHelper.LeftButtonReleased && InputHelper.IsKeyDown(Keys.L))
            {
                Door d = new Door();
                d.myRect = SelectRect;
                d.LoadContent(@"Art/Door", Content);
                DoorList.Add(d);
                mouseClickPos = Vector2.Zero;
            }
            else if( InputHelper.LeftButtonReleased)
            {
                SpriteRectangle r = new SpriteRectangle();
                r.myRect = SelectRect;
                rectList.Add(r);
                mouseClickPos = Vector2.Zero;

            }
            
            if(InputHelper.RightButtonClicked && InputHelper.IsKeyDown(Keys.LeftControl))
            {
                foreach(SpriteRectangle rect in rectList)
                {
                    if(rect.myRect.Contains(InputHelper.MouseScreenPos))
                    {
                        rect.Active = false;
                    }
                }
            }

            rectList.RemoveAll(x => x.Active == false);


            if (InputHelper.IsKeyPressed(Keys.H))
            {
                CreateSwitch();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here

            foreach(Food f in foodList)
            {
                f.Draw(spriteBatch);
            }


            if(mouseClickPos!= Vector2.Zero)
            {
                DrawSelectRect(spriteBatch);
            }

            foreach(SpriteRectangle rect in rectList)
            {
                DrawRectangle(spriteBatch, rect.myRect, SelectTex);
            }

            foreach(Switch s in SwitchList)
            {

                s.Draw(spriteBatch);
            }

            foreach(Door d in DoorList.FindAll(x=>x.open == false))
            {
                DrawRectangle(spriteBatch, d.myRect, d._Texture);
                d.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            base.Draw(gameTime);
            spriteBatch.End();
        }

        private void DrawRectangle(SpriteBatch sb, Rectangle rect, Texture2D tex)
        {
            int border = 3;

            //Left Line
            sb.Draw(tex, new Rectangle(rect.X, rect.Y, rect.Width, rect.Height), Color.White);
            //sb.Draw(SelectTex, new Rectangle(rect.X, rect.Y, 1, rect.Height), Color.White);

            //top line
            sb.Draw(tex, new Rectangle(rect.X, rect.Y, rect.Width + border, border), Color.White);

            //right line
            sb.Draw(tex, new Rectangle(rect.X + rect.Width, rect.Y, border, rect.Height + border), Color.White);

            //bottom line
            sb.Draw(tex, new Rectangle(rect.X, rect.Y + rect.Height, rect.Width + border, border), Color.White);
        }

        private void DrawSelectRect(SpriteBatch sb)
        {
            int border = 3;
            int width = (int)(InputHelper.MouseScreenPos.X - mouseClickPos.X);
            int height = (int)(InputHelper.MouseScreenPos.Y - mouseClickPos.Y);

            Rectangle rect = new Rectangle((int)mouseClickPos.X, (int)mouseClickPos.Y, width, height);



            sb.Draw(SelectTex, new Rectangle(rect.X, rect.Y, border, rect.Height + border), Color.White);
            sb.Draw(SelectTex, new Rectangle(rect.X, rect.Y, rect.Width + border, border), Color.White);
            sb.Draw(SelectTex, new Rectangle(rect.X + rect.Width, rect.Y, border, rect.Height + border), Color.White);
            sb.Draw(SelectTex, new Rectangle(rect.X, rect.Y + rect.Height, rect.Width + border, border), Color.White);
        }

        private void CreateFood(bool good)
        {
            Vector2 newPos = new Vector2(ran.Next(20, 780), ran.Next(20, 460));
            CreateFood(good, newPos);
        }

        private void CreateFood(bool good, Vector2 newPos)
        {
            Food f;
            f = foodList.Find(x => x._CurrentState == Sprite.SpriteState.kStateInActive);
            //Vector2 newPos = new Vector2(ran.Next(20, 780), ran.Next(20, 460));

            if (f != null)
            {
                f.Activate(newPos);
                f.goodFood = good;
            }
            else
            {
                f = new Food();
                f.LoadContent(@"Art/Food", Content);
                f.Activate(newPos);
                f.goodFood = good;
                foodList.Add(f);
            }

        }

        private void CreateSwitch()
        {
            Switch f;
            f = SwitchList.Find(x => x._CurrentState == Sprite.SpriteState.kStateInActive);
            //Vector2 newPos = new Vector2(ran.Next(20, 780), ran.Next(20, 460));
            Vector2 newPos = InputHelper.MouseScreenPos;
            if (f != null)
            {
                f.Activate(newPos);
            }
            else
            {
                f = new Switch();
                f.LoadContent(@"Art/Switch", Content);
                f.Activate(newPos);
                SwitchList.Add(f);
            }

        }
    }
}
