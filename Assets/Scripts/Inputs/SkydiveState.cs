public class SkydiveState
{
    public SkydiveState(FreefallOrientation freefallOrientation,ISelectable target = null, int slot = 0 , float baseRotation = 0, Grip leftGrip = null, Grip rightGrip = null)
    {
        Orientation = freefallOrientation;
        LeftGrip = leftGrip;
        RightGrip = rightGrip;
        Target = target;
        Slot = slot;
        BaseRotation = baseRotation;
    }

    public Grip LeftGrip { get; private set; }
    public Grip RightGrip { get; private set; }
    public ISelectable Target { get; private set; }
    public int Slot { get; private set; }
    public float BaseRotation{ get; private set; }
    public FreefallOrientation Orientation { get; private set; }
}