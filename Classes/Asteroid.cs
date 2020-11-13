using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JuegoFinal.Classes
{
    public class Asteroid
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public Vector2 origin;
        public Vector2 position;
        public float rotationAngle;
        public int speed;

        public bool isVisible;
        public float randX, randY;

        Random rnd = new Random();

        public Asteroid(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            texture = newTexture;
            speed = 4;
            isVisible = true;
            randX = rnd.Next(1300,1800);
            randY = rnd.Next(0, 450);
        }

        public void LoadContent(ContentManager Content)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, 64, 64);

            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;

            //Movimiento asteroide
            position.X -= speed;
            if(position.X <= (0 - texture.Width))
            {
                position.X = 1280;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (isVisible)
            {
                //spritebatch.Draw(texture, position, null, Color.White, rotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
                spritebatch.Draw(texture, position, Color.White);
            }
        }
    }
}
