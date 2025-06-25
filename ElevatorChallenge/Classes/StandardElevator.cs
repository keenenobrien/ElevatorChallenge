using ElevatorChallenge.Enums;
using ElevatorChallenge.Interfaces;

namespace ElevatorChallenge.Classes;

public class StandardElevator(int id) : IElevator
{
  public int Id { get; set; } = id;
  public int CurrentFloor { get; set; }
  public int MaxCapacity { get; } = 20; // Maximum people capacity
  public int CurrentLoad { get; set; } = 0; // Default to no people on the elevator
  public ElevatorState State { get; set; } = ElevatorState.Idle; // Default state
  public int SecondsToMoveFloor { get; } = 5; // Time in seconds to move one floor
  public int HighestFloor { get; set; }
  public int LowestFloor { get; set; }
  public ElevatorDirection MoveDirection { get; set; } = ElevatorDirection.None; // Default direction
}
