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
  /// Gets the elevators in the building.
  /// </summary>
  public List<IElevator> Elevators { get; }

  /// <summary>
  /// Adds an elevator to the building.
  /// </summary>
  /// <param name="elevator">The elevator to add.</param>
  /// <param name="highestFloor">Optional highest floor for the elevator. If not provided, it defaults to the building's highest floor.</param>
  /// <param name="lowestFloor">Optional lowest floor for the elevator. If not provided, it defaults to the building's lowest floor.</param>
  public void AddElevator(IElevator elevator, int? highestFloor = null, int? lowestFloor = null);

  /// <summary>
  /// Gets the closest elevator to the floor you are moving to.
  /// </summary>
  /// <param name="floor">The floor you are going to.</param>
  IElevator GetNearestElevator(int floor);
}
