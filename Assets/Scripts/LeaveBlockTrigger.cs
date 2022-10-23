using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveBlockTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        LevelGenerator.sharedInstance.AddNewBlock();
        LevelGenerator.sharedInstance.RemoveOldBlock();
    }
}
