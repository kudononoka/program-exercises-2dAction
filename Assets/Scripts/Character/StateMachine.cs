using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステートマシーン
/// このクラスを持つ場合ステートの状態を表すEnumを作成する必要がある
/// </summary>
/// <typeparam name="StateType">各ステートの状態(TypeはEnumのみ)</typeparam>
public class StateMachine<StateType> where StateType : Enum
{
    /// <summary>ステートの基底クラス・各ステートはこのクラスを継承する</summary>
    public abstract class StateBase
    {
        StateMachine<StateType> _stateMachine;
        StateType _stateType;
        /// <summary>ステートのタイプ</summary>
        public StateType StateType => _stateType;
        /// <summary>ステートマシーン</summary>
        public StateMachine<StateType> StateMachine =>_stateMachine;
        /// <summary>ステートが切り替わリ後最初に１回呼ばれる</summary>
        public abstract void OnStart();
        /// <summary>毎フレーム呼ばれる</summary>
        public abstract void OnUpdate();
        /// <summary>ステートが切り替わリ前に１回呼ばれる</summary>
        public abstract void OnEnd();
    }
    /// <summary>現在のState</summary>
    private StateBase _currentState;
    /// <summary>各Stateをもつ</summary>
    private readonly Dictionary<StateType, StateBase> _states;

    /// <summary>ステートの登録</summary>
    /// <param name="state">登録したいステート</param>
    public void StateAdd(StateBase state)
    {
        //すでに登録していたら何もしない
        if (_states.ContainsKey(state.StateType))
        {
            return;
        }
        //登録
        _states.Add(state.StateType, state);
    }
    /// <summary>最初に行うステートの設定</summary>
    /// <param name="state">ステートのタイプ</param>
    public void OnStart(StateType state)
    {
        //登録していなかったらエラーを出す
        if (!_states.ContainsKey(state))
        {
            Debug.LogError("not set state! : " + state);
            return;
        }
        //最初に行われるステートとして設定
        _currentState = _states[state];
        _currentState.OnStart();
    }
    /// <summary>現在のステートを毎フレーム行う</summary>
    public void OnUpdate()
    {
        _currentState.OnUpdate();
    }

    /// <summary>ステートの切り替え</summary>
    /// <param name="state">切り替えたいステートのタイプ</param>
    public void OnChangeState(StateType state)
    {
        _currentState.OnEnd();
        if (!_states.ContainsKey(state))
        {
            Debug.LogError("not set state! : " + state);
            return;
        }
        // ステートを切り替える
        _currentState = _states[state];
        _currentState.OnStart();
    }
}
