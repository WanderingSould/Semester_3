using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Bird_Windows_Form
{
    public partial class Form1 : Form
    {
        int pipeSpeed = 8;
        int gravity = 15; 
        int score = 0;
        int incr = 5;
        int Running = 1;

        public Form1()
        {
            InitializeComponent();
            loadingUi.Start();

        }

        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                gravity = -15;
            }
        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                gravity = 15;
            }
        }

        private void endGame()
        {
            gameTimer.Stop();
            scoreText.Text = "Score: " + score;
            var result = MessageBox.Show(this, "You Scored : "+ score + "\n Do you want to retry?", "caption", MessageBoxButtons.RetryCancel);

            if (result == DialogResult.Retry) {

                OnRetry();
            }
            else if (result == DialogResult.Cancel) {
                System.Windows.Forms.Application.Exit();
            }
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity;
            pipeBottom.Left -= pipeSpeed; 
            pipeTop.Left -= pipeSpeed;
            scoreText.Text = "Score: " + score;
            Random rnd = new Random();

            if(pipeBottom.Left < -150)
            {
                pipeBottom.Left =  rnd.Next(1000, 1200);
;
                score++;
            }
            if(pipeTop.Left < -180)
            {
                pipeTop.Left = rnd.Next(1150, 1350);
                score++;
            }

            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                flappyBird.Bounds.IntersectsWith(ground.Bounds) || flappyBird.Top < -25
                )
            {
                endGame();
            }

            for (int i = 1; i < 10 && Running == 1; i++) {
                if (score > incr * i) {
                    pipeSpeed = 8 * (i+1);
                }
            }
        }

        public void OnRetry() {

            this.Controls.Remove(flappyBird);
            this.Controls.Remove(pipeTop);
            this.Controls.Remove(pipeBottom);
            this.Controls.Remove(ground);
            this.Controls.Remove(scoreText);

            Running = 0;
            score = 0;
            InitializeComponent();
            this.Controls.Remove(pictureBox1);
            this.Controls.Remove(pictureBox2);
            this.Controls.Remove(button1);
            this.Controls.Remove(button2);
            this.Controls.Remove(button3);
            this.Controls.Remove(progressBar1);
            flappyBird.BackColor = Color.Transparent;
            pipeTop.BackColor = Color.Transparent;
            pipeBottom.BackColor = Color.Transparent;
            gameTimer.Start();
            pipeSpeed = 8;
            Running = 1;
            gravity = 15;
            scoreText.Text = "Score: " + score;

        }

        public void OnLoad() {

            this.Controls.Remove(progressBar1);
            loadingUi.Stop();
            pictureUi.Start();
            flappyBird.BackColor = Color.Transparent;
            pipeTop.BackColor = Color.Transparent;
            pipeBottom.BackColor = Color.Transparent;

        }

        private void loadingUi_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 5;
            }
            else if (progressBar1.Value == 100) {

                OnLoad();
            }
        }

        private void pictureUi_Tick(object sender, EventArgs e)
        {
            if (pictureBox2.Location.Y == 39)
            {
                pictureUi.Stop();
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;

            }
            else
            {
                pictureBox2.Location = new Point(pictureBox2.Location.X, pictureBox2.Location.Y - 10);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(pictureBox1);
            this.Controls.Remove(pictureBox2);
            this.Controls.Remove(button1);
            this.Controls.Remove(button2);
            this.Controls.Remove(button3);

            gameTimer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a group project created by the following members : \n\nSwaroop \nRahul \nAbhinanth \nFaiz \nPooja");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }


    }
}
