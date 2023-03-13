using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Objects assigned in editor")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject sickle;

    [Header("Variables from scriptable object on Start")]
    [SerializeField] private MovementInfo movementInfo;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float cutRange;
    

    [Header("Assigned on Start")]
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;


    private void Start()
    {
        playerSpeed = movementInfo.playerSpeed;
        cutRange = movementInfo.cutRange;

        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move = new Vector3(-joystick.Vertical, 0, joystick.Horizontal);
        controller.Move(playerSpeed * Time.deltaTime * move);
        controller.Move(Time.deltaTime * Vector3.up * Physics.gravity.y);

        if (move != Vector3.zero)
        {
            transform.forward = move;

            animator.SetTrigger("isWalking");
            animator.ResetTrigger("isAttacking");

            sickle.GetComponent<BoxCollider>().enabled = false;
            sickle.SetActive(false);
        }
        else
        {
            animator.ResetTrigger("isWalking");
        }
    }

    private void FixedUpdate()
    {

        if (controller.velocity.x != 0 && controller.velocity.z != 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, cutRange, 8))
            {
                sickle.SetActive(true);
                animator.SetTrigger("isAttacking");
            }
        }
    }

    public void ToggleWeaponState(string isActive) => sickle.GetComponent<BoxCollider>().enabled = string.IsNullOrEmpty(isActive);

}




