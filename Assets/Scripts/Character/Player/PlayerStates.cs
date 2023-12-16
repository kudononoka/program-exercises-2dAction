using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class PlayerMove : StateMachine<PlayerController>.StateBase
{
    [SerializeField]
    [Header("歩行速度")]
    float _moveSpeed = 5;

    Rigidbody2D _rb = null;

    /// <summary>移動方向</summary>
    float _moveDirX = 0f;

    public Rigidbody2D Rigidbody2D { set { _rb = value; } }
    public override void OnStart()
    {
        Vector3 velocity = _rb.velocity;
        velocity.y = 0f;
        _rb.velocity = velocity;
    }
    public override void OnUpdate()
    {
        _moveDirX = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StateMachine.OnChangeState((int)PlayerController.StateType.Jump);
        }
    }
    public override void OnFixedUpdate()
    {
        _rb.velocity = new Vector2(_moveDirX * _moveSpeed, _rb.velocity.y);
    }

    public override void OnEnd()
    {

    }
}
[System.Serializable]
public class PlayerJump : StateMachine<PlayerController>.StateBase
{

    [SerializeField]
    [Tooltip("ジャンプ力")]
    float _jumpPower = 10;

    [SerializeField]
    [Tooltip("ジャンプ中に加える重力")]
    float _gravity = 3;
    float _movedir = 0f;
    Rigidbody2D _rb = null;
    GroundJudge _groundJudge = null;
    public GroundJudge GroundJudge { set { _groundJudge = value; } }
    public Rigidbody2D Rigidbody2D { set { _rb = value; } }
    public override void OnStart()
    {
        _rb.AddForce(_jumpPower * Vector2.up, ForceMode2D.Impulse);
        Vector3 velocity = _rb.velocity;
        velocity.y -= _gravity;
        _rb.velocity = velocity;
    }

    public override void OnUpdate()
    {
        _movedir = Input.GetAxisRaw("Horizontal");
        if (_groundJudge.IsGround)
        {
            StateMachine.OnChangeState((int)PlayerController.StateType.Move);
        }
        if (_movedir != 0)
        {
            Vector3 velocity = _rb.velocity;
            velocity.x *= _movedir;
            _rb.velocity = velocity;
        }
    }
    public override void OnFixedUpdate()
    {
        
    }

    public override void OnEnd()
    {
    }
}