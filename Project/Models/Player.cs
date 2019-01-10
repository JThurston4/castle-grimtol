using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Player : IPlayer
  {
    public string PlayerName { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public List<Item> Inventory { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
  }
}