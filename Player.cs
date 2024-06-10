using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Making_a_Player_Class
{
    class Player
    {
        private Texture2D _texture;
        private Rectangle _location;
        private Vector2 _speed;
        private Rectangle _window;

        public Player(Texture2D texture, int x, int y, Rectangle window)
        {
            _texture = texture;
            _location = new Rectangle(x, y, 30, 30);
            _speed = Vector2.Zero;
            _window = window;
        }
  

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.White);
        }

     
        public void UndoMove()
        {
            _location.Offset(-_speed);
        }

        public void Update(KeyboardState keyboardState)
        {
            _speed = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.D))
                _speed.X += 3;
            if (keyboardState.IsKeyDown(Keys.A))
                _speed.X += -3;
            if (keyboardState.IsKeyDown(Keys.W))
                _speed.Y += -3;
            if (keyboardState.IsKeyDown(Keys.S))
                _speed.Y += 3;

            _location.Offset(_speed);
            if (!_window.Contains(_location))
                UndoMove();
        }

        public bool Collide(Rectangle item)
        {
            return _location.Intersects(item);
        }

        public Boolean Contains(Rectangle item)
        {
            return _location.Contains(item);
        }

        public void Grow()
        {
            _location.Width += 1;
            _location.Height += 1;
        }
    }
}
