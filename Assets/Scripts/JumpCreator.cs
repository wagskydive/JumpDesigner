
public static class JumpCreator
{
    public static JumpSequence FourWayTestJump(FreefallOrientation orientation)
    {
        JumpSequence jumpSequence = new JumpSequence();
        jumpSequence.AddFormation(Star4Way(orientation));
        jumpSequence.AddFormation(OpenAccordian4Way(orientation));
        jumpSequence.AddFormation(DoubleTwins4Way(orientation));
        jumpSequence.AddFormation(Compressed4Way(orientation));
        return jumpSequence;
    }

    private static Formation DoubleTwins4Way(FreefallOrientation orientation)
    {
        Formation thirdFormation = new Formation(orientation);
        thirdFormation.AddSlot(new SkydiveFormationSlot(1, orientation, 0, 2, 180));
        thirdFormation.AddSlot(new SkydiveFormationSlot(2, orientation, 0, 0, 0));
        thirdFormation.AddSlot(new SkydiveFormationSlot(3, orientation, 2, 2, 180));
        return thirdFormation;
    }
    
    private static Formation Compressed4Way(FreefallOrientation orientation)
    {
        Formation thirdFormation = new Formation(orientation);
        thirdFormation.AddSlot(new SkydiveFormationSlot(1, orientation, 0, 4, 180));
        thirdFormation.AddSlot(new SkydiveFormationSlot(2, orientation, 3, 4, 180));
        thirdFormation.AddSlot(new SkydiveFormationSlot(3, orientation, 0, 5, 180));
        return thirdFormation;
    }

    private static Formation OpenAccordian4Way(FreefallOrientation orientation)
    {
        Formation secondFormation = new Formation(orientation);
        secondFormation.AddSlot(new SkydiveFormationSlot(1, orientation, 0, 1, 180));
        secondFormation.AddSlot(new SkydiveFormationSlot(2, orientation, 3, 1, 180));
        secondFormation.AddSlot(new SkydiveFormationSlot(3, orientation, 0, 3, 180));
        return secondFormation;
    }

    private static Formation Star4Way(FreefallOrientation orientation)
    {
        Formation firstFormation = new Formation(orientation);
        firstFormation.AddSlot(new SkydiveFormationSlot(1, orientation, 0, 1, 90));
        firstFormation.AddSlot(new SkydiveFormationSlot(2, orientation, 0, 3, -90));
        firstFormation.AddSlot(new SkydiveFormationSlot(3, orientation, 2, 3, -90));
        return firstFormation;
    }
}
