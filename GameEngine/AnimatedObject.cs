using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Xml.Serialization;

namespace GameEngine
{
    public class AnimatedObject : GameObject
    {
        protected int currentAnimationFrame;
        protected float animationTimer;
        protected int currentAnimationX = -1, currentAnimationY = -1;
        protected AnimationSet animationSet = new AnimationSet();
        protected Animation currentAnimation;

        protected bool flipRightFrames = true;
        protected bool flipLeftFrames = false;
        protected SpriteEffects spriteEffect = SpriteEffects.None;

        protected enum Animations
        {
            RunLeft, RunRight, IdleLeft, IdleRight,
        }


        protected void LoadAnimation(string path, ContentManager content)
        {
            AnimationData animationData = AnimationLoader.Load(path);
            animationSet = animationData.animation;

            centre.X = animationSet.width / 2;
            centre.Y = animationSet.height / 2 ;

            if (animationSet.animationList.Count > 0)
            {
                currentAnimation = animationSet.animationList[0];

                currentAnimationFrame = 0;
                animationTimer = 0f;
                CalculateFramePosition();
            }

        }


        public override void Update(List<GameObject> objects, Map map)
        {
            base.Update(objects, map);
            if (currentAnimation != null)
                UpdateAnimations();
        }


        protected virtual void UpdateAnimations()
        {
            if (currentAnimation.animationOrder.Count < 1)
                return;

            animationTimer -= 1;
            if (animationTimer <= 0)
            {
                animationTimer = currentAnimation.speed;
                if (AnimationComplete() == true)
                    currentAnimationFrame = 0;
                else
                    currentAnimationFrame++;

                CalculateFramePosition();
            }
        }

        protected virtual void ChangeAnimation(Animations newAnimation)
        {
            currentAnimation = GetAnimation(newAnimation);

            if (currentAnimation == null)
                return;


            currentAnimationFrame = 0;
            animationTimer = currentAnimation.speed;

            CalculateFramePosition();


            if (flipRightFrames == true && currentAnimation.name.Contains("Right") || flipLeftFrames == true && currentAnimation.name.Contains("Left"))
                spriteEffect = SpriteEffects.FlipHorizontally;
            else
                spriteEffect = SpriteEffects.None;

        }


        private Animation GetAnimation(Animations animation)
        {
            string name = GetAnimationName(animation);

            for (int i = 0; i < animationSet.animationList.Count; i++)
            {
                if (animationSet.animationList[i].name == name)
                    return animationSet.animationList[i];
            }
            return null;
        }


        protected void CalculateFramePosition()
        {
            int coordinate = currentAnimation.animationOrder[currentAnimationFrame];

            currentAnimationX = (coordinate % animationSet.gridX) * animationSet.width;
            currentAnimationY = (coordinate / animationSet.gridX) * animationSet.height;

        }


        public bool AnimationComplete()
        {
            return currentAnimationFrame >= currentAnimation.animationOrder.Count - 1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (active == false)
                return;

            if (currentAnimationX == -1 || currentAnimationY == -1)
                base.Draw(spriteBatch);
            else
                spriteBatch.Draw(image, position, new Rectangle(currentAnimationX, currentAnimationY, animationSet.width, animationSet.height), Color, rotation, Vector2.Zero, scale, spriteEffect, layerDepth);

        }


        protected string GetAnimationName(Animations animation)
        {
            //Make an accurately spaced string. Example: "RunLeft" will now be "Run Left":
            return AddSpacesToSentence(animation.ToString(), false);
        }

        protected bool AnimationIsNot(Animations input)
        {
            //Used to check if our currentAnimation isn't set to the one passed in:
            return currentAnimation != null && GetAnimationName(input) != currentAnimation.name;
        }

        public string AddSpacesToSentence(string text, bool preserveAcronyms) //IfThisWasPassedIn... "If This Was Passed In" would be returned
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                        (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                         i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                        newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }

    }
}
