using UnityEngine;

public class JumpState : IAnimation
{
    private PlayerController _controller;
    public JumpState(PlayerController Controller)
    {
        _controller = Controller;
    }

    public void Enter()
    {
        _controller.IsJump = false;
    }
    public void Update()
    {
        _controller._animator.Play("jump");
    }

    public EState Exit()
    {
        if (_controller.IsDie)
        {
            return EState.DIE;
        }
        else if (_controller.IsGround == true)
        {
            _controller._rb.AddForce(Vector2.down * 200);
            return EState.RUN;
        }
        else return EState.JUMP;
    }
}
