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
        


        JSONObject colorsObject = saveRead.GetValueOrDefault("Colors", saveRead).AsObject;

        Color[] colors = new Color[colorsObject.Count];

        for (int i = 0; i < colorsObject.Count; i++)
        {
            colors[i] = ColorFromHex(colorsObject[i].Value);
        }

        CharacterData characterData = new CharacterData(characterName);
        characterData.SetColors(colors);




        return characterData;
    }

    public static JSONObject JSONFromCharacterData(CharacterData characterData)
    {
        JSONObject root = new JSONObject();
        root.Add("Name", characterData.Name);

        JSONObject colors = new JSONObject();
        for (int i = 0; i < characterData.Colors.Length; i++)
        {
            colors.Add("Color " + i.ToString(), HexFromColor(characterData.Colors[i]));
        }

        root.Add("Colors", colors);

        return root;

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
