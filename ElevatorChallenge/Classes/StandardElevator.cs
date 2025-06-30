using ElevatorChallenge.Enums;
using ElevatorChallenge.Interfaces;

namespace ElevatorChallenge.Classes;

public class StandardElevator(int id, Building building) : IElevator
{
  public int Id { get; set; } = id;
  public int CurrentFloor { get; set; }
  public int MaxCapacity { get; } = 20; // Maximum people capacity
  public int CurrentLoad { get; set; } = 0; // Default to no people on the elevator
  public ElevatorState State { get; set; } = ElevatorState.Idle; // Default state
  public int SecondsToMoveFloor { get; set; } = 2; // Time in seconds to move one floor
  public int HighestFloor { get; set; }
  public int LowestFloor { get; set; }
  public ElevatorDirection MoveDirection { get; set; } = ElevatorDirection.None; // Default direction
  public Queue<int> FloorRequests { get; set; } = new Queue<int>(); // Queue for floor requests

  public void MoveToFloor(int floor)
  {
    Console.WriteLine($"Elevator {Id} is now moving from floor {CurrentFloor} to floor {floor}.");
    CurrentFloor = floor;
    Task.Delay(SecondsToMoveFloor * 1000).Wait(); // Simulate time taken to move
  }

  public void SetState(ElevatorState state)
  {
    if (state != State)
    {
      State = state;
      Console.WriteLine($"Elevator {Id} is now {State}.");
    }
  }

  public void SetDirection(ElevatorDirection direction)
  {
    if (direction != MoveDirection)
    {
      MoveDirection = direction;
      if (direction == ElevatorDirection.None)
      {
        Console.WriteLine($"Elevator {Id} has stopped.");
        return;
      }

      Console.WriteLine($"Elevator {Id} is now going {MoveDirection}.");
    }
  }

  public void HandlePeopleEntering(int amountEntering)
  {
    if (amountEntering <= 0)
    {
      throw new ArgumentOutOfRangeException(nameof(amountEntering), "Amount of people entering must be greater than zero.");
    }

    if (CurrentLoad + amountEntering > MaxCapacity)
    {
      throw new InvalidOperationException($"{amountEntering} person(s) cannot enter the elevator. Maximum capacity is {MaxCapacity}.");
    }

    CurrentLoad += amountEntering;
    Console.WriteLine($"{amountEntering} person(s) entered elevator {Id}. Current load is now {CurrentLoad}.");
  }

  public void HandlePeopleExiting(int amountExiting)
  {
    if (amountExiting < 0)
    {
      throw new ArgumentOutOfRangeException(nameof(amountExiting), "Amount of people exiting can't be less than zero.");
    }

    if (amountExiting > CurrentLoad)
    {
      throw new InvalidOperationException($"{amountExiting} person(s) cannot exit the elevator, {CurrentLoad} are currently in the elevator.");
    }

    CurrentLoad -= amountExiting;
    Console.WriteLine($"{amountExiting} person(s) exited elevator {Id}. Current load is now {CurrentLoad}.");
  }

  public void AddFloorRequest(int floor)
  {
    if (floor < LowestFloor || floor > HighestFloor)
    {
      throw new ArgumentOutOfRangeException(nameof(floor), $"Floor {floor} is out of range for this elevator. Valid floors are between {LowestFloor} and {HighestFloor}.");
    }
    if (CurrentFloor == floor)
    {
      Console.WriteLine($"Elevator {Id} is already on floor {CurrentFloor}. No movement needed.");
    }

    if (!FloorRequests.Contains(floor))
    {
      FloorRequests.Enqueue(floor);
      Console.WriteLine($"Floor {floor} added to the request queue for elevator {Id}.");

    }
    else
    {
      Console.WriteLine($"Floor {floor} is already in the request queue for elevator {Id}.");
    }

    Move();
  }

  public void Move()
  {
    if (FloorRequests.Count == 0)
    {
      Console.WriteLine("No floor requests in the queue.");
      //If there are no floor requests, set the elevator to idle and stop moving
      SetState(ElevatorState.Idle);
      SetDirection(ElevatorDirection.None);
      /* 
      The elevator is now idle so it calls the building to process any floor requests that may have been added but weren't assigned to
      any elevators because they were not in the correct state or direction. 
      */
      building.ProcessFloorRequests();
      return;
    }

    //Sort the floor requests before moving so the elevator knows if it needs to stop at any floors along the way to its original destination
    SortFloorRequests();

    //Peek the next floor so we can check if the elevator is already there
    var nextFloor = FloorRequests.Peek();

    if (CurrentFloor == nextFloor)
    {
      FloorRequests.Dequeue();
      Console.WriteLine($"Elevator {Id} has arrived at floor {CurrentFloor}.");
    }

    if (CurrentFloor < nextFloor)
    {
      SetDirection(ElevatorDirection.Up);
      MoveToFloor(CurrentFloor + 1);
    }
    else if (CurrentFloor > nextFloor)
    {
      SetDirection(ElevatorDirection.Down);
      MoveToFloor(CurrentFloor - 1);
    }

    Move();
  }

  public void SortFloorRequests()
  {
    var sortedRequests = new List<int>();
    
    //First, we separate the requests based on the current direction of the elevator
    var firstRequests = FloorRequests.Where(e =>
    {
      if (MoveDirection == ElevatorDirection.Up)
      {
        return e >= CurrentFloor;
      }
      else if (MoveDirection == ElevatorDirection.Down)
      {
        return e <= CurrentFloor;
      }
      return true; // If no direction is set, include all requests
    }).ToList();

    //Sort the first requests based on their distance from the current floor
    firstRequests.Sort((e1, e2) => Math.Abs(CurrentFloor - e1).CompareTo(Math.Abs(CurrentFloor - e2)));

    /* 
    Now we get the remaining requests that are not in the first requests list
    These are the requests that are in the opposite direction of the current direction of the elevator
    These requests would not come from the building assigning them, but would come from the user pressing a button inside the elevator
    that would cause the elevator to go the opposite direction of the current direction.
    */

    var remainingRequests = FloorRequests.Where(e => firstRequests.Contains(e) == false).ToList();

    //Sort the remaining requests based on their distance from the current floor
    remainingRequests.Sort((e1, e2) => Math.Abs(CurrentFloor - e1).CompareTo(Math.Abs(CurrentFloor - e2)));

    sortedRequests.AddRange(firstRequests);
    sortedRequests.AddRange(remainingRequests);

    //Clear the current queue and re-add the sorted requests
    FloorRequests.Clear();
    foreach (var request in sortedRequests)
    {
      FloorRequests.Enqueue(request);
    }
  }
}
