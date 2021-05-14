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
    public class GameObject
    {
        //position , Color , sprite , scale , rotation , layerDepth , active , center , collision , sfx

        protected Texture2D image;
        public Vector2 position;
        public Color Color = Color.White ;
        public float rotation = 0f, scale = 1f ,layerDepth = 0.5f;
        public bool active = true ;
        protected Vector2 centre;

        public GameObject()
        {

        }

        public virtual void Initialize()
        {

        }

        public virtual void Load(ContentManager content)
        {
            CalculateCenter();
        }


        public virtual void Update(List<GameObject> objects)
        {

        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(image != null && active == true)
            {
                spriteBatch.Draw(image, position, null, Color, rotation, centre, scale, SpriteEffects.None, layerDepth);
            }

        }

        private void CalculateCenter()
        {
            if (image == null) return;

            centre.X = image.Width / 2;
            centre.Y = image.Height / 2;
        }


    }
}
