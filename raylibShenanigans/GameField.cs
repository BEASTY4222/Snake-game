using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace raylibShenanigans
{
    internal class GameField
    {
        private Texture2D appleTexture;
        private Image apple;

        // Vars for the sprite a 50x50 square
        private Rectangle appleVars;

        // Vars for the walls
        private Rectangle topWall;
        private Rectangle bottomWall;
        private Rectangle leftWall;
        private Rectangle rightWall;

        // Constructor
        public GameField(int PosX,int PosY){
            // For school PC 
            //apple = Raylib.LoadImage("C:\\Users\\USER69\\Desktop\\11B IG\\Informatik\\C#\\raylibShenanigans\\apple.png");
            // My PC
            apple = Raylib.LoadImage("C:\\Users\\IvanSuperPC\\source\\repos\\BEASTY4222\\Snake-game\\apple.png");
            // For .exe
            //apple = Raylib.LoadImage("assets\\apple.png");

            // apple vars
            appleVars.X = PosX;
            appleVars.Y = PosY;
            appleVars.Width = 50;
            appleVars.Height = 50;

            // Wall vars
            topWall = new Rectangle(0, 0, 1400, 20);
            bottomWall = new Rectangle(0, 680, 1400, 20);
            leftWall = new Rectangle(0, 0, 20, 700);
            rightWall = new Rectangle(1380, 00, 20, 700);
        }

        // Walls
        public void drawWalls(){
            // Top wall
            Raylib.DrawRectangleRec(topWall, Color.Black);
            // Bottom wall
            Raylib.DrawRectangleRec(bottomWall, Color.Black);
            // Left wall
            Raylib.DrawRectangleRec(leftWall, Color.Black);
            // Right wall
            Raylib.DrawRectangleRec(rightWall, Color.Black);
        }

        // Getters
        public Rectangle getTopWall(){ return topWall; }
        public Rectangle getBottomWall(){ return bottomWall; }
        public Rectangle getLeftWall() {  return leftWall; }
        public Rectangle getRightWall() {  return rightWall; }

        // Cherry stuff down from here

        // Getters
        public Rectangle getAppleVars() { return appleVars; }

        // Draw the cherry
        public void drawApples()
        {
            Raylib.UnloadTexture(appleTexture);
            appleTexture = Raylib.LoadTextureFromImage(apple);
            Raylib.DrawTexture(appleTexture, (int)appleVars.X, (int)appleVars.Y, Color.White);
        }

        // Generate a cherry
        public void makeNew(Set container)
        {
            int randomPosX = 0;
            int randomPosY = 0;
            bool made = false;
            while (!made)
            {
                randomPosX = Raylib.GetRandomValue(2, 13);
                randomPosY = Raylib.GetRandomValue(2, 6);

                List<int> Multipliers = new List<int>() { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

                int randomXMultiplier = Raylib.GetRandomValue(0, 9);
                randomPosX *= Multipliers[randomXMultiplier];

                int randomYMultiplier = Raylib.GetRandomValue(0, 9);
                randomPosY *= Multipliers[randomYMultiplier];

                for (int i = 0; i < container.count(); i++)
                {
                    if (container[i].X == randomPosX && container[i].Y == randomPosY)
                    {
                        break;
                    }
                }

                made = true;
            }


            appleVars.X = randomPosX;
            appleVars.Y = randomPosY;
            appleVars.Width = 50;
            appleVars.Height = 50;
        }
        
    }
}
