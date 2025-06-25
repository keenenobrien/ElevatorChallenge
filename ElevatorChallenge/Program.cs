using ElevatorChallenge.Classes;
using ElevatorChallenge.Enums;
using ElevatorChallenge.Interfaces;

Console.WriteLine("Elevator Challenge is running");

string? input;
Building building = new (0, 10);

IElevator? currentElevator = null;

building.AddElevator(new StandardElevator(1));

InputState inputState = InputState.CallElevator;

do
{
  //Display all the elevators and their status
  DisplayAllInfo();

  //Request input from the user based on the current state
  RequestInput(inputState);
  input = Console.ReadLine();

  //Check if the input is valid based on the current state
  bool valid = CheckInputIsValid(input, inputState);
  if (valid)
  {
    //If the input is valid, handle the input based on the current state
    inputState = HandleInput(input!, inputState);
  }
}
//Exit the app if exit is typed
while (input != "exit");

void DisplayAllInfo()
{
  Console.WriteLine("-----------------------------------------------------------------------");
  Console.WriteLine("Elevator Information");

  foreach (var elevator in building.Elevators)
  {
    Console.WriteLine("-----------------------------------------------------------------------");
    Console.WriteLine(@$"
    Elevator Number: {elevator.Id}
    Current Floor: {elevator.CurrentFloor}
    State: {elevator.State}
    Direction: {elevator.MoveDirection}
    Current Load: {elevator.CurrentLoad}/{elevator.MaxCapacity}
    ");
    Console.WriteLine("You can type 'exit' to quit the program at any time.");
    Console.WriteLine("-----------------------------------------------------------------------");
  }
}

void RequestInput(InputState inputState)
{
  switch (inputState)
  {
    case InputState.CallElevator:
      Console.WriteLine($"What floor are you calling the elevator from? Please enter a floor from {building.LowestFloor} to {building.HighestFloor}.");
      break;
    case InputState.PeopleExiting:
      Console.WriteLine("How many people are exiting the elevator?");
      break;
    case InputState.PeopleEntering:
      Console.WriteLine("How many people are entering the elevator?");
      break;
    case InputState.FloorSelection:
      Console.WriteLine($"What floor do you want to go to? Please enter a floor from {building.LowestFloor} to {building.HighestFloor}.");
      break;
    default:
      Console.WriteLine("Invalid input state. Something has gone wrong");
      break;
  }
}

InputState HandleInput(string input, InputState state)
{
  try
  {
    switch (state)
    {
      case InputState.CallElevator:
        {
          currentElevator = building.GetNearestElevator(int.Parse(input));
          currentElevator.MoveToFloor(int.Parse(input));
          return InputState.PeopleExiting;
        }
      case InputState.PeopleExiting:
        {
          int exiting = int.Parse(input);
          currentElevator!.HandlePeopleExiting(exiting);
          return InputState.PeopleEntering;
        }
      case InputState.PeopleEntering:
        {
          int entering = int.Parse(input);
          currentElevator!.HandlePeopleEntering(entering);
          return InputState.FloorSelection;
        }
      case InputState.FloorSelection:
        {
          int selectedFloor = int.Parse(input);
          currentElevator!.MoveToFloor(selectedFloor);
          // This is a placeholder for now since there is no way for other people to exit the elevator.
          //This line will assume that everybody who enetered the elevator has exited at the selected floor.
          currentElevator!.HandlePeopleExiting(currentElevator.CurrentLoad);
          return InputState.CallElevator; // Reset to initial state
        }
      default:
        throw new InvalidOperationException("Invalid input state. Something has gone wrong");
    }
  }
  catch (Exception ex)
  {
    Console.WriteLine(ex.Message);
    return state;
  }
}

bool CheckInputIsValid(string? input, InputState state)
{
  try
  {
    if (string.IsNullOrWhiteSpace(input))
    {
      Console.WriteLine("Input cannot be empty. Please try again.");
      return false;
    }
    switch (state)
    {
      //If at this state, the user is inputting what floor they are calling the elevator from.
      //In a real scenario, the user would push the button on the floor they are on so this value will be controlled
      case InputState.CallElevator:
        if (int.TryParse(input, out int _))
        {
          return true;
        }
        else
        {
          Console.WriteLine("Invalid input. Please enter a valid floor number.");
          return false;
        }
      //If at this state, the user is inputting how many people are exiting the elevator.
      case InputState.PeopleExiting:
        if (int.TryParse(input, out int _))
        {
          return true;
        }
        else
        {
          Console.WriteLine("Invalid number of person(s) exiting. Please enter a valid number of people exiting.");
          return false;
        }
      //If at this state, the user is inputting how many people are entering the elevator.
      case InputState.PeopleEntering:
        if (int.TryParse(input, out int _))
        {
          return true;
        }
        else
        {
          Console.WriteLine("Invalid number of person(s) entering. Please enter a valid number of people entering.");
          return false;
        }
      //If at this state, the user is inputting what floor they want to go to.
      //In a real scenario, the user would push the button inside the elevator so this value will be controlled
      case InputState.FloorSelection:
        if (int.TryParse(input, out int _))
        {
          return true;
        }
        else
        {
          Console.WriteLine("Invalid input. Please enter a valid floor number.");
          return false;
        }
      default:
        throw new InvalidOperationException("Invalid input state. Something has gone wrong");
    }
  }
  catch (Exception ex)
  {
    Console.WriteLine($"An error occurred while validating input: {ex.Message}");
    return false;
  }
}
