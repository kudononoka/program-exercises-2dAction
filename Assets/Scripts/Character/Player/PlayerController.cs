using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour,IDamage
{
    [SerializeField] 
    PlayerMove _moveState = new();

    [SerializeField]
    PlayerJump _jumpState = new();

    GroundJudge _groundJudge;
    PlayerParameter _parameter;
    public enum StateType
    {
        Move,
        Jump,
        Attack,
        Defense
    }
    Rigidbody2D _rb;
    StateMachine<PlayerController> _playerStateMachine = new();
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _groundJudge = GetComponent<GroundJudge>();
        _jumpState.Rigidbody2D = _rb;
        _jumpState.GroundJudge = _groundJudge;
        _jumpState.Transform = transform;
        _moveState.Rigidbody2D = _rb;
        _playerStateMachine.StateAdd(_playerStateMachine,(int)StateType.Move, _moveState);
        _playerStateMachine.StateAdd(_playerStateMachine,(int)StateType.Jump, _jumpState);
        _playerStateMachine.OnStart((int)StateType.Move);
    }

    // Update is called once per frame
    void Update()
    {
        _playerStateMachine.OnUpdate();
    }

    private void FixedUpdate()
    {
        _playerStateMachine.OnFixedUpdate();
    }

    public void Damage(int damage)
    {
        _parameter.ChangeHp(_parameter.CurrentHp - damage);
    }
}
