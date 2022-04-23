public class JumpLog
{
    public int number { get; private set; }
    public JumpType jumpType { get; private set; }
    public int day { get; private set; }
    public int[] time { get; private set; }
    public string location { get; private set; }
    public string aircraftType { get; private set; }

    public JumpLog(JumpType _jumpType, int _number, int _day, int[] _time, string _location, string _aircraftType)
    {
        jumpType = _jumpType;
        number = _number;
        day = _day;
        time = _time;
        location = _location;
        aircraftType = _aircraftType;
    }
}

