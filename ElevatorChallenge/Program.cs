using ElevatorChallenge.BusinessLogic;
using ElevatorChallenge.Classes;

Console.WriteLine("Elevator Challenge is running");

Building building = new (0, 10);
building.AddElevator(new StandardElevator(1, building));
building.AddElevator(new StandardElevator(2, building));
building.AddElevator(new StandardElevator(3, building), 10, 5);

UserInput userInput = new(building);
Simulation simulation = new(building);

userInput.UserInputProcess();

// simulation.SimulateOneElevator();
