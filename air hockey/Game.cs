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
        SFML.System.Vector2f ballMoveDirection;

        Random rnd = new Random();
        public void Play()
        {
            window = new RenderWindow(new VideoMode(1600, 900), "Game window");
            window.Closed += WindowClosed;

            player1Position = new SFML.System.Vector2f(50, window.Size.Y / 2 - 25);
            player2Position = new SFML.System.Vector2f(window.Size.X - 50, window.Size.Y / 2 - 25);

            ballSpawnPosition = new SFML.System.Vector2f(window.Size.X / 2, window.Size.Y / 2);
            ballPosition = ballSpawnPosition;

            while (window.IsOpen)
            {
                window.Clear();
                window.DispatchEvents();

                DrawLines();
                BallLogic();
                MoveLine();
                DrawScores();

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

        void MoveLine()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) player1Position.Y -= lineSpeed;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) player1Position.Y += lineSpeed;
            if (player1Position.Y < line1.Size.Y) player1Position.Y = line1.Size.Y;
            if (player1Position.Y + line1.Size.Y > window.Size.Y) player1Position.Y = window.Size.Y - line1.Size.Y;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up)) player2Position.Y -= lineSpeed;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down)) player2Position.Y += lineSpeed;
            if (player2Position.Y < line2.Size.Y) player2Position.Y = line2.Size.Y;
            else if (player2Position.Y > window.Size.Y - line2.Size.Y) player2Position.Y = window.Size.Y - line2.Size.Y;
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
            ball.Radius = 2f;
            ball.FillColor = Color.White;
            ball.Position = ballPosition;
            if (!isBallMoving)
            {
                while (ballMoveDirection == new SFML.System.Vector2f(0,0)) 
                    ballMoveDirection = new SFML.System.Vector2f((float)rnd.NextDouble() * rnd.Next(-1, 1), (float)rnd.NextDouble() * rnd.Next(-1, 1));
                isBallMoving = true;
            }
            ballPosition += ballMoveDirection * ballSpeed;
            CheckIfSomebodyGotScore(ball);
            CheckIfShouldChangeDirection(ball);
            CheckIfHitPlayerLine(ball);
            window.Draw(ball);
        }

        void CheckIfSomebodyGotScore(CircleShape ball)
        {
            if (ball.Position.X >= window.Size.X - ball.Radius)
            {
                ballPosition = ballSpawnPosition;
                isBallMoving = false;
                player1Score++;
            }
            else if(ball.Position.X <= ball.Radius)
            {
                ballPosition = ballSpawnPosition;
                isBallMoving = false;
                player2Score++;
            }
        }
        void CheckIfShouldChangeDirection(CircleShape ball)
        {
            if (ball.Position.Y > window.Size.Y - ball.Radius && ballMoveDirection.Y > 0) ballMoveDirection.Y *= -1f;
            else if (ball.Position.Y < ball.Radius && ballMoveDirection.Y < 0) ballMoveDirection.Y *= -1f;
        }
        void CheckIfHitPlayerLine(CircleShape ball)
        {
            if ((ball.Position.Y > line1.Position.Y - line1.Size.Y) && (ball.Position.Y < line1.Position.Y + line1.Size.Y))
            {
                if ((line1.Position.X >= ball.Position.X + ball.Radius) && (line1.Position.X <= ball.Position.X)) ballMoveDirection *= -1f;
            }

            if ((ball.Position.Y > line2.Position.Y - line1.Size.Y) && (ball.Position.Y < line2.Position.Y + line1.Size.Y))
            {
                if ((line2.Position.X <= ball.Position.X + ball.Radius) && (line2.Position.X >= ball.Position.X)) ballMoveDirection *= -1f;
            }
        }
    }
}