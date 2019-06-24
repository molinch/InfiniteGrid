# Infinite Grid

Consider an infinite grid of white and black squares. The grid is initially all white and there is a machine in one cell facing right. It will move based on the following rules:

* If the machine is in a white square, turn 90° clockwise and move forward 1 unit;

* If the machine is in a black square, turn 90° counter-clockwise and move forward 1 unit;

* At every move flip the color of the base square.

Implement an application that will receive HTTP PUT requests with a number of steps the simulation should run, always starting from the same conditions, and output the resulting grid to a file. Please provide support documentation

## How to run project

Prerequisites : install dotnet core see --> https://dotnet.microsoft.com/download

To Build and publish run this command at the root of the project :
```bash
dotnet clean && dotnet publish InfiniteGrid.Api  -o ../target
```

Then run and choose your port of your choice
```bash
dotnet InfiniteGrid.Api.dll --server.urls "http://localhost:59480"
```


***
Known Limitations:

