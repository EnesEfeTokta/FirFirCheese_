//TODO: Zıplama animasyonuna uygun optimizasyon yap.
//TODO: FreeLookCamera 'yı hedefe bakacak şekilde ayarla.



using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;



// Gerekli olan Component 'ler ekleniyor
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]



public class CharacterControl : MonoBehaviour
{
    [Header("CharacterController")]
    [SerializeField] private CharacterController controller;



    [Header("Movement")]
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float sprintSpeed = 2f;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float gravity = -10f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float forceLeft = -10f;//#3



    [Header("Ground")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    bool doubleJump = false;



    Vector3 velocity = Vector3.zero;
    bool isGrounded;
    Rigidbody rb;



    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private bool isBackDeath = false;
    [SerializeField] private bool isPreDeath = false;



    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();//#2

    }



    void Update()
    {
        Movement();
        Jump();

        if (isBackDeath)
        {
            animator.SetTrigger("isBackDeath");
        }
        if (isPreDeath)
        {
            animator.SetTrigger("isPreDeath");
        }
    }

    void ForceNpc()//#5
    {
        rb.AddForce(forceLeft, 5, 5 , ForceMode.Impulse);

    }



    void Movement()
    {
        // Klavye girdileri alınıyor
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;

        if (moveHorizontal != 0f || moveVertical != 0f)
        {
            animator.SetBool("isWalk", true);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                // Koşma hızında hareket gerçekleşiyor
                controller.Move(move * sprintSpeed * Time.deltaTime);
            }
            else
            {
                // Yürüme hızında hareket gerçekleşiyor
                controller.Move(move * speed * Time.deltaTime);
            }
        }
        else
        {
            // Karakter beklemede
            animator.SetBool("isWalk", false);
        }

        transform.Rotate(Vector3.up * moveHorizontal * rotationSpeed * Time.deltaTime);

        // Karakterin yönünü sadece ileri ve geri hareketlerde güncelle
        if (moveVertical > 0 || moveVertical < 0)
        {
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }

    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            ForceNpc();
        }
    }



    void Jump()
    {
        // Zemin Layer 'ına taması kontrol ediliyor
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Zıplama kontrolü
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            //animator.SetTrigger("isJumping");
            doubleJump = true;
        }
        else if (Input.GetButtonDown("Jump") && doubleJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            //animator.SetTrigger("isJumping");
            doubleJump = false;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
