using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wargame
{
    class Bloodsplat : GameObj
    {
        public Bloodsplat()
        {
            Time = 0;
            Frame = 0;
            Active = true;
            AnimationSpeed = 50;
            Angle = 0;
        }

        public int Time
        {
            get;
            set;
        }
        public int Frame
        {
            get;
            set;
        }
        public int AnimationSpeed
        {
            get;
            set;
        }
        public bool Active
        {
            get;
            set;
        }
        public override double Radie
        {
            get
            {
                return (26.0);
            }
            set
            {
            }
        }
        public void Update(GameTime gameTime)
        {
            Time += gameTime.ElapsedGameTime.Milliseconds;
            if (Time >= AnimationSpeed)
            {
                Time = 0;
                Frame++;
                if (Frame > 15)
                {
                    Active = false;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 DrawOffset)
        {
            Rectangle tmp = new Rectangle((Frame % 4) * 64, (Frame / 4) * 64, 64, 64);

            spriteBatch.Draw(Grafik,
                Position - DrawOffset + new Vector2(400, 300),
                tmp, Color.White, base.Angle,
                new Vector2(32, 32), 1.0f, SpriteEffects.None, 0);
        }
    }
}
