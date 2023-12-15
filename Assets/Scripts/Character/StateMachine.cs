using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステートマシーン
/// このクラスを持つ場合ステートの状態を表すEnumを作成する必要がある
/// </summary>
/// <typeparam name="StateType">各ステートの状態(TypeはEnumのみ)</typeparam>
public class StateMachine<T>
{
    /// <summary>ステートの基底クラス・各ステートはこのクラスを継承する</summary>
    public abstract class StateBase
    {
        StateMachine<T> _stateMachine;
        /// <summary>ステートマシーン</summary>
        public StateMachine<T> StateMachine { get { return _stateMachine; } set { _stateMachine = value; } }
        /// <summary>ステートが切り替わリ後最初に１回呼ばれる</summary>
        public abstract void OnStart();
        /// <summary>毎フレーム呼ばれる</summary>
        public abstract void OnUpdate();
        /// <summary>FixedUpdateで呼ばれる</summary>
        public abstract void OnFixedUpdate();
        /// <summary>ステートが切り替わリ前に１回呼ばれる</summary>
        public abstract void OnEnd();
    }
    /// <summary>現在のState</summary>
    private StateBase _currentState;
    /// <summary>各Stateをもつ</summary>
    private readonly Dictionary<int, StateBase> _states = new Dictionary<int, StateBase>();

    /// <summary>ステートの登録</summary>
    /// <param name="state">登録したいステート</param>
    public void StateAdd(StateMachine<T> machine, int stateId, StateBase state)
    {
        //すでに登録していたら何もしない
        if (_states.ContainsKey(stateId))
        {
            Debug.LogError("not set state! : " + stateId);
            return;
        }
        state.StateMachine = machine;
        //登録
        _states.Add(stateId, state);
    }
    /// <summary>最初に行うステートの設定</summary>
    /// <param name="state">ステートのタイプ</param>
    public void OnStart(int stateId)
    {
        //登録していなかったらエラーを出す
        if (!_states.ContainsKey(stateId))
        {
            Debug.LogError("not set state! : " + stateId);
            return;
        }
        //最初に行われるステートとして設定
        _currentState = _states[stateId];
        _currentState.OnStart();
    }
    /// <summary>現在のステートを毎フレーム行う</summary>
    public void OnUpdate()
    {
        _currentState.OnUpdate();
    }

    public void OnFixedUpdate()
    {
        _currentState.OnFixedUpdate();
    }

    /// <summary>ステートの切り替え</summary>
    /// <param name="state">切り替えたいステートのタイプ</param>
    public void OnChangeState(int stateId)
    {
        _currentState.OnEnd();
        if (!_states.ContainsKey(stateId))
        {
            Debug.LogError("not set state! : " + stateId);
            return;
        }
        // ステートを切り替える
        _currentState = _states[stateId];
        _currentState.OnStart();
    }
}
