using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }
    public bool LockedRoom { get; set; }
    public Room(string name, string description, bool lockedroom = false)
    {
      Name = name;
      Description = description;
      LockedRoom = lockedroom;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
    }

    public IRoom ChangeRoom(string direction)
    {
      IRoom room = Exits[direction];
      if (room.LockedRoom == true)
      {
        System.Console.WriteLine("cant enter");
      }
    }
  }
}