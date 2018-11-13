using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wargame
{
    class GameObj
    {
        public Vector2 Position
        {
            get;
            set;
        }
        public Texture2D Grafik
        {
            get;
            set;
        }
        public float Angle
        {
            get;
            set;
        }
        public bool CheckCollision(GameObj target)
        {
            bool collision;
            double xdiff = this.Position.X - target.Position.X;
            double ydiff = this.Position.Y - target.Position.Y;

            if ((xdiff * xdiff + ydiff * ydiff) < (this.Radie + target.Radie) * (this.Radie + target.Radie))
            {
                collision = true;
            }
            else
            {
                collision = false;
            }
            return (collision);
        }
        public virtual double Radie
        {
            get
            {
                return (this.Grafik.Height / 2);
            }
            set
            {
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 DrawOffset)
        {
            spriteBatch.Draw(Grafik, Position - DrawOffset + new Vector2(400, 300), null, Color.White, Angle + (float)Math.PI / 2, new Vector2(Grafik.Width / 2, Grafik.Height / 2), 1.0f, SpriteEffects.None, 0);
        }
       
        }
    }

