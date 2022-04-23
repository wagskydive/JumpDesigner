using System;
using System.Collections.Generic;

public class Logbook
{
    public event Action OnJumpAdded;
    public event Action<Ratings> OnRatingAdded;

    public Dictionary<JumpType, int> previousJumps;

    public List<JumpLog> jumpsTotal { get; private set; }

    public List<Ratings> myRatings { get; private set; }

    public Logbook()
    {

        jumpsTotal = new List<JumpLog>();
        myRatings = new List<Ratings>();
        previousJumps = new Dictionary<JumpType, int>();
    }

    public void AddJumpToLogbook(JumpType jumpType, int day, int[] time, string _location = "my dropzone", string _aircraftType = "My aircraft")
    {
        jumpsTotal.Add(new JumpLog(jumpType, jumpsTotal.Count + 1, day, time, _location, _aircraftType));
        OnJumpAdded?.Invoke();
    }


        public void AddRating(Ratings _rating)
    {
        if (_rating == Ratings.TandemPassenger || !myRatings.Contains(_rating))
        {
            myRatings.Add(_rating);
            OnJumpAdded?.Invoke();
        }
        else
        {

        }

    }
    public int GetTotalPerJumpType(JumpType jumpType)
    {
        int runningTotal = 0;
        if (previousJumps.ContainsKey(jumpType))
        {
            runningTotal += previousJumps[jumpType];
        }
        for (int i = 0; i < jumpsTotal.Count; i++)
        {
            if (jumpsTotal[i].jumpType == jumpType)
            {
                runningTotal++;
            }
        }

        return runningTotal;
    }


    public int GetJumpNumber()
    {
        int runningtotal = 0;
        foreach (var key in previousJumps.Keys)
        {
            runningtotal += previousJumps[key];
        }
        runningtotal += jumpsTotal.Count;
        return runningtotal;
    }

    public void AddPreviousJumps(JumpType jumpType, int amount)
    {

        if (previousJumps.ContainsKey(jumpType))
        {
            previousJumps[jumpType] += amount;
        }
        else
        {
            previousJumps.Add(jumpType, amount);
        }
    }
}

