using UnityEngine;

public class RunState : IAnimation
{
    private PlayerController _controller;
    public RunState(PlayerController Controller)
    {
        _controller = Controller;
    }

    public void Enter()
    {
        _controller._animator.Play("run");
    }
    public void Update()
    {
        
    }

    public EState Exit()
    {
        if(_controller.IsDie)
        {
            return EState.DIE;
        }
        else if (_controller.IsJump)
        {
            return EState.JUMP;
        }
        else return EState.RUN;
    }
}
