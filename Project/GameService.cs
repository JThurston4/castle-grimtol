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
      string fog = "an ominous fog blocks your view.";
      string inFog = "You have sailed into the thick fog and lost all visibility. In the distance the sound of lightning cracks through the air. The waters are shifting rapidly and your crew looks nervous. You hear a few of them whisper among themselves 'surely there is nothing worth the risk of these waters'.";
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
      Room G6 = new Room("G6", $"{onIsland} To the West you see a barrell floating in the sea. The water over East {rough} Looking further East you see a massive warship, its sails are black with a laughing Jolly Roger. {OpenSea} North and South.");
      Room D5 = new Room("D5", $"{onIsland} The water North and West {rough} {OpenSea} East and South.");
      Room E5 = new Room("E5", $"{empty} The water North {rough}, to the West {island}, and you see {openSea} to the East and South.");
      Room F5 = new Room("F5", $"{empty} To the North the water {rough}. Looking South {island}. {OpenSea} East and West.");
      Room G5 = new Room("G5", $"{empty} The water to the North and East {rough}. Down South {island} and {openSea} to the West.");
      Room A8 = new Room("A8", $"{empty} A faint glimmer catches your eye toward the East. {OpenSea} North. Both West and South {edge}");
      Room B8 = new Room("B8", $"You stumble upon a bottle floating in the middle of the sea. To the North {island} and {openSea} East and West. Looking South {edge}");
      Room C8 = new Room("C8", $"{empty} Looking East {rough2} A faint glimmer catches your eye toward the West. {OpenSea} North however over South {edge}");
      Room H8 = new Room("H8", $"{empty} To the North {island} and to the West {rough2}. Towards East and South {edge}");
      Room A7 = new Room("A7", $"{empty} You can faintly see a pirate ship over North. Upon closer inspection it the sails appear to be yellow with a smiling Jolly Roger. To the East {island} and {openSea} South. Looking West {edge}");
      Room B7 = new Room("B7", $"{onIsland} A faint glimmer catches your eye toward the South. {OpenSea} North, East, and West.");
      Room C7 = new Room("C7", $"{empty} To the East {rough2} Toward the West {island}. Looking North you see bizarre creatures bobbing at the water. {OpenSea} South");
      Room H7 = new Room("H7", $"{onIsland} A massive warship looms North, its sails are black with a laughing Jolly Roger. Etched on its side you read the words 'Queen Anne's Revenge'. The sheer sight of it fills you with dread. To the West {rough2} {OpenSea} South and looking East {edge}");
      Room A6 = new Room("A6", $"You have made contact with pirate ship Happy Delivery. Its captain George Lowther appears to want nothing more than to sink your vessel and plunder anything that remains. To the North {island} and {openSea} East and South. Over West {edge}");
      Room B6 = new Room("B6", $"{empty} You can faintly see a pirate ship over West. Upon closer inspection it the sails appear to be yellow with a smiling Jolly Roger. Down South {island}. Looking East you see bizarre creatures bobbing at the water and {openSea} North.");
      Room C6 = new Room("C6", $"You encounter some of the most grotesque creatures you've ever seen. From the waist down their body appears to be human and above the waist is fish. The inefficient design of their bodies is making it difficult for them to swim but as you see the mouth of one open you notice multipe rows of razor sharp teeth. To the East {rough2} {OpenSea} North, South, and West.");
      Room H6 = new Room("H6", $"You are now face to face with The Queen Anne's Revenge. Captained by the fearsome pirate Edward Teach this monstrous vessel knows no defeat. Outfitted with 40 cannons all pointing towards The Drowning Whale, you know escape is to late. You must fight if you want any chance of survival no matter how slim. To the East {edge} Perhaps sailing toward it would be a more merciful death...");
      Room A5 = new Room("A5", $"{onIsland} To the North {fog} You can faintly see a pirate ship down South. Upon closer inspection it the sails appear to be yellow with a smiling Jolly Roger. {OpenSea} East. Looking West {edge}");
      Room B5 = new Room("B5", $"{empty} To the North {fog} Looking West {island} and {openSea} East and South.");
      Room C5 = new Room("C5", $"{empty} Looking South you see bizarre creatures bobbing at the water. Towards the East {rough2} {OpenSea} North and West.");
      Room H5 = new Room("H5", $"{empty} A massive warship looms South, its sails are black with a laughing Jolly Roger. Etched on its side you read the words 'Queen Anne's Revenge'. The sheer sight of it fills you with dread. To the North {island}. Towards the West {rough2} Looking East {edge}");
      Room A4 = new Room("A4", $"{inFog}");
      Room B4 = new Room("B4", $"{inFog}"); ;
      Room C4 = new Room("C4", $"{inFog}");
      Room D4 = new Room("D4", $"{empty} To the West {fog} Down South {rough2} {OpenSea} North and East.");
      Room E4 = new Room("E4", $"{empty} In the distance ");
      Room F4 = new Room("F4", $"");
      Room G4 = new Room("G4", $"");
      Room H4 = new Room("H4", $"");

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