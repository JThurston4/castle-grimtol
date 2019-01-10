using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    public IRoom CurrentRoom { get => set =>  }
    public Player CurrentPlayer { get => set =>  }
    IRoom IGameService.CurrentRoom { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    Player IGameService.CurrentPlayer { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void GetUserInput()
    {

    }

    public void Go(string direction)
    {

    }

    public void Help()
    {

    }

    public void Inventory()
    {

    }

    public void Look()
    {

    }

    public void Quit()
    {

    }

    public void Reset()
    {

    }

    public void Setup()
    {
      string empty = "The water seems calm, not much here.";
      string rough = "seems extremely rough, caution is required";
      string rough2 = "the waters seem extremely rough but would pose no threat to you and your skilled crew.";
      string openSea = "nothing but open seas to the";
      string OpenSea = "Nothing but open seas to the";
      string island = "you see a small island";
      string onIsland = "You sail up to a small island, there appears to be someone on the island waving as you draw near.";
      string edge = "the water drops straight down with nothing but open air beyond.  This appears to be the edge of the world.";
      //create instances of rooms and items and everything youll encounter during gameplay
      Room F7 = new Room("F7", "The water seems calm, not much here. To the North you see a barrell floating in the water, open sea surrounds you to the East, South, and West.");
      Room G7 = new Room("G7", $"{empty} To the North you see a small island, the water to the east {rough}, {openSea} south and west.");
      Room E7 = new Room("E7", $"{empty} You see a ship to the West with a ragged looking green skull on it's sails.  To the South you see a small island, {openSea} North and East.");
      Room F8 = new Room("F8", $"{empty} To the South {edge} To the West {island}, {openSea} to the North and East.");
      Room F6 = new Room("F6", $"You see a barrell floating in the ocean, to the East {island}. {OpenSea} North, West, and South.");
      Room D7 = new Room("D7", $"You come face to face with the infamous Flying Dutchmen!  This ghostly crew doesnt look to happy to see you. To the West the water {rough}. {OpenSea} North, East and South.");
      Room D8 = new Room("D8", $"{empty} You see a ship to the North with a ragged looking green skull on it's sails.  To the East {island}.  The water to the West {rough}. When you look South {edge}");
      Room E8 = new Room("E8", $"{onIsland} {OpenSea} North, East, and West.  When you look South {edge}");
      Room G8 = new Room("G8", $"{empty} To the East the water {rough}. {OpenSea} North and West. When you look South {edge}");
      Room D6 = new Room("D6", $"{empty} You see a ship to the South with a ragged looking green skull on it's sails. To the West the water {rough}. Looking North {island} and {openSea} East.");
      Room E6 = new Room("E6", $"{empty} To the East you see a barrell floating in the sea.  {OpenSea} North, South, and West.");
      Room G6 = new Room("G6", $"{onIsland} To the West you see a barrell floating in the sea. The water over East {rough}. {OpenSea} North and South.");
      Room D5 = new Room("D5", $"{onIsland} The water North and West {rough} {OpenSea} East and South.");
      Room E5 = new Room("E5", $"{empty} The water North {rough}, to the West {island}, and you see {openSea} to the East and South.");
      Room F5 = new Room("F5", $"{empty} To the North the water {rough}. Looking South {island}. {OpenSea} East and West.");
      Room G5 = new Room("G5", $"{empty} The water to the North and East {rough}. Down South {island} and {openSea} to the West.");
      Room A8 = new Room("A8", $"{empty} A faint glimmer catches your eye toward the East. {OpenSea} North. Both West and South {edge}");
      Room B8 = new Room("B8", $"You stumble upon a bottle floating in the middle of the sea. To the North {island} and {openSea} East and West. Looking South {edge}");
      Room C8 = new Room("C8", $"{empty} Looking East {rough2} A faint glimmer catches your eye toward the West. {OpenSea} North however over South {edge}");
      Room H8 = new Room("H8", $"{empty} To the North {island} and to the West {rough2}. Towards East and South {edge}");
      Room A7 = new Room("")

      F7.Exits.Add(G7);
      //add items to rooms
      //add rooms to room's exits
    }

    public void StartGame()
    {
      //Setup()
      //while(playing)
      //draw room description
      //GetUserInput()
      //...
    }

    public void TakeItem(string itemName)
    {

    }

    public void UseItem(string itemName)
    {

    }
  }
}