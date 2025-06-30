namespace ElevatorChallenge.Interfaces;

public interface IBuilding
{
  /// <summary>
  /// Number of floors in the building.
  /// </summary>
  int NumberOfFloors { get; set; }

  /// <summary>
  /// The lowest floor in the building.
  /// </summary>
  int LowestFloor { get; set; }

  /// <summary>
  /// The highest floor in the building.
  /// </summary>
  int HighestFloor { get; set; }

  /// <summary>
  /// List of floor requests for the building.
  /// </summary>
  Queue<int> BuildingFloorRequests { get; set; }

  /// <summary>
  /// Gets the elevators in the building.
  /// </summary>
  public List<IElevator> Elevators { get; }

  /// <summary>
  /// Adds an elevator to the building.
  /// </summary>
  /// <param name="elevator">The elevator to add.</param>
  /// <param name="lowestFloor">Optional lowest floor for the elevator. If not provided, it defaults to the building's lowest floor.</param>
  /// <param name="highestFloor">Optional highest floor for the elevator. If not provided, it defaults to the building's highest floor.</param>
  public void AddElevator(IElevator elevator, int? lowestFloor = null, int? highestFloor = null);

  /// <summary>
  /// Gets the closest elevator to the floor you are moving to.
  /// </summary>
  /// <param name="floor">The floor you are going to.</param>
  IElevator? GetNearestElevator(int floor);

  /// <summary>
  /// Assigns a floor request to the nearest elevator.
  /// </summary>
  /// <param name="floor">The floor to assign.</param>
  IElevator? AssignFloorRequest(int floor);

  /// <summary>
  /// Processes all floor requests in the queue.
  /// </summary>
  void ProcessFloorRequests();
}
