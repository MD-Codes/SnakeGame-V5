using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameV4
{
    class FoodGenerator : IEnumerable
    {
        Random random = new Random();
        private Image scissors = Properties.Resources.scissors;
        private Image banana = Properties.Resources.banana;
        private Image apple = Properties.Resources.apple;
        Image currentFood;
        public bool BeEaten { get; set; }
        public bool cutBody { get; protected set; }
        public int FoodWidth = 16;
        public int FoodHeight = 16;
        public int X { get; set; }
        public int Y { get; set; }

        public FoodGenerator()
        {
            X = 0;
            Y = 0;
            BeEaten = false;
            currentFood = RandomFood();

        }
        public Image RandomFood()
        {
            int randFood = random.Next(1, 101);

            if (BeEaten == true || currentFood == null)
            {
                if (randFood >= 90)
                {
                    currentFood = scissors;
                    cutBody = true;
                }
                else if (randFood >= 45)             
                {
                    currentFood = banana;
                    cutBody = false;

                }
                else 
                {
                    currentFood = apple;
                    cutBody = false;
                }
                BeEaten = false;
            }
            return currentFood;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
