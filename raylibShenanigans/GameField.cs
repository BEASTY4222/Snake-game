using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace raylibShenanigans
{
    internal class GameField
    {
        // For school PC 
        //private Image cherry = Raylib.LoadImage("C:\\Users\\USER69\\Desktop\\11B IG\\Informatik\\C#\\raylibShenanigans\\cherry.png");
        // My PC
        private Image cherry = Raylib.LoadImage("C:\\Users\\IvanSuperPC\\source\\repos\\BEASTY4222\\Snake-game\\cherry.png");
        private Texture2D cherryTexture;

        // Vars for the sprite a 50x50 square
        private Rectangle CherryVars;

        public GameField(int PosX,int PosY)
        {
            CherryVars.X = PosX;
            CherryVars.Y = PosY;
            CherryVars.Width = 50;
            CherryVars.Height = 50;
        }

        // Getters
        public int getCherryWidth() {  return (int)CherryVars.Width; }
        public int getCherryHeight() {  return (int)CherryVars.Height; }
        public int getCherryPosX() { return (int)CherryVars.X; }
        public int getCherryPosY() { return (int)CherryVars.Y; }
        public Rectangle getCherryVars() { return CherryVars; }
        public void drawCherries()
        {
            Raylib.UnloadTexture(cherryTexture);
            cherryTexture = Raylib.LoadTextureFromImage(cherry);
            Raylib.DrawTexture(cherryTexture, (int)CherryVars.X, (int)CherryVars.Y, Color.White);
        }

        public void makeNew()
        {
            int randomPosX = Raylib.GetRandomValue(10, 1300);
            int randomPosY = Raylib.GetRandomValue(10, 600);

            CherryVars.X = randomPosX;
            CherryVars.Y = randomPosY;
            CherryVars.Width = 50;
            CherryVars.Height = 50;
        }
        
    }
}
