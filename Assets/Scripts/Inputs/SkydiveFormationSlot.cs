using System.Collections.Generic;


public class SkydiveFormationSlot
{
    public int SkydiverIndex { get; private set; }

    public int TargetIndex { get; private set; }
    public int Slot { get; private set; }
    public float BaseRotation { get; private set; }
    public FreefallOrientation Orientation { get; private set; }

    public Grip LeftGrip { get; private set; }
    public Grip RightGrip { get; private set; }


    public SkydiveFormationSlot(int skydiverIndex, FreefallOrientation orientation, int targetIndex, int slot, float baseRotation, Grip leftGrip = null, Grip rightGrip = null)
    {
        SkydiverIndex = skydiverIndex;
        Orientation = orientation;
        TargetIndex = targetIndex;
        Slot = slot;
        BaseRotation = baseRotation;

        LeftGrip = leftGrip;
        RightGrip = rightGrip;

    }


    
}
