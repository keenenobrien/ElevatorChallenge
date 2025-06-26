using ElevatorChallenge.Interfaces;

namespace ElevatorChallenge.Classes;

public class Building(int lowestFloor, int highestFloor) : IBuilding
{
  //Number of floors is calculated based on the highest and lowest floors
  public int NumberOfFloors { get; set; } = highestFloor - lowestFloor + 1;
  public int LowestFloor { get; set; } = lowestFloor;
  public int HighestFloor { get; set; } = highestFloor;
  public List<IElevator> Elevators { get; } = [];

  public void AddElevator(IElevator elevator, int? highestFloor = null, int? lowestFloor = null)
  {
    if (elevator == null)
    {
      throw new ArgumentNullException(nameof(elevator), "Elevator cannot be null.");
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

    Elevators.Add(elevator);
  }

  public IElevator GetNearestElevator(int floor)
  {
    return Elevators[0]; // Placeholder, logic to get the nearest elevator will go here.
  }
}
