using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Column
    {
        public Column(int _ID, int _amountOfFloors, int _amountOfElevators, List<int> _servedFloors, bool _isBasement)
        {
            ID = _ID;
            amountOfFloors = _amountOfFloors;
            amountOfElevators = _amountOfElevators;
            servedFloorsList = _servedFloors;
            isBasement = _isBasement;
            elevatorsList = new List<Elevator> ();
            callButtonList = new List<CallButton>();
            createElevators(_amountOfFloors,_amountOfElevators);
            createCallButtons(_amountOfFloors, _isBasement);
           
        }
        public int ID;
        public int amountOfFloors;
        public int amountOfElevators;
        public List<int> servedFloorsList;
        public List<Elevator> elevatorsList;
        public List<CallButton> callButtonList;
        public bool isBasement;
        public Elevator elevator;

        public void createCallButtons(int _amountOfFloors, bool _isBasement)
        {
            if (_isBasement){
                int callButtonID = 1;
                int buttonFloor = -1;
                for (int i = 0; i< _amountOfFloors; i++)
                {
                    CallButton callButton= new CallButton(callButtonID, buttonFloor, "up");
                    callButtonList.Add(callButton);
                    buttonFloor++;
                    callButtonID++;
                }
            }
            else 
            {
                int buttonFloor = 1;
                int callButtonID = 1;
                for (int i = 0; i< _amountOfFloors; i++){
                   
                    CallButton callButton = new CallButton(callButtonID, buttonFloor, "down");
                    callButtonList.Add(callButton);
                    buttonFloor++;
                    callButtonID++;
                }
            }
        }
        public void createElevators(int _amountOfFloors, int _amountOfElevators)
        {   int elevatorID = 1;
            for (int i = 0; i< _amountOfElevators; i++){
               
                Elevator elevator= new Elevator(elevatorID, "idle", _amountOfFloors,  new List<int>{1});
                elevatorsList.Add(elevator);
                elevatorID++;
                // Console.WriteLine(elevatorsList.Count);
            }

        }
        //Simulate when a user press a button on a floor to go back to the first floor
        public Elevator requestElevator(int userPosition, string direction)
        {
            elevator = findElevator(userPosition,direction);
            elevator.addNewRequest(userPosition);
            elevator.move();
            return elevator;
        }
        
        public Elevator findElevator(int _requestedFloor, string _requestedDirection){
            Elevator bestElevator = default;
            int bestScore = 6;
            int refereneceGap = 1000000;
            Tuple<Elevator, int, int> bestElevatorInformations = default;
            
            if (_requestedFloor == 1)
            { 
                foreach(Elevator elevator in elevatorsList){
                
                    if (elevator.currentFloor == 1 && elevator.status =="stopped")
                    {
                        bestElevatorInformations = checkIfElevatorIsBetter(1, elevator,bestScore, refereneceGap, bestElevator, _requestedFloor);
                    }
                    else if (elevator.currentFloor == 1 && elevator.status =="idle")
                    {
                        bestElevatorInformations = checkIfElevatorIsBetter(2, elevator,bestScore, refereneceGap, bestElevator, _requestedFloor);
                    }
                    else if (1 > elevator.currentFloor && elevator.direction =="up")
                    {
                        bestElevatorInformations = checkIfElevatorIsBetter(3, elevator,bestScore, refereneceGap, bestElevator, _requestedFloor);
                    }
                    else if (1 < elevator.currentFloor && elevator.direction =="down")
                    {
                       
                        bestElevatorInformations = checkIfElevatorIsBetter(3, elevator,bestScore, refereneceGap, bestElevator, _requestedFloor);
                    }
                    else if (elevator.status =="idle")
                    {
                        bestElevatorInformations = checkIfElevatorIsBetter(4, elevator,bestScore, refereneceGap, bestElevator, _requestedFloor);
                    }
                    else {
                        
                        bestElevatorInformations = checkIfElevatorIsBetter(5, elevator,bestScore, refereneceGap, bestElevator, _requestedFloor);
                    }
                bestElevator = bestElevatorInformations.Item1;
            bestScore = bestElevatorInformations.Item2;
            refereneceGap = bestElevatorInformations.Item3;
            
                }
            }
            else 
            {
                foreach(Elevator elevator in elevatorsList){
                    if (_requestedFloor == elevator.currentFloor && elevator.status =="stopped" && _requestedDirection== elevator.direction)
                    {
                        bestElevatorInformations = checkIfElevatorIsBetter(1, elevator,bestScore, refereneceGap, bestElevator, _requestedFloor);
                    }
                    else if (_requestedFloor > elevator.currentFloor && elevator.direction =="up" && _requestedDirection== "up")
                    {
                        bestElevatorInformations = checkIfElevatorIsBetter(2, elevator,bestScore, refereneceGap, bestElevator, _requestedFloor);
                    }
                    else if (_requestedFloor < elevator.currentFloor && elevator.direction =="down" && _requestedDirection== "down")
                    {
                        bestElevatorInformations = checkIfElevatorIsBetter(2, elevator,bestScore, refereneceGap, bestElevator, _requestedFloor);
                    }
                    else if (elevator.status =="idle")
                    {
                        bestElevatorInformations = checkIfElevatorIsBetter(4, elevator,bestScore, refereneceGap, bestElevator, _requestedFloor);
                    }
                    else 
                    {
                        bestElevatorInformations = checkIfElevatorIsBetter(5, elevator,bestScore, refereneceGap, bestElevator, _requestedFloor);
                    } 
                bestElevator = bestElevatorInformations.Item1;
            bestScore = bestElevatorInformations.Item2;
            refereneceGap = bestElevatorInformations.Item3;
            
                }
            }
            

            return bestElevator;
        }
        public Tuple <Elevator,int,int>  checkIfElevatorIsBetter(int scoreToCheck, Elevator newElevator, int bestScore, int referenceGap, Elevator bestElevator, int floor){
            if (scoreToCheck < bestScore)
            {
                bestScore = scoreToCheck;
                bestElevator = newElevator;
                referenceGap = Math.Abs(newElevator.currentFloor - floor);
            }
            else if (bestScore == scoreToCheck){
                int gap = Math.Abs(newElevator.currentFloor - floor);
                if (referenceGap>gap){
                    bestElevator = newElevator;
                    referenceGap = gap;
                }
            }
            Tuple <Elevator,int,int> bestEleInfo = new Tuple<Elevator,int,int> (bestElevator,bestScore,referenceGap);
            return  bestEleInfo;
        }
        
    }
}