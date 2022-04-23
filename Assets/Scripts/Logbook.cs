using System;
using System.Collections.Generic;

public class Logbook
{
    public event Action OnJumpAdded;
    public event Action<string> OnRatingAdded;

    public Dictionary<JumpType, int> previousJumps;

    public List<JumpLog> jumpLogs { get; private set; }

    public List<string> myRatings { get; private set; }

    public Logbook(List<JumpLog> jumpLogs=null, List<string> myRatings=null, Dictionary<JumpType, int> previousJumps=null)
    {
        this.jumpLogs = jumpLogs;
        this.myRatings = myRatings;
        this.previousJumps = previousJumps;

        if(jumpLogs == null)
        {
            jumpLogs = new List<JumpLog>();
        }
        if(myRatings == null)
        {
            myRatings = new List<string>();
        }
        if(previousJumps == null)
        {
            previousJumps = new Dictionary<JumpType, int>();
        }
    }

    public void AddJumpToLogbook(JumpType jumpType, int day, int[] time, string _location = "my dropzone", string _aircraftType = "My aircraft")
    {
        jumpLogs.Add(new JumpLog(jumpType, jumpLogs.Count + 1, day, time, _location, _aircraftType));
        OnJumpAdded?.Invoke();
    }


        public void AddRating(string _rating)
    {
        if (!myRatings.Contains(_rating))
        {
            myRatings.Add(_rating);
            OnJumpAdded?.Invoke();
        }


    }
    public int GetTotalPerJumpType(JumpType jumpType)
    {
        int runningTotal = 0;
        if (previousJumps.ContainsKey(jumpType))
        {
            runningTotal += previousJumps[jumpType];
        }
        for (int i = 0; i < jumpLogs.Count; i++)
        {
            if (jumpLogs[i].jumpType == jumpType)
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
        runningtotal += jumpLogs.Count;
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

