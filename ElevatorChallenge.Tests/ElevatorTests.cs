using ElevatorChallenge.Classes;
using ElevatorChallenge.Enums;
using ElevatorChallenge.Interfaces;

namespace ElevatorChallenge.Tests;

public class ElevatorTests
{
  readonly Building building = new(0, 10);
  readonly IElevator elevator;
  public ElevatorTests()
  {
    building.AddElevator(new StandardElevator(1, building)
    {
      SecondsToMoveFloor = 0,
    });
    elevator = building.GetNearestElevator(5) ?? throw new InvalidOperationException("No elevator found for the given floor.");
  }

  [Fact]
  public void GIVEN_ValidFloor_WHEN_MoveToFloor_THEN_ElevatorMovesToFloor()
  {
    // Arrange
    int floor = 7;

    // Act
    elevator.AddFloorRequest(floor);

    // Assert
    Assert.Equal(floor, elevator.CurrentFloor);
    Assert.Equal(ElevatorDirection.None, elevator.MoveDirection);
    Assert.Equal(ElevatorState.Idle, elevator.State);
  }

  [Fact]
  public void GIVEN_FloorBelowLowest_WHEN_MoveToFloor_THEN_ThrowsArgumentOutOfRangeException()
  {
    // Arrange
    int floor = elevator.LowestFloor - 3;

    // Act and Assert
    var exception = Assert.Throws<ArgumentOutOfRangeException>(() => elevator.AddFloorRequest(floor));
    Assert.Equal("floor", exception.ParamName);
  }

  [Fact]
  public void GIVEN_ValidNumberOfPeopleEntering_WHEN_HandlePeopleEntering_THEN_ElevatorLoadIncreases()
  {
    // Arrange
    int numberOfPeopleEntering = 5;

    // Act
    elevator.HandlePeopleEntering(numberOfPeopleEntering);

    // Assert
    Assert.Equal(numberOfPeopleEntering, elevator.CurrentLoad);
  }

  [Fact]
  public void GIVEN_ExceedingMaxCapacity_WHEN_HandlePeopleEntering_THEN_ThrowsInvalidOperationException()
  {
    // Arrange
    int numberOfPeopleEntering = elevator.MaxCapacity + 7;

    // Act and Assert
    var exception = Assert.Throws<InvalidOperationException>(() => elevator.HandlePeopleEntering(numberOfPeopleEntering));
    Assert.Equal($"{numberOfPeopleEntering} person(s) cannot enter the elevator. Maximum capacity is {elevator.MaxCapacity}.", exception.Message);
  }

  [Fact]
  public void GIVEN_ValidNumberOfPeopleExiting_WHEN_HandlePeopleExiting_THEN_ElevatorLoadDecreases()
  {
    // Arrange
    int CurrentLoad = 10;
    elevator.CurrentLoad = CurrentLoad;
    int numberOfPeopleExiting = 3;

    // Act
    elevator.HandlePeopleExiting(numberOfPeopleExiting);

    // Assert
    Assert.Equal(CurrentLoad - numberOfPeopleExiting, elevator.CurrentLoad);
  }

  [Fact]
  public void GIVEN_InvalidNumberOfPeopleExiting_WHEN_HandlePeopleExiting_THEN_ThrowsInvalidOperationException()
  {
    // Arrange
    int CurrentLoad = 5;
    elevator.CurrentLoad = CurrentLoad;
    int numberOfPeopleExiting = 7;

    // Act and Assert
    var exception = Assert.Throws<InvalidOperationException>(() => elevator.HandlePeopleExiting(numberOfPeopleExiting));
    Assert.Equal($"{numberOfPeopleExiting} person(s) cannot exit the elevator, {CurrentLoad} are currently in the elevator.", exception.Message);
  }
}
