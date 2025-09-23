using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using static UnityEngine.CullingGroup;

public enum EState
{
    DIE,
    RUN,
    JUMP

}
public class PlayerAnimation : MonoBehaviour
{
    PlayerController _controller;

    JumpState _jumpState;
    DeadState _dieState;
    RunState _runState;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();

        _runState = new RunState(_controller);
        _jumpState = new JumpState(_controller);
        _dieState = new DeadState(_controller);

        _controller.curState = _runState;
    }
    private void Update()
    {
        switch (_controller.curState.Exit())
        {
            case EState.DIE:
                _controller.curState = _dieState;
                _controller.curState.Enter();
                break;
            case EState.RUN:
                _controller.curState = _runState;
                _controller.curState.Enter();
                break;
            case EState.JUMP:
                _controller.curState = _jumpState;
                _controller.curState.Enter();
                break;
            default:
                break;
        }
        _controller.curState.Update();
    }




}
