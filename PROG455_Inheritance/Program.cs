namespace PROG455_Inheritance
{
    internal class Program
    {
        static Game game;
        static void Main(string[] args)
        {
            UI.Print(new string[]
            {
                "------------------------------------",
                "   Welcome to PROG455 Inheritance",
                "------------------------------------"
            });

            UI.Print("Please enter a name: ");
            var name = Console.ReadLine();

            game = new Game(name);
            game.Loop();
        }
    }
}