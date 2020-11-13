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
    public class Enemy
    {
        public Rectangle rectangle;
        public Texture2D texture, btexture;
        public Vector2 position;
        public int health, speed, bulletdelay, currentDifficulty;
        public bool isVisible;
        public List<Bullet> bulletlist;

        public Enemy (Texture2D newTexture, Vector2 newPosition, Texture2D newBTexture)
        {
            bulletlist = new List<Bullet>();
            texture = newTexture;
            btexture = newBTexture;
            health = 2;
            position = newPosition;
            currentDifficulty = 1;
            bulletdelay = 150;
            speed = 3;
            isVisible = true;
        }

        public void Update(GameTime gameTime)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //Movimiento del enemigo
            position.X -= speed;
            if(position.X <= 0 - texture.Width)
            {
                position.Y = 1350;
            }

            EnemyShoot();
            UpdateBullets();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet b in bulletlist)
            {
                b.Draw(spriteBatch);
            }
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void UpdateBullets()
        {
            foreach (Bullet b in bulletlist)
            {
                b.rectangle = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);
                b.position.X -= b.speed;

                if (b.position.X <= 0)
                {
                    b.isVisible = false;
                }
            }

            for (int i = 0; i < bulletlist.Count; i++)
            {
                if (!bulletlist[i].isVisible)
                {
                    bulletlist.RemoveAt(i);
                    i--;
                }
            }
        }

        public void EnemyShoot()
        {
            if (bulletdelay >= 0)
            {
                bulletdelay--;
            }

            if (bulletdelay <= 0)
            {
                Bullet newBullet = new Bullet(btexture);
                newBullet.position = new Vector2(position.X + texture.Width - newBullet.texture.Width / 2, position.Y + 30);
                newBullet.isVisible = true;
                if (bulletlist.Count() < 20)
                {
                    bulletlist.Add(newBullet);
                }
            }

            if (bulletdelay == 0)
            {
                bulletdelay = 150;
            }

        }
    }
}
