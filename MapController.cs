using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    class MapController
    {
        public Image gameSet;

        public const int mapWidth = 20;
        public const int mapHeight = 28;
        public int[,] map = new int[mapHeight, mapWidth];
        public MapController()
        {
            gameSet = new Bitmap("C:\\Users\\Камилла\\Desktop\\game\\gameset.png");
        }
       

        public void AddLine()
        {
            for (int i = MapController.mapHeight - 2; i > 0; i--)
            {
                for (int j = 0; j < MapController.mapWidth; j += 2)
                {
                    map[i, j] = map[i - 1, j];
                }
            }
            Random r = new Random();
            for (int j = 0; j < MapController.mapWidth; j += 2)
            {
                int currPlatform = r.Next(1, 5);
                map[0, j] = currPlatform;
                map[0, j + 1] = currPlatform + currPlatform * 10;
            }
        }
        public void DrawMap(Graphics g)
        {
            for (int i = 0; i < MapController.mapHeight; i++)
            {
                for (int j = 0; j < MapController.mapWidth; j++)
                {
                    if (map[i, j] == 9)
                    {
                        g.DrawImage(gameSet, new Rectangle(new Point(j * 20, i * 20), new Size(60, 20)), 560, 24, 205, 80, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 8)
                    {
                        g.DrawImage(gameSet, new Rectangle(new Point(j * 20, i * 20), new Size(20, 20)), 1135, 773, 100, 100, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 1)
                    {
                        g.DrawImage(gameSet, new Rectangle(new Point(j * 20, i * 20), new Size(40, 20)), 30, 25, 240, 80, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 2)
                    {
                        g.DrawImage(gameSet, new Rectangle(new Point(j * 20, i * 20), new Size(40, 20)), 30, 560, 240, 70, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 3)
                    {
                        g.DrawImage(gameSet, new Rectangle(new Point(j * 20, i * 20), new Size(40, 20)), 30, 660, 240, 70, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 4)
                    {
                        g.DrawImage(gameSet, new Rectangle(new Point(j * 20, i * 20), new Size(40, 20)), 30, 770, 240, 70, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 5)
                    {
                        g.DrawImage(gameSet, new Rectangle(new Point(j * 20, i * 20), new Size(40, 20)), 30, 450, 240, 70, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 7)
                    {
                        g.DrawImage(gameSet, new Rectangle(new Point(j * 20, i * 20), new Size(20, 20)), 1134, 650, 100, 100, GraphicsUnit.Pixel);
                    }
                }
            }
        }
        public void DrawArea(Graphics g)
        {
            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, MapController.mapWidth * 20, MapController.mapHeight * 20));
        }

    }
}
