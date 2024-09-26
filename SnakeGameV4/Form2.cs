using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace SnakeGameV4
{
    public partial class Form2 : Form
    {
        private SnakeObject Snake = new SnakeObject();
        private FoodGenerator food = new FoodGenerator();
        Random rand = new Random();
        public static int maxWidth;
        public static int maxHeight;
        public bool isGameOver;
        public static int Score { get; set; }
        public int HighScore;

        public Form2()
        {
            isGameOver = true;
            Score = 0;
            InitializeComponent();
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left && Snake.directions != "right")
            {
                Snake.goLeft = true;

            }
            if (e.KeyCode == Keys.Right && Snake.directions != "left")
            {
                Snake.goRight = true;
            }
            if (e.KeyCode == Keys.Up && Snake.directions != "down")
            {
                Snake.goUp = true;
            }
            if (e.KeyCode == Keys.Down && Snake.directions != "up")
            {
                Snake.goDown = true;
            }

            if (e.KeyCode == Keys.A && Snake.directions != "right")
            {
                Snake.goLeft = true;

            }
            if (e.KeyCode == Keys.D && Snake.directions != "left")
            {
                Snake.goRight = true;
            }
            if (e.KeyCode == Keys.W && Snake.directions != "down")
            {
                Snake.goUp = true;
            }
            if (e.KeyCode == Keys.S && Snake.directions != "up")
            {
                Snake.goDown = true;
            }
        }
    

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                Snake.goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                Snake.goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                Snake.goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                Snake.goDown = false;
            }

            if (e.KeyCode == Keys.A)
            {
                Snake.goLeft = false;
            }
            if (e.KeyCode == Keys.D)
            {
                Snake.goRight = false;
            }
            if (e.KeyCode == Keys.W)
            {
                Snake.goUp = false;
            }
            if (e.KeyCode == Keys.S)
            {
                Snake.goDown = false;
            }
        }

        private void StartGame(object sender, EventArgs e)
        {
            isGameOver = false;
            RestartGame();
        }

        private void TakeSnapShot(object sender, EventArgs e)
        {
            Label caption = new Label();
            caption.Text = "I scored: " + Score + " and my Highscore is " + HighScore + " on the Snake Game from MOO ICT";
            caption.Font = new Font("Ariel", 12, FontStyle.Bold);
            caption.ForeColor = Color.Purple;
            caption.AutoSize = false;
            caption.Width = picCanvas.Width;
            caption.Height = 30;
            caption.TextAlign = ContentAlignment.MiddleCenter;
            picCanvas.Controls.Add(caption);

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "Snake Game SnapShot MOO ICT";
            dialog.DefaultExt = "jpg";
            dialog.Filter = "JPG Image File | *.jpg";
            dialog.ValidateNames = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                int width = Convert.ToInt32(picCanvas.Width);
                int height = Convert.ToInt32(picCanvas.Height);
                Bitmap bmp = new Bitmap(width, height);
                picCanvas.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
                bmp.Save(dialog.FileName, ImageFormat.Jpeg);
                picCanvas.Controls.Remove(caption);
            }

        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            Snake.SnakeDirection();
            Snake.NextMove();
            Snake.SnakeColision(food);
            
            txtScore.Text = "Score: " + Score;

            if (food.BeEaten == true)
            {
                food = new FoodGenerator { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };
                Score++;
            }
            if (Snake.gameOver == true)
            {
                GameOver();

            }
            //Game speed level
            gameTimer.Interval = Snake.GameLevel();
            picCanvas.Invalidate();

        }

        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {
            if (isGameOver == false)
            {
                Graphics canvas = e.Graphics;
                Snake.DrawSnake(canvas);
                canvas.DrawImage(food.RandomFood(), food.X * food.FoodWidth, food.Y * food.FoodHeight, food.FoodWidth, food.FoodHeight);
            }
        }

        private void RestartGame()
        {
            maxWidth = picCanvas.Width / Snake.SnakeWidth - 1;
            maxHeight = picCanvas.Height / Snake.SnakeHeight - 1;

            Snake.SnakeBody1.Clear();
            
            startButton.Enabled = false;
            snapButton.Enabled = false;
            Score = 0;
            txtScore.Text = "Score: " + Score;
            

            SnakeObject head = new SnakeObject { PositionX = 10, PositionY = 5 };
            Snake.SnakeBody1.Add(head); // adding the head part of the snake to the list

            for (int i = 1; i < 6; i++)
            {
                SnakeObject body = new SnakeObject();
                Snake.SnakeBody1.Add(body);
            }
            food = new FoodGenerator { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };

            gameTimer.Start();

        }

        public void GameOver()
        {
            gameTimer.Stop();
            startButton.Enabled = true;
            snapButton.Enabled = true;
            Snake.gameOver = false;

            if (Score > HighScore)
            {
                HighScore = Score;

                txtHighScore.Text = "High Score: " + Environment.NewLine + HighScore;

                txtHighScore.ForeColor = Color.Maroon;
                txtHighScore.TextAlign = ContentAlignment.MiddleCenter;
            }
            Score = 0;
        }


    }
}
