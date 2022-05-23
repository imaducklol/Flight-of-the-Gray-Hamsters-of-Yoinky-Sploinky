using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeunGenorator : MonoBehaviour
{
    

    private RoomBank Bank;
    private int rando;
    private Vector2 spawnPos; 

    //remembers where the exit to the last room was where 0 => left, 1 => top, 2 => right
    public int exitTracker;

    /*
        -Rooms must not overlap with eachother
            - find the appropriate room
            - find the transform position of the entrance spawner for that rooms prefab
            - If the room spawned will be to the right or left: 
                - take the negative x value of the position then add the x position value of the exit spawner(the gameobject this script is attached to)
                - instantiate at that position with the same y
            - If the room spawned will be ot the middle: 
                - find the y offset of the entrance spawner for the prefab
                - take the abs
                - add to the y-pos of the exit spawner 
                - instantiate at that pos
        -Rooms must not spawn on top of eachother
            - added a ridgidbody and box collider to the spawnpoints 
            - on contact with anything, destroy the spawnpoint
                - the only thing it should be contacting is the ground of the new room
                    - if it destroys before spawning a room due to being too close to the last room, increase the spawnpoint's offset


    */

        
    void Spawner()
    {
        Bank = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomBank>();

        rando = Random.Range(0, Bank.spawnRooms.Length);
        Instantiate(Bank.spawnRooms[rando], transform.position, Quaternion.identity);
        exitTracker = rando;

        //Last rooms exit was to the left
        if(exitTracker == 0) {
            
            
        }

        //last rooms exit was to the middle
        if (exitTracker == 1) {
            rando = Random.Range(0, Bank.middleRooms.Length);
            //spawnPos =
            Instantiate(Bank.middleRooms[rando], spawnPos, Quaternion.identity);
            //get the exit status !!!
            // exitTracker = exit status
        }
        
        //last rooms exit was to the right
        if(exitTracker == 2) {
            

        }
         
    }
    
}
