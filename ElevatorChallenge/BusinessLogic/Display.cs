using System;
using ElevatorChallenge.Classes;

namespace ElevatorChallenge.BusinessLogic;

public class Display(Building building)
{
  public void DisplayAllInfo()
  {
    Console.WriteLine("-----------------------------------------------------------------------");
    Console.WriteLine("Elevator Information");

    foreach (var elevator in building.Elevators)
    {
      Console.WriteLine("-----------------------------------------------------------------------");
      Console.WriteLine(@$"
      Elevator Number: {elevator.Id}
      Current Floor: {elevator.CurrentFloor}
      State: {elevator.State}
      Direction: {elevator.MoveDirection}
      Current Load: {elevator.CurrentLoad}/{elevator.MaxCapacity}
      ");
      Console.WriteLine("You can type 'exit' to quit the program at any time.");
      Console.WriteLine("-----------------------------------------------------------------------");
    }
  }
}
