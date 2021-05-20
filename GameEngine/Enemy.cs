﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameEngine
{
    public class Enemy : Character
    {
        int respawnTimer;
        const int maxRespawnTimer = 60;

        Random random = new Random();

        public Enemy(Vector2 vect)
        {

        }

        public override void Initialize()
        {
            active = true;
            collidable = false;
            position.X = random.Next(0, 1100);

            base.Initialize();
        }

        public override void Load(ContentManager content)
        {
            image = content.Load <Texture2D>("enemy") ;

            base.Load(content);
        }

        public override void Update(List<GameObject> objects, Map map)
        {
            if(respawnTimer > 0)
            {
                respawnTimer--;
                if (respawnTimer <= 0)
                    Initialize();
            }

            base.Update(objects, map);
        }


        public override void BulletResponse()
        {
            active = false;
            respawnTimer = maxRespawnTimer;



            base.BulletResponse();
        }



    }
}
