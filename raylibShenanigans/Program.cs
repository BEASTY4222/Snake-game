using Raylib_cs;
using raylibShenanigans;
using System.Numerics;

class Program
{
    static void Main()
    {
        Raylib.InitWindow(1400, 700, "SNAKE GAME");
        Raylib.SetTargetFPS(60);


        Player player = new(700, 550);
        GameField gameField = new(100,400);


        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            if (player.getStatus()){
                player.handleMovement(gameField);

                player.drawScore(gameField);
                player.drawPlayer();
                gameField.drawApples();
                gameField.drawWalls();
            }else{
                player.gameOver();
                player.saveBestScore();
            }
           
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
