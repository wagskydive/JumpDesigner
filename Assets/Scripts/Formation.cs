using System.Collections.Generic;

public class Formation
{
    public List<SkydiveFormationSlot> FormationSlots;

    public int AmountOfJumpers => FormationSlots.Count + 1;

    public FreefallOrientation BaseOrientation { get; private set; }

    public Formation(FreefallOrientation baseOrientation)
    {
        BaseOrientation = baseOrientation;
        FormationSlots = new List<SkydiveFormationSlot>();
    }

    public void SetBaseOrientation(FreefallOrientation freefallOrientation)
    {
        BaseOrientation = freefallOrientation;
    }

    public void AddSlot(SkydiveFormationSlot slot)
    {
        if (ValidateNewSlot(slot))
        {
            FormationSlots.Add(slot);
        }
    }

    public FreefallOrientation GetOrientation(int skydiverIndex)
    {
        if(skydiverIndex == 0)
        {
            return BaseOrientation;
        }
        else
        {
            return FormationSlots[skydiverIndex - 1].Orientation;
        }
    }

    public float GetRotation(int skydiverIndex)
    {
        if (skydiverIndex == 0)
        {
            return 0;
        }
        else
        {
            return FormationSlots[skydiverIndex - 1].BaseRotation;
        }
    }

    private bool ValidateNewSlot(SkydiveFormationSlot slot)
    {
        for (int i = 0; i < FormationSlots.Count; i++)
        {
            if(FormationSlots[i].SkydiverIndex == slot.SkydiverIndex)
            {
                return false;
            }
            if(FormationSlots[i].TargetIndex == slot.TargetIndex)
            {
                if(FormationSlots[i].Slot == slot.Slot)
                {
                    return false;
                }
            }
           
        }
        return true;
    }
}
