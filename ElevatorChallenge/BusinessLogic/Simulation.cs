using ElevatorChallenge.Classes;
using ElevatorChallenge.Enums;
using ElevatorChallenge.Interfaces;

namespace ElevatorChallenge.BusinessLogic;

public class Simulation(Building building)
{
  public void SimulateOneElevator()
  {
    while (true)
    {
      IElevator? elevator;
      int requestFloor = Random.Shared.Next(building.LowestFloor, building.HighestFloor + 1);
      do
      {
        Console.WriteLine($"Requesting elevator to floor {requestFloor}.");
        elevator = building.AssignFloorRequest(requestFloor);
        if (elevator != null)
        {
          do
          {
            elevator = building.GetNearestElevator(requestFloor);
          }
          while (elevator == null || elevator.CurrentFloor != requestFloor);
          try
          {
            elevator.HandlePeopleExiting(Random.Shared.Next(0, elevator.CurrentLoad + 1));
            elevator.HandlePeopleEntering(Random.Shared.Next(1, elevator.MaxCapacity - elevator.CurrentLoad + 1));
            int floorToGoTo = Random.Shared.Next(building.LowestFloor, building.HighestFloor + 1);
            Console.WriteLine($"Elevator {elevator.Id} is now going to floor {floorToGoTo} at passenger request.");
            elevator.AddFloorRequest(floorToGoTo);
            elevator.HandlePeopleExiting(Random.Shared.Next(0, elevator.CurrentLoad + 1));
            break;
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex.Message);
            continue;
          }
        }
        else
        {
          Console.WriteLine("No elevators available for the requested floor.");
          break;
        }
      }
      while (elevator != null);

    }
  }
}
