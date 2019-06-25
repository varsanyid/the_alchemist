using UnityEngine;
using System.Collections;

public class ControllerState2D
{
    public bool IsCollidingRight { get; set; }
    public bool IsCollidingLeft { get; set; }
    public bool IsCollidingAbove { get; set; }
    public bool IsCollidingBelow { get; set; }
    public bool IsMovingDownSlope { get; set; }
    public bool IsMovingUpSlope { get; set; }
    public bool IsGrounded { get { return IsCollidingBelow; } }
    public float SlopeAngle { get; set; }
    public bool HasCollisions
    {
        get
        {
            return IsCollidingAbove || IsCollidingBelow || IsCollidingLeft || IsCollidingRight;
        }
    }
    public void Reset()
    {
        IsMovingUpSlope =
            IsMovingDownSlope =
            IsCollidingLeft =
            IsCollidingRight =
            IsCollidingBelow =
            IsCollidingAbove = false;
        SlopeAngle = 0;
    }

    public override string ToString()
    {
        return
            string.Format("controller: right:{0} left:{1} above:{2} below:{3} down-slope:{4} up-slope:{5} angle:{6}",
            IsCollidingRight, IsCollidingLeft, IsCollidingAbove, IsCollidingBelow, IsMovingDownSlope, IsMovingUpSlope, SlopeAngle);
    }
}
