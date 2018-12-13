using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TypingGame
{
    class Stage
    {
        private KeyboardState oldKeyState;
        private SpriteFont font;
        private string word;


        public Stage(KeyboardState keyState)
        {
            oldKeyState = keyState;
            word = "tricycle";
        }
        public void Update(KeyboardState keyState)
        {
            Keys[] currentPressedKeys = keyState.GetPressedKeys();
            foreach (Keys key in currentPressedKeys)
            {
                if (!oldKeyState.IsKeyDown(key))
                {
                    Press(key);
                }
            }
            oldKeyState = keyState;
        }
        private void Press(Keys key)
        {
            if (key.ToString().ToLower().ToCharArray()[0] == word[0])
            {
                word = word.Substring(1);
                if (word.Length == 0)
                {
                    word = "ubiquitous";
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, word, new Vector2(100, 100), Color.Black);
        }
        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Text");
        }
    }
}
