using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    public partial class Form1 : Form
    {
        MapController map;
        Player player;
        Physics2DController physics;


        public Label scoreLabel;
        public Label livesLabel;
        public Form1()
        {
            InitializeComponent();
            scoreLabel = new Label();
            scoreLabel.Location = new Point((MapController.mapWidth) * 20 + 1, 50);

            livesLabel = new Label();
            livesLabel.Location= new Point((MapController.mapWidth) * 20 + 1, 100);

            this.Controls.Add(scoreLabel);
            this.Controls.Add(livesLabel);

            timer1.Tick += new EventHandler(update);
            this.KeyUp += new KeyEventHandler(inputCheck);
            Init();
        }
        private void inputCheck(object sender, KeyEventArgs e)
        {
            map.map[player.platformY, player.platformX] = 0;
            map.map[player.platformY, player.platformX + 1] = 0;
            map.map[player.platformY, player.platformX + 2] = 0;
            switch (e.KeyCode)
            {
                case Keys.Right:
                    if (player.platformX + 2 < MapController.mapWidth - 1)
                        player.platformX++;
                    break;
                case Keys.Left:
                    if (player.platformX > 0)
                        player.platformX--;
                    break;
            }
            map.map[player.platformY, player.platformX] = 9;
            map.map[player.platformY, player.platformX + 1] = 99;
            map.map[player.platformY, player.platformX + 2] = 999;
        }
        public void update(object sender, EventArgs e)
        {
            if (player.ballY + player.dirY > MapController.mapHeight-1)
            {
                player.lives--;
                if (player.lives <= 0)
                    GameOver();
                else Continue();
            }
            map.map[player.ballY, player.ballX] = 0;
            if (!physics.IsCollide(player, map, scoreLabel))
                player.ballX += player.dirX;
            if(!physics.IsCollide(player, map, scoreLabel))
                player.ballY += player.dirY;
            map.map[player.ballY, player.ballX] = 8;
                        
            if (player.score % 50 == 0 && player.score > 0) 
            {
                if (player.heartY <= MapController.mapHeight - 2)
                {
                    map.map[player.heartY, player.heartX] = 0;
                    player.heartY++;
                    map.map[player.heartY, player.heartX] = 7;
                    if (map.map[player.heartY, player.heartX] == 9 || map.map[player.heartY, player.heartX] == 99 || map.map[player.heartY, player.heartX] == 999)
                    {
                        player.lives++;
                        map.map[player.heartY, player.heartX] = 0;
                    }
                }
                if (player.heartY > MapController.mapHeight - 2)
                {
                    map.map[player.heartY, player.heartX] = 0;
                }
            }

            map.map[player.platformY, player.platformX] = 9;
            map.map[player.platformY, player.platformX + 1] = 99;
            map.map[player.platformY, player.platformX + 2] = 999;

            Invalidate();
        }
        public void GeneratePlatforms()
        {
            Random r = new Random();
            for(int i = 0; i < MapController.mapHeight / 3; i++)
            {
                for(int j = 0; j < MapController.mapWidth; j+=2)
                {
                    int currPlatform = r.Next(1, 5);
                    map.map[i, j] = currPlatform;
                    map.map[i, j + 1] = currPlatform + currPlatform * 10;
                }
            }
        }
        public void Continue()
        {
            timer1.Interval = 40;
            scoreLabel.Text = "Score: " + player.score;
            livesLabel.Text = "Lives: " + player.lives;
            map.map[player.heartY, player.heartX] = 0;
            map.map[player.platformY, player.platformX] = 9;
            map.map[player.platformY, player.platformX + 1] = 99;
            map.map[player.platformY, player.platformX + 2] = 999;
            map.map[player.ballY, player.ballX] = 0;

            player.ballY = player.platformY - 1;
            player.ballX = player.platformX + 1;

            map.map[player.ballY, player.ballX] = 8;

            player.dirX = 1;
            player.dirY = -1;

            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
        }
        public void Init()
        {
            map = new MapController();
            player = new Player();
            physics = new Physics2DController();

            this.Width = (MapController.mapWidth + 5) * 20;
            this.Height = (MapController.mapHeight + 2) * 20;

            timer1.Interval = 40;

            player.score = 0;
            player.lives = 5;
            scoreLabel.Text = "Score: " + player.score;
            livesLabel.Text = "Lives: " + player.lives;

            for (int i = 0; i < MapController.mapHeight; i++)
            {
                for(int j = 0; j < MapController.mapWidth; j++)
                {
                    map.map[i, j] = 0;
                }
            }

            player.platformX = (MapController.mapWidth - 1) / 2;
            player.platformY = MapController.mapHeight - 2;

            map.map[player.platformY, player.platformX] = 9;
            map.map[player.platformY, player.platformX + 1] = 99;
            map.map[player.platformY, player.platformX + 2] = 999;

            player.ballY = player.platformY - 1;
            player.ballX = player.platformX + 1;
           
            map.map[player.ballY, player.ballX] = 8;
            map.map[player.heartY, player.heartX] = 0;

            player.heartY = 10;
            Random r = new Random();
            player.heartX = r.Next(0, 20);
            
            player.dirX = 1;
            player.dirY = -1;
            GeneratePlatforms();
            
            timer1.Start();
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            map.DrawArea(e.Graphics);
            map.DrawMap(e.Graphics);
        }
        public void GameOver()
        {
            timer1.Stop();
            DialogResult result = MessageBox.Show("Play again?", "Game Over", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                Init();
            else
                Application.Exit();
        }
    }
}
