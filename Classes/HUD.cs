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
    public class HUD
    {
        public int playerScore, screenW, screenH;
        public SpriteFont playerScoreFont;
        public Vector2 playerScorePosition;
        public bool showHUD;

        public HUD()
        {
            playerScore = 0;
            showHUD = true;
            screenH = 720;
            screenW = 1280;
            playerScoreFont = null;
            playerScorePosition = new Vector2(screenW / 2, 50);
        }

        public void LoadContent(ContentManager Content)
        {
            playerScoreFont = Content.Load<SpriteFont>("font");
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (showHUD)
            {
                spriteBatch.DrawString(playerScoreFont, "Score : " + playerScore, playerScorePosition, Color.Red);
            }
        }
    }
}
