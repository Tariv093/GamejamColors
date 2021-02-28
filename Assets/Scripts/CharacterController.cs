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
    PlayerInput pInput;
    public float speed, jumpforce, boost, climbValue, timeScore,finalScore;
    public bool isJumping;
    private float time;
    Vector2 m_move, m_rightstick;
    private InputAction m_jump, m_slide, m_colorChange;
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
        startPos = transform.position;
        // mat = GetComponentInChildren<SkinnedMeshRenderer>().material;

    }

    // Update is called once per frame
    void Update()
    {
        timeScore -= Time.deltaTime;
        time = Time.time;
        timerText.text = "Timer: " + time.ToString();
        anim.SetFloat("IdleTimer", anim.GetFloat("IdleTimer") + Time.deltaTime);
        // Debug.Log(anim.GetFloat("IdleTimer"));
        if (anim.GetFloat("IdleTimer") > 5)
        {
            anim.SetFloat("IdleTimer", 0);
        }

        if (m_move.x == -1)
        {
            transform.rotation = new Quaternion(0, 180, 0, transform.rotation.w);
        }
        else if (m_move.x == 1) transform.rotation = new Quaternion(0, 0, 0, transform.rotation.w);
        transform.position += new Vector3(0, 0, m_move.x) * speed * Time.deltaTime * boost;

        if (m_move.x == 0) anim.SetBool("IsRunning", false);
        //rBody.AddForce(new Vector3(0, -8.1f, 0)); // gravitys

    }
    public void OnMove(InputValue value)
    {
        anim.SetBool("IsRunning", true);
        m_move = value.Get<Vector2>();
        anim.SetFloat("Movement 0", m_move.x);
        anim.SetFloat("IdleTimer", 0);

    }


    public void OnJump(InputValue value)
    {

        if (isJumping == true)
        {
            return;
        }

        isJumping = true;

        rBody.velocity = new Vector3(0, 0, 0);
        rBody.AddForce(new Vector3(0, jumpforce, 0));
        anim.SetTrigger("Jump");

    }

    
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == 11)
        {
            if(m_rightstick == new Vector2(0,0))
            checkpoint = transform.position;
            
        }
       
        if (collision.gameObject.tag == "Wall")
        {
            if (isJumping == true)
            {
                Debug.Log("wall run");
                rBody.AddForce(new Vector3(0, climbValue, 0));
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
                finalScore = -timeScore * 24;
                scoreText.text += "Final Score: " + finalScore.ToString();
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
        }
        else if (m_rightstick.y < -0.2f)
        {
            gameObject.layer = 10;
            surfaceRenderer.material.color = color[2];
            jointRenderer.material.color = color[2];
        }

        if (m_rightstick.x > 0.2f)
        {
            gameObject.layer = 9;
            surfaceRenderer.material.color = color[3];
            jointRenderer.material.color = color[3];
        }
        else if (m_rightstick.x < -0.2f)
        {
            gameObject.layer = 12;
            surfaceRenderer.material.color = color[4];
            jointRenderer.material.color = color[4];
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
        time = 0;
        finalScore = 0;
        timerText.text = "Timer: ";
        scoreText.text = " ";
        transform.position = startPos;
    }
}
