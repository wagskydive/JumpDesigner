public class SkydiveState
{
    public SkydiveState(SkydiveFormationSlot slot)
    {
        FormationSlot = slot;
        Orientation = slot.Orientation;
    }

    public SkydiveState(FreefallOrientation orientation)
    {
        Orientation = orientation;
    }


    public int Target { get => FormationSlot.TargetIndex; }
    public int Slot { get => FormationSlot.Slot; }
    public float BaseRotation{ get => FormationSlot.BaseRotation; }
    public FreefallOrientation Orientation;

    public SkydiveFormationSlot FormationSlot;
}