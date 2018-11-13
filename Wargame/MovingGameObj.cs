using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wargame
{
    class MovingGameObj : GameObj
    {
        public Vector2 Direction
        {
            get;
            set; 
        }
       public float Speed
        {
            get;
            set;
        }
        public virtual void Update(GameTime gameTime)
        {
            Position += Direction * Speed;
        }
    }
}
