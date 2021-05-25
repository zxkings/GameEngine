using System;
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
        public Map map = new Map() ;

        GameHUD gameHUD = new GameHUD();
        Editor editor;

        float s;
        int x, y;
        KeyboardState ks;      


        public Game1() //This is the constructor, this function is called whenever the game class is created.
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Resolution.Init(ref graphics);
            Resolution.SetResolution(1280, 720, false);
            Resolution.ResetViewport();


        }

        /// <summary>
        /// This function is automatically called when the game launches to initialize any non-graphic variables.
        /// </summary>
        protected override void Initialize()
        {
            x = 0;
            y = 0;
            float s = 0f;

#if DEBUG   
            editor = new Editor(this);
            editor.Show();  
#endif

            base.Initialize();
            Camera.Initialize();
        }

        /// <summary>
        /// Automatically called when your game launches to load any game assets (graphics, audio etc.)
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

#if DEBUG
            editor.LoadTextures(Content);
#endif

            map.Load(Content);
            gameHUD.Load(Content);
            LoadLevel("Level1.LVL");

        }

        /// <summary>
        /// Called each frame to update the game. Games usually runs 60 frames per second.
        /// Each frame the Update function will run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            //Update the things FNA handles for us underneath the hood:

           
            UpdateObjects();
            map.Update(objects);
            UpdateCamera();

#if DEBUG
            editor.Update(objects, map);
#endif

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game is ready to draw to the screen, it's also called each frame.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            //This will clear what's on the screen each frame, if we don't clear the screen will look like a mess:
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Resolution.BeginDraw();

            spriteBatch.Begin(SpriteSortMode.BackToFront , BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Camera.GetTransformMatrix());
#if DEBUG
            editor.Draw(spriteBatch);
#endif
            DrawObjects();
            map.DrawWalls(spriteBatch);
            spriteBatch.End();

            gameHUD.Draw(spriteBatch);

            //Draw the things FNA handles for us underneath the hood:
            base.Draw(gameTime);
        }

        public void LoadLevel(string fileName)
        {
            Global.levelName = fileName;

            LevelData levelData = XmlHelper.Load("Content\\Levels\\" + fileName);

            map.walls = levelData.walls;
            map.decor = levelData.decor;
            objects = levelData.objects;

            map.LoadMap(Content);


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
                objects[i].Update(objects, map);
            }
        }

        public void DrawObjects()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Draw(spriteBatch);
            }
            for (int i = 0; i <map.decor.Count; i++)
            {
                map.decor[i].Draw(spriteBatch);
            }
        }

        private void UpdateCamera()
        {
            if (objects.Count == 0)
                return;

            Camera.Update(objects[0].position);
        }


    }
}