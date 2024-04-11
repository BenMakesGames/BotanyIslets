using BenMakesGames.PlayPlayMini;
using BenMakesGames.PlayPlayMini.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BotanyIslets.GameStates.Sailing;

public sealed class Sailing: GameState
{
    private const int WaterHeight = 40;

    private GraphicsManager Graphics { get; }
    private GameStateManager GSM { get; }
    private MouseManager Mouse { get; }
    private KeyboardManager Keyboard { get; }

    private Boat Player { get; }
    private List<Island> Islands { get; }

    public Sailing(GraphicsManager graphics, GameStateManager gsm, MouseManager mouse, KeyboardManager keyboard)
    {
        Graphics = graphics;
        GSM = gsm;
        Mouse = mouse;
        Keyboard = keyboard;

        Player = new Boat()
        {
            MovementSpeed = 200
        };

        Islands =
        [
            new Island()
            {
                X = 120,
                Width = 50,
                Color = Color.Brown
            },
            new Island()
            {
                X = -200,
                Width = 120,
                Color = Color.Green
            }
        ];
    }

    public override void Input(GameTime gameTime)
    {
        Player.HandleInput(Keyboard);

        // debugging features (not available in release builds)
        #if DEBUG
            if(Keyboard.PressedKey(Keys.F12))
                Player.Teleport(0);
        #endif
    }

    public override void Update(GameTime gameTime)
    {
        Player.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        Graphics.Clear(Color.SkyBlue);

        var waterSurfaceY = Graphics.Height - WaterHeight;

        foreach (var island in Islands)
            Graphics.DrawIsland(island, waterSurfaceY, Player.PixelX);

        Graphics.DrawBoat(Player, waterSurfaceY);

        Graphics.DrawFilledRectangle(0, waterSurfaceY, Graphics.Width, WaterHeight, new Color(0, 0, 0.8f, 0.6f));

        // only draw the mouse cursor once
        if(GSM.CurrentState == this)
            Mouse.Draw(gameTime);
    }
}
