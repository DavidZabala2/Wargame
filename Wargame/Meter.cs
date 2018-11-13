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


namespace Wargame
{
    class Meter : GameObj
    {
        public Meter()
        {
            Value = 0;
        }
        public float Value
        {
            get;
            set;
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 DrawOffset)
        {

            spriteBatch.Draw(Grafik, new Rectangle((int)base.Position.X, (int)base.Position.Y, (int)this.Value, Grafik.Height), Color.White);
            base.Draw(spriteBatch, DrawOffset);
        }
    }
}
