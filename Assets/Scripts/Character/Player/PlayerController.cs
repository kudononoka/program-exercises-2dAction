using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    PlayerMove _moveState = new();
    public enum StateType
    {
        Move,
        Attack,
        Defense
    }
    Rigidbody2D _rb;
    StateMachine<PlayerController> _playerStateMachine = new();
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _moveState.Rigidbody2D = _rb;
        _playerStateMachine.StateAdd(_playerStateMachine,(int)StateType.Move, _moveState);
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
}
