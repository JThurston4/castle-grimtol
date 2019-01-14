using System.Collections.Generic;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project.Interfaces
{
  public interface IRoom
  {
    string Name { get; set; }
    string Description { get; set; }
    List<Item> Items { get; set; }
    Dictionary<string, IRoom> Exits { get; set; }
    bool LockedRoom { get; set; }
    bool DoomedRoom { get; set; }
    bool EdgeRoom { get; set; }
    bool FogEdge { get; set; }


    IRoom ChangeRoom(string direction);
  }
}
