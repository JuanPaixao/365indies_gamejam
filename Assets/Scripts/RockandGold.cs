using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockandGold : MonoBehaviour
{
    public bool attached, finished;
    private Claw _claw;
    public AudioClip gold, rock;
    public bool soundPlayed;

    void Start()
    {
        soundPlayed = false;
        _claw = GameObject.FindObjectOfType<Claw>().GetComponent<Claw>();
        finished = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "box")
        {
            Debug.Log(other.gameObject.name);
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
            }
        }

        if (other.gameObject.name == "insideBox" && finished == false)
        {
            _claw.BoxedObject(this.gameObject.name);
            if (this.gameObject.name == "rock")
            {
                _claw.rocks++;
            }
            finished = true;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name != "ClawSweetspot" || other.gameObject.name != "Wall")
        {
            if (!soundPlayed)
            {
                if (this.gameObject.name == "gold")
                {
                    if (other.gameObject.name == "gold " || other.gameObject.name == "rock")
                    {
                        if (!other.gameObject.GetComponent<RockandGold>().finished)
                        {
                            AudioManager.instance.PlayOneShot(rock, 0.5f);
                            soundPlayed = true;
                        }
                    }
                    else
                    {
                        AudioManager.instance.PlayOneShot(rock, 0.5f);
                        soundPlayed = true;
                    }
                }
                else if (other.gameObject.name == "gold " || other.gameObject.name == "rock")
                {
                    if (!other.gameObject.GetComponent<RockandGold>().finished)
                    {
                        AudioManager.instance.PlayOneShot(rock, 0.5f);
                        soundPlayed = true;
                    }
                }
                else
                {
                    AudioManager.instance.PlayOneShot(rock, 0.5f);
                    soundPlayed = true;
                }
            }
        }
    }
}

