using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    public CapsuleCollider hitbox;
    public GameObject particles;
    //PlayerInput pInput;
    public float speed, jumpforce, boost, climbValue;
    private float timerScore, finalScore;
    public bool isJumping;
    public Vector3 velocity;
    Vector2 m_move, m_rightstick;
    //private InputAction m_jump, m_slide, m_colorChange;
    private Rigidbody rBody;
    [SerializeField]
    private Renderer surfaceRenderer, jointRenderer;
    [SerializeField]
    Color[] color;
    public TMP_Text timerText, scoreText;
    [SerializeField]
    Vector3 checkpoint, startPos;


    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        hitbox = GetComponent<CapsuleCollider>();
        startPos = transform.position;
        // mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        timerScore += Time.deltaTime;
        timerText.text = "Timer: " + timerScore.ToString();
        anim.SetFloat("IdleTimer", anim.GetFloat("IdleTimer") + Time.deltaTime);
        // Debug.Log(anim.GetFloat("IdleTimer"));
        velocity = rBody.velocity;
        if (anim.GetFloat("IdleTimer") > 5)
        {
            anim.SetFloat("IdleTimer", 0);
        }

        if (m_move.x == -1)
        {
            transform.rotation = new Quaternion(0, 180, 0, transform.rotation.w);
        }
        else if (m_move.x == 1) transform.rotation = new Quaternion(0, 0, 0, transform.rotation.w);

        if (m_move.x == 0)
        {
            anim.SetBool("IsRunning", false);
            rBody.velocity = new Vector3(0, rBody.velocity.y, 0);
        }
        else rBody.velocity = new Vector3(0, rBody.velocity.y, m_move.x * speed * boost);
        //rBody.AddForce(new Vector3(0, -8.1f, 0)); // gravitys

        if (boost > 1)
        {
            boost -= Time.deltaTime;
        }
        else if (boost < 1) boost = 1;
    }
    public void OnMove(InputValue value)
    {
        anim.SetBool("IsRunning", true);
        m_move = value.Get<Vector2>();
        anim.SetFloat("Movement 0", m_move.x);
        anim.SetFloat("IdleTimer", 0);
    }


    public void OnJump()
    {

        if (isJumping == true)
        {
            return;
        }

        isJumping = true;

        rBody.velocity = new Vector3(0, 0, 0);
        rBody.AddForce(new Vector3(0, jumpforce * speed, m_move.x));
        anim.SetTrigger("Jump");
    }

    public void OnSlide(InputValue value)
    {

        float slideVar = value.Get<float>();
        if (isJumping == false)
        {
            boost = 2;
            if (value.isPressed)
            {
                anim.SetBool("Slide", true);
                hitbox.center = new Vector3(0, 0.5f, 0) ;
                hitbox.direction = 2;
            }
            else
            {
                anim.SetBool("Slide", false);

                hitbox.center = new Vector3(0, 0.95f, 0);
                hitbox.direction = 1;
            }
        }
       // anim.SetBool("Slide",false);

    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == 11)
        {
            if (m_rightstick == new Vector2(0, 0))
                checkpoint = transform.position;

        }

        if (collision.gameObject.tag == "Wall")
        {
            if (isJumping == true)
            {
                Debug.Log("wall run");
                rBody.AddForce(new Vector3(0, climbValue * boost, 0));
                anim.SetTrigger("WallClimb");
            }
        }
        isJumping = false;
        anim.ResetTrigger("Jump");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeathPlane")
        {
            this.transform.position = checkpoint;


        }
        if (other.gameObject.tag == "Flag")
        {
            if (finalScore == 0)
            {
                finalScore = timerScore;
                scoreText.text += "Your time: " + finalScore.ToString() + " seconds";
            }
        }

    }
    public void OnLook(InputValue value)
    {
        m_rightstick = value.Get<Vector2>();
        // north == green
        if (m_rightstick.y > 0.2f)
        {
            surfaceRenderer.material.color = color[1];
            gameObject.layer = 8;
            jointRenderer.material.color = color[1];
            GameObject nP = Instantiate(particles, transform.position, Quaternion.identity) as GameObject;
            ParticleSystem pSys = nP.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule ma = pSys.main;
            ma.startColor = color[1];
            
            nP.transform.position = this.transform.position;
        

        }
        else if (m_rightstick.y < -0.2f)
        {
            gameObject.layer = 10;
            surfaceRenderer.material.color = color[2];
            jointRenderer.material.color = color[2];
            GameObject nP = Instantiate(particles, transform.position, Quaternion.identity) as GameObject;
            ParticleSystem pSys = nP.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule ma = pSys.main;
            ma.startColor = color[2];

            nP.transform.position = this.transform.position;

        }

        if (m_rightstick.x > 0.2f)
        {
            gameObject.layer = 9;
            surfaceRenderer.material.color = color[3];
            jointRenderer.material.color = color[3];
            GameObject nP = Instantiate(particles, transform.position, Quaternion.identity) as GameObject;
            ParticleSystem pSys = nP.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule ma = pSys.main;
            ma.startColor = color[3];

            nP.transform.position = this.transform.position;

        }
        else if (m_rightstick.x < -0.2f)
        {
            gameObject.layer = 12;
            surfaceRenderer.material.color = color[4];
            jointRenderer.material.color = color[4];
            GameObject nP = Instantiate(particles, transform.position, Quaternion.identity) as GameObject;
            ParticleSystem pSys = nP.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule ma = pSys.main;
            ma.startColor = color[4];

            nP.transform.position = this.transform.position;

        }
        if (m_rightstick == new Vector2(0, 0))
        {
            gameObject.layer = 11;
            surfaceRenderer.material.color = color[0];
            jointRenderer.material.color = color[0];
        }
        // south == blue
        // west == purple
        // east == red
        // neutral == white

    }

    public void OnReset(InputValue value)
    {

        finalScore = 0;
        timerScore = 0;
        timerText.text = "Timer: ";
        scoreText.text = " ";
        transform.position = startPos;
    }
}
