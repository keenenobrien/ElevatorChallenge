using ElevatorChallenge.Classes;

namespace ElevatorChallenge.Tests;

public class BuildingTests
{
    [Fact]
    public void GIVEN_ValidFloorValues_WHEN_CreateBuilding_THEN_BuildingCreatesSuccessfully()
    {
        // Arrange
        int lowestFloor = 1;
        int highestFloor = 10;
    
        // Act
        var building = new Building(lowestFloor, highestFloor);
    
        // Assert
        Assert.Equal(10, building.NumberOfFloors);
        Assert.Equal(1, building.LowestFloor);
        Assert.Equal(10, building.HighestFloor);
        Assert.Empty(building.Elevators);
    }
}
