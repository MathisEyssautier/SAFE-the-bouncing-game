//PLAYERCONTROLLER FOR PROJECT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float speed = 10.00f;
    private float rotSpeed = 70.00f;
    private int zRangeLeft = -19;
    private int zRangeRight = 10;
    private Rigidbody playerRb;
    private float jumpForce = 130;
    public bool isOnGround;
    public AudioSource powerUpSound;
    public AudioSource glasBreak;

    public GameObject tramp;
    public GameObject ambulance;
    public Material bonusMat;
    private Vector3 ambulanceScale;
    public Material currentTrampMat;
    private Animator playerAnim;
    private GroundManager groundManag;

    public bool hasPowerUp = false;

    private GameManager gameManager;

    private void Start()
    {
        groundManag = GameObject.Find("Sol").GetComponent<GroundManager>();
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ambulanceScale = ambulance.transform.localScale;
        currentTrampMat = tramp.GetComponent<MeshRenderer>().material;
    }
    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameActive)
        {
            if (Input.GetMouseButton(1))//Clic droit
                tramp.transform.Rotate(Vector3.right * rotSpeed * Time.deltaTime);

            if (Input.GetMouseButton(0))//Clic gauche
                tramp.transform.Rotate(-Vector3.right * rotSpeed * Time.deltaTime);

            if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }


            horizontalInput = Input.GetAxis("Horizontal");
            playerAnim.SetFloat("direction", horizontalInput);

            transform.Translate(Vector3.left * horizontalInput * Time.deltaTime * speed);

            if (transform.position.z < zRangeLeft)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zRangeLeft);
            }
            if (transform.position.z > zRangeRight)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zRangeRight);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup") && !hasPowerUp)
        {
            powerUpSound.Play();
            hasPowerUp = true;
            if(other.name == "present1(Clone)")
            {
                StartCoroutine(PowerUpCountdownRoutine1());
            }
            if(other.name == "present2(Clone)")
            {
                StartCoroutine(PowerUpCountdownRoutine2());
            }
            if(other.name == "present4(Clone)")
            {
                StartCoroutine(PowerUpCountdownRoutine3());
            }
            Destroy(other.gameObject);
        }
        if(other.CompareTag("Bad"))
        {
            groundManag.groundCpt += 3;
            glasBreak.Play();
            Destroy(other.gameObject);
        }
    }

    IEnumerator PowerUpCountdownRoutine1()
    {
        speed += 10;
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        speed -= 10;
    }

    IEnumerator PowerUpCountdownRoutine2()
    {
        tramp.transform.localScale = new Vector3(0.004653326f, 0.0004705998f, 0.04f);
        tramp.GetComponent<MeshRenderer>().material = bonusMat;

        yield return new WaitForSeconds(7);

        tramp.transform.localScale = new Vector3(0.004653326f, 0.0004705998f, 0.01330093f);
        tramp.GetComponent<MeshRenderer>().material = currentTrampMat;
        hasPowerUp = false;
    }

    IEnumerator PowerUpCountdownRoutine3()
    {
        ambulance.transform.localScale += new Vector3(4, 0, 0);
        ambulance.transform.position += new Vector3(0, 0, 3);
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        ambulance.transform.localScale = ambulanceScale;
        ambulance.transform.position -= new Vector3(0, 0, 3);
    }
}
