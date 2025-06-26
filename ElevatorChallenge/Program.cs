using ElevatorChallenge.BusinessLogic;
using ElevatorChallenge.Classes;
using ElevatorChallenge.Enums;

Console.WriteLine("Elevator Challenge is running");

string? input;
Building building = new (0, 10);

UserInput userInput = new(building);
Display display = new(building);

building.AddElevator(new StandardElevator(1));

InputState inputState = InputState.CallElevator;

do
{
  //Display all the elevators and their status
  display.DisplayAllInfo();

  //Request input from the user based on the current state
  userInput.RequestInput(inputState);
  input = Console.ReadLine();
  Console.Clear();

  //Check if the input is valid based on the current state
  if (userInput.CheckInputIsValid(input, inputState))
  {
    //If the input is valid, handle the input based on the current state
    inputState = userInput.HandleInput(input!, inputState);
  }
}
//Exit the app if exit is typed
while (input != "exit");

