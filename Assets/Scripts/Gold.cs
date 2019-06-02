using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RobotMovement robot = other.gameObject.GetComponent<RobotMovement>();
            if (!robot.full)
            {
                robot.ObtainedGold();
                Destroy(this.gameObject);
            }
        }
    }
}
