using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeunGenorator : MonoBehaviour
{
    
    public int RoomAmount; 
    private RoomBank Bank;
    private int randoNum;
    public Vector3 spawnPos; 
    public Vector3 entrance;

    //remembers where the exit to the last room was where 0 => left, 1 => top, 2 => right
    public int exitTracker;
    //tracks how many rooms have been spawned
    public int roomTracker;

    /*
        //high priority
        -Rooms must not overlap with eachother
            - find the appropriate room 
            - find the transform position of the entrance spawner for that rooms prefab
                - just instantiate and move the prefab
            - If the room spawned will be to the right or left: 
                - take the negative x value of the position then add the x position value of the exit spawner(the gameobject this script is attached to)
                - instantiate at that position with the same y
            - If the room spawned will be at the middle: 
                - find the y offset of the entrance spawner for the prefab
                - take the abs
                - add to the y-pos of the exit spawner 
                - instantiate at that pos
        - Rooms must spawn at the exit of the last room
            - new Vector3 (-displacementX + SpawnPointX, -displacementY + SpawnPointY, 0)
            - How do we find the spawnpointPos? 
                - Move the spawnpoint Gameobject around? 
                    > 
                - New prefab attarched to all rooms that we set to the spawnpoint after the old room is created? 
                    > might run into order issues
                    > have to destroy the old spawnpoint
                        > 
        
        (done)-Rooms need to have an way to track where the exit is
            - set each room prefab's tag to where the exit is
            - find room's tag and convert to exitTracker


    */


    void findExit(GameObject lastRoom) 
    {
        if (lastRoom.tag == "exitLeft") {exitTracker = 0;}
        if (lastRoom.tag == "exitMiddle") {exitTracker = 1;}
        if (lastRoom.tag == "exitRight") {exitTracker = 2;}
    }

    void entrancePos(GameObject lastRoom) 
    {
        entrance = lastRoom.transform.Find("snapPoint").gameObject.transform.position;
    }

    void resetSpawnPoint(GameObject lastRoom) 
    {
        spawnPos = lastRoom.transform.Find("exitPoint").gameObject.transform.position;
    }



    private float invertedX;
    private float invertedY;
    

    void Spawner(int roomAmount)
    {
        Bank = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomBank>();

        randoNum = Random.Range(0, Bank.spawnRooms.Length);
        Instantiate(Bank.spawnRooms[randoNum], transform.position, Quaternion.identity);
        Debug.Log("spawn room created");
        resetSpawnPoint(Bank.spawnRooms[randoNum]);
        roomAmount -= 1;
        exitTracker = randoNum;
        

        for (var i = 0; i < roomAmount; i++)
        {
            //left
            if(exitTracker == 0) {
                randoNum = Random.Range(0, Bank.leftRooms.Length);
                entrancePos(Bank.leftRooms[randoNum]);
                invertedX = -entrance.x;
                invertedY = -entrance.y;
                Instantiate(Bank.leftRooms[randoNum], new Vector3 (spawnPos.x + invertedX, spawnPos.y + invertedY, 0), Quaternion.identity);
                Debug.Log("left entrance room created");
                findExit(Bank.leftRooms[randoNum]);
                resetSpawnPoint(Bank.leftRooms[randoNum]);
                roomAmount -= 1; 
                
            }
            else if (exitTracker == 1) {
                entrancePos(Bank.middleRooms[randoNum]);
                invertedX = -entrance.x;
                invertedY = -entrance.y;
                Instantiate(Bank.middleRooms[randoNum], new Vector3 (spawnPos.x + invertedX, spawnPos.y + invertedY, 0), Quaternion.identity);
                Debug.Log("middle entrance room created");
                findExit(Bank.middleRooms[randoNum]);
                resetSpawnPoint(Bank.middleRooms[randoNum]);
                roomAmount -= 1; 

            } 
            else if(exitTracker == 2) {
                

            }
        }
        Debug.Log("all rooms created");
                
    }
    
 
    void Start() 
    {
        RoomAmount = 4; 
        Spawner(RoomAmount);
    }

}
