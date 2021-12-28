using System.Collections.Generic;

public class JumpSequence
{
    public int Count => DiveFlow.Count;

    public int TotalSkydivers()
    {
        int running = 0;
        for (int i = 0; i < DiveFlow.Count; i++)
        {
            if (running < DiveFlow[i].AmountOfJumpers)
            {
                running = DiveFlow[i].AmountOfJumpers;
            }
        }
        return running;
    }

    public List<Formation> DiveFlow { get; private set; }

    public JumpSequence()
    {
        DiveFlow = new List<Formation>();
    }

    public void AddFormation(Formation formation)
    {
        DiveFlow.Add(formation);
    }
}
