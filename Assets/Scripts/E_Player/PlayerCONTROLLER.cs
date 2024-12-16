using UnityEngine;

public class PlayerCONTROLLER : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _gravity = 9.81f;
    [SerializeField] private float _jumpPower = 3f;
    [SerializeField] private float _speedRun = 6f;
    [SerializeField] private float _speedSit = 1f;
    [SerializeField] private float _ManaSpend = 0.8f;
    [SerializeField] private float _ManaReset = 0.8f;


    private CharacterController _characterController;
    private Vector3 _walkDirection;
    private Vector3 _velocity;
    private float _speedWalk = 0f;
    private float Mana = 100f;
    bool flagSpendMana = true;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Jump(_characterController.isGrounded && Input.GetKey(KeyCode.Space));
        Run(Input.GetKey(KeyCode.LeftShift), Input.GetKey(KeyCode.LeftControl));
        Sit(Input.GetKey(KeyCode.LeftControl), Input.GetKey(KeyCode.LeftShift));
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _walkDirection = transform.right * x + transform.forward * z;
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

    private void Run(bool canRun, bool canSit)
    {
        if (!canSit)
        {
            _speedWalk = (canRun && Mana > 0 && flagSpendMana) ? _speedRun : _speed;
            if (canRun && Mana > 0 && flagSpendMana)
            {
                Mana -= _ManaSpend;
            }
        }
        if (Mana < 0f)
        {
            flagSpendMana = false;
        }
        if (Mana < 100f && !(canRun && flagSpendMana))
        {
            Mana += _ManaReset;
        }
        if (Mana >= 25f)
        {
            flagSpendMana = true;
        }
    }

    private void Sit(bool canSit, bool canRun)
    {
        if (!canRun)
        {
            _speedWalk = canSit ? _speedSit : _speed;
            _characterController.height = (canSit && !canRun) ? 0.8f : 2f;
        }
    }
}
