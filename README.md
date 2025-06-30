# ElevatorChallenge

This project impliments logic to manage an elevator system in a building. It's focus is on evelator efficiency, ensuring that people do not need to wait long for an elevator but also so that elevators do not get stuck servicing requests far away.

# How it works

When a user calls an elevator from a floor, a request is sent to the building. The building will then assign the request to the closest elevator of valid evelators able to accept that request. For an elevator to be able to accept the request it must be idle OR already be at/ on the way to the requested floor.

If no elevators can accept the request. It goes into a request queue for the building which will be processed each time an elevator becomes idle.

Each elevator has a request queue. This queue is sorted each time the elevator moves to ensure that the elevator stops at the correct floors along the way to it's original request.

Each time people get on/off the elevator, the amount of people entering/leaving change, the amount is validated.

**Setting upp a building**

To create a new building and add elevators to it you need to do the following:

Create the building and specify its lowest and highest floors
```cs
Building building = new (0, 10);
```

Add elevators and give each a unique id, this id will be used as the elevator number. (Optionally specify the elevators lowest and highest floors. It will default to the lowest and highest floors of the building if not specified)
```cs
building.AddElevator(new StandardElevator(1, building));
building.AddElevator(new StandardElevator(2, building));
building.AddElevator(new StandardElevator(3, building), 10, 5);
```

This example would create a building with the lowest floor being 0 and the highest being 10. Three elevators will be added. The first two will be able to go from floor 0 to floor 10. The third can only go from floor 5 to floor 10.

The time it takes for an elevator to move from floor to floor is a part of the elevators class. By default it is two seconds. This value can be changed in the elevators class (In this case StandardElevator.cs). Changing this value would change the speed at which the processes below run.

This example is the default building structure used for the processes below.

**User Input Process**

There is a step by step process that allows a user to interact with the elevator system. This project is a console app so some logic was implimented that wouldn't be nessicary in a real world scenario. The step by step process is as follows:

1) The user enters a floor they are calling an elevator from
   1) The floor they have entered is validated according to the elevators' highest and lowest floors.
      1) In a real world scenario this validation would not be needed as the button would be assigned a floor number.
2) The relevand elevator is sent to the users location.
3) The user enters how many people leave the elevator
   1) The number of people they entered to exit is validated according to how many people were in the elevator already. You cannot have more people exit than were inside the elevator.
      1) Normally this is not something a user would enter/control
4) The user enters the number of users that are entering the elevator
   1) The number of people they entered to enter is validated according to how many people were in the elevator already. You cannot have people enter if it would exceed the limit of people.
      1) Normally this is not something a user would enter/control
5) The user eneters the floor that they want to go to.
   1) The floor is validated against the floors that the current elevator can go to.
      1) In a real world scenario, the user would be pressing buttons inside the elevator to decide which floor to go to. The buttons inside would be limited to only the floors the elevator could access. So the user would not enter a value and it wouldn't need to be validated.
6) The elevator moves to the selected floor and all of the people who got on will get off.

To run the user input process, you can uncomment this line in program.cs:
```cs
userInput.UserInputProcess();
```

When this is run, each elevators state is logged before each user input and the console is cleared after every input for a better user experience.

**Simulation**

There is also a simulation which will just move people around a building using elevators. This process works in a golden path' fashion. Where the simulation will never make mistakes on purpose, just to that it keeps running smoothly to show how the system works. This process works as follows:

1) An elevator is called to a floor (The floor selected is random between the minimum and maximum floors in the building)
2) The nearest elevator moves to the requested floor
3) People exit the elevator (A random number from 0 up to the current capacity)
4) People enter the elevator (A random number from 1 up to the maximum capacity of the elevator. This takes into account the number of people inside the elevator currently to ensure the max capacity is not reached.)
5) A floor is selected for the elevator to go to. (The floor selected is random between the min and max floor that the current elevator can reach.)
6) The elevator moves to the selected floor
7) People exit the elevator (A random number from 0 up to the current capacity)

To run the simulation process, you can uncomment this line in program.cs:
```cs
simulation.SimulateOneElevator();
```
When the simulation is run, the elevators will log their states, their movement direction, the number of people entering/exiting etc as these values are changing rather than all the elevators logging their current details. This is so that the log doesn't get filled up with non relevant information and so that it is easier to see what is happening as the simulation runs.

# Testing

This solution includes a project called ElevatorChallenge.Tests. This project contained all the tests related to the ElevatorChallenge project.

# Improvements that could be made

There are a number of improvements that could be made to this solution, including:

1) Elevators should be able to run asyncronously and queues should handle this.
2) The code could be far more modular which would increase it's testability. Currenty there are methods that are not single responsibility.
3) Elevator number could be added as its own field and ID could be used as a unique identifyer. Currently the ID column does both.
4) A better simulation, a simulation could be made to include errors and how the errors are handled. A simulation could be made to show how elevators are capable of running asyncronously.
5) Interfaces could be implimented better. Currently the interfaces are less modular than they should be. and example could be: There could be an interface for maximum and minumum floors since both elevators and buildings have both values.
6) Elevators and buildings could have a list of floors rather than a max and min. This would allow floor management to be more granular.
7) User experience can be better. Adding things like different colors for different logs or different colors for different elevators could make reading the console much easier for the user.
8) The console app could request values from the user to allow them to build their own building with elevators for the simulation they want to run/ the structure they want to test. It could then ask if they want to simulate it or to manually run through it. This would leave all the setup in the users control with no code changes.
