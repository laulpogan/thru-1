using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Button
{

	public Texture2D Texture { get; set; }
	public bool isPressed { get; set; }
	private MouseState oldState;
	private string Text;

	public Button(Texture2D texture, string text)
	{
		Texture = texture;
        Text = text;
		isPressed = false;
    }
    protected bool isInsideRectangle(Vector2 point1, Vector2 point2, Rectangle bound)
	{

		return bound.Contains(point1 - point2);

	}
	public void Update()
	{
		MouseState newState = Mouse.GetState();
		int x = newState.X;
		int y = newState.Y;
		if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
		{
			if (isInsideRectangle(new Vector2(x, y), new Vector2(100, 200), Texture.Bounds))
			{
				isPressed = !isPressed;

			}
		}
		oldState = newState; // this reassigns the old state so that it is ready for next time

	}

	public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {
        if (isPressed)
        {
			spriteBatch.Draw(Texture, location, Color.Green);
		} else
		{
			spriteBatch.Draw(Texture, location, Color.Red);
		}

	}
}
