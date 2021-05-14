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
    public class Joueur : GameObject
    {
        

        KeyboardState ks;
        public Joueur()
        {
            position = new Vector2(640, 360);
        }

        public Joueur(Vector2 newPostion)
        {
            position = newPostion;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Load(ContentManager content)
        {
            image = content.Load<Texture2D>("Test");
            base.Load(content);
        }

        public override void Update(List<GameObject> objects)
        {
            Input();
            base.Update(objects);
        }


        private void Input()
        {
            ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Right))
            {
                position.X += 5;
            }
            else if (ks.IsKeyDown(Keys.Left))
            {
                position.X -= 5;
            }

            if (ks.IsKeyDown(Keys.Down))
            {
                position.Y += 5;
            }
            else if (ks.IsKeyDown(Keys.Up))
            {
                position.Y -= 5;
            }

        }


         

    }
}
