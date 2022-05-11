using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeungenorator : MonoBehaviour
{
    

    private RoomBank Bank;
    private int rando;
    private int exitRand; 
    
    private int exitTracker; //remembers where the exit to the last room was where 0 => left, 1 => top, 2 => right

    void Spawner()
    {
        
        Bank = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomBank>();

        rando = Random.Range(0, Bank.spawnRooms.Length);
        Instantiate(Bank.spawnRooms[rando], transform.position, Quaternion.identity);
        exitTracker = rando;

        //Last rooms exit was to the left
        if(exitTracker == 0) {
            exitRand = Random.Range(0,1);
            if (exitRand == 0) {
                //spawn a random room with a left entrance and a left exit
                exitTracker = 0;
            }else if (exitRand == 1) {
                //spawn a random room with a left entrance and a top exit
                exitTracker = 1;
            }
        }

        //last rooms exit was to the middle
        if (exitTracker == 1) {
            exitRand = Random.Range(0,2);
            if (exitRand == 0) {
                //spawn a random room with a bottom entrance and a left exit
                exitTracker = 0;
            }else if (exitRand == 1) {
                //spawn a random room with a bottom entrance and a top exit
                exitTracker = 1;
            } else if (exitRand == 2) {
                //spawn a random room that fuffiles b-r
                exitTracker = 2;
            }
        }
        
        //last rooms exit was to the right
        if(exitTracker == 2) {
            exitRand = Random.Range(0,1);
            if (exitRand == 0) {
                //spawn a random room with a left entrance and a left exit
                exitTracker = 0;
            }else if (exitRand == 1) {
                //spawn a random room with a left entrance and a top exit
                exitTracker = 1;
            }
        }
         
    }
    
}
