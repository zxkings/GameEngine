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
    public class Joueur : Character
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

        public override void Update(List<GameObject> objects, Map map)
        {
            Input();
            base.Update(objects , map);
        }


        private void Input()
        {
            ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Right))
            {
                MoveRight();
            }
            else if (ks.IsKeyDown(Keys.Left))
            {
                Moveleft();
            }

            if (ks.IsKeyDown(Keys.Down))
            {
                MoveDown() ;
            }
            else if (ks.IsKeyDown(Keys.Up))
            {
                MoveUp() ;
            }
            Console.WriteLine("X = {0} , y = {1}", velocity.X, velocity.Y);
        }


         

    }
}
