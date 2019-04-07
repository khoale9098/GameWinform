using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelicopterFlyingAndShootingGame
{
    public partial class Form1 : Form
    {
        bool goup;
        bool godown;
        bool shot = false;
        int score = 0;
        int speed = 8;
        Random rd = new Random();
        int playerSpeed = 7;
        int index;    

        public Form1()
        {
            InitializeComponent();
        }

        private void changeUFO()
        {
            index += 1;
            if (index > 3)
            {
                index = 1;
            }
            switch(index)
            {
                case 1:
                    ufo.Image = Properties.Resources.alien1;
                    break;
                case 2:
                    ufo.Image = Properties.Resources.alien2;
                    break;
                case 3:
                    ufo.Image = Properties.Resources.alien3;
                    break;
            }
        }

        private void makeBullet()
        {
            PictureBox bullet = new PictureBox();
            bullet.BackColor = System.Drawing.Color.DarkOrange;
            bullet.Height = 5;
            bullet.Width = 10;
            bullet.Left = player.Left + player.Width;
            bullet.Top = player.Top + player.Height;
            bullet.Tag = "Bullet";
            this.Controls.Add(bullet);
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) 
            {
                goup = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = true;
            }
            if (e.KeyCode == Keys.Space && shot == false)
            {
                makeBullet();
                shot = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }
            if (shot == true)
            {
                shot = false;
            }
        }


        private void gametick(object sender, EventArgs e)
        {
            pillar1.Left -= speed;
            pillar2.Left -= speed;
            ufo.Left -= speed;
            label1.Text = "Score : " + score;
            if (goup)
            {
                player.Top -= playerSpeed;
            }
            if (godown)
            {
                player.Top += playerSpeed;
            }
            if (pillar1.Left < -150)
            {
                pillar1.Left = 900;
            }
            if (pillar2.Left < -150)
            {
                pillar2.Left = 1000;
            }
            if (ufo.Left < 5 ||
                player.Bounds.IntersectsWith(ufo.Bounds) ||
                player.Bounds.IntersectsWith(pillar1.Bounds) ||
                player.Bounds.IntersectsWith(pillar2.Bounds)
                )
            {
                gameTimer.Stop();
                MessageBox.Show("You Failed The Mission, You Killed " + score + "Ufo's");
            }
            foreach (Control X in this.Controls)
            {
                if (X is PictureBox && X.Tag == "Bullet")
                {
                    X.Left += 25;
                    if (X.Left > 900)
                    {
                        this.Controls.Remove(X);
                        X.Dispose();
                    }
                    if (X.Bounds.IntersectsWith(ufo.Bounds))
                    {
                        score += 1;
                        this.Controls.Remove(X);
                        X.Dispose();
                        ufo.Left = 1000;
                        ufo.Top = rd.Next(5, 330) - ufo.Height;
                        changeUFO();
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
