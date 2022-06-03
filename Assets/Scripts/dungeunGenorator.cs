using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeunGenorator : MonoBehaviour
{
    
    public int RoomAmount; 
    private RoomBank Bank;
    private int randoNum;
    private Vector3 spawnPos; 
    private Vector3 entrance;

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

        //low priority
        -Rooms must not spawn on top of eachother
            - added a ridgidbody and box collider to the spawnpoints 
            - on contact with anything, destroy the spawnpoint
                - the only thing it should be contacting is the ground of the new room
                    - if it destroys before spawning a room due to being too close to the last room, increase the spawnpoint's offset
        -Rooms need to have an way to track where the exit is
            - set each room prefab's tag to where the exit is
            - find room's tag and convert to exitTracker

    */

    void Spawner(int roomAmount)
    {
        Bank = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomBank>();

        randoNum = Random.Range(0, Bank.spawnRooms.Length);
        Instantiate(Bank.spawnRooms[randoNum], transform.position, Quaternion.identity);
        exitTracker = randoNum;
        
        for (var i = 0; i < roomAmount; i++)
        {
            //left
            if(exitTracker == 0) {
                randoNum = Random.Range(0, Bank.leftRooms.Length);
                Instantiate(Bank.leftRooms[randoNum], new Vector3(0, 0, 0), Quaternion.identity);
                

                
            }
            //middle
            if (exitTracker == 1) {
                randoNum = Random.Range(0, Bank.middleRooms.Length);
                //spawnPos = 
                Instantiate(Bank.middleRooms[randoNum], spawnPos, Quaternion.identity);
                //get the exit status !!!
                // exitTracker = exit status
            }
            
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
