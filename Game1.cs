using JuegoFinal.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JuegoFinal
{

    public class Game1 : Game
    {
        public enum State
        {
            Menu,
            Playing,
            GameOver
        }

       
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D txbg, txmenu, txgo;
        Rectangle rbg, rbg2, rmenu, rgo;

        List<Asteroid> astrlist = new List<Asteroid>();
        Player ship = new Player();
        public int enemyBulletdamage;
        List<Enemy> enemyList = new List<Enemy>();
        HUD hud = new HUD();
        SoundManager sm = new SoundManager();

        Random rnd = new Random();

        State gameState = State.Menu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            enemyBulletdamage = 25;
            txmenu = null;
        }


        protected override void Initialize()
        {
            rbg = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            rbg2 = new Rectangle(graphics.PreferredBackBufferWidth, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            rmenu = new Rectangle(300, 100, 640, 360);
            rgo = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            txbg = Content.Load<Texture2D>("bg");
            txmenu = Content.Load<Texture2D>("menu");
            txgo = Content.Load<Texture2D>("gameover");
            hud.LoadContent(Content);
            ship.LoadContent(Content);
            sm.LoadContent(Content);            
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            switch (gameState)
            {
                case State.Playing:
                    {
                        // Logica para el escenario BEGIN
                        rbg.X -= 2;
                        rbg2.X -= 2;

                        if (rbg.X <= -graphics.PreferredBackBufferWidth)
                        {
                            rbg.X = graphics.PreferredBackBufferWidth;
                        }
                        if (rbg2.X <= -graphics.PreferredBackBufferWidth)
                        {
                            rbg2.X = graphics.PreferredBackBufferWidth;
                        }
                        //Logica para el escenario END

                        //Logica para Asteroides BEGIN
                        foreach (Asteroid a in astrlist)
                        {
                            if (a.rectangle.Intersects(ship.rectangle))
                            {
                                a.isVisible = false;
                                sm.explodeAstr.Play();
                                ship.health -= 50;
                                hud.playerScore -= 10;
                            }
                            for (int i = 0; i < ship.bulletlist.Count; i++)
                            {
                                if (a.rectangle.Intersects(ship.bulletlist[i].rectangle))
                                {
                                    a.isVisible = false;
                                    sm.explodeAstr.Play();
                                    ship.bulletlist.ElementAt(i).isVisible = false;
                                    hud.playerScore += 3;
                                }
                            }
                            a.Update(gameTime);
                        }
                        //Logica para Asteroides END

                        //Logica para Enemigos BEGIN
                        foreach (Enemy e in enemyList)
                        {
                            if (e.rectangle.Intersects(ship.rectangle))
                            {
                                ship.health -= 100;
                                hud.playerScore -= 50;
                                e.isVisible = false;
                                sm.explodeEnemy.Play();
                            }
                            for (int i = 0; i < e.bulletlist.Count; i++)
                            {
                                if (ship.rectangle.Intersects(e.bulletlist[i].rectangle))
                                {
                                    ship.health -= enemyBulletdamage;
                                    e.bulletlist.ElementAt(i).isVisible = false;
                                }
                            }
                            for (int i = 0; i < ship.bulletlist.Count; i++)
                            {
                                if (e.rectangle.Intersects(ship.bulletlist[i].rectangle))
                                {
                                    e.health -= 1;
                                    ship.bulletlist.ElementAt(i).isVisible = false;
                                    if (e.health == 0)
                                    {
                                        e.isVisible = false;
                                        sm.explodeEnemy.Play();
                                        hud.playerScore += 5;
                                    }
                                }
                            }
                            e.Update(gameTime);
                        }
                        if (ship.health <= 0)
                        {
                            gameState = State.GameOver;
                        }
                        //Logica para Enemigos END
                        loadEnemy();
                        loadAstr();
                        ship.Update(gameTime);
                        hud.Update(gameTime);
                        break;
                    }
                case State.Menu:
                    {
                        KeyboardState keyState = Keyboard.GetState();

                        if (keyState.IsKeyDown(Keys.Enter))
                        {
                            gameState = State.Playing;
                            MediaPlayer.Play(sm.bgmusic);
                        }
                        break;
                    }
                case State.GameOver:
                    {
                        KeyboardState keyState = Keyboard.GetState();
                        MediaPlayer.Stop();
                        if (keyState.IsKeyDown(Keys.Enter))
                        { 
                            ship.position = new Vector2(300,300);
                            enemyList.Clear();
                            astrlist.Clear();
                            ship.health = 200;
                            hud.playerScore = 0;
                            gameState = State.Playing;
                            MediaPlayer.Play(sm.bgmusic);
                        }
                        
                        break;
                    }
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            switch (gameState)
            {
                case State.Playing:
                    {
                        spriteBatch.Draw(txbg, rbg, Color.White);
                        spriteBatch.Draw(txbg, rbg2, Color.White);
                        hud.Draw(spriteBatch);
                        ship.Draw(spriteBatch);
                        foreach (Enemy b in enemyList)
                        {
                            b.Draw(spriteBatch);
                        }
                        foreach (Asteroid a in astrlist)
                        {
                            a.Draw(spriteBatch);
                        }   
                        break;
                    }
                case State.Menu:
                    {
                        spriteBatch.Draw(txbg, rbg, Color.White);
                        spriteBatch.Draw(txbg, rbg2, Color.White);
                        spriteBatch.Draw(txmenu, rmenu, Color.White);
                        break;
                    }
                case State.GameOver:
                    {
                        spriteBatch.Draw(txgo, rgo, Color.White);
                        spriteBatch.DrawString(hud.playerScoreFont, "Puntuacion Final :" + hud.playerScore.ToString(), new Vector2(580, 150), Color.Red);
                        break;
                    }
            }
            
            
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void loadAstr()
        {
            int randX = rnd.Next(1300,1800);
            int randY = rnd.Next(0, 450);

            if(astrlist.Count() < 3)
            {
                astrlist.Add(new Asteroid(Content.Load<Texture2D>("astr"), new Vector2(randX, randY)));
            }

            for(int i=0; i < astrlist.Count; i++)
            {
                if (!astrlist[i].isVisible)
                {
                    astrlist.RemoveAt(i);
                    i--;
                }
            }
        }

        public void loadEnemy()
        {
            int randX = rnd.Next(1300, 1800);
            int randY = rnd.Next(0, 450);

            if (enemyList.Count() < 2)
            {
                enemyList.Add(new Enemy(Content.Load<Texture2D>("krell"), new Vector2(randX, randY), Content.Load<Texture2D>("shote")));
            }

            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].isVisible)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}

