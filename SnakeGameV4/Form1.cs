namespace SnakeGameV4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void gameStart(object sender, EventArgs e)
        {
            Form2 gameWindow = new Form2();

            gameWindow.Show();
            this.Hide();
        }
    }
}