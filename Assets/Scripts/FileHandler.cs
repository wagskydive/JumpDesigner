using System.IO;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;


public static class FileHandler
{
    public static JSONObject JSONFromAircraftType(AircraftType aircraftType)
    {
        JSONObject json = new JSONObject();
        json.Add("name", aircraftType.AircraftTypeName);
        json.Add("fileName", aircraftType.FileName);
        json.Add("movingSpeed", aircraftType.MovingSpeed);
        json.Add("amountOfSeats", aircraftType.AmountOfSeats);
        return json;
    }

    public static AircraftType AircraftTypeFromJSON(JSONObject json)
    {
        return new AircraftType(json["name"], json["fileName"], json["movingSpeed"].AsFloat, json["amountOfSeats"].AsInt);
    }

    public static List<AircraftType> LoadAircraftTypes()
    {
        List<AircraftType> aircraftTypes = new List<AircraftType>();
        string directoryPath = Application.dataPath + "/Resources/AircraftTypes/";

        foreach (string filePath in Directory.GetFiles(directoryPath))
        {
            string fileName = Path.GetFileName(filePath);
            if (fileName.EndsWith(".json"))
            {
                string jsonString = File.ReadAllText(filePath);
                JSONObject json = (JSONObject)JSON.Parse(jsonString);
                aircraftTypes.Add(AircraftTypeFromJSON(json));
            }
        }


        return aircraftTypes;
    }



    public static List<JumpSequence> ReadInternalJumps()
    {
        List<JumpSequence> internalJumpSequences = new List<JumpSequence>();

        string path = "DefaultJumps/";

        var loadFiles = Resources.LoadAll<TextAsset>(path);

        for (int i = 0; i < loadFiles.Length; i++)
        {

            internalJumpSequences.Add(SequenceFromTextAsset(loadFiles[i]));
        }


        return internalJumpSequences;
    }

    public static JumpSequence SequenceFromTextAsset(TextAsset textAsset)
    {
        Debug.Log(textAsset.text);

        JSONObject saveRead = JSONNode.Parse(textAsset.text).AsObject;

        return JumpSequenceFromJSON(saveRead);
    }


    public static List<JumpSequence> ReadSavedJumps()
    {
        List<JumpSequence> savedJumpSequences = new List<JumpSequence>();

        string path = Application.persistentDataPath + "/saves/";


        DirectoryInfo directoryInfo = new DirectoryInfo(path);

        for (int i = 0; i < directoryInfo.GetFiles().Length; i++)
        {

            if (directoryInfo.GetFiles()[i].Extension == ".json")
            {
                JSONObject saveRead = JSONNode.Parse(File.ReadAllText(directoryInfo.GetFiles()[i].FullName)).AsObject;
                savedJumpSequences.Add(JumpSequenceFromJSON(saveRead));
            }

        }

        return savedJumpSequences;
    }

    public static string[] GetRatingStrings()
    {

        TextAsset textAsset = Resources.Load<TextAsset>("RatingTree") as TextAsset;
        JSONObject fileRead = JSONNode.Parse(textAsset.text).AsObject;

        List<string> ratingStrings = new List<string>();

        foreach (KeyValuePair<string, JSONNode> entry in fileRead)
        {
            ratingStrings.Add(entry.Key);
        }

        

        Debug.Log(ratingStrings);


        return ratingStrings.ToArray();
    }


    public static List<CharacterData> ReadSavedCharacaters()
    {
        List<CharacterData> savedJumpSequences = new List<CharacterData>();

        string path = Application.persistentDataPath + "/characters/";


        DirectoryInfo directoryInfo = new DirectoryInfo(path);

        for (int i = 0; i < directoryInfo.GetFiles().Length; i++)
        {

            if (directoryInfo.GetFiles()[i].Extension == ".json")
            {
                JSONObject saveRead = JSONNode.Parse(File.ReadAllText(directoryInfo.GetFiles()[i].FullName)).AsObject;
                savedJumpSequences.Add(CharacterDataFromJSON(saveRead));
            }

        }

        return savedJumpSequences;
    }

