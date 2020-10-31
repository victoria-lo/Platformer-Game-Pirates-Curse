using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    public float maxSpeed = 6;
    public float jumpTakeOffSpeed;
    public Transform player;
    public int attackPower = 1;
    public bool invisible = false;

    private Animator animator;
    public bool m_FacingRight = true;
    public bool dead = false;
    public Vector2 move;

    protected Joystick joystick;
    protected Joybutton jumpButton, attackButton;
    protected Joybutton upButton, downButton;

#pragma warning disable 649
    [SerializeField] private GameMaster gm;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip sfxChest;
    [SerializeField] AudioClip sfxJump;
    [SerializeField] AudioClip sfxThrow;
#pragma warning restore 649
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        jumpButton = GameObject.Find("Jump").GetComponent<Joybutton>();
        attackButton = GameObject.Find("Attack").GetComponent<Joybutton>();
        upButton = GameObject.Find("Up").GetComponent<Joybutton>();
        downButton = GameObject.Find("Down").GetComponent<Joybutton>();

        if (GameObject.FindGameObjectWithTag("GM") != null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
            transform.position = gm.lastCheckPointPos;
        }
        source = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        if (Input.GetKeyDown(KeyCode.X) || attackButton.attack)
        {
            animator.SetTrigger("throw");
            source.PlayOneShot(sfxThrow);
            attackButton.attack = false;
        }

        if (Chest.happy && (Input.GetKeyDown(KeyCode.UpArrow) || upButton.up))
        {
            animator.SetTrigger("happy");
            GetComponent<PlayerPlatformerController>().enabled = false;
            source.PlayOneShot(sfxChest);
            GetComponent<PlayerPlatformerController>().enabled = true;
            Chest.happy = false;
            upButton.up = false;
        }
        move = Vector2.zero;

        move.x = Input.GetAxisRaw("Horizontal") + joystick.Horizontal;

        if ((Input.GetButtonDown("Jump") || jumpButton.jump) && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
            source.PlayOneShot(sfxJump);
        }
        else if (Input.GetButtonUp("Jump") || !jumpButton.jump && jumpButton.used)
        {
            {
                if (velocity.y > 0)
                {
                    Debug.Log("space");
                    velocity.y = velocity.y * 0.5f;
                    jumpButton.used = false;
                }
            }
        }
        if (move.x > 0 && !m_FacingRight)
        {
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        if (move.x < 0 && m_FacingRight)
        {
            Flip();
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        if (!dead)
            targetVelocity = move * maxSpeed;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}