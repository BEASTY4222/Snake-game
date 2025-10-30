using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using Color = Raylib_cs.Color;
using Rectangle = Raylib_cs.Rectangle;


namespace raylibShenanigans
{
    internal class Player
    {
        private const int MOVE_FOWARD = 50;
        //Raylib.LoadImage("C:\\Users\\USER69\\Desktop\\11B IG\\Informatik\\C#\\raylibShenanigans\\snakeHead.png");
        //Raylib.LoadImage("C:\\Users\\USER69\\Desktop\\11B IG\\Informatik\\C#\\raylibShenanigans\\snakeBody.png");

        // Spirites and textures
        private Image playerSprite;
        private Image bodySprite;
        private Texture2D playerTexture;
        private Texture2D bodyTexture;
        //malbetam po gsm
        // How long the body should be
        private List<int> body;
        private Set headPoses;
        private int posesIndex;
        // For directions so we can rotate the sprite
        private bool facingLeft;
        private bool facingRight;
        private bool facingUp;
        private bool facingDown;
        private bool sittingStill;
        private bool alive;

        // Vars for the sprite a 50x50 square
        private Rectangle playerVars;

        public Player() { }
        public Player(int x, int y) {
            // Textures and sprites
            playerSprite = Raylib.LoadImage("C:\\Users\\IvanSuperPC\\source\\repos\\BEASTY4222\\Snake-game\\snakeHead.png");
            bodySprite = Raylib.LoadImage("C:\\Users\\IvanSuperPC\\source\\repos\\BEASTY4222\\Snake-game\\snakeBody.png");

            playerVars.X = x;
            playerVars.Y = y;
            playerVars.Width = 50;
            playerVars.Height = 50;

            facingLeft = true;
            facingRight = false;
            facingUp = false;
            facingDown = false;
            sittingStill = true;
            alive = true;

            body = new List<int>();// 2 = head 1 = body
            body.Add(2);
            headPoses = new Set();
            posesIndex = 0;
        }
        public void drawPlayer(){
            Raylib.UnloadTexture(bodyTexture);
            Raylib.UnloadTexture(playerTexture);
            
            bodyTexture = Raylib.LoadTextureFromImage(bodySprite);
            playerTexture = Raylib.LoadTextureFromImage(playerSprite);

            for (int j = headPoses.count() - 1, h = 0; j > body.Count - 1; j--, h++)
            {
                if (h == 0)
                    Raylib.DrawTexture(playerTexture, (int)headPoses[j].X, (int)headPoses[j].Y, Color.White);
                else
                    Raylib.DrawTexture(bodyTexture, (int)headPoses[j].X, (int)headPoses[j].Y, Color.White);
            }
        }
        // Score
        public void drawScore()
        {
            Raylib.DrawText("SCORE: " + Convert.ToString(body.Count-1),630,40,35,Color.Black);
        }
        