    public static CharacterData CharacterDataFromJSON(JSONObject saveRead)
    {
        string characterName = saveRead.GetValueOrDefault("Name", saveRead);
        string characterGender = saveRead.GetValueOrDefault("Gender", saveRead);
        string characterOccupation =saveRead.GetValueOrDefault("Occupation", saveRead);

        float characterTalent = saveRead.GetValueOrDefault("Talent", saveRead);



        Logbook Logbook = LogbookFromJSON(saveRead.GetValueOrDefault("Logbook", saveRead).AsObject);

        List<OwnedItem> inventory = InventoryFromJSON(saveRead.GetValueOrDefault("Inventory", saveRead).AsArray);

        Color[] colors = ColorsFromJSON(saveRead.GetValueOrDefault("Colors", saveRead).AsObject);   

        CharacterData characterData = new CharacterData(characterName, colors, inventory, Logbook, characterGender, characterOccupation, characterTalent);
        



        return characterData;
    }

    private static Color[] ColorsFromJSON(JSONObject colorObject)
    {
        Color[] colors = new Color[colorObject.Count];

        for (int i = 0; i < colorObject.Count; i++)
        {
            colors[i] = ColorFromHex(colorObject[i]);
        }

        return colors;
    }

    public static JSONObject JSONFromJumpLog(JumpLog jumpLog)
    {
        JSONObject json = new JSONObject();
        json.Add("number", jumpLog.number);
        json.Add("jumpType", jumpLog.jumpType.ToString());
        json.Add("day", jumpLog.day);
        json.Add("hour", jumpLog.time[0]);
        json.Add("minute", jumpLog.time[1]);
        json.Add("location", jumpLog.location);
        json.Add("aircraftType", jumpLog.aircraftType);
        return json;
    }


    public static JSONObject JSONFromLogbook(Logbook logbook)
    {
        JSONObject json = new JSONObject();
        json.Add("Ratings", JSONFromRatings(logbook.myRatings));

        JSONArray jsonArray = new JSONArray();
        for(int i = 0; i < logbook.jumpLogs.Count; i++)
        {
            jsonArray.Add(JSONFromJumpLog(logbook.jumpLogs[i]));
        }

        json.Add("JumpLogs", jsonArray);

        json.Add("Previous Jumps", JSONFromPreviousJumps(logbook.previousJumps));




        return json;
    }

    public static JSONObject JSONFromPreviousJumps(Dictionary<JumpType, int> previousJumps)
    {
        JSONObject json = new JSONObject();

        foreach(var previousJump in previousJumps)
        {
            json.Add(previousJump.Key.ToString(), previousJump.Value);
        }

        return json;
    }

    public static JSONObject JSONFromRatings(List<string> ratings)
    {
        JSONObject json = new JSONObject();
        for (int i = 0; i < ratings.Count; i++)
        {
            json.Add(ratings[i].ToString());
        }
        return json;
    }



    public static Logbook LogbookFromJSON(JSONObject json)
    {


        
        List<string> myRatings = RatingsFromJSON(json.GetValueOrDefault("Ratings", json));

        List<JumpLog> jumpLogs = JumpLogsFromJSON(json.GetValueOrDefault("JumpLogs", json));

        Dictionary<JumpType, int> previousJumps = PreviousJumpsFromJSON(json.GetValueOrDefault("Previous Jumps", json));


        Logbook logbook = new Logbook(jumpLogs, myRatings);

        return logbook;
    }

    private static Dictionary<JumpType, int> PreviousJumpsFromJSON(JSONNode jSONNode)
    {
        Dictionary<JumpType, int> previousJumps = new Dictionary<JumpType, int>();

        foreach(var previousJump in jSONNode.AsObject)
        {
            previousJumps.Add(JumpTypeFromString(previousJump.Key), previousJump.Value.AsInt);
        }

        return previousJumps;
    }

    private static JumpType JumpTypeFromString(string key)
    {
        return (JumpType)Enum.Parse(typeof(JumpType), key);
    }

    private static List<JumpLog> JumpLogsFromJSON(JSONNode jSONNode)
    {
        List<JumpLog> jumpLogs = new List<JumpLog>();

        for (int i = 0; i < jSONNode.Count; i++)
        {
            JSONObject json = jSONNode[i].AsObject;

            jumpLogs.Add(JumpLogFromJSON(json));
        }

        return jumpLogs;
    }

    private static JumpLog JumpLogFromJSON(JSONObject json)
    {
        JumpType jumpType = (JumpType)Enum.Parse(typeof(JumpType), json.GetValueOrDefault("jumpType", json));
        int number = json.GetValueOrDefault("number", json);
        int day = json.GetValueOrDefault("day", json);
        int hour = json.GetValueOrDefault("hour", json);
        int minute = json.GetValueOrDefault("minute", json);
        string location = json.GetValueOrDefault("location", json);
        string aircraftType = json.GetValueOrDefault("aircraftType", json);

        int[] time = new int[2];
        time[0] = hour;
        time[1] = minute;

        JumpLog jumpLog = new JumpLog(jumpType,number, day, time, location, aircraftType);

        return jumpLog;
    }

