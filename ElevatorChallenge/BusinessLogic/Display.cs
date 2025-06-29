using ElevatorChallenge.Classes;

namespace ElevatorChallenge.BusinessLogic;

public class Display(Building building)
{
  public void DisplayAllInfo()
  {
    Console.WriteLine("-----------------------------------------------------------------------");
    Console.WriteLine("Elevator Information");
    Console.WriteLine("-----------------------------------------------------------------------");

    foreach (var elevator in building.Elevators)
    {
      Console.WriteLine(@$"
      Elevator Number: {elevator.Id} ({elevator.CurrentLoad}/{elevator.MaxCapacity}) Current Floor: {elevator.CurrentFloor} is now {elevator.State} Direction: {elevator.MoveDirection}
      ");
      Console.WriteLine("-----------------------------------------------------------------------");
    }
      Console.WriteLine("You can type 'exit' to quit the program at any time.");
  }
}
