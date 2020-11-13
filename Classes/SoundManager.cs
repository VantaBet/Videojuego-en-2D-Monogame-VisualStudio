using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;


namespace JuegoFinal.Classes
{
    public class SoundManager
    {
        public SoundEffect playerShoot, explodeEnemy, explodeAstr;
        public Song bgmusic;

        public SoundManager()
        {
            playerShoot = null;
            explodeEnemy = null;
            explodeAstr = null;
            bgmusic = null;
        }

        public void LoadContent(ContentManager Content)
        {
            playerShoot = Content.Load<SoundEffect>("shooteffect");
            explodeEnemy = Content.Load<SoundEffect>("explodeenemy");
            explodeAstr = Content.Load<SoundEffect>("explodeastr");
            bgmusic = Content.Load<Song>("bgmusic");
        }
    }
}
