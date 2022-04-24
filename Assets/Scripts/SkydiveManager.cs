using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SkydiveManager : MonoBehaviour
{
    public event Action<ISelectable> OnSkydiverAdded;
    public event Action<int> OnNextFormationSet;

    public event Action<JumpSequence> OnPlaybackStarted;

    public event Action<AircraftInstance> OnAircraftSet;
    public event Action OnJumpRunSet;
    public event Action OnExitStarted;

    public void TogglePauseGame()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public List<ISelectable> SpawnedSkydivers = new List<ISelectable>();

    public List<Transform> SpawnedGhosts = new List<Transform>();

    public CharacterData[] GetCharacters()
    {
        if(SpawnedSkydivers == null)
        {
            return null;
        }
        CharacterData[] characters = new CharacterData[SpawnedSkydivers.Count];

        for(int i = 0; i < SpawnedSkydivers.Count; i++)
        {
            if(SpawnedSkydivers[i] != null)
            {
                characters[i] = SpawnedSkydivers[i].transform.GetComponent<CharacterDataHandler>().CharacterData;
            }
        }
        return characters;
    }

    public ISelectable GetSelectableFromCharacterData(CharacterData characterData)
    {
        foreach(ISelectable selectable in SpawnedSkydivers)
        {
            if(selectable.transform.GetComponent<CharacterDataHandler>().CharacterData == characterData)
            {
                return selectable;
            }
        }
        return null;
    }



    SkydiveSpawner spawner;

    [SerializeField]
    public Transform middlepointNPCS;

    public JumpSequence CurrentJumpSequence;

    int exitAltitude;


    [SerializeField]
    GameObject startButton;

    [SerializeField]
    public AircraftInstance aircraft;

    [SerializeField]
    AircraftSelector aircraftSelector;

    GameObject offsetPlacementObject;

    bool isPlayingBack;

    private void Awake()
    {
        spawner = FindObjectOfType<SkydiveSpawner>();
        offsetPlacementObject = new GameObject();
    }

    public void SetAircraft(AircraftInstance aircraft)
    {
        this.aircraft = aircraft;
        OnAircraftSet?.Invoke(aircraft);
    }



    public void SetupJumpRun(JumpSequence selectedSequence, int altitude, CharacterData[] characters = null)
    {
        CurrentJumpSequence = selectedSequence;

        if(characters == null)
        {
            characters = new CharacterData[CurrentJumpSequence.TotalSkydivers()];
            for(int i = 0; i < characters.Length; i++)
            {
                characters[i] = RandomCharacterGenerator.CreateRandomCharacter();
            }
        }


        aircraft.transform.position = Vector3.up * altitude;


        int skydiversNeeded = CurrentJumpSequence.TotalSkydivers();
        if (SpawnedSkydivers.Count < skydiversNeeded)
        {
            int missing = skydiversNeeded - SpawnedSkydivers.Count;
            for (int i = 0; i < missing; i++)
            {
                AddSkydiver();
            }
        }
        if (SpawnedSkydivers.Count > skydiversNeeded)
        {
            int missing = skydiversNeeded - SpawnedSkydivers.Count;
            for (int i = skydiversNeeded; i < SpawnedSkydivers.Count; i++)
            {
                SpawnedSkydivers[i].transform.gameObject.SetActive(false);
                SpawnedGhosts[i].gameObject.SetActive(false);
            }
        }

        SetCharacterDatas(characters);

        SetSkydiversOnExitPositions();


        startButton.SetActive(true);
        OnJumpRunSet?.Invoke();
    }

    private void SetCharacterDatas(CharacterData[] characters)
    {
        for(int i = 0; i < characters.Length; i++)
        {
            SpawnedSkydivers[i].transform.GetComponent<CharacterDataHandler>().SetCharacterData(characters[i]);
        }
    }

    void SetSkydiversOnExitPositions()
    {
        if(aircraft == null)
        {
            Debug.Log("aircraft is null");
            return;
        }
        for (int i = 0; i < SpawnedSkydivers.Count; i++)
        {

            int exitPosition = i;

            
            SpawnedSkydivers[i].transform.gameObject.SetActive(true);
            SpawnedGhosts[i].gameObject.SetActive(true);

            SpawnedSkydivers[i].transform.GetComponent<Rigidbody>().isKinematic = true;
            SpawnedSkydivers[i].transform.position = aircraft.AircraftTransforms.ExitPositions[exitPosition].position;
            SpawnedSkydivers[i].transform.SetParent(aircraft.transform);
            SpawnedSkydivers[i].transform.localEulerAngles = Vector3.zero;
            SpawnedSkydivers[i].transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }

    public void StartButtonPressed()
    {
        startButton.SetActive(false);
        
        OnExitStarted?.Invoke();
        Invoke("DelayedPlayback", 2);
             
    }

    public int currentSequenceIndex;

    public void StartDefaultJump(int amount)
    {
        StartPlayback(JumpCreator.DefaultJump(amount),4600);
        
    }

    void DelayedPlayback()
    {
        StartPlayback(CurrentJumpSequence, exitAltitude);
    }

    public void StartPlayback(JumpSequence sequence, int altitude)
    {
        CurrentJumpSequence = sequence;

        for (int i = 0; i < SpawnedSkydivers.Count; i++)
        {
            SpawnedSkydivers[i].transform.GetComponent<Rigidbody>().isKinematic = false;
            SpawnedSkydivers[i].transform.SetParent(null);
        }

        currentSequenceIndex = 0;
        isPlayingBack = true;
        OnPlaybackStarted?.Invoke(sequence);
        HandoutNextSlots();
        startButton.SetActive(false);
    }

    public void SetNextFormation()
    {

            currentSequenceIndex++;
        
        if (currentSequenceIndex >= CurrentJumpSequence.DiveFlow.Count)
        {
            currentSequenceIndex = 0;
        }
        HandoutNextSlots();
        OnNextFormationSet?.Invoke(currentSequenceIndex);
    }

    public void StopPlayback()
    {
        isPlayingBack = false;
    }

    public Vector3 FormationOffset(SkydiveFormationSlot formationSlot)
    {
        if(formationSlot == null)
        {
            return Vector3.zero;
        }

        List<int> formationFlow = new List<int>();
        offsetPlacementObject.transform.position = Vector3.zero;
        offsetPlacementObject.transform.rotation = Quaternion.identity;
        if (formationSlot.TargetIndex != 0)
        {

            SkydiveFormationSlot slot = formationSlot;

            for (int i = slot.TargetIndex; i > 0; i = slot.TargetIndex)
            {
                formationFlow.Add(slot.TargetIndex);

                slot = CurrentJumpSequence.DiveFlow[currentSequenceIndex].FormationSlots[i - 1];
            }
            formationFlow.Add(slot.TargetIndex);

            formationFlow.Reverse();

            for (int j = 0; j < formationFlow.Count; j++)
            {
                Vector3 Offset = SlotPositionHelper.SlotOffset(slot.Slot);
                //Offset = Quaternion.AngleAxis(slot.BaseRotation, go.transform.up) * Offset;
                offsetPlacementObject.transform.position += Offset;
                offsetPlacementObject.transform.Rotate(new Vector3(0, slot.BaseRotation, 0));

                if (formationFlow[j] != 0)
                {

                    slot = CurrentJumpSequence.DiveFlow[currentSequenceIndex].FormationSlots[formationFlow[j] - 1];
                }

                
            }
            return SpawnedSkydivers[0].transform.TransformPoint(offsetPlacementObject.transform.position);
        }
        else
        {
            return SlotPositionHelper.SlotOffset(formationSlot.Slot);
        }
        
    }

    void HandoutNextSlots()
    {
        skydiversInSlots = new bool[CurrentJumpSequence.TotalSkydivers()];
        SpawnedSkydivers[0].transform.GetComponent<NPC_Ai_FromState>().SetState(new SkydiveState(CurrentJumpSequence.DiveFlow[currentSequenceIndex].BaseOrientation));
        for (int i = 0; i < CurrentJumpSequence.DiveFlow[currentSequenceIndex].FormationSlots.Count; i++)
        {
            SkydiveFormationSlot formationSlot = CurrentJumpSequence.DiveFlow[currentSequenceIndex].FormationSlots[i];
            SpawnedSkydivers[i+1].transform.GetComponent<NPC_Ai_FromState>().SetState(new SkydiveState(formationSlot));
        }

    }

    public void SelectAllButtonPress()
    {
        if(!SelectionHandler.Instance.selectedList.SequenceEqual(SpawnedSkydivers))
        {
            SelectionHandler.Instance.SetSelectionList(new List<ISelectable>(SpawnedSkydivers));
        }
        
    }

    public void AddSkydiver()
    {
        int newIndex = SpawnedSkydivers.Count;
        ISelectable skydiver = spawner.SpawnSkydiverWithAi(newIndex).GetComponent<ISelectable>();
        skydiver.transform.gameObject.name = "Skydiver " + SpawnedSkydivers.Count.ToString();
        Transform ghost = spawner.SpawnGhost(newIndex);
        ghost.GetComponent<GhostController>().ConnectSkydiver(skydiver.transform);
        ghost.GetComponent<GhostController>().OnSkydiverArrived += SkydiverArrived;
        ghost.GetComponent<GhostController>().OnSkydiverGone += SkydiverGone;
        ghost.gameObject.name = "Ghost " + SpawnedGhosts.Count.ToString();



        SpawnedGhosts.Add(ghost);

        SpawnedSkydivers.Add(skydiver);

        OnSkydiverAdded?.Invoke(skydiver);
    }

    private void SkydiverGone(int skydiver)
    {
        skydiversInSlots[skydiver] = false;
    }

    bool[] skydiversInSlots;

    private void SkydiverArrived(int skydiver)
    {
        skydiversInSlots[skydiver] = true;
        if (AllTrueInArray(skydiversInSlots))
        {
            SetNextFormation();
        }
    }

    bool AllTrueInArray(bool[] bools)
    {
        for (int i = 0; i < bools.Length; i++)
        {
            if (!bools[i])
            {
                return false;
            }

        }
        return true;
    }

    public void RemoveSkydiverButtonPress()
    {
        if(SpawnedSkydivers.Count > 1)
        {
            ISelectable skydiverToDestroy = SpawnedSkydivers[SpawnedSkydivers.Count - 1];
            SpawnedSkydivers.RemoveAt(SpawnedSkydivers.Count - 1);
            Destroy(skydiverToDestroy.transform.gameObject);
        }        
    }

    public Vector3 GetAveragePosition()
    {
        if (SpawnedSkydivers.Count > 0)
        {
            int playerControlled = 0;
            Vector3 running = Vector3.zero;
            for (int i = 0; i < SpawnedSkydivers.Count; i++)
            {
                if (SpawnedSkydivers[i].transform.GetComponent<MovementController>().inputSource.GetType() != typeof(AccelAndTouchUiControl))
                {
                    running += SpawnedSkydivers[i].transform.position;
                }
                else
                {
                    playerControlled = 1;
                }
                
            }
            return running / (SpawnedSkydivers.Count- playerControlled);
        }
        else
        {
            return Vector3.zero;
        }
    }

    void SetAllToOrientation(FreefallOrientation freefallOrientation)
    {
        for (int i = 0; i < SpawnedSkydivers.Count; i++)
        {
            //SpawnedSkydivers[i].transform.GetComponent<MovementController>().tr
        }
    }

    private void FixedUpdate()
    {
        if (SpawnedSkydivers.Count > 0)
        {
            middlepointNPCS.position = GetAveragePosition();
        }
    }
}
