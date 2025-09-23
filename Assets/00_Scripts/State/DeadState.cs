using UnityEngine;

public class DeadState : IAnimation
{
    private PlayerController _controller;
    public DeadState(PlayerController Controller)
    {
        _controller = Controller;
    }

    public void Enter()
    {
        _controller._animator.Play("die");
    }
    public void Update()
    {
        
    }

    public EState Exit()
    {
        if (_controller.isDie)
        {
            return EState.DIE;
        }
        else
            return EState.RUN;
    }


}
