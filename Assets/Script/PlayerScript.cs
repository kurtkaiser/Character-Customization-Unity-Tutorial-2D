using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public BodyPartScript[] bodyParts;
    public BodyPartScript[] nonCustomizableBodyParts;
    private Vector2 movement;
    private float moveSpeed = 5f;
    Rigidbody2D rb;

    // Key Bools
    bool isRightKeyPressed = false;
    bool isLeftKeyPressed = false;
    bool isUpKeyPressed = false;
    bool isDownKeyPressed = false;

    Animator animator;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        isRightKeyPressed = Input.GetKey(KeyCode.RightArrow);
        isLeftKeyPressed = Input.GetKey(KeyCode.LeftArrow);
        isUpKeyPressed = Input.GetKey(KeyCode.UpArrow);
        isDownKeyPressed = Input.GetKey(KeyCode.DownArrow);
    }

    private void FixedUpdate()
    {
        Vector2 direction = movement.normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

        if (isRightKeyPressed)
        {
            PlayAnimation("WalkLeftArrow", "right");
        } else if (isLeftKeyPressed)
        {
            PlayAnimation("WalkLeftArrow", "left");
        } else if (isUpKeyPressed)
        {

        } else if (isDownKeyPressed)
        {
            PlayAnimation("WalkDownArrow", "down");
        }
    }

    public void PlayAnimation(string nextAnim, string dir)
    {
        if(!GetComponent<Animator>()
            .GetCurrentAnimatorStateInfo(0).IsName(nextAnim))
        {
            ChangeBodyPartDirection(dir);
            animator.Play(nextAnim);
        }
    }

    private void ChangeBodyPartDirection(string dir)
    {
        foreach(BodyPartScript part in bodyParts)
        {
            part.SpriteDirectionChange(dir);
        }
        foreach (BodyPartScript otherPart in nonCustomizableBodyParts)
        {
            otherPart.SpriteDirectionChange(dir);
        }
    }


    void ChangeBodyPartSprite(int partIndex, int spriteIndex)
    {
        if(spriteIndex < bodyParts[partIndex].sprites.Length - 1)
        {
            spriteIndex = 0;
        }
        bodyParts[partIndex].UpdateSprite(spriteIndex);
    }
}
