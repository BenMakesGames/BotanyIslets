using BenMakesGames.PlayPlayMini.Services;
using Microsoft.Xna.Framework;

namespace BotanyIslets.GameStates.Sailing;

public class Island
{
    public required int X { get; init; }
    public required int Width { get; init; }
    public required Color Color { get; init; }
}

public static class IslandExtensions
{
    public static void DrawIsland(this GraphicsManager graphics, Island island, int waterSurfaceY, int cameraX)
    {
        var leftX = graphics.Width / 2 - island.Width / 2 + island.X - cameraX;
        var topY = waterSurfaceY - 20;
        var height = graphics.Height - topY;

        // for now, just draw a filled rectangle; it's "fine"
        graphics.DrawFilledRectangle(leftX, topY, island.Width, height, island.Color);
    }
}
