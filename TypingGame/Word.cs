using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace TypingGame
{
    class Word
    {
        private Vector2 position;
        private static Texture2D blackBar, redBar, greenBar;
        private Rectangle barRect, fillRect;
        private int totalTime, remainingTime, percentFull;
        private string text;
        private static SpriteFont font;
        private bool active, selected;

        public Word(Vector2 position, int totalTime, string text)
        {
            this.position = position;
            this.totalTime = totalTime;
            this.text = text;
            remainingTime = totalTime;
            barRect = new Rectangle((int)position.X, (int)position.Y + 40, 100, 20);
            fillRect = barRect;
            active = true;
            selected = false;
        }

        public static void CreateTextures(GraphicsDevice graphicsDevice, ContentManager content)
        {
            blackBar = new Texture2D(graphicsDevice, 1, 1);
            Color[] data = new Color[] { Color.Black };
            blackBar.SetData(data);
            redBar = new Texture2D(graphicsDevice, 1, 1);
            data = new Color[] { Color.Red };
            redBar.SetData(data);
            greenBar = new Texture2D(graphicsDevice, 1, 1);
            data = new Color[] { Color.Green };
            greenBar.SetData(data);

            font = content.Load<SpriteFont>("text");
        }
        public void Update()
        {
            remainingTime--;
            percentFull = (int)(((float)remainingTime / totalTime) * 100);
            fillRect.Width = percentFull;
            if (remainingTime < 0)
            {
                active = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (selected)
            {
                spriteBatch.DrawString(font, text, position, Color.Blue);
            }
            else
            {
                spriteBatch.DrawString(font, text, position, Color.Black);
            }
            spriteBatch.Draw(blackBar, barRect, Color.White);
            if (percentFull < 40)
            {
                spriteBatch.Draw(redBar, fillRect, Color.White);
            }
            else
            {
                spriteBatch.Draw(greenBar, fillRect, Color.White);
            }
        }
        public string GetText()
        {
            return text;
        }
        public void Press()
        {
            selected = true;
            text = text.Substring(1);
            if (text.Length == 0)
            {
                // word = words[random.Next(0, words.Length - 1)];
                active = false;
            }
        }
        public bool GetIsActive()
        {
            return active;
        }
        public Vector2 GetPosition()
        {
            return position;
        }
    }
}
