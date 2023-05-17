using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    class Physics2DController
    {
        
        public bool IsCollide(Player player, MapController map, Label scoreLabel)
        {
            bool isColliding = false;
            if (player.ballX + player.dirX > MapController.mapWidth - 1 || player.ballX + player.dirX < 0)
            {
                player.dirX *= -1;
                isColliding = true;
            }
            if (player.ballY + player.dirY < 0)
            {
                player.dirY *= -1;
                isColliding = true;
            }
            if (map.map[player.ballY + player.dirY, player.ballX] != 0)
            {
                bool addScore = false;
                isColliding = true;
                if (map.map[player.ballY + player.dirY, player.ballX] > 10 && map.map[player.ballY + player.dirY, player.ballX] < 99)
                {
                    map.map[player.ballY + player.dirY, player.ballX] = 0;
                    map.map[player.ballY + player.dirY, player.ballX - 1] = 0;
                    addScore = true;
                }
                else if (map.map[player.ballY + player.dirY, player.ballX] < 7)
                {
                    map.map[player.ballY + player.dirY, player.ballX] = 0;
                    map.map[player.ballY + player.dirY, player.ballX + 1] = 0;
                    addScore = true;
                }
                if (addScore)
                {
                    player.score += 5;
                    if (player.score % 50 == 0 && player.score > 0)
                    {
                        map.AddLine();
                    }
                }
                player.dirY *= -1;
            }
            if (map.map[player.ballY, player.ballX + player.dirX] != 0)
            {
                bool addScore = false;
                isColliding = true;

                if (map.map[player.ballY, player.ballX + player.dirX] > 10 && map.map[player.ballY + player.dirY, player.ballX] < 99)
                {
                    map.map[player.ballY, player.ballX + player.dirX] = 0;
                    map.map[player.ballY, player.ballX + player.dirX - 1] = 0;
                    addScore = true;
                }
                else if (map.map[player.ballY, player.ballX + player.dirX] < 7)
                {
                    map.map[player.ballY, player.ballX + player.dirX] = 0;
                    map.map[player.ballY, player.ballX + player.dirX + 1] = 0;
                    addScore = true;
                }
                if (addScore)
                {
                    player.score += 5;
                    if (player.score % 50 == 0 && player.score > 0)
                    {
                        map.AddLine();
                    }
                }
                player.dirX *= -1;
            }
            scoreLabel.Text = "Score: " + player.score;
            return isColliding;
        }
    }
}
