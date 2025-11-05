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
        private Texture2D cherryTexture;
        private Image cherry;

        // Vars for the sprite a 50x50 square
        private Rectangle CherryVars;

        // Vars for the walls
        private Rectangle topWall;
        private Rectangle bottomWall;
        private Rectangle leftWall;
        private Rectangle rightWall;

        // Constructor
        public GameField(int PosX,int PosY){
            // For school PC 
            cherry = Raylib.LoadImage("C:\\Users\\USER69\\Desktop\\11B IG\\Informatik\\C#\\raylibShenanigans\\cherry.png");
            // My PC
            //cherry = Raylib.LoadImage("C:\\Users\\IvanSuperPC\\source\\repos\\BEASTY4222\\Snake-game\\cherry.png");
            // For .exe
            //cherry = Raylib.LoadImage("assets\\cherry.png");

            // Cherry vars
            CherryVars.X = PosX;
            CherryVars.Y = PosY;
            CherryVars.Width = 50;
            CherryVars.Height = 50;

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
        public Rectangle getCherryVars() { return CherryVars; }

        // Draw the cherry
        public void drawCherries()
        {
            Raylib.UnloadTexture(cherryTexture);
            cherryTexture = Raylib.LoadTextureFromImage(cherry);
            Raylib.DrawTexture(cherryTexture, (int)CherryVars.X, (int)CherryVars.Y, Color.White);
        }

        // Generate a cherry
        public void makeNew(Set container)
        {
            int randomPosX = Raylib.GetRandomValue(20, 1300);
            int randomPosY = Raylib.GetRandomValue(20, 600);


            Vector2 cherryPos = new Vector2(randomPosX, randomPosY);
            for (int i = 0;i < container.count();i++)
            {
                // FIX CHERY SPAWN ON BODY
            }





            CherryVars.X = randomPosX;
            CherryVars.Y = randomPosY;
            CherryVars.Width = 50;
            CherryVars.Height = 50;
        }
        
    }
}
