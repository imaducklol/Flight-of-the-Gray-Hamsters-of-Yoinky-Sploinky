using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeungenorator : MonoBehaviour
{
    private int roomTracker;
    //remembers where the exit to the last room was (0 => left, 1 => top, 2 => right)

    void Start() //generate a 10 room dungeon 
    {
        //generate starting room
        randomInt(0,2);
        if (0) {
            //spawn left version of spawn room 
            roomTracker = 0;
        }
        if (1) {
            //spawn top version of spawn room
             roomTracker = 1;
        }
        if (2) {
            // spawn right version of spawn room
             roomTracker = 2;
        }

        roomGenerator() {
            
            if(roomTracker == 0) {
                randomInt(0,1);
                if (0) {
                    //spawn a random room with a left entrance and a left exit
                    roomTracker = 0;
                }
                if (1) {
                    //spawn a random room with a left entrance and a top exit
                    roomTracker = 1;
                }
            }
            if (roomTracker == 1) {
                randomInt(0,2);

            }
        //repeat code above for right spawn rooms but replace right with left
        //if the last spawned room was middle than get randomInt(0,2); and spawn a random room 
           // with a top entrance and a random exit


        }

       
         
    }


}
