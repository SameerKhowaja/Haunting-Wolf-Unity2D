using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : MonoBehaviour
{
    public Rigidbody2D wolf_RB;
    public BoxCollider2D wolf_colider;
    public Animator wolf_Animator;

    public float speed;
    public float walk_speed, run_speed;
    Vector2 movement;

    int zero = 0;

    bool howlCheck;
    bool hitCheck;
    bool deathCheck;
    bool attackCheck1, attackCheck2;
    bool shiftPressed;

    private void Start()
    {
        wolf_colider.enabled = false;
        zero = 1;
        shiftPressed = howlCheck = hitCheck = deathCheck = attackCheck1 = attackCheck2 = false;
    }

    void Update()
    {
        //Move Axis
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = 0f;
        //

        //For Rotation to x and -x
        if (movement.x == -1)   transform.localScale = new Vector3(-1f, 1f, 1f);
        if (movement.x == 1)    transform.localScale = new Vector3(1f, 1f, 1f);
        //

        //Walk or Run Speed Checks
        wolf_Animator.SetFloat("speed", speed);
        wolf_Animator.SetBool("shiftPressed", shiftPressed);
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && (movement.x == 1 || movement.x == -1))
        {
            speed = run_speed;
            shiftPressed = true;
        }
        else if(movement.x == 1 || movement.x == -1)
        {
            speed = walk_speed;
            shiftPressed = false;
        }
        else
        {
            speed = 0;
            shiftPressed = false;
        }
        //

        //Howl Check
        wolf_Animator.SetBool("howlCheck", howlCheck);
        if (Input.GetKey(KeyCode.H))
        {
            StartCoroutine(waitHowl());
        }
        //

        //Attack Check Mouth Attack
        wolf_Animator.SetBool("attackCheck1", attackCheck1);
        if (Input.GetKey(KeyCode.F))
        {
            attackCheck1 = true;
            StartCoroutine(waitAttack());
        }
        else
        {
            attackCheck1 = false;
        }
        //

        //Attack Check Hand Attack
        wolf_Animator.SetBool("attackCheck2", attackCheck2);
        if (Input.GetKey(KeyCode.G))
        {
            attackCheck2 = true;
            StartCoroutine(waitAttack());
        }
        else
        {
            attackCheck2 = false;
        }
        //

    }


    void FixedUpdate()
    {
        wolf_RB.MovePosition(wolf_RB.position + movement * speed * Time.fixedDeltaTime * zero);
    }


    IEnumerator waitHowl()
    {
        howlCheck = true;
        zero = 0;
        yield return new WaitForSeconds(1.2f);
        howlCheck = false;
        zero = 1;
    }

    IEnumerator waitAttack()
    {
        wolf_colider.enabled = true;
        yield return new WaitForSeconds(1f);
        wolf_colider.enabled = false;
    }


}
