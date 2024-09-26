using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameV4
{
    internal class SnakeObject
    {
        
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int SnakeWidth { get; set; }
        public int SnakeHeight { get; set; }
        public string directions;
        public int CurrentLevel { get; protected set; }
        public bool gameOver;
        public bool goLeft, goRight, goDown, goUp;
        public DynamicArray<SnakeObject> SnakeBody1;

        

        public SnakeObject()
        {
            PositionX = 0;
            PositionY = 0;
            SnakeWidth = 16;
            SnakeHeight = 16;
            directions = "left";
            CurrentLevel = 200;
            gameOver = false;
            SnakeBody1 = new DynamicArray<SnakeObject>();

        }
        public int BodyParts()
        {
            return SnakeBody1.Count -1;
        }
        public void EatFood()
        {
            
            SnakeObject body = new SnakeObject
            {
                PositionX = SnakeBody1.Count,
                PositionY = SnakeBody1.Count
            };

            SnakeBody1.Add(body);

        }
        public int GameLevel()
        {

            switch (Form2.Score)
                
            {
                case 5:
                    CurrentLevel = 150;
                    break;
                case  10:
                    CurrentLevel = 50;
                    break;
                case 15:
                    CurrentLevel = 150;
                    break;
                case 20:
                    CurrentLevel = 50;
                    break;


            }
            return CurrentLevel;
        }
        public void CutBody()
        {

            int bodyToCut = SnakeBody1.Count() / 2;
            for (int i = SnakeBody1.Count() -1; i >= bodyToCut; i--)
            {
                SnakeBody1.RemoveAt(i);
            }
        }
        public void SnakeDirection()
        {

            if (goLeft)
            {
                directions = "left";
            }
            if (goRight)
            {
                directions = "right";
            }
            if (goDown)
            {
                directions = "down";
            }
            if (goUp)
            {
                directions = "up";
            }
        }
        public void NextMove()
        {
            int prevX = SnakeBody1.PeekFirst().PositionX;
            int prevY = SnakeBody1.PeekFirst().PositionY;
            int i = 0;

            switch (directions)
            {
                case "left":
                    SnakeBody1.PeekFirst().PositionX--;
                    break;
                case "right":
                    SnakeBody1.PeekFirst().PositionX++;
                    break;
                case "down":
                    SnakeBody1.PeekFirst().PositionY++;
                    break;
                case "up":
                    SnakeBody1.PeekFirst().PositionY--;
                    break;
            }

            foreach (var item in SnakeBody1)
            {
                if (i > 0)
                {
                    int tempX = item.PositionX;
                    int tempY = item.PositionY;
                    item.PositionX = prevX;
                    item.PositionY = prevY;
                    prevX = tempX;
                    prevY = tempY;
                }
                i++;
            }
        }
            public void SnakeColision(FoodGenerator food)
            {
            
            //boar colision
            if (SnakeBody1.PeekFirst().PositionX < 0)
            {
                SnakeBody1.PeekFirst().PositionX = Form2.maxWidth;
            }
            if (SnakeBody1.PeekFirst().PositionX > Form2.maxWidth)
            {
                SnakeBody1.PeekFirst().PositionX = 0;
            }
            if (SnakeBody1.PeekFirst().PositionY < 0)
            {
                SnakeBody1.PeekFirst().PositionY = Form2.maxHeight;
            }
            if (SnakeBody1.PeekFirst().PositionY > Form2.maxHeight)
            {
                SnakeBody1.PeekFirst().PositionY = 0;
            }

            // Food colision 
            if (SnakeBody1.PeekFirst().PositionX == food.X && SnakeBody1.PeekFirst().PositionY == food.Y)
            {
                food.BeEaten = true;
                EatFood();

                if (food.cutBody == true)
                {
                    CutBody();
                }

            }
            //body colision
            foreach (var item in SnakeBody1.Skip(1))
            {
                if (SnakeBody1.PeekFirst().PositionX == item.PositionX && SnakeBody1.PeekFirst().PositionY == item.PositionY)
                {
                    gameOver = true;
                }
            }
        }
            public void DrawSnake(Graphics canvas)
            {
                int i = 0;
                Brush snakeColour;
                foreach (var bodyPart in SnakeBody1)
                {
                    if (i == 0)
                    {
                        snakeColour = Brushes.Black;
                    }
                    else
                    {
                        snakeColour = Brushes.DarkGreen;
                    }

                    canvas.FillEllipse(snakeColour, new Rectangle
                    (
                    bodyPart.PositionX * SnakeWidth,
                    bodyPart.PositionY * SnakeHeight,
                    SnakeWidth, SnakeHeight
                    ));
                    i++;
                }           
            
        }

    }
}
