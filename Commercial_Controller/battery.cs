using System;
using System.Collections.Generic;
namespace Commercial_Controller
{
    public class Battery
    {
        public Battery(int _ID, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            floorRequestButtonID = 1;
            ID = _ID;
            status = "Online";
            columnsList = new List<Column> ();
            floorRequestsButtonsList = new List <FloorRequestButton>();
            if (_amountOfBasements > 0 )
            {
                createBasementFloorRequestButtons(_amountOfBasements);
                createBasementColumn(_amountOfBasements, _amountOfElevatorPerColumn);
            }
            createFloorRequestButtons(_amountOfFloors);
            createColumns(_amountOfColumns-1, _amountOfFloors, _amountOfElevatorPerColumn);
        }
        public int ID{get; set; }
        public string status{get; set; }
        public List<Column> columnsList{get; set; }
        public List<FloorRequestButton> floorRequestsButtonsList{get; set; }
        public int floorRequestButtonID; 
        int columnID = 1;
        public void createBasementColumn(int _amountOfBasements, int _amountOfElevatorPerColumn){
         
            List<int> servedFloors = new List<int>();
            int floor = -1;
            
            for (int i = 0; i< _amountOfBasements; i++)
            {
                servedFloors.Add(floor);
                floor--;
            }
            servedFloors.Add(1);

            Column column = new Column(columnID, _amountOfBasements, _amountOfElevatorPerColumn, servedFloors, true);
            
            columnsList.Add(column);
            columnID ++;
        }
        public void createColumns(int _amountOfColumns, int _amountOfFloors,  int _amountOfElevatorPerColumn)
        {
            double amountOfFloorsPerColumn = Math.Ceiling(Convert.ToDouble(_amountOfFloors)/Convert.ToDouble(_amountOfColumns));
            int floor = 1;
            
            for (int i = 0; i< _amountOfColumns; i++)
            {List<int> servedFloors = new List<int>(); 
                for (int n = 0; n< amountOfFloorsPerColumn; n++)
                {
                    if (floor <= _amountOfFloors){
                        servedFloors.Add(floor);
                        floor++;
                    }
                }
                servedFloors.Add(1);
                
                Column column = new Column(columnID, _amountOfFloors, _amountOfElevatorPerColumn, servedFloors, false);
                 
                 columnsList.Add(column);
                
                 columnID ++;
                
            }
            
            
        }
        public void createFloorRequestButtons(int _amountOfFloors)
        {
            int buttonFloor = 1;
            
            for (int i = 0; i< _amountOfFloors; i++){
                FloorRequestButton floorRequestButton = new FloorRequestButton(floorRequestButtonID, buttonFloor, "up");
                floorRequestsButtonsList.Add(floorRequestButton);
                buttonFloor++;
                floorRequestButtonID++;
            }
        }
        public void createBasementFloorRequestButtons(int _amountOfBasements)
        {
            int buttonFloor = -1;
            for (int i = 0; i< _amountOfBasements; i++)
            {
               FloorRequestButton floorRequestButton = new FloorRequestButton(floorRequestButtonID, buttonFloor, "down");
               floorRequestsButtonsList.Add(floorRequestButton);
               buttonFloor--;
               floorRequestButtonID++;            
            }
        }


        public Column findBestColumn(int _requestedFloor)
        {
            Column returnColumn = null;
            foreach(Column column in columnsList)
            {
                if (column.servedFloorsList.Contains(_requestedFloor))
                {
                    returnColumn = column;
                    return returnColumn;
                }

            }
            return returnColumn;
        }
        //Simulate when a user press a button at the lobby
        public (Column, Elevator) assignElevator(int _requestedFloor, string _direction)
        {
            
            Column column = findBestColumn(_requestedFloor);
            
            Elevator elevator = column.findElevator(1, _direction);
            elevator.addNewRequest(1);
            elevator.move();
            elevator.addNewRequest(_requestedFloor);
            
            elevator.move();
           
            return (column, elevator);
        }
    }
}

