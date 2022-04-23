using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RandomCharacterGenerator
{
    public static CharacterData CreateRandomCharacter()
    {
        string firstNameString = LoadRandomNFirstameString();

        string surname = LoadRandomSurname();
        // keep only what in front of first comma
        string[] firstNameSplit = firstNameString.Split(',');
        int age = Random.Range(14, 100);

        string occupation = LoadRandomOccupation();


        string gender =  "female";
        bool isMale = CheckIfMale(firstNameString);
        if(isMale)
        {
            gender = "male";
        }

        Color[] colors = ColorCreator.ColorWheel();

        List<OwnedItem> inventory = new List<OwnedItem>();

        // add random amount of random items

        int randomItemAmount = Random.Range(1,20);

        for(int i = 0; i < randomItemAmount; i++)
        {
            inventory.Add(CreateRandomItem());
        }



        CharacterData characterData = new CharacterData(firstNameSplit[0] + " " + surname, colors, inventory, CreateRandomLogbook(),gender,occupation, Random.Range(.2f,1.9f));

        return characterData;
    }


    static Logbook CreateRandomLogbook()
    {
        List<JumpLog> jumpLogs = new List<JumpLog>();



        List<string> myRatings = RandomRatingsCreator().ToList();

        Dictionary<JumpType, int> previousJumps = new Dictionary<JumpType, int>();

        return new Logbook(jumpLogs, myRatings, previousJumps);
    }

    static string[] RandomRatingsCreator()
    {


        
        string[] allRatings = FileHandler.GetRatingStrings();

        int randomRatingAmount = Random.Range(1, allRatings.Length);

        string[] ratings = new string[randomRatingAmount];
        for(int i = 0; i < randomRatingAmount; i++)
        {
            string randomRating = allRatings[Random.Range(0, allRatings.Length)];
            ratings[i] = randomRating;
        }
        return ratings;
    }



    static OwnedItem CreateRandomItem()
    {
        string[] keysFromItemDictionary = ItemDatabase.Instance.Items.Keys.ToArray();

        string randomKey = keysFromItemDictionary[Random.Range(0, keysFromItemDictionary.Length)];

        OwnableType randomType = ItemDatabase.Instance.GetItem(randomKey);

        OwnershipSpecs specs = CreateRandomOwnershipSpecs(randomType);



        OwnedItem ownedItem = new OwnedItem(randomType, specs);




        return ownedItem;
    }

    static OwnershipSpecs CreateRandomOwnershipSpecs(OwnableType type)
    {

        int age = Random.Range(0, type.lifeTime);

        //string size is "small" or "medium" or "large"

        int size = Random.Range(0, 3);

        Color[] colors = ColorCreator.PastelRainbowPallet();




        OwnershipSpecs ownershipSpecs = new OwnershipSpecs(age, size, colors, 0);


        return ownershipSpecs;
    }


    static string LoadRandomNFirstameString()
    {
        string names = Resources.Load<TextAsset>("names").text;

        string[] nameArray = names.Split('\n');


        return nameArray[Random.Range(0, nameArray.Length)];

    
    }

    static string LoadRandomSurname()
    {
        string surnames = Resources.Load<TextAsset>("lastNames").text;

        string[] surnameArray = surnames.Split('\n');

        return surnameArray[Random.Range(0, surnameArray.Length)];
    }

    static string LoadRandomOccupation()
    {
        string occupations = Resources.Load<TextAsset>("occupationsList").text;

        string[] occupationArray = occupations.Split('\n');

        return occupationArray[Random.Range(0, occupationArray.Length)];
    }

    static bool CheckIfMale(string name)
    {
        // if character after first comma is "M"
        if (name.Split(',')[1].Trim() == "M")
        {
            return true;
        }
        else
        {
            return false;
        }  
    }
    



}
