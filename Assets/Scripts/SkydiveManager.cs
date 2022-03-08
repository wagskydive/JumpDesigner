using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SkydiveManager : MonoBehaviour
{
    public event Action<ISelectable> OnSkydiverAdded;
    public event Action<int> OnNextFormationSet;
    public List<ISelectable> SpawnedSkydivers = new List<ISelectable>();

    public List<Transform> SpawnedGhosts = new List<Transform>();

    public event Action<JumpSequence> OnPlaybackStarted;
    public event Action OnJumpRunSet;

    SkydiveSpawner spawner;

    [SerializeField]
    public Transform middlepointNPCS;

    public JumpSequence CurrentJumpSequence;

    int exitAltitude;
    [SerializeField]
    GameObject startButton;

    [SerializeField]
    AircraftTransforms aircraft;

    public void SetupJumpRun(JumpSequence selectedSequence, int altitude)
    {
        CurrentJumpSequence = selectedSequence;
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

        for (int j = 0; j < skydiversNeeded; j++)
        {
            SpawnedSkydivers[j].transform.gameObject.SetActive(true);
            SpawnedGhosts[j].gameObject.SetActive(true);
        }

        aircraft.transform.position = Vector3.up * altitude;

        for (int i = 0; i < SpawnedSkydivers.Count; i++)
        {
            SpawnedSkydivers[i].transform.GetComponent<Rigidbody>().isKinematic = true;
            SpawnedSkydivers[i].transform.position = aircraft.ExitPositions[i].position;
            SpawnedSkydivers[i].transform.SetParent(aircraft.transform);
            SpawnedSkydivers[i].transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        startButton.SetActive(true);
        OnJumpRunSet?.Invoke();
    }



    public void StartButtonPressed()
    {
        startButton.SetActive(false);
        StartPlayback(CurrentJumpSequence, exitAltitude);
        
    }

    public int currentSequenceIndex;

    bool isPlayingBack;

    private void Awake()
    {
        spawner = FindObjectOfType<SkydiveSpawner>();
        go = new GameObject();
    }



    public void StartDefaultJump(int amount)
    {
        StartPlayback(JumpCreator.DefaultJump(amount),4600);
        
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
    GameObject go;

    public Vector3 FormationOffset(SkydiveFormationSlot formationSlot)
    {
        if(formationSlot == null)
        {
            return Vector3.zero;
        }

        List<int> formationFlow = new List<int>();
        go.transform.position = Vector3.zero;
        go.transform.rotation = Quaternion.identity;
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
                go.transform.position += Offset;
                go.transform.Rotate(new Vector3(0, slot.BaseRotation, 0));

                if (formationFlow[j] != 0)
                {

                    slot = CurrentJumpSequence.DiveFlow[currentSequenceIndex].FormationSlots[formationFlow[j] - 1];
                }

                
            }
            return SpawnedSkydivers[0].transform.TransformPoint(go.transform.position);
        }
        else
        {
            return SlotPositionHelper.SlotOffset(formationSlot.Slot);
        }
        
    }

    void HandoutNextSlots()
    {
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


        SpawnedGhosts.Add(spawner.SpawnGhost(newIndex));

        SpawnedSkydivers.Add(skydiver);
        OnSkydiverAdded?.Invoke(skydiver);
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