    private static List<string> RatingsFromJSON(JSONNode jSONNode)
    {
        List<string> ratings = new List<string>();

        for (int i = 0; i < jSONNode.Count; i++)
        {
            ratings.Add(jSONNode[i].Value);
        }

        return ratings;
    }



    public static JSONObject JSONFromCharacterData(CharacterData characterData)
    {


        JSONObject json = new JSONObject();
        json.Add("Name", characterData.Name);

        json.Add("Gender",characterData.Gender);
        json.Add("Occupation", characterData.Occupation);

        json.Add("Talent", characterData.Talent);






        JSONObject colors = new JSONObject();
        for (int i = 0; i < characterData.Colors.Length; i++)
        {
            colors.Add("Color " + i.ToString(), HexFromColor(characterData.Colors[i]));
        }

        json.Add("Colors", colors);

        json.Add("Logbook", JSONFromLogbook(characterData.logbook));

        json.Add("Inventory", JSONFromInventory(characterData.Inventory));

        return json;

    }

    public static void SaveAllCharacters(CharacterData[] characters)
    {
        JSONObject json = new JSONObject();

        for (int i = 0; i < characters.Length; i++)
        {
            json.Add(characters[i].Name, JSONFromCharacterData(characters[i]));
        }

        string jsonString = json.ToString();

        File.WriteAllText(Application.persistentDataPath + "/Characters.json", jsonString);
    }

    public static CharacterData[] LoadAllCharacters()
    {
        string jsonString = File.ReadAllText(Application.persistentDataPath + "/Characters.json");

        JSONObject json = JSONObject.Parse(jsonString).AsObject;

        CharacterData[] characters = new CharacterData[json.Count];

        for (int i = 0; i < json.Count; i++)
        {
            characters[i] = CharacterDataFromJSON(json[i].AsObject);
        }

        return characters;
    }

    public static void SaveCharacterData(CharacterData characterData)
    {
        string saveString = JSONFromCharacterData(characterData).ToString();

        File.WriteAllText(Application.persistentDataPath + "characters/"+characterData.Name+".json", saveString);
    }

    public static CharacterData LoadCharacterData(string characterName)
    {
        string saveString = File.ReadAllText(Application.persistentDataPath + "characters/" + characterName + ".json");

        JSONObject json = JSONObject.Parse(saveString).AsObject;

        return CharacterDataFromJSON(json);
    }


    public static List<OwnedItem> InventoryFromJSON(JSONArray json)
    {
        List<OwnedItem> inventory = new List<OwnedItem>();

        for (int i = 0; i < json.Count; i++)
        {
            inventory.Add(OwnedItemFromJSON(json[i].AsObject));
        }

        return inventory;
    }

    private static OwnedItem OwnedItemFromJSON(JSONObject itemJson)
    {
        OwnershipSpecs specs = OwnershipSpecsFromJSON(itemJson.GetValueOrDefault("Specs", itemJson).AsObject);


        string type = itemJson.GetValueOrDefault("Type", itemJson);
        return new OwnedItem(ItemDatabase.Instance.GetItem(type), specs);
    }

    private static OwnershipSpecs OwnershipSpecsFromJSON(JSONObject obj)
    {

        JSONArray colorsArray = obj.GetValueOrDefault("Colors", obj).AsArray;
        string[] colorStrings = new string[colorsArray.Count];



        Color[] colors = new Color[colorsArray.Count];

        for (int i = 0; i < colorsArray.Count; i++)
        {
            colors[i] = ColorFromHex(colorsArray[i].Value);
        }


        return new OwnershipSpecs(obj.GetValueOrDefault("Age",obj), obj.GetValueOrDefault("Size",obj), colors, obj.GetValueOrDefault("Damage Percentage",obj));	
    }

    private static JSONNode JSONFromInventory(List<OwnedItem> inventory)
    {
        JSONArray jsonArray = new JSONArray();

        for (int i = 0; i < inventory.Count; i++)
        {
            jsonArray.Add(JSONFromOwnedItem(inventory[i]));
        }

        return jsonArray;
    }

