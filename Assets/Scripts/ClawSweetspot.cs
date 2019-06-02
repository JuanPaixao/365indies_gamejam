using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawSweetspot : MonoBehaviour
{
    public bool withObject;
    private GameObject mineral;
    public Claw claw;
    public AudioClip clawCatch;
    void Update()
    {
        if (withObject)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                transform.DetachChildren();
                mineral.GetComponent<RockandGold>().attached = false;
                claw.WithObject(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gold") || other.gameObject.CompareTag("Rock"))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                withObject = true;
                other.transform.parent = this.gameObject.transform;
                claw.WithObject(true);
                AudioManager.instance.PlayOneShot(clawCatch, 0.75f);
                mineral = other.gameObject;
                mineral.GetComponent<RockandGold>().attached = true;
            }
        }
    }
}
