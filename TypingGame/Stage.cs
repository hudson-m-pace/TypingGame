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
        private string[] words;
        private Random random;
        private Texture2D background;
        private List<Word> wordList;
        private Word selectedWord;
        private Vector2[] slots;
        private List<Vector2> availableSlots;


        public Stage(KeyboardState keyState)
        {
            random = new Random();
            oldKeyState = keyState;
            words = System.IO.File.ReadAllText(@".\Content\medium_words.txt").Split('\n');
            wordList = new List<Word>();
            slots = new Vector2[] { new Vector2(0, 5), new Vector2(150, 5), new Vector2(0,165), new Vector2(150,165), new Vector2(0, 325), new Vector2(150, 325) };
            availableSlots = new List<Vector2>(slots);
        }
        public void Update(KeyboardState keyState)
        {
            Keys[] currentPressedKeys = keyState.GetPressedKeys();
            foreach (Keys key in currentPressedKeys)
            {
                if (!oldKeyState.IsKeyDown(key))
                {
                    Press(key);
                    break;
                }
            }
            oldKeyState = keyState;

            if (availableSlots.Count > 0 && random.Next(0, 100) == 10)
            {
                Vector2 slot = availableSlots[random.Next(0, availableSlots.Count - 1)];
                availableSlots.Remove(slot);
                wordList.Add(new Word(slot, 1000, words[random.Next(0, words.Length - 1)]));
            }

            if (selectedWord != null && !selectedWord.GetIsActive())
            {
                if (selectedWord.GetText() == "")
                {
                    //success
                }
                else
                {
                    //fail
                }
                availableSlots.Add(selectedWord.GetPosition());
                wordList.Remove(selectedWord);
                selectedWord = null;
            }
            
            foreach (Word word in wordList)
            {
                word.Update();
            }
        }
        private void Press(Keys key)
        {
            if (selectedWord == null)
            {
                foreach (Word word in wordList)
                {
                    if (word.GetText()[0] == key.ToString().ToLower().ToCharArray()[0])
                    {
                        selectedWord = word;
                        break;
                    }
                }
            }
            if (selectedWord != null && key.ToString().ToLower().ToCharArray()[0] == selectedWord.GetText()[0])
            {
                selectedWord.Press();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            //spriteBatch.DrawString(font, word, new Vector2(100, 100), Color.Black);
            foreach(Word word in wordList)
            {
                word.Draw(spriteBatch);
            }
        }
        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            font = content.Load<SpriteFont>("Text");
            background = content.Load<Texture2D>("burger_station");
            Word.CreateTextures(graphicsDevice, content);
        }
    }
}
