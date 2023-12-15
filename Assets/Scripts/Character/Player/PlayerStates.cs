using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerMove : StateMachine<PlayerController>.StateBase
{
    [SerializeField]
    [Header("���s���x")]
    float _moveSpeed = 5;

    Rigidbody2D _rb = null;

    /// <summary>�ړ�����</summary>
    float _moveDirX = 0f;

    public Rigidbody2D Rigidbody2D { set { _rb = value; } }
    public override void OnStart()
    {
    }
    public override void OnUpdate()
    {
        _moveDirX = Input.GetAxisRaw("Horizontal");
    }
    public override void OnFixedUpdate()
    {
        _rb.velocity = new Vector2(_moveDirX * _moveSpeed, _rb.velocity.y);
    }

    public override void OnEnd()
    {

    }
}