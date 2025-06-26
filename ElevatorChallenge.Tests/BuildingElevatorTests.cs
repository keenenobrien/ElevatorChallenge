using ElevatorChallenge.Classes;

namespace ElevatorChallenge.Tests;

public class BuildingElevatorTests
{
  readonly Building building = new(0, 10);

  [Fact]
  public void GIVEN_ValidElevatorValues_WHEN_AddElevator_THEN_ElevatorAddsSuccessfully()
  {
    // Arrange
    int elevatorId = 1;
    var elevator = new StandardElevator(elevatorId);

    // Act
    building.AddElevator(elevator);

    // Assert
    Assert.Equal(building.LowestFloor, elevator.LowestFloor);
    Assert.Equal(building.HighestFloor, elevator.HighestFloor);
    Assert.Equal(elevatorId, elevator.Id);
  }

  [Fact]
  public void GIVEN_ValidElevatorValuesWithFloorOverride_WHEN_AddElevator_THEN_ElevatorAddsSuccessfullyWithOverrides()
  {
    // Arrange
    int elevatorId = 1;
    var elevator = new StandardElevator(1);
    int customLowestFloor = 2;
    int customHighestFloor = 7;

    // Act
    building.AddElevator(elevator, customHighestFloor, customLowestFloor);

    // Assert
    Assert.Equal(customLowestFloor, elevator.LowestFloor);
    Assert.Equal(customHighestFloor, elevator.HighestFloor);
    Assert.Equal(elevatorId, elevator.Id);
  }

  [Fact]
  public void GIVEN_NullElevator_WHEN_AddElevator_THEN_ThrowsArgumentNullException()
  {
    // Arrange
    StandardElevator? elevator = null;

    // Act and Assert
    var exception = Assert.Throws<ArgumentNullException>(() => building.AddElevator(elevator!));
    Assert.Equal("elevator", exception.ParamName);
  }

  [Fact]
  public void GIVEN_InvalidElevatorId_WHEN_AddElevator_THEN_ThrowsArgumentException()
  {
    // Arrange
    var elevator = new StandardElevator(0); // Invalid ID

    // Act and Assert
    var exception = Assert.Throws<ArgumentException>(() => building.AddElevator(elevator));
    Assert.Equal("elevator", exception.ParamName);
    Assert.Contains("Elevator ID must be a positive integer.", exception.Message);
  }

  [Fact]
  public void GIVEN_HighestFloorOutOfRange_WHEN_AddElevator_THEN_ThrowsArgumentOutOfRangeException()
  {
    // Arrange
    var elevator = new StandardElevator(1);
    int outOfRangeHighestFloor = building.HighestFloor + 5;

    // Act and Assert
    var exception = Assert.Throws<ArgumentOutOfRangeException>(() => building.AddElevator(elevator, outOfRangeHighestFloor));
    Assert.Equal("highestFloor", exception.ParamName);
    Assert.Contains($"Highest floor must be between {building.LowestFloor} and {building.HighestFloor}.", exception.Message);
  }

  [Fact]
  public void GIVEN_LowestFloorOutOfRange_WHEN_AddElevator_THEN_ThrowsArgumentOutOfRangeException()
  {
    // Arrange
    var elevator = new StandardElevator(1);
    int outOfRangeLowestFloor = building.LowestFloor - 3;

    // Act and Assert
    var exception = Assert.Throws<ArgumentOutOfRangeException>(() => building.AddElevator(elevator, lowestFloor: outOfRangeLowestFloor));
    Assert.Equal("lowestFloor", exception.ParamName);
    Assert.Contains($"Lowest floor must be between {building.LowestFloor} and {building.HighestFloor}.", exception.Message);
  }

  [Fact]
  public void GIVEN_HighestFloorLowerThanLowestFloor_WHEN_AddElevator_THEN_ThrowsArgumentException()
  {
    // Arrange
    var elevator = new StandardElevator(1);
    int invalidHighestFloor = 1;
    int lowestFloor = 2;

    // Act and Assert
    var exception = Assert.Throws<ArgumentException>(() => building.AddElevator(elevator, invalidHighestFloor, lowestFloor));
    Assert.Equal("highestFloor", exception.ParamName);
    Assert.Contains("Highest floor cannot be lower than the lowest floor.", exception.Message);
  }
  
  //TODO - Update this test when the GetNearestElevator method is implemented properly
  [Fact]
  public void GIVEN_ValidFloor_WHEN_GetNearestElevator_THEN_ReturnsNearestElevator()
  {
    // Arrange
    int floor = 5;
    var elevator = new StandardElevator(1);
    building.AddElevator(elevator);

    // Act
    var nearestElevator = building.GetNearestElevator(floor);

    // Assert
    Assert.NotNull(nearestElevator);
    Assert.Equal(elevator.Id, nearestElevator.Id);
  }
}
