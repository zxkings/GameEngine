﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameEngine
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public List<GameObject> objects = new List<GameObject>() ;

        float s;
        int x, y;
        KeyboardState ks;
        Texture2D Animation;        


        public Game1() //This is the constructor, this function is called whenever the game class is created.
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// This function is automatically called when the game launches to initialize any non-graphic variables.
        /// </summary>
        protected override void Initialize()
        {
            x = 0;
            y = 0;
            float s = 0f;
            base.Initialize();
        }

        /// <summary>
        /// Automatically called when your game launches to load any game assets (graphics, audio etc.)
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            LoadLevel();
            Animation = Content.Load<Texture2D>("New Piskel");
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// Called each frame to update the game. Games usually runs 60 frames per second.
        /// Each frame the Update function will run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            //Update the things FNA handles for us underneath the hood:

            s += (float)gameTime.ElapsedGameTime.TotalMilliseconds;


            if(s > 10f)
            {
                if (x == 320  && y == 320 * 2)
                {
                    x = 0;
                    y = 0;
                }

                else if (x == 320 * 2)
                {
                    x = 0;
                    y += 320;
                }
                else
                {
                    x += 320  ;
                }
                Console.WriteLine("X = {0}  Y = {1}", x, y);
                s = 0;
            }





            ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Up))
            {
                y-=5;
                Console.WriteLine("X = {0}  Y = {1}", x, y);
            }
            if (ks.IsKeyDown(Keys.Down))
            {
                y+=5;
                Console.WriteLine("X = {0}  Y = {1}", x, y);
            }
            if (ks.IsKeyDown(Keys.Left))
            {
                x-=5;
                Console.WriteLine("X = {0}  Y = {1}", x, y);
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                x+=5;
                Console.WriteLine("X = {0}  Y = {1}", x, y);
            }

            UpdateObjects();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game is ready to draw to the screen, it's also called each frame.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            //This will clear what's on the screen each frame, if we don't clear the screen will look like a mess:
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            DrawObjects();
            spriteBatch.Draw(Animation, new Rectangle(0,0 ,360 , 360) , new Rectangle(x,y,360,360) , Color.White);
            spriteBatch.End();
            //Draw the things FNA handles for us underneath the hood:
            base.Draw(gameTime);
        }

        public void LoadLevel()
        {
            objects.Add(new Joueur());
            loadObjects();
        }



        public void loadObjects()
        {
            for(int i = 0; i < objects.Count; i++)
            {
                objects[i].Initialize();
                objects[i].Load(Content);
            }
        }

        public void UpdateObjects()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Update(objects);
            }
        }

        public void DrawObjects()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Draw(spriteBatch);
            }
        }

    }
}