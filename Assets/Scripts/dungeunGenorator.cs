using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeunGenorator : MonoBehaviour
{
    
    public int RoomAmount; 
    private RoomBank Bank;
    private int randoNum;
    private Vector3 spawnPos; 
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

    private int xDisplacment;
    private int yDisplacment; 


    void exitPos(GameObject lastRoom) 
    {
        entrance = lastRoom.transform.Find("snapPoint").gameObject.transform.position;

    }

    void Spawner(int roomAmount)
    {
        Bank = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomBank>();

        randoNum = Random.Range(0, Bank.spawnRooms.Length);
        Instantiate(Bank.spawnRooms[randoNum], transform.position, Quaternion.identity);
        roomAmount -= 1;
        //b/c there are only three spawnrooms i can put them in order instead of getting the tag
        exitTracker = randoNum;


        
        for (var i = 0; i < roomAmount; i++)
        {
            //left
            if(exitTracker == 0) {
                randoNum = Random.Range(0, Bank.leftRooms.Length);
                //Bank.leftRooms[randoNum].findG;
                Instantiate(Bank.leftRooms[randoNum], new Vector3(0, 0, 0), Quaternion.identity);
                findExit(Bank.leftRooms[randoNum]);
                exitPos(Bank.leftRooms[randoNum]);
                roomAmount += 1; 
                
            }

            //middle
            if (exitTracker == 1) {
                randoNum = Random.Range(0, Bank.middleRooms.Length);

                Instantiate(Bank.middleRooms[randoNum], new Vector3(0,0,0), Quaternion.identity);

            }
            
            //right
            if(exitTracker == 2) {
                

            }
        }
                
    }
    
 
    void Start() 
    {
        RoomAmount = 4; 
        Spawner(RoomAmount);
    }

}