        // Collision
        private bool checkIfColliding()
        {
            for (int i = 0; i < body.Count; i++)
            {
                if (body.Count > 5)
                {
                    for (int j = headPoses.count() - 1; j > body.Count - 1; j--){
                        return Raylib.CheckCollisionRecs(playerVars,new Rectangle((int)headPoses[j].X, (int)headPoses[j].Y,50,50));
                        //Raylib.DrawTexture(playerTexture, (int)headPoses[j].X, (int)headPoses[j].Y, Color.White);
                    }
                }
            }
            return false;
        }
        public void gameOver(){
            alive = false;
            Raylib.DrawText("YOU DIED", 580, 250, 50, Color.Black);
            Raylib.DrawText("You collided with yourself", 520, 300, 30, Color.Black);

            Raylib.DrawRectangle(630,350,150,50,Color.Gray);
            Raylib.DrawText("Restart",655,365, 25,Color.Black);

            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(),new Rectangle( 630,350,150,50 ))){
                Raylib.DrawRectangle(630, 350, 150, 50, Color.Green);
                Raylib.DrawText("Restart", 655, 365, 25, Color.Black);
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                    reset();
                
            }

        }

        // Eating
        public void eatCherry(GameField gameField)
        {
            if (Raylib.CheckCollisionRecs(this.playerVars,gameField.getCherryVars()))
            {
                body.Add(1);
                gameField.makeNew();
            }
        }

        // Movement 
        public void moveLeft(){
            if (body.Count > 0 && facingRight)
                return;
            playerVars.X -= MOVE_FOWARD;
            if (facingRight == true)
            {
                    Raylib.ImageFlipHorizontal(ref playerSprite);
            }
            else if (facingUp == true)
            {
                Raylib.ImageRotateCCW(ref playerSprite);
            }
            else if (facingDown == true)
            {
                Raylib.ImageRotateCW(ref playerSprite);
            }
            facingLeft = true;

            facingRight = false;
            facingUp = false;
            facingDown = false;

        }
        public void moveRight()
        {
            if (body.Count > 0 && facingLeft)
                return;
            playerVars.X += MOVE_FOWARD;
            if (facingLeft == true)
            {
                Raylib.ImageFlipHorizontal(ref playerSprite);
            }else if (facingUp == true)
            {
                Raylib.ImageRotateCW(ref playerSprite);
            }
            else if (facingDown == true)
            {
                Raylib.ImageRotateCCW(ref playerSprite);
            }
            facingRight = true;

            facingLeft = false;
            facingUp = false;
            facingDown = false;
        }
        public void moveUp()
        {
            if (body.Count > 0 && facingDown)
                return;
            playerVars.Y -= MOVE_FOWARD;
            if (facingDown == true)
            {
                Raylib.ImageFlipVertical(ref playerSprite);
            }
            else if (facingRight == true)
            {
                Raylib.ImageRotateCCW(ref playerSprite);
            }
            else if (facingLeft == true)
            {
                Raylib.ImageRotateCW(ref playerSprite);
            }
            facingUp = true;

            facingLeft = false;
            facingRight = false;
            facingDown = false;
        }
        public void moveDown()
        {
            if (body.Count > 0 && facingUp)
                return;
            playerVars.Y += MOVE_FOWARD;
            if (facingUp == true)
            {
                Raylib.ImageFlipVertical(ref playerSprite);
            }
            else if (facingRight == true)
            {
                Raylib.ImageRotateCW(ref playerSprite);
            }
            else if (facingLeft == true)
            {
                Raylib.ImageRotateCCW(ref playerSprite);
            }
            facingDown = true;

            facingLeft = false;
            facingRight = false;
            facingUp = false;
        }
        public void handleMovement(GameField gameField)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.A) || Raylib.IsKeyPressed(KeyboardKey.Left)){
                sittingStill = false;
                moveLeft();
                eatCherry(gameField);
                if (checkIfColliding() || headPoses.getWrongPosesDeath())
                    gameOver();
                handleHeadPoses();
                
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.D) || Raylib.IsKeyPressed(KeyboardKey.Right))
            {
                
                sittingStill = false;
                moveRight();
                eatCherry(gameField);
                if (checkIfColliding() || headPoses.getWrongPosesDeath())
                    gameOver();
                handleHeadPoses();
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.W) || Raylib.IsKeyPressed(KeyboardKey.Up))
            {
                
                sittingStill = false;
                moveUp();
                eatCherry(gameField);
                if (checkIfColliding() || headPoses.getWrongPosesDeath())
                    gameOver();
                handleHeadPoses();

            }
            else if (Raylib.IsKeyPressed(KeyboardKey.S) || Raylib.IsKeyPressed(KeyboardKey.Down))
            {
                
                sittingStill = false;
                moveDown();
                eatCherry(gameField);
                if (checkIfColliding() || headPoses.getWrongPosesDeath())
                    gameOver();
                handleHeadPoses();

            }
            else
            {
                sittingStill = true;
                handleHeadPoses();
            }
                
            
        }
        private void handleHeadPoses()
        {
            if (!sittingStill)
            {
                headPoses.add(new Vector2(playerVars.X, playerVars.Y));
                for (int i = 0; i < body.Count; i++)
                {
                    if (headPoses.count() > body.Count * 2){
                        headPoses.removeAt(0);
                    }
                }
            }
        }

        // Getters
        public Vector2 getV2(){ return new Vector2(playerVars.X,playerVars.Y); }
        public bool getStatus() { return alive; }

        public void reset()
        {
            playerSprite = Raylib.LoadImage("C:\\Users\\IvanSuperPC\\source\\repos\\BEASTY4222\\Snake-game\\snakeHead.png");
            bodySprite = Raylib.LoadImage("C:\\Users\\IvanSuperPC\\source\\repos\\BEASTY4222\\Snake-game\\snakeBody.png");

            playerVars.X = 700;
            playerVars.Y = 500;
            playerVars.Width = 50;
            playerVars.Height = 50;

            facingLeft = true;
            facingRight = false;
            facingUp = false;
            facingDown = false;
            sittingStill = true;
            alive = true;

            body = new List<int>();// 2 = head 1 = body
            body.Add(2);
            headPoses = new Set();
            posesIndex = 0;
        }
    }
}
