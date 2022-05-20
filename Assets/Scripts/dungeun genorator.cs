using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeunGenorator : MonoBehaviour
{
    

    private RoomBank Bank;
    private int rando;
    private int exitRand; 
    private Vector2 spawnPos; 
    private int exitTracker; //remembers where the exit to the last room was where 0 => left, 1 => top, 2 => right

    /*




    */

    /*
        -Rooms must not overlap with eachother
            - find the appropriate room
            - find the transform position of the entrance spawner for that rooms prefab
            - take the negative x value of the position then add the x position value of the exit spawner
            - instantiate at that position


    */

    void Start() 
    {
        Bank = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomBank>();
    }


    void Spawner()
    {
        rando = Random.Range(0, Bank.spawnRooms.Length);
        Instantiate(Bank.spawnRooms[rando], transform.position, Quaternion.identity);
        exitTracker = rando;

        //Last rooms exit was to the left
        if(exitTracker == 0) {
            
            
        }

        //last rooms exit was to the middle
        if (exitTracker == 1) {
            rando = RandomRange(0, Bank.middleRooms.length);

            Instantiate(Bank.middleRooms[rando], transform.position, Quaternion.identity);
            //get the exit status 
            // exitTracker 
        }
        
        //last rooms exit was to the right
        if(exitTracker == 2) {
            //

        }
         
    }
    
}
