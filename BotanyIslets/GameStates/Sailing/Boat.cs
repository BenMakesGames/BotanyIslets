using BenMakesGames.PlayPlayMini.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BotanyIslets.GameStates.Sailing;

public class Boat
{
    public float X { get; private set; }
    public float XVelocity { get; private set; }
    public required float MovementSpeed { get; set; }
    public int PixelX => (int)Math.Floor(X);
    public Facing Direction { get; set; } = Facing.Right;

    public void HandleInput(KeyboardManager kb)
    {
        var xVelocity = 0;

        if (kb.AnyKeyDown([Keys.A, Keys.Left, Keys.NumPad4]))
            xVelocity--;

        if(kb.AnyKeyDown([Keys.D, Keys.Right, Keys.NumPad6]))
            xVelocity++;

        XVelocity = xVelocity;
    }

    public void Teleport(int x)
    {
        X = x;
        XVelocity = 0;
    }

    public void Update(GameTime gameTime)
    {
        if (XVelocity < 0)
            Direction = Facing.Left;
        else if (XVelocity > 0)
            Direction = Facing.Right;

        X += XVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds * MovementSpeed;
    }
}

public enum Facing
{
    Left,
    Right
}

public static class BoatExtensions
{
    public static void DrawBoat(this GraphicsManager graphics, Boat boat, int waterSurfaceY)
    {
        var picture = graphics.Pictures["Boat"];

        // always draw the boat in the bottom-center of the screen... for now!
        var topX = graphics.Width / 2 - picture.Width / 2;
        var leftY = waterSurfaceY - picture.Height + 10; // submerged 10 pixels into the water

        if(boat.Direction == Facing.Left)
            graphics.DrawPicture("Boat", topX, leftY);
        else
            graphics.DrawTextureFlipped(picture, topX, leftY, SpriteEffects.FlipHorizontally);
    }
}
