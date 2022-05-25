using SFML.Window;
using SFML.Graphics;
namespace air_hockey
{
    internal class Line : RectangleShape
    {
        RectangleShape line;
        public Line(RenderWindow window, SFML.System.Vector2f position)
        {
            line = new RectangleShape();
            line.Size = new SFML.System.Vector2f(5, 100);
            line.Position = position;
            window.Draw(line);
        }
    }
}