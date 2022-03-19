using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  private const float DashDuration = 0.1f;

  [SerializeField] private Animator anim;
  [SerializeField] private float speed;
  private Rigidbody2D body;
  private bool isGrounded;
  private bool isDashing;
  private float dashDistance = 25f;
  private bool facingRight;
  private bool hasDashed;
  private int jumpCounter;

  private void Awake()
  {
    body = GetComponent<Rigidbody2D>();
    facingRight = true;
    hasDashed = true;
    jumpCounter = 0;
  }

  // Update is called once per frame
  private void Update()
  {
    HandleJump();
    HandleDash();
  }

  private void HandleJump()
  {
    if (jumpCounter < 2 && !isDashing && Input.GetKeyDown(KeyCode.Z))
    {
      anim.SetTrigger("Jump");
      anim.SetBool("Land", false);
      body.velocity = new Vector2(body.velocity.x, speed + 5f);
      isGrounded = false;
      jumpCounter += 1;
    }
  }

  private void HandleDash()
  {
    if (Input.GetKeyDown(KeyCode.X) && !hasDashed)
    {
      if (facingRight)
      {
        StartCoroutine(Dash(1f));
      }
      else
      {
        StartCoroutine(Dash(-1f));
      }
      if (!isGrounded)
      {
        hasDashed = true;
      }
    }
  }

  private void FixedUpdate()
  {
    if (!isDashing)
    {
      anim.SetFloat("Y", body.velocity.y);
      float val = Input.GetAxis("Horizontal");
      bool inputDirection = val < 0;
      int magnitude = 0;
      if (val == 0)
      {
        magnitude = 0;
      }
      else if (inputDirection)
      {
        magnitude = -1;
        transform.localScale = new Vector3(1f, 1f, 1f);
      }
      else
      {
        magnitude = 1;
        transform.localScale = new Vector3(-1f, 1f, 1f);
      }
      body.velocity = new Vector2(magnitude * speed, body.velocity.y);
      if (Input.GetAxis("Horizontal") != 0)
      {
        facingRight = (Input.GetAxis("Horizontal") > 0);
      }
    }
  }

  IEnumerator Dash(float direction)
  {
    isDashing = true;
    anim.SetBool("Dash", true);
    body.velocity = new Vector2(0, 0f);
    body.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
    float gravity = body.gravityScale;
    body.gravityScale = 0;
    yield return new WaitForSeconds(DashDuration);
    isDashing = false;
    anim.SetBool("Dash", false);
    body.gravityScale = gravity;
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    isGrounded = true;
    hasDashed = false;
    jumpCounter = 0;
    anim.SetBool("Land", true);
  }
}
