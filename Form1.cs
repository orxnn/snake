using System;
using System.Drawing;
using System.Windows.Forms;

namespace snake.apple
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        string applePath = "C:\\Users\\LENOVO\\source\\repos\\Snake_Game_Project\\Snake_Game_Project\\apple.png";
        string bombPath = "C:\\Users\\LENOVO\\source\\repos\\Snake_Game_Project\\Snake_Game_Project\\bomb.png";
        int score = 0;
        int timeLeft = 5;
        bool isBomb = false;
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            KeyDown += Form1_KeyDown;
            SpawnItem();
            UpdateScoreLabel();
            timer1.Start();
        }
        void SpawnItem()
        {
            int maxX = Math.Max(0, ClientSize.Width - pictureBox2.Width);
            int maxY = Math.Max(30, ClientSize.Height - pictureBox2.Height);

            pictureBox2.Left = random.Next(0, maxX + 1);
            pictureBox2.Top = random.Next(30, maxY + 1);

            isBomb = random.Next(0, 100) < 30;

            if (isBomb)
            {
                pictureBox2.ImageLocation = bombPath;
            }
            else
            {
                pictureBox2.ImageLocation = applePath;
            }

            timeLeft = 5;
            Text = "Snake and Apple | Vaxt: " + timeLeft + "s";
        }
        void CheckCollision()
        {
            if (!pictureBox1.Bounds.IntersectsWith(pictureBox2.Bounds))
                return;
            if (isBomb)
            {
                Rectangle bombHitbox = pictureBox2.Bounds;
                bombHitbox.Inflate(-10, -10);

                if (pictureBox1.Bounds.IntersectsWith(bombHitbox))
                {
                    timer1.Stop();
                    MessageBox.Show("Bombaya dəydin! Oyun bitdi.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ResetGame();
                }
            }
            else
            {
                score += 1;
                UpdateScoreLabel();
                SpawnItem();
            }
        }
        void ResetGame()
        {
            score = 0;
            UpdateScoreLabel();

            pictureBox1.Left = (ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = (ClientSize.Height - pictureBox1.Height) / 2;

            SpawnItem();
            timer1.Start();
        }
        void UpdateScoreLabel()
        {
            label1.Text = "Xal: " + score;
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            Text = "Snake and Apple | Vaxt: " + timeLeft + "s";

            if (timeLeft <= 0)
            {
                timer1.Stop();
                MessageBox.Show("Vaxt bitdi! Almanı 5 saniyə ərzində yeyə bilmədin.", "Vaxt Bitdi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetGame();
            }
        }
        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && pictureBox1.Left > 0)
            {
                pictureBox1.Left -= 20;
            }
            if (e.KeyCode == Keys.Right && pictureBox1.Right < ClientSize.Width)
            {
                pictureBox1.Left += 20;
            }
            if (e.KeyCode == Keys.Up && pictureBox1.Top > 0)
            {
                pictureBox1.Top -= 20;
            }
            if (e.KeyCode == Keys.Down && pictureBox1.Bottom < ClientSize.Height)
            {
                pictureBox1.Top += 20;
            }

            CheckCollision();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
