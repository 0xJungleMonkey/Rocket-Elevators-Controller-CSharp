using System;
using System.Collections.Generic;
namespace Commercial_Controller
{
    public class Elevator
    {
        public Elevator(int _elevatorID, string _status, int _currentFloor, List<int> _floorRequestsList)
        {
            ID = _elevatorID;
            status = _status;
            currentFloor = _currentFloor;
            door = new Door(ID);
            completedRequestsList = new List<int>();
            floorRequestsList = _floorRequestsList;
            direction = default;
        }
        public int ID;
        public string status;
        public int amountOfFloors;
        public int currentFloor;
        public Door door;

        public List<int> completedRequestsList;
        public List<int> floorRequestsList;
        public string direction;
        public void move()
        {
            while (floorRequestsList.Count > 0)
            {   sortFloorList();
                int destination = floorRequestsList[0];
                status = "moving";
               
                if (currentFloor < destination)
                {
                    direction = "up";
                    while (currentFloor < destination)
                    {
                        currentFloor++;
                    }


                }
                else if (currentFloor > destination)
                {
                    direction = "down";
                    while (currentFloor > destination)
                    {
                        currentFloor--;
                    }

                }
                status = "stopped";
                completedRequestsList.Add(destination);
   
                floorRequestsList.RemoveAt(0);

        }
            status = "idle";
        }
        public void sortFloorList()
        {
            if (direction == "up")
            {
                floorRequestsList.Sort();
            }
            else
            {
                floorRequestsList.Reverse();
            }
        }
        public void addNewRequest(int _requestedFloor)
        {
            if (floorRequestsList.Contains(_requestedFloor) == false)
            {
                floorRequestsList.Add(_requestedFloor);
                
            }
            if (currentFloor < _requestedFloor)
            {
                direction = "up";
            }
            if (currentFloor > _requestedFloor)
            {
                direction = "down";
            }
            
        }
    }
}