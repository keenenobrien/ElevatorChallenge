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
  /// Queue that manages elevator floor requests.
  /// </summary>
  Queue<int> FloorRequests { get; set; }

  /// <summary>
  /// Moves the elevator to a specified floor.
  /// </summary>
  /// <param name="floor">The floor to move to.</param>
  void MoveToFloor(int floor);

  /// <summary>
  /// Handles the state of the elevator.
  /// </summary>
  /// <param name="state">The state of the elevator.</param>
  void SetState(ElevatorState state);

  /// <summary>
  /// Handles the state of the elevator.
  /// </summary>
  /// <param name="state">The state of the elevator.</param>
  void SetDirection(ElevatorDirection direction);

  /// <summary>
  /// Handles the amount of people entering the elevator.
  /// </summary>
  /// <param name="amountEntering">The total amount of people entering the elevator.</param>
  void HandlePeopleEntering(int amountEntering);

  /// <summary>
  /// Handles the amount of people exiting the elevator.
  /// </summary>
  /// <param name="amountExiting">The total amount of people exiting
  void HandlePeopleExiting(int amountExiting);

  /// <summary>
  /// Adds a floor request to the elevator's queue.
  /// </summary>
  /// <param name="floor">The floor to request.</param>
  void AddFloorRequest(int floor);

  /// <summary>
  /// Moves the elevator based on the floor requests in the queue.
  /// </summary>
  void Move();

  /// <summary>
  /// Sorts the queue of floor requests
  /// </summary>
  void SortFloorRequests();
}
