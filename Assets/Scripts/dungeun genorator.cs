using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeungenorator : MonoBehaviour
{
    

    private RoomBank Bank;
    private int rando;
    private int exitTracker;
    //remembers where the exit to the last room was (0 => left, 1 => top, 2 => right)

    void Start() //generate a 10 room dungeon 
    {
        
        Bank = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomBank>();

        rando = Random.Range(0, Bank.spawnRooms.Length);
        Instantiate(bank.spawnRooms[rand], transform.position, Quaternion.identity);
        exitTracker = rando;

        if(exitTracker == 0) {
            randomInt(0,1);
            if (0) {
                //spawn a random room with a left entrance and a left exit
                exitTracker = 0;
            }
            if (1) {
                //spawn a random room with a left entrance and a top exit
                exitTracker = 1;
            }
        }
        if (exitTracker == 1) {
            randomInt(0,2);

        
        //repeat code above for right spawn rooms but replace right with left
        //if the last spawned room was middle than get randomInt(0,2); and spawn a random room 
           // with a top entrance and a random exit


        }

       
         
    }

    
}