    private static JSONObject JSONFromOwnedItem(OwnedItem ownedItem)
    {
        JSONObject json = new JSONObject();
        json.Add("Type",ownedItem.TypeOfOwnable.OwnableTypeName);
        json.Add("Specs",JSONFromOwnershipSpecs(ownedItem.Specs));

        
        return json;
    }

    private static JSONNode JSONFromOwnershipSpecs(OwnershipSpecs specs)
    {
        JSONObject json = new JSONObject();
        json.Add("Age", specs.Age);
        json.Add("Size", specs.Size);
        json.Add("Damage Percentage", specs.DamagePercentage);

        JSONArray colors = new JSONArray();
        for (int i = 0; i < specs.CustomColors.Length; i++)
        {
            colors.Add(HexFromColor(specs.CustomColors[i]));
        }

        json.Add("Colors", colors);	

        return json;
    }



    public static string HexFromColor(Color color)
    {
        return ColorUtility.ToHtmlStringRGBA(color);
    }

    public static Color ColorFromHex(string hex)
    {
        Color color;

        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }
        else
        {
            return Color.black;
        }
    }


    public static void WriteJumpSequenceToFile(JumpSequence jumpSequence)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        }
        string path = Application.persistentDataPath + "/saves/" + jumpSequence.JumpName + ".json";
        Debug.Log(path);

        File.WriteAllText(path, JSONFromJumpSeqence(jumpSequence).ToString());
    }


    public static JumpSequence JumpSequenceFromJSON(JSONObject root)
    {
        JumpSequence jumpSequence = new JumpSequence(root[0].Value);
        JSONObject sequence = root[1].AsObject;

        for (int i = 0; i < sequence.Count; i++)
        {
            JSONObject formationObject = sequence[i].AsObject;

            string orientationString = formationObject.GetValueOrDefault("Base Orientation", formationObject);

            FreefallOrientation orientation;

            FreefallOrientation.TryParse(orientationString, out orientation);

            Formation formation = new Formation(orientation);

            JSONObject formationSlots = formationObject.GetValueOrDefault("Formation Slots", formationObject).AsObject;

            for (int j = 0; j < formationSlots.Count; j++)
            {
                JSONObject slotObject = formationSlots[j].AsObject;

                float rotation = slotObject.GetValueOrDefault("Base Rotation", slotObject).AsFloat;
                int skydiverIndex = slotObject.GetValueOrDefault("Skydiver Index", slotObject).AsInt;
                FreefallOrientation childOrientation;
                FreefallOrientation.TryParse(slotObject.GetValueOrDefault("Orientation", slotObject), out childOrientation);
                int targetIndex = slotObject.GetValueOrDefault("Target Index", slotObject).AsInt;
                int slot = slotObject.GetValueOrDefault("Slot", slotObject).AsInt;


                SkydiveFormationSlot skydiveFormationSlot = new SkydiveFormationSlot(skydiverIndex, childOrientation, targetIndex, slot, rotation);
                formation.AddSlot(skydiveFormationSlot);
            }



            jumpSequence.AddFormation(formation);
        }
        return jumpSequence;


    }

    public static JSONObject JSONFromJumpSeqence(JumpSequence jumpSequence)
    {
        JSONObject root = new JSONObject();
        root.Add("Name", jumpSequence.JumpName);

        JSONObject sequence = new JSONObject();

        for (int i = 0; i < jumpSequence.DiveFlow.Count; i++)
        {
            Formation formation = jumpSequence.DiveFlow[i];
            JSONObject formationObject = new JSONObject();

            formationObject.Add("Amount", formation.AmountOfJumpers);
            formationObject.Add("Base Orientation", formation.BaseOrientation.ToString());

            JSONObject formationSlotsObject = new JSONObject();

            for (int j = 0; j < formation.FormationSlots.Count; j++)
            {
                JSONObject formationSlot = new JSONObject();
                SkydiveFormationSlot slot = formation.FormationSlots[j];
                formationSlot.Add("Base Rotation", slot.BaseRotation);
                formationSlot.Add("Orientation", slot.Orientation.ToString());
                formationSlot.Add("Skydiver Index", slot.SkydiverIndex);
                formationSlot.Add("Slot", slot.Slot);
                formationSlot.Add("Target Index", slot.TargetIndex);

                formationSlotsObject.Add("Slot" + j.ToString(), formationSlot);
            }
            formationObject.Add("Formation Slots", formationSlotsObject);
            sequence.Add("Formation" + i.ToString(), formationObject);
        }
        root.Add("Sequence", sequence);

        return root;
    }



}
