using System;
using CastleGrimtol.Project;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.Clear();
      GameService gameService = new GameService();
      gameService.Setup();
      gameService.StartGame();
    }
  }
}
