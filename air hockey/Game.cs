using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace air_hockey
{
    internal class Game
    {
        RenderWindow window;

        Line line1;
        Line line2;
        float lineSpeed = 0.2f;

        int player1Score = 0;
        int player2Score = 0;

        SFML.System.Vector2f player1Position;
        SFML.System.Vector2f player2Position;

        float ballSpeed = 0.3f;
        bool isBallMoving = false;
        SFML.System.Vector2f ballSpawnPosition;
        SFML.System.Vector2f ballPosition;

        Random rnd = new Random();
        public void Play()
        {
            window = new RenderWindow(new VideoMode(1600, 900), "Game window");
            window.Closed += WindowClosed;

            player1Position = new SFML.System.Vector2f(50, window.Size.Y / 2);
            player2Position = new SFML.System.Vector2f(window.Size.X - 50, window.Size.Y / 2);
            ballSpawnPosition = new SFML.System.Vector2f(window.Size.X / 2, window.Size.Y / 2);
            while (window.IsOpen)
            {
                window.Clear();
                window.DispatchEvents();

                DrawLines();

                //DrawScores();

                window.Display();
            }
        }
        void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
        
        void DrawLines()
        {
            line1 = new Line(window, player1Position);
            line2 = new Line(window, player2Position);

            window.Draw(line1);
            window.Draw(line2);
        }

        void DrawScores()
        {
            string stringScores = player1Score + " : " + player2Score;
            Text scores = new Text();
            scores.DisplayedString = stringScores;
            scores.CharacterSize = 15;
            scores.FillColor = Color.White;
            scores.Position = new SFML.System.Vector2f(window.Size.X / 2, 160);
            window.Draw(scores);
        }

        void BallLogic()
        {
            CircleShape ball = new CircleShape();
            if (!isBallMoving)
            {

                isBallMoving = true;
            }
        }
    }
}