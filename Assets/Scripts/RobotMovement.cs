using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RobotMovement : MonoBehaviour
{
    private RaycastHit2D _hit;
    [SerializeField] private float _speed, _movH, _movV;
    private Vector3 _target;
    private Rigidbody2D _rigidbody;
    public bool isMoving, takingDamage, win;
    private int _gold = 0;
    public int remaingGold, directionNumber;
    private GameObject[] golds;
    public GameObject[] goldImage;
    public Text goldText, hpText;
    public bool full = false;
    public string direction;
    private Animator _animator;
    public int HP;
    public string nextScene;
    private SpriteRenderer _sprite;
    public AudioClip left, right, gold;
    void Start()
    {
        win = false;
        _sprite = GetComponent<SpriteRenderer>();
        HP = 4;
        hpText.text = HP.ToString();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _target = this.transform.position;
        golds = GameObject.FindGameObjectsWithTag("Gold");
        foreach (GameObject gold in golds)
        {
            remaingGold++;
        }

        goldText.text = remaingGold.ToString();
    }
    void FixedUpdate()
    {
        if (HP > 0 && !win)
        {
            Movement();
        }
        if (remaingGold <= 0)
        {
            _animator.SetBool("Win", true);
            StartCoroutine(ChangeScene());
        }

        switch (_gold)
        {
            case 0:
                goldImage[0].SetActive(false);
                goldImage[1].SetActive(false);
                goldImage[2].SetActive(false);
                goldImage[3].SetActive(false);
                break;
            case 1:
                goldImage[0].SetActive(true);
                goldImage[1].SetActive(false);
                goldImage[2].SetActive(false);
                goldImage[3].SetActive(false);
                break;
            case 2:
                goldImage[0].SetActive(true);
                goldImage[1].SetActive(true);
                goldImage[2].SetActive(false);
                goldImage[3].SetActive(false);
                break;
            case 3:
                goldImage[0].SetActive(true);
                goldImage[1].SetActive(true);
                goldImage[2].SetActive(true);
                goldImage[3].SetActive(false);
                break;
            case 4:
                goldImage[0].SetActive(true);
                goldImage[1].SetActive(true);
                goldImage[2].SetActive(true);
                goldImage[3].SetActive(true);
                break;
        }
        if (_gold >= 4)
        {
            full = true;
        }
    }

    void Movement()
    {

        _movH = Input.GetAxis("Horizontal");
        _movV = Input.GetAxis("Vertical");
        if (_movH != 0 || _movV != 0)
        {
            isMoving = true;
            _animator.SetBool("Moving", isMoving);
        }
        else
        {
            isMoving = false;
            _animator.SetBool("Moving", isMoving);
        }
        _rigidbody.velocity = new Vector2(_movH, _movV) * Time.deltaTime * _speed;

        if (_movH > 0)
        {
            direction = "right";
            _animator.SetInteger("Direction", 3);
            _sprite.flipX = false;
        }
        else if (_movH < 0)
        {
            direction = "left";
            _animator.SetInteger("Direction", 3);
            _sprite.flipX = true;
        }
        if (_movV > 0)
        {
            direction = "up";
            _animator.SetInteger("Direction", 1);
        }
        else if (_movV < 0)
        {
            direction = "down";
            _animator.SetInteger("Direction", -1);
        }
    }


    public void ObtainedGold()
    {
        if (_gold <= 4)
        {
            _gold++;
            AudioManager.instance.PlayOneShot(gold, 1f);
        }
    }
    public void DepositGold()
    {
        remaingGold -= _gold;
        goldText.text = remaingGold.ToString();
        _gold = 0;
        full = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Machine"))
        {
            if (full)
            {
                DepositGold();
            }
        }
        if (other.CompareTag("Poison"))
        {
            HP--;
            takingDamage = true;
            hpText.text = HP.ToString();
            if (HP <= 0)
            {
                StartCoroutine(Die());
            }
            else
            {
                StartCoroutine(TakingDamage());
            }
        }
    }

    private IEnumerator Die()
    {
        _animator.SetBool("Dead", true);
        yield return new WaitForSecondsRealtime(0.3f);
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.isKinematic = true;
        yield return new WaitForSeconds(1.25f);
        SceneManager.LoadScene("Underground", LoadSceneMode.Single);
    }
    private IEnumerator TakingDamage()
    {
        yield return new WaitForSeconds(2);
        takingDamage = false;
    }
    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }
    public void LeftSound()
    {
        AudioManager.instance.PlayOneShot(left, 1f);
    }
    public void RightSound()
    {
        AudioManager.instance.PlayOneShot(right, 1f);
    }
}
//void Rotate()
//{
// Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//Quaternion rot = Quaternion.LookRotation(this.transform.position - mousePosition, Vector3.forward);
//transform.rotation = rot;
//this.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
//_rigidbody.angularVelocity = 0;
//}
//}


//old movement system;
// isMoving = (_target.x == this.transform.position.x && _target.y == this.transform.position.y) ? false : true;
//if (Input.GetMouseButton(0))
//{
//   _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//  _hit = Physics2D.Raycast(_target, Vector2.zero);
//  Rotate();
//_rigidbody.position = Vector2.MoveTowards(this.transform.position, _target, _speed* Time.deltaTime);