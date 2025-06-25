using ElevatorChallenge.Enums;

namespace ElevatorChallenge.Interfaces;

public interface IElevator
{
  /// <summary>
  /// Unique identifier of the elevator.
  /// </summary>
  int Id { get; set; }

  /// <summary>
  /// Manages the current floor of the elevator.
  /// </summary>
  int CurrentFloor { get; set; }

  /// <summary>
  /// Manages the highest floor of the elevator.
  /// </summary>
  int HighestFloor { get; set; }

  /// <summary>
  /// Manages the lowest floor of the elevator.
  /// </summary>
  int LowestFloor { get; set; }

  /// <summary>
  /// Gets the maximum total capacity of the elevator.
  /// </summary>
  int MaxCapacity { get; }

  /// <summary>
  /// Gets the current load of the elevator.
  /// </summary>
  int CurrentLoad { get; set; }

  /// <summary>
  /// Manages the state of the elevator.
  /// </summary>
  ElevatorState State { get; set; }

  /// <summary>
  /// Gets the number of seconds it takes for the elevator to move one floor.
  /// </summary>
  int SecondsToMoveFloor { get; }

  /// <summary>
  /// Controls the direction of the elevator.
  /// </summary>
  ElevatorDirection MoveDirection { get; set; }

  /// <summary>
  /// Moves the elevator to a specified floor.
  /// </summary>
  /// <param name="floor">The floor to move to.</param>
  void MoveToFloor(int floor)
  {
    if (floor < LowestFloor)
    {
      throw new ArgumentOutOfRangeException(nameof(floor), $"Floor cannot be below the lowest floor ({LowestFloor}).");
    }

    if (floor > HighestFloor)
    {
      throw new ArgumentOutOfRangeException(nameof(floor), $"Floor cannot be above the highest floor ({HighestFloor}).");
    }

    if (CurrentFloor == floor)
    {
      Console.WriteLine($"Already on floor {CurrentFloor}. No movement needed.");
      return;
    }

    MoveDirection = floor > CurrentFloor ? ElevatorDirection.Up : ElevatorDirection.Down;

    int numberOfFloorsToMove = Math.Abs(CurrentFloor - floor);

    Console.WriteLine($"Moving {numberOfFloorsToMove} floor(s) from floor {CurrentFloor} to floor {floor}.");
    //Console.WriteLine($"Movement will take {numberOfFloorsToMove * SecondsToMoveFloor} seconds.");
    CurrentFloor = floor;
    SetState(ElevatorState.Moving);
    //Task.Delay(numberOfFloorsToMove * SecondsToMoveFloor * 1000).Wait(); // Simulate time taken to move
    Console.WriteLine($"Arrived at floor {CurrentFloor}.");
    SetState(ElevatorState.Idle);
  }

  /// <summary>
  /// Handles the state of the elevator.
  /// </summary>
  /// <param name="state">The state of the elevator.</param>
  private void SetState(ElevatorState state)
  {
    State = state;
    Console.WriteLine($"Elevator is now {State}.");
  }

  /// <summary>
  /// Handles the amount of people entering the elevator.
  /// </summary>
  /// <param name="amountEntering">The total amount of people entering the elevator.</param>
  void HandlePeopleEntering(int amountEntering)
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
    Console.WriteLine($"{amountEntering} person(s) entered the elevator. Current load is now {CurrentLoad}.");
  }

  /// <summary>
  /// Handles the amount of people exiting the elevator.
  /// </summary>
  /// <param name="amountExiting">The total amount of people exiting
  void HandlePeopleExiting(int amountExiting)
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
    Console.WriteLine($"{amountExiting} person(s) exited the elevator. Current load is now {CurrentLoad}.");
  }
}
