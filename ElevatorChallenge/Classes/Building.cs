using ElevatorChallenge.Enums;
using ElevatorChallenge.Interfaces;

namespace ElevatorChallenge.Classes;

public class Building(int lowestFloor, int highestFloor) : IBuilding
{
  //Number of floors is calculated based on the highest and lowest floors
  public int NumberOfFloors { get; set; } = highestFloor - lowestFloor + 1;
  public int LowestFloor { get; set; } = lowestFloor;
  public int HighestFloor { get; set; } = highestFloor;
  public List<IElevator> Elevators { get; } = [];
  public Queue<int> BuildingFloorRequests { get; set; } = new Queue<int>();

  public void AddElevator(IElevator elevator, int? highestFloor = null, int? lowestFloor = null)
  {
    if (elevator == null)
    {
      throw new ArgumentNullException(nameof(elevator), "Elevator cannot be null.");
    }

    if (Elevators.Any(e => e.Id == elevator.Id))
    {
      throw new ArgumentException($"An elevator with ID {elevator.Id} already exists in the building.", nameof(elevator));
    }

    if (elevator.Id <= 0)
    {
      throw new ArgumentException("Elevator ID must be a positive integer.", nameof(elevator));
    }

    if (highestFloor.HasValue && (highestFloor < LowestFloor || highestFloor > HighestFloor))
    {
      throw new ArgumentOutOfRangeException(nameof(highestFloor), $"Highest floor must be between {LowestFloor} and {HighestFloor}.");
    }

    if (lowestFloor.HasValue && (lowestFloor < LowestFloor || lowestFloor > HighestFloor))
    {
      throw new ArgumentOutOfRangeException(nameof(lowestFloor), $"Lowest floor must be between {LowestFloor} and {HighestFloor}.");
    }

    if (highestFloor.HasValue && lowestFloor.HasValue && highestFloor < lowestFloor)
    {
      throw new ArgumentException("Highest floor cannot be lower than the lowest floor.", nameof(highestFloor));
    }

    //Set the highest and lowest floors for the elevator if provided, otherwise it uses the building's values.
    elevator.HighestFloor = highestFloor ?? HighestFloor;
    elevator.LowestFloor = lowestFloor ?? LowestFloor;
    elevator.CurrentFloor = elevator.LowestFloor;

    Elevators.Add(elevator);
  }

  public IElevator? GetNearestElevator(int floor)
  {
    // Check if any elevators are available for the requested floor
    var validElevators = Elevators.Where(e => e.LowestFloor <= floor && e.HighestFloor >= floor).ToList();
    if (validElevators.Count == 0)
    {
      throw new InvalidOperationException($"No elevators available for floor {floor}.");
    }

    // Filter elevators that are either idle or moving in the correct direction towards the requested floor
    var elevatorsOnRoute = validElevators.Where(e =>
    {
      if (e.State == ElevatorState.Idle)
      {
        return true;
      }
      else if (e.State == ElevatorState.Moving)
      {
        return e.MoveDirection == ElevatorDirection.Up && e.CurrentFloor <= floor ||
               e.MoveDirection == ElevatorDirection.Down && e.CurrentFloor >= floor;
      }
      return false;
    }).ToList();


    // If there are elevators on route, sort them by their distance to the requested floor
    if (elevatorsOnRoute.Count > 0)
    {
      elevatorsOnRoute.Sort((e1, e2) => Math.Abs(e1.CurrentFloor - floor).CompareTo(Math.Abs(e2.CurrentFloor - floor)));
      return elevatorsOnRoute[0];
    }
    else
    {
      //If there are no valid elevators on route, return null. This will add the floor request to the building queue.
      return null;
    }
  }

  public IElevator? AssignFloorRequest(int floor)
  {
    // Gets the nearest elevator to the requested floor
    var elevator = GetNearestElevator(floor);
    if (elevator == null)
    {
      //Assign the floor request to the building queue if no elevator is available
      BuildingFloorRequests.Enqueue(floor);
      return null;
    }

    if (elevator.CurrentFloor > floor)
    {
      elevator.SetDirection(ElevatorDirection.Down);
    }
    else if (elevator.CurrentFloor < floor)
    {
      elevator.SetDirection(ElevatorDirection.Up);
    }

    // If an elevator is found, add the floor request to its queue
    elevator.AddFloorRequest(floor);
    return elevator;
  }

  public void ProcessFloorRequests()
  {
    /* 
    Process all floor requests in the building queue
    This method is called when an elevator is idle and has no floor requests in its queue.
    */
    if (BuildingFloorRequests.Count == 0)
    {
      Console.WriteLine("No building requests in the queue.");
      return;
    }

    //Retries to assign each floor request in the queue to an elevator
    for (int i = 0; i < BuildingFloorRequests.Count; i++)
    {
      int floor = BuildingFloorRequests.Dequeue();
      AssignFloorRequest(floor);
    }
  }
}
