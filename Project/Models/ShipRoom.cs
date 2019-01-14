using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class ShipRoom : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }
    public int CrewToWin { get; set; }
    public int UpragesToWin { get; set; }
    public bool LockedRoom { get; set; }
    public bool DoomedRoom { get; set; }
    public bool EdgeRoom { get; set; }
    public bool FogEdge { get; set; }

    public ShipRoom(string name, string description, int requiredCrew, int requiredUpgrades, bool lockedroom = false, bool doomedRoom = false)
    {
      Name = name;
      Description = description;
      LockedRoom = lockedroom;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
      CrewToWin = requiredCrew;
      UpragesToWin = requiredUpgrades;
    }

    public IRoom ChangeRoom(string direction)
    {
      IRoom room = Exits[direction];
      if (Exits.ContainsKey(direction) && room.LockedRoom == true)
      {
        System.Console.WriteLine("cant enter");
        return this;
      }
      else
      {
        return Exits[direction];
      }
    }
  }


}