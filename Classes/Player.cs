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
    public class Player
    {
        public Texture2D texture, bulletTexture, healthtx;
        public Rectangle rectangle, healthrect;
        public Vector2 position, healthbarposition;
        public int speed, health;
        public bool isColliding;

        SoundManager sm = new SoundManager();

        public float bulletdelay;
        public List<Bullet> bulletlist;


        public Player()
        {
            texture = null;
            position = new Vector2(300, 300);
            healthbarposition = new Vector2(20,20);
            speed = 5;
            isColliding = false;
            bulletdelay = 1;
            bulletlist = new List<Bullet>();
            health = 200;
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("mbot");
            bulletTexture = Content.Load<Texture2D>("bullet");
            healthtx = Content.Load<Texture2D>("healthbar");
            sm.LoadContent(Content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthtx, healthrect, Color.White);
            foreach (Bullet b in bulletlist)
            {
                b.Draw(spriteBatch);
            }
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            rectangle = new Rectangle((int)position.X, (int)position.Y, 60, 120);

            healthrect = new Rectangle((int)healthbarposition.X, (int)healthbarposition.Y, health, 150);
            //Controles
            if (keyState.IsKeyDown(Keys.Up))
            {
                position.Y -= speed;
            }
            
            if (keyState.IsKeyDown(Keys.Down))
            {
                if (position.Y <= 470)
                {
                    position.Y += speed;
                }
                else
                {
                    //Game over por colision
                }
            }
            if (keyState.IsKeyDown(Keys.Left))
            {
                position.X -= speed;
            }
            if (keyState.IsKeyDown(Keys.Right))
            {
                position.X += speed;
            }
            if (keyState.IsKeyDown(Keys.LeftAlt))
            {
                Shoot();
            }
            UpdateBullets();

            //Mantener la nave en la pantalla
            if (position.X <= 0)
            {
                position.X = 0;
            }
            if(position.X >= 1280 - texture.Width)
            {
                position.X = 1280 - texture.Width;
            }
            if (position.Y <= 0)
            {
                position.Y = 0;
            }
            if (position.Y >= 720 - texture.Height)
            {
                position.Y = 720 - texture.Height;
            }

        }
        //Metodo de disparar
        public void Shoot()
        {
            if(bulletdelay >= 0)
            {
                bulletdelay--;
                //sm.playerShoot.Play(volume: 0.2f, pitch: 0.0f, pan: 0.0f);
            }

            if (bulletdelay <= 0)
            {
                Bullet newBullet = new Bullet(bulletTexture);
                newBullet.position = new Vector2(position.X + 80 - newBullet.texture.Width / 2, position.Y + 42);
                newBullet.isVisible = true;
                if (bulletlist.Count() < 20)
                {
                    bulletlist.Add(newBullet);
                }
            }

            if(bulletdelay == 0)
            {
                bulletdelay = 20;
            }
           
        }

        public void UpdateBullets()
        {
            foreach(Bullet b in bulletlist)
            {
               b.rectangle = new Rectangle((int)b.position.X, (int)b.position.Y, 50, 10);
               b.position.X += b.speed;

               if(b.position.X >= 1280)
               {
                    b.isVisible = false;
               }
            }

            for(int i=0; i < bulletlist.Count; i++)
            {
                if (!bulletlist[i].isVisible)
                {
                    bulletlist.RemoveAt(i);
                    i--;
                }
            }
        }
    }   
}
