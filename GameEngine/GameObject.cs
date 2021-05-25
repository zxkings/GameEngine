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
        protected Vector2 direction = new Vector2(1, 0);

        public bool collidable = true;
        protected int boundingBoxWidth, boundingBoxHeight;
        protected Vector2 boundingBoxOffset;
        Texture2D boundingBoxImage;
        const bool drawBoundingBoxes = true;

        public Vector2 startPosition = new Vector2(-1, -1);

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)(position.X + boundingBoxOffset.X), (int)(position.Y + boundingBoxOffset.Y), boundingBoxWidth, boundingBoxHeight);
            }
        }

        public GameObject()
        {

        }

        public virtual void Initialize()
        {
            if (startPosition == new Vector2(-1, -1))
                startPosition = position;
        }

        public virtual void SetToDefaultPosition()
        {
            startPosition = position;
        }

        public virtual void Load(ContentManager content)
        {
            boundingBoxImage = content.Load<Texture2D>("pixel");

            CalculateCenter();

            if(image != null)
            {
                boundingBoxWidth = image.Width;
                boundingBoxHeight = image.Height;
            }

        }


        public virtual void Update(List<GameObject> objects, Map map)
        {

        }


        public virtual bool CheckCollision(Rectangle input)
        {
            return BoundingBox.Intersects(input);
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (boundingBoxImage != null && drawBoundingBoxes == true && active == true)
                spriteBatch.Draw(boundingBoxImage, new Vector2(BoundingBox.X, BoundingBox.Y), BoundingBox, new Color(120, 120, 120, 120), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);

            if(image != null && active == true)
            {
                spriteBatch.Draw(image, position, null, Color, rotation, Vector2.Zero , scale, SpriteEffects.None, layerDepth);
            }

        }

        public virtual void BulletResponse()
        {

        }


        private void CalculateCenter()
        {
            if (image == null) return;

            centre.X = image.Width / 2;
            centre.Y = image.Height / 2;
        }


    }
}
