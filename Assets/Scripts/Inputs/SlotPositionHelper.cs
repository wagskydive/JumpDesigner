using UnityEngine;

public static class SlotPositionHelper
{
    public static Vector3 SlotOffset(int slot)
    {

        if (slot == 0)
        {
            return new Vector3(-1, 0, 1);
        }



        if (slot == 1)
        {
            return new Vector3(0, 0, 1);
        }


        if (slot == 2)
        {
            return new Vector3(1, 0, 1);
        }


        if (slot == 3)
        {
            return new Vector3(-1, 0, 0);
        }



        if (slot == 4)
        {
            return new Vector3(1, 0, 0);
        }


        if (slot == 5)
        {
            return new Vector3(-1, 0, -1);
        }


        if (slot == 6)
        {
            return new Vector3(0, 0, -1);
        }


        if (slot == 7)
        {
            return new Vector3(1, 0, -1);
        }
        else
        {
            return Vector3.zero;
        }
    }
}
