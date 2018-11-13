using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Wargame
{
    class Shot : MovingGameObj 
    {
        public float Power
        {
            get;
            set;
        }
        public override void Update(GameTime gameTime)
        {

            Power -= 1.1f;
            base.Update(gameTime);
        }

    }
}
