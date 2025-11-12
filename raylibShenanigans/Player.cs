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
        private const int AUTO_MOVE = 5;
        

        // Spirites and textures
        private Image playerSprite;
        private Image bodySprite;
        private Texture2D playerTexture;
        private Texture2D bodyTexture;

        // How long the body should be
        private List<int> body;
        private Set headPoses;
        private List<Vector2> autoPosses;

        // Score
        private int bestScore;

        // For directions so we can rotate the sprite and other sruff
        private bool facingLeft;
        private bool facingRight;
        private bool facingUp;
        private bool facingDown;
        private bool sittingStill;
        private bool alive;
        private bool startedPlaying;
        private string deathMessege;
        private string movementMode;

        // Vars for the sprite a 50x50 square
        private Rectangle playerVars;

        public Player() { }
        public Player(int x, int y) {
            // Textures and sprites

            // For school PC
            playerSprite =  Raylib.LoadImage("C:\\Users\\USER69\\Desktop\\11B IG\\Informatik\\C#\\raylibShenanigans\\snakeHead.png");
            bodySprite = Raylib.LoadImage("C:\\Users\\USER69\\Desktop\\11B IG\\Informatik\\C#\\raylibShenanigans\\snakeBody.png");
            // For my PC
            //playerSprite = Raylib.LoadImage("C:\\Users\\IvanSuperPC\\source\\repos\\BEASTY4222\\Snake-game\\snakeHead.png");
            //bodySprite = Raylib.LoadImage("C:\\Users\\IvanSuperPC\\source\\repos\\BEASTY4222\\Snake-game\\snakeBody.png");
            // For .exe
            //playerSprite = Raylib.LoadImage("assets\\snakeHead.png");
            //bodySprite = Raylib.LoadImage("assets\\snakeBody.png");

            
            playerVars.X = x;
            playerVars.Y = y;
            playerVars.Width = 50;
            playerVars.Height = 50;

            load();
            facingLeft = true;
            facingRight = false;
            facingUp = false;
            facingDown = false;
            sittingStill = true;
            alive = true;
            startedPlaying = false;
            deathMessege = "";
            movementMode = "manual";

            autoPosses = new List<Vector2>();
            body = new List<int>();// 2 = head 1 = body
            body.Add(2);
            headPoses = new Set();
        }
        public void drawPlayer(){
            Raylib.UnloadTexture(bodyTexture);
            Raylib.UnloadTexture(playerTexture);
            
            bodyTexture = Raylib.LoadTextureFromImage(bodySprite);
            playerTexture = Raylib.LoadTextureFromImage(playerSprite);
            if(movementMode == "manual") 
            { 
                for (int j = headPoses.count() - 1, h = 0; j > body.Count - 1; j--, h++)
                {
                    if (h == 0)
                        Raylib.DrawTexture(playerTexture, (int)headPoses[j].X, (int)headPoses[j].Y, Color.White);
                    else
                        Raylib.DrawTexture(bodyTexture, (int)headPoses[j].X, (int)headPoses[j].Y, Color.White);
                } 
            }
            else
            {
                for (int j = 0, h = autoPosses.Count - 1; h > body.Count; j++, h--)
                {
                    if (j == autoPosses.Count - 1)
                    {
                        Raylib.DrawTexture(playerTexture, (int)autoPosses[h].X, (int)autoPosses[h].Y, Color.White);
                    }
                    else
                        Raylib.DrawTexture(bodyTexture, (int)autoPosses[h].X, (int)autoPosses[h].Y, Color.White);
                }
            }
        }
        // Score and start text
        public void drawScore(){
            if (!startedPlaying)
            {
                Raylib.DrawRectangleLines(25, 365, 245, 30, Color.Black);
                Raylib.DrawText("Eat this to gain points", 30, 370, 20, Color.Black);

                Raylib.DrawRectangleLines(500, 230, 425, 250, Color.Black);
                Raylib.DrawText("Move left to start", 520, 250, 40, Color.Black);
                Raylib.DrawText("Rules:", 660, 290, 30, Color.Black);
                Raylib.DrawText("1.Don't move in the opposite ", 550, 320, 25, Color.Black);
                Raylib.DrawText("dirrection of your head", 565, 340, 25, Color.Black);
                Raylib.DrawText("2.Don't ram into your body", 550, 360, 25, Color.Black);
                Raylib.DrawText("3.Don't hit the walls", 550, 380, 25, Color.Black);
                Raylib.DrawText("Press f to switch from", 550, 400, 25, Color.Black);
                Raylib.DrawText("manual to automatic movement", 550, 420, 25, Color.Black);
                Raylib.DrawText("current mode - " + movementMode, 550, 440, 25, Color.Black);

                if (Raylib.IsKeyPressed(KeyboardKey.F))
                {
                    if (movementMode == "manual")
                    {
                        movementMode = "automatic";
                    }
                    else
                    {
                        movementMode = "manual";
                    }
                }
            }
            else
            {
                Raylib.DrawText("SCORE: " + Convert.ToString(body.Count - 1), 630, 40, 35, Color.Black);
                Raylib.DrawText("best score: " + bestScore, 640, 70, 20, Color.Black);
            }
        }
        
        // Collision
        private bool checkIfColliding(GameField gameField)
        {
            for (int i = 0; i < body.Count; i++)
                if(body.Count >= 4)
                    for (int j = headPoses.count() - 1; j >= body.Count()+2; j--)
                        if(Raylib.CheckCollisionRecs(playerVars, new Rectangle((int)headPoses[j].X, (int)headPoses[j].Y, 50, 50)))
                            return true;

            if (Raylib.CheckCollisionRecs(playerVars,gameField.getTopWall()) ||
                Raylib.CheckCollisionRecs(playerVars,gameField.getBottomWall()) ||
                Raylib.CheckCollisionRecs(playerVars,gameField.getLeftWall()) ||
                Raylib.CheckCollisionRecs(playerVars,gameField.getRightWall())
                ){
                deathMessege = "You collided with a wall";
                return true;
            }

            return false;
        }
        public void gameOver(){
            alive = false;
            Raylib.DrawText("YOU DIED", 580, 250, 50, Color.Black);
            if(deathMessege == "")
                Raylib.DrawText("You collided with yourself", 530, 300, 30, Color.Black);
            else
                Raylib.DrawText(deathMessege, 520, 300, 30, Color.Black);

            Raylib.DrawRectangle(630, 350, 150, 50, Color.Gray);
            Raylib.DrawText("Restart",655,365, 25,Color.Black);
            Raylib.DrawText("(or press R)", 663, 385, 13, Color.Black);

            if (Raylib.IsKeyPressed(KeyboardKey.R))
                reset();

            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(),new Rectangle( 630,350,150,50 ))){
                Raylib.DrawRectangle(630, 350, 150, 50, Color.Green);
                Raylib.DrawText("Restart", 655, 365, 25, Color.Black);
                Raylib.DrawText("(or press R)", 663, 385, 13, Color.Black);
                if (Raylib.IsMouseButtonPressed(MouseButton.Left) || Raylib.IsKeyPressed(KeyboardKey.R))
                    reset();
                
            }

        }

        // Eating
        public void eatApple(GameField gameField)
        {
            if (Raylib.CheckCollisionRecs(this.playerVars,gameField.getAppleVars())){
                body.Add(1);
                gameField.makeNew(headPoses);
                if(body.Count > bestScore)
                {
                    bestScore = body.Count - 1;
                }
            }
        }

        // Movement 
        public void moveLeft(bool manual){
            if(manual){
                if (body.Count > 1 && facingRight){
                    alive = false;
                    return;
                }
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
            else
            {
                if (body.Count > 1 && facingRight)
                {
                    alive = false;
                    return;
                }
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
        }
        public void moveRight(bool manual)
        {
            if(manual){
                if (body.Count > 1 && facingLeft){
                    alive = false;
                    return;
                }
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
            else
            {
                if (body.Count > 1 && facingLeft)
                {
                    alive = false;
                    return;
                }
                if (facingLeft == true)
                {
                    Raylib.ImageFlipHorizontal(ref playerSprite);
                }
                else if (facingUp == true)
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
        }
        public void moveUp(bool manual)
        {
            if (manual){ 
                if (body.Count > 1 && facingDown){
                    alive = false;
                    return;
                }
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
            else
            {
                if (body.Count > 1 && facingDown)
                {
                    alive = false;
                    return;
                }
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
        }
        public void moveDown(bool manual)
        {
            if (manual)
            {
                if (body.Count > 1 && facingUp)
                {
                    alive = false;
                    return;
                }
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
            else 
            {
                if (body.Count > 1 && facingUp)
                {
                    alive = false;
                    return;
                }
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
        }
        public void handleMovement(GameField gameField)
        {
            if(movementMode == "manual"){
                if (Raylib.IsKeyPressed(KeyboardKey.A) || Raylib.IsKeyPressed(KeyboardKey.Left))
                {
                    sittingStill = false;
                    moveLeft(true);
                    eatApple(gameField);
                    if (checkIfColliding(gameField) || headPoses.getWrongPosesDeath())
                        gameOver();
                    handleHeadPoses();
                    startedPlaying = true;

                }
                else if (Raylib.IsKeyPressed(KeyboardKey.D) || Raylib.IsKeyPressed(KeyboardKey.Right))
                {

                    sittingStill = false;
                    moveRight(true);
                    eatApple(gameField);
                    if (checkIfColliding(gameField) || headPoses.getWrongPosesDeath())
                        gameOver();
                    handleHeadPoses();
                    startedPlaying = true;
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.W) || Raylib.IsKeyPressed(KeyboardKey.Up))
                {

                    sittingStill = false;
                    moveUp(true);
                    eatApple(gameField);
                    if (checkIfColliding(gameField) || headPoses.getWrongPosesDeath())
                        gameOver();
                    handleHeadPoses();
                    startedPlaying = true;
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.S) || Raylib.IsKeyPressed(KeyboardKey.Down))
                {

                    sittingStill = false;
                    moveDown(true);
                    eatApple(gameField);
                    if (checkIfColliding(gameField) || headPoses.getWrongPosesDeath())
                        gameOver();
                    handleHeadPoses();
                    startedPlaying = true;
                }
                else
                {
                    sittingStill = true;
                    handleHeadPoses();
                }
            }
            else if (movementMode == "automatic")
            {
                if (facingLeft)
                    playerVars.X -= AUTO_MOVE ;
                if (facingRight)
                    playerVars.X += AUTO_MOVE ;
                if (facingUp)
                    playerVars.Y -= AUTO_MOVE ;
                if (facingDown)
                    playerVars.Y += AUTO_MOVE ;
                startedPlaying = true;
                sittingStill = false;
                if (Raylib.IsKeyPressed(KeyboardKey.A) || Raylib.IsKeyPressed(KeyboardKey.Left))
                    moveLeft(false);

                else if (Raylib.IsKeyPressed(KeyboardKey.D) || Raylib.IsKeyPressed(KeyboardKey.Right))
                    moveRight(false);
                    
                else if (Raylib.IsKeyPressed(KeyboardKey.W) || Raylib.IsKeyPressed(KeyboardKey.Up))
                    moveUp(false);
                
                else if (Raylib.IsKeyPressed(KeyboardKey.S) || Raylib.IsKeyPressed(KeyboardKey.Down))
                    moveDown(false);

                eatApple(gameField);
                if (checkIfColliding(gameField) || headPoses.getWrongPosesDeath())
                    gameOver();
                handleHeadPoses();
            }
                
            
        }
        private void handleHeadPoses()
        {
            if (!sittingStill)
            {
                if (movementMode == "manual"){
                    headPoses.add(new Vector2(playerVars.X, playerVars.Y));
                    for (int i = 0; i < body.Count; i++)
                    {
                        if (headPoses.count() > body.Count * 2)
                        {
                            headPoses.removeAt(0);
                        }
                    }
                }
                else
                {
                    if ((facingLeft && headPoses[headPoses.count() - 1].X - 45 > playerVars.X) 
                        || (Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.W) 
                        || Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown(KeyboardKey.S) 
                        || Raylib.IsKeyDown(KeyboardKey.Left) || Raylib.IsKeyDown(KeyboardKey.Right) 
                        || Raylib.IsKeyDown(KeyboardKey.Down) || Raylib.IsKeyDown(KeyboardKey.Up))){
                        headPoses.addAt(new Vector2(playerVars.X, playerVars.Y), headPoses.count() - 1);
                    }else if ((facingRight && headPoses[headPoses.count() - 1].X + 45 < playerVars.X) 
                        || (Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.W)
                        || Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown(KeyboardKey.S)
                        || Raylib.IsKeyDown(KeyboardKey.Left) || Raylib.IsKeyDown(KeyboardKey.Right)
                        || Raylib.IsKeyDown(KeyboardKey.Down) || Raylib.IsKeyDown(KeyboardKey.Up))){
                        headPoses.addAt(new Vector2(playerVars.X, playerVars.Y), headPoses.count() - 1);
                    }else if((facingUp && headPoses[headPoses.count() - 1].Y - 45 > playerVars.Y) 
                        || (Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.W)
                        || Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown(KeyboardKey.S)
                        || Raylib.IsKeyDown(KeyboardKey.Left) || Raylib.IsKeyDown(KeyboardKey.Right)
                        || Raylib.IsKeyDown(KeyboardKey.Down) || Raylib.IsKeyDown(KeyboardKey.Up))){
                        headPoses.addAt(new Vector2(playerVars.X, playerVars.Y), headPoses.count() - 1);
                    }
                    else if((facingDown && headPoses[headPoses.count() - 1].Y + 45 < playerVars.Y) 
                        || (Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.W)
                        || Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown(KeyboardKey.S)
                        || Raylib.IsKeyDown(KeyboardKey.Left) || Raylib.IsKeyDown(KeyboardKey.Right)
                        || Raylib.IsKeyDown(KeyboardKey.Down) || Raylib.IsKeyDown(KeyboardKey.Up))){
                        headPoses.addAt(new Vector2(playerVars.X, playerVars.Y), headPoses.count()-1);
                    }

                    autoPosses.Add(new Vector2(playerVars.X, playerVars.Y));    

                    for (int i = 0; i < body.Count; i++)
                    {
                        if (headPoses.count() > body.Count * 2){
                            headPoses.removeAt(headPoses.count() - 1);
                        }
                        if (autoPosses.Count >= body.Count * 10){
                            autoPosses.RemoveAt(0);
                        }
                    }

                }
                
            }
        }

        // Getters
        public Vector2 getV2(){ return new Vector2(playerVars.X,playerVars.Y); }
        public bool getStatus() { return alive; }

        public void reset()
        {
            // So we are facing the right way at the start
            if (facingRight == true)
                Raylib.ImageFlipHorizontal(ref playerSprite);
            else if (facingUp == true)
                Raylib.ImageRotateCCW(ref playerSprite);
            else if (facingDown == true)
                Raylib.ImageRotateCW(ref playerSprite);


            playerVars.X = 700;
            playerVars.Y = 500;
            playerVars.Width = 50;
            playerVars.Height = 50;

            load();
            facingLeft = false;
            facingRight = false;
            facingUp = false;
            facingDown = false;
            sittingStill = true;
            alive = true;
            startedPlaying = false;
            deathMessege = "";
            movementMode = "manual";

            autoPosses = new List<Vector2>();
            body = new List<int>();// 2 = head 1 = body
            body.Add(2);
            headPoses = new Set();
        }

        // Saving
        public void saveBestScore()
        {   if(body.Count > bestScore)
                File.WriteAllText("C:\\Users\\USER69\\Desktop\\11B IG\\Informatik\\C#\\raylibShenanigans\\data.txt", Convert.ToString(body.Count - 1));
        }

        public void load()
        {
            bestScore = int.Parse(File.ReadAllText("C:\\Users\\USER69\\Desktop\\11B IG\\Informatik\\C#\\raylibShenanigans\\data.txt"));
        }
    }
}
