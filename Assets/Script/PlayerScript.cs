using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public BodyPartScript[] bodyParts;
    public BodyPartScript[] notCustomBodyParts;

    // Movement Bools
    bool isRightPressed = false;
    bool isLeftPressed = false;
    bool isUpPressed = false;
    bool isDownPressed = false;

    float horizontal;
    float vertical;
    Vector2 movement;
    float moveSpeed = 5;
    Rigidbody2D rb;
    Animator animator;


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        // Animation of character
        isRightPressed = Input.GetKey(KeyCode.RightArrow);
        isLeftPressed = Input.GetKey(KeyCode.LeftArrow);
        isUpPressed = Input.GetKey(KeyCode.UpArrow);
        isDownPressed = Input.GetKey(KeyCode.DownArrow);

        // Player Movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + (movement * moveSpeed * Time.fixedDeltaTime));

    }

    private void FixedUpdate()
    {
        if (isRightPressed)
        {
            PlayAnimation("WalkRightKey");
        }
        if (isLeftPressed)
        {
            PlayAnimation("WalkLeftKey");
        }
        if (isUpPressed)
        {
           // PlayAnimation("WalkRightKey");
        }
        if (isDownPressed)
        {
            PlayAnimation("WalkDownKey");
        }
        if(!isRightPressed || isLeftPressed || isUpPressed || isDownPressed)
        {
            Invoke("PlayIdle", 0.15f);
        }
    }

    private void PlayAnimation(string nextAnimation)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(nextAnimation))
        {
            animator.Play(nextAnimation);
            UpdateBodySpritesDirection(nextAnimation);
        }
            
    }

    private void PlayIdle()
    {
        animator.Play("Idle");
    }


    void ChangeBodyPartSprite(int partIndex, int spriteIndex)
    {
        if(spriteIndex < bodyParts[partIndex].sprites.Length - 1)
        {
            spriteIndex = 0;
        }
        bodyParts[partIndex].UpdateSprite(spriteIndex);
    }

    public void ChangePlayerScale(float newScale)
    {
        transform.localScale = new Vector3(newScale, newScale, 0);
    }

    private void UpdateBodySpritesDirection(string dir)
    {
        foreach(BodyPartScript partScript in bodyParts)
        {
            partScript.ChangeSpriteDirection(dir);
        }
        foreach (BodyPartScript partScript in notCustomBodyParts)
        {
            partScript.ChangeSpriteDirection(dir);
        }
    }
}
