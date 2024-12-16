using UnityEngine;

public class PlayerCONTROLLER : MonoBehaviour
{
    [SerializeField] private float _speedWalk;
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _speedRun;
    [SerializeField] private float _speedSit;


    private CharacterController _characterController;
    private Vector3 _walkDirection;
    private Vector3 _velocity;
    private float _speed;
    private void Start()
    {
        _speed = _speedWalk;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Jump(_characterController.isGrounded && Input.GetKey(KeyCode.Space));
        Run(Input.GetKey(KeyCode.LeftShift));
        Sit(Input.GetKey(KeyCode.LeftControl));
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _walkDirection = transform.right * x + transform.forward * z;
    }

    private void FixedUpdate()
    {
        Walk(_walkDirection);
        DoGravity(_characterController.isGrounded);
    }
    private void Walk(Vector3 direction)
    {
        _characterController.Move(direction * _speedWalk * Time.fixedDeltaTime);
    }

    private void DoGravity(bool isGrounded)
    {
        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -1f;
        }

        _velocity.y -= _gravity * Time.fixedDeltaTime;
        _characterController.Move(_velocity * Time.fixedDeltaTime);
    }

    private void Jump(bool canjump)
    {
        if (canjump)
            _velocity.y = _jumpPower;
    }

    private void Run(bool canRun)
    {
        _speedWalk = canRun ? _speedRun : _speed;

    }

    private void Sit(bool canSit)
    {
        _speedWalk = canSit ? _speedSit : _speed;
        _characterController.height = canSit ? 1f : 2f;

    }



}
