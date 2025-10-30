using Raylib_cs;
using raylibShenanigans;
using System.Numerics;

class Program
{
    static void Main()
    {
        Raylib.InitWindow(1400, 700, "SNAKE GAME");
        Raylib.SetTargetFPS(60);


        Player player = new Player(700, 550);
        GameField gameField = new GameField(100,400);


        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            if (player.getStatus()){
                player.drawScore();
                player.drawPlayer();
                gameField.drawCherries();

                player.handleMovement(gameField);
            }else
                player.gameOver();
           
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
