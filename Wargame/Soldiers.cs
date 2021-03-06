﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wargame
{
    class Soldiers : MovingGameObj
    {
        
        public Soldiers()
        {
            MaxSpeed = 2.5F;
            ShotPower = 0;
            prevKs = Keyboard.GetState();
            Life = 100F;
            Kills = 0;
            Angle = -(float)(Math.PI / 2);
        }
        public bool Enemy
        {
            get;
            set;
        }
        public float MaxSpeed
        {
            get;
            set;
        }
        public float ShotPower
        {
            get;
            set;
        }
        public int WeaponType
        {
            get;
            set;
        }
        public bool ShotFired
        {
            get;
            set;
        }
        public float Life
        {
            get;
            set;
        }
        public int Kills
        {
            get;
            set;
        }
        protected KeyboardState prevKs;


        public void Respawn()
        {
            Life = 100F;
            Random randomerare = new Random();
            Position = new Vector2(randomerare.Next(600), randomerare.Next(600));
            Angle = 0;

        }
        public override void Update(GameTime gameTime)
        {

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Up))
            {
                if (Speed < 0) Speed = 0;
                if (Speed < MaxSpeed) Speed = Speed * 1.005F + 0.01F;
                else Speed = MaxSpeed;
            }
            if (ks.IsKeyDown(Keys.Down))
            {
                if (Speed > -1.0F) Speed -= 0.04F;
                else Speed = -1.0F;
            }
            if (ks.IsKeyUp(Keys.Down) && ks.IsKeyUp(Keys.Up) && Speed > 0)
            {
                Speed -= 0.01F;
                if (Speed <= 0) Speed = 0;
            }
            if (ks.IsKeyUp(Keys.Down) && ks.IsKeyUp(Keys.Up) && Speed < 0)
            {
                Speed += 0.01F;
                if (Speed >= 0) Speed = 0;
            }

            if (ks.IsKeyUp(Keys.Left))
            {
                Angle += 0.02F;
            }
            if (ks.IsKeyUp(Keys.Right))
            {
                Angle -= 0.02F;
            }
            if (ks.IsKeyDown(Keys.O))
            {
                if (ShotPower < 100)
                    ShotPower += 0.5F;
                else
                    ShotPower = 100;
            }

            if (ks.IsKeyUp(Keys.O) && prevKs.IsKeyDown(Keys.O))
            {
                //ShotPower = 0;
                ShotFired = true;
            }

            

            prevKs = ks;
            Direction = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));

            base.Update(gameTime);
        }

    }
}
