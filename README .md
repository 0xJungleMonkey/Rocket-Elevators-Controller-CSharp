# Rocket-Elevators-Csharp-Controller
This is the project for the C# commercial controller. In the Commercial_Controller folder, you will find the necessary files to run tests.

### Installation

As long as you have **.NET 6.0** installed on your computer, nothing more needs to be installed:

The code to run the scenarios is included in the Commercial_Controller folder, and can be executed there with:

`dotnet run <SCENARIO-NUMBER>`

### Running the tests

To launch the tests, make sure to be at the root of the repository and run:

`dotnet test`

With a fully completed project, you should get an output like:

![Screenshot from 2021-06-15 17-31-02](https://github.com/0xJungleMonkey/Rocket-Elevators-Controller-CSharp/blob/e132c600b5af86657d8109aad1586182cb04e138/output1.png)

You can also get more details about each test by adding the `-v n` flag: 

`dotnet test -v n` 

which should give something like: 

![Screenshot from 2021-06-15 18-00-52](https://github.com/0xJungleMonkey/Rocket-Elevators-Controller-CSharp/blob/e132c600b5af86657d8109aad1586182cb04e138/output2.png)
 Good luck!