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
    public class Player : FireCharacter
    {
        public static int score;

        KeyboardState ks;
        public Player()
        {
            position = new Vector2(640, 360);
        }

        public Player(Vector2 newPostion)
        {
            position = newPostion;
        }

        public override void Initialize()
        {
            score = 0;
            base.Initialize();
        }

        public override void Load(ContentManager content)
        {
            image = content.Load<Texture2D>("spritesheet");

            LoadAnimation("shyboy.anm", content);
            ChangeAnimation(Animations.IdleLeft);


            base.Load(content);

            boundingBoxOffset.X = 0; boundingBoxOffset.Y = 0; boundingBoxWidth = animationSet.width; boundingBoxHeight = animationSet.height;
        }

        public override void Update(List<GameObject> objects, Map map)
        {
            Input(objects,map);
            base.Update(objects , map);
        }


        private void Input(List<GameObject> objects, Map map)
        {
            ks = Keyboard.GetState();
            if(Character.applyGravity == false)
            {
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
                    MoveDown();
                }
                else if (ks.IsKeyDown(Keys.Up))
                {
                    MoveUp();
                }
            }
            else
            {
                if (ks.IsKeyDown(Keys.Right))
                    MoveRight();
                else if (ks.IsKeyDown(Keys.Left))
                    Moveleft();

                if (ks.IsKeyDown(Keys.Up))
                    Jump(map);
            }

            if (ks.IsKeyDown(Keys.Space))
                Fire();

        }

        protected override void UpdateAnimations()
        {
            if (currentAnimation == null)
                return;

            base.UpdateAnimations();

            if (velocity != Vector2.Zero || jumping == true)
            {
                if (direction.X < 0 && AnimationIsNot(Animations.RunLeft))
                    ChangeAnimation(Animations.RunLeft);
                else if (direction.X > 0 && AnimationIsNot(Animations.RunRight))
                    ChangeAnimation(Animations.RunRight);
            }
            else if (velocity != Vector2.Zero || jumping == false)
            {
                if (direction.X < 0 && AnimationIsNot(Animations.IdleLeft))
                    ChangeAnimation(Animations.IdleLeft);
                else if (direction.X > 0 && AnimationIsNot(Animations.IdleRight))
                    ChangeAnimation(Animations.IdleRight);
            }

        }




    }
}
