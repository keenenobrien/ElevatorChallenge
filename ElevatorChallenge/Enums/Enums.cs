namespace ElevatorChallenge.Enums;

public enum ElevatorState
{
  Idle = 1,
  Moving = 2
}

public enum ElevatorDirection
{
  Up = 1,
  Down = 2,
  None = 3
}

public enum InputState
{
  CallElevator = 1,
  PeopleExiting = 2,
  PeopleEntering = 3,
  FloorSelection = 4
}
