using Unity.Netcode;

using UnityEngine;
using UnityEngine.UIElements;

public class thirdpersonmovement : NetworkBehaviour
{
    public Transform player;
    public CharacterController controller;
    public Transform groundCheck;
    public Transform headCheck;
    public LayerMask groundMask;

    public float baseSpeed, runSpeed, jumpHeight, airDescentSpeed;

    private Vector3 verticalMovementAmount = new Vector3(0f, -2f, 0f);
    private float activespeed;
    private float ground_check_size = 0.42f;
    private bool groundContact;
    private bool headContact;

    private Vector3 previousPosition;
    private Vector3 velocity;

    private bool isJumping;
    private float groundAttractionAmount;

    private void Start()
    {
        if (!IsOwner) return;
        previousPosition = transform.position;

        groundAttractionAmount = airDescentSpeed / 4f * -1f;
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        velocity = transform.position - previousPosition;
        previousPosition = transform.position;
        groundContact = Physics.CheckSphere(groundCheck.position, ground_check_size, groundMask);
        headContact = Physics.CheckSphere(headCheck.position, ground_check_size, groundMask);
    }

    void Update()
    {
        if (!IsOwner) return;
        isJumping = verticalMovementAmount.y > 0f;

        if (!groundContact) {
            if (headContact) {
                verticalMovementAmount.y = groundAttractionAmount / 4;
            }
        }
        if (groundContact && !isJumping) {
            verticalMovementAmount.y = groundAttractionAmount;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 move = player.transform.right * x + player.transform.forward * z;


        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            activespeed = runSpeed;
        } else {
            activespeed = baseSpeed;
        }


        if (Input.GetButtonDown("Jump") && groundContact) {
            verticalMovementAmount.y = Mathf.Sqrt(jumpHeight * airDescentSpeed);
        }
        verticalMovementAmount.y -= airDescentSpeed * Time.deltaTime;

        controller.Move((move.normalized * activespeed + verticalMovementAmount) * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, ground_check_size);
        Gizmos.DrawWireSphere(headCheck.position, ground_check_size);
    }
}