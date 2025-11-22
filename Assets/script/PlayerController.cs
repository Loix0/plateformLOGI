using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseClassScript
{
    public float jumpForce = 400;
    public float horizontalMove;
    public bool jump = false;
    Rigidbody2D rigidBody;

    //add sword
    public GameObject WarriorSword;
    public GameObject RougeSword;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        WarriorClass();
        //RougeClass();
    }

    void WarriorClass()
    {
        ClassName = "Warrior";
        Health = 5;
        Strength = 4;
        Intelligence = 2;
        Agility = 16;
        Damage = Strength / Intelligence;
        var sword = Instantiate(WarriorSword, gameObject.transform);
        sword.transform.position += new Vector3(0.6f, 0.3f, 0);
    }

    void RougeClass()
    {
        ClassName = "Rouge";
        Health = 3;
        Strength = 2;
        Intelligence = 2;
        Agility = 20;
        Damage = Strength / Intelligence;

        var sword = Instantiate(RougeSword, gameObject.transform);
        sword.transform.position += new Vector3(0.6f, 0.3f, 0);

    }

    private void Update()
    {
        //Moving direction
        horizontalMove = Input.GetAxis("Horizontal") * Agility;

        //Rotating
        if (horizontalMove < 0f) transform.localEulerAngles = new Vector3(0, 180, 0);
        if (horizontalMove > 0f) transform.localEulerAngles = new Vector3(0, 0, 0);

        //Jumping
        if (Input.GetButtonDown("Jump")) jump = true;
    }
    private void FixedUpdate()
    {
        //Call moving function
        Moving(horizontalMove, jump);
    }
    void Moving(float movement, bool canjump)
    {
        rigidBody.velocity = new Vector2(movement * Agility * Time.fixedDeltaTime, rigidBody.velocity.y);

        if (canjump && GetComponent<CircleCollider2D>().IsTouchingLayers())
        {
            rigidBody.AddForce(new Vector2(0, jumpForce));
            jump = !canjump;
        }
    }
}