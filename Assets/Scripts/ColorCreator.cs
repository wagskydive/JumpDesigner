using UnityEngine;

public static class ColorCreator
{

    public static Color[] RainbowBright()
    {
        Color[] colors = new Color[10];

        colors[0] = new Color(1, 0, 0);
        colors[1] = new Color(1, 0.5f, 0);
        colors[2] = new Color(1, 1, 0);
        colors[3] = new Color(0.5f, 1, 0);
        colors[4] = new Color(0, 1, 0);
        colors[5] = new Color(0, 1, 0.5f);
        colors[6] = new Color(0, 1, 1);
        colors[7] = new Color(0, 0.5f, 1);
        colors[8] = new Color(0, 0, 1);
        colors[9] = new Color(0.5f, 0, 1);


        return colors;


    }


    public static Color[] ColorWheel()
    {
        Color[] colors = new Color[12];


        colors[0] = new Color(1, 0, 0);

        colors[1] = new Color(1, 0.5f, 0);

        colors[2] = new Color(1, 1, 0);

        colors[3] = new Color(0.5f, 1, 0);

        colors[4] = new Color(0, 1, 0);

        colors[5] = new Color(0, 1, 0.5f);

        colors[6] = new Color(0, 1, 1);

        colors[7] = new Color(0, 0.5f, 1);

        colors[8] = new Color(0, 0, 1);

        colors[9] = new Color(0.5f, 0, 1);

        colors[10] = new Color(1, 0, 1);

        colors[11] = new Color(1, 0, 0.5f);




        return colors;

    }

       public static  Color[] PastelRainbowPallet()
    {
        Color[] colors = new Color[10];
        // red pastel


        colors[0] = new Color(1, 0.5f, 0.5f);

        // orange pastel
        colors[1] = new Color(1, 0.75f, 0.5f);
        colors[2] = new Color(1, 1, 0.5f);
        colors[3] = new Color(0.75f, 1, 0.5f);
        colors[4] = new Color(0.5f, 1, 0.5f);
        colors[5] = new Color(0.5f, 1, 0.75f);
        colors[6] = new Color(0.5f, 1, 1);
        colors[7] = new Color(0.5f, 0.75f, 1);
        colors[8] = new Color(0.5f, 0.5f, 1);

        // green pastel
        colors[9] = new Color(0.75f, 0.5f, 1);



        return colors;
    }

    public static Color[] ShadesOfBrown()
    {       
        Color[] colors = new Color[10];
        // brown
        colors[0] = new Color(0.5f, 0.3f, 0.1f);
        colors[1] = new Color(0.6f, 0.4f, 0.2f);
        

        // dark brown
        colors[2] = new Color(0.4f, 0.2f, 0.1f);
        colors[3] = new Color(0.5f, 0.3f, 0.2f);
        colors[4] = new Color(0.6f, 0.4f, 0.3f);
        colors[5] = new Color(0.7f, 0.5f, 0.4f);
        colors[6] = new Color(0.8f, 0.6f, 0.5f);
        colors[7] = new Color(0.9f, 0.7f, 0.6f);
        colors[8] = new Color(1f, 0.8f, 0.7f);
        colors[9] = new Color(1f, 0.9f, 0.8f);
        return colors;
    }

}
