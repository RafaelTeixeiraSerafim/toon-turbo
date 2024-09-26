using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    [SerializeField]private List<CheckpointSingle> checkPointSingleListPlayerOne, checkPointSingleListPlayerTwo;
    [SerializeField]private CheckpointSingle FinalCheckpointPlayerOne, FinalCheckpointPlayerTwo;
    //private int nextCheckpointSingleIndex;
    //private bool firstLap = true;

    private void Awake()
    {
        PopulateRingIndexes();
        FinalCheckpointPlayerOne.Hide();
        FinalCheckpointPlayerTwo.Hide();
    }

    private void PopulateRingIndexes()
    {
        for (int i = 0; i < checkPointSingleListPlayerOne.Count; i++) 
        {
            checkPointSingleListPlayerOne[i].Index = i;
        }

        for (int i = 0; i < checkPointSingleListPlayerTwo.Count; i++)
        {
            checkPointSingleListPlayerTwo[i].Index = i;
        }
    }

    public void ReactivateCheckpoints(Enums.PlayerList player)
    {
        switch (player)
        {
            case Enums.PlayerList.PlayerOne:
                foreach (CheckpointSingle item in checkPointSingleListPlayerOne)
                {
                    item.Show();
                }
                break;
            case Enums.PlayerList.PlayerTwo:
                foreach (CheckpointSingle item in checkPointSingleListPlayerTwo)
                {
                    item.Show();
                }
                break;
            default:
                break;
        }
    }

    public void ActivateFinalCheckpoint(Enums.PlayerList player)
    {
        switch (player)
        {
            case Enums.PlayerList.PlayerOne:
                FinalCheckpointPlayerOne.Show();
                break;

            case Enums.PlayerList.PlayerTwo:
                FinalCheckpointPlayerTwo.Show();
                break;

            default:
                break;
        }
    }
/*
    private void Awake()
    {
        checkPointSingleListPlayerOne = new List<CheckpointSingle>();
        checkPointSingleListPlayerTwo = new List<CheckpointSingle>();

        nextCheckpointSingleIndex = 0;
    }
    
    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle)
    {
        Debug.Log(checkpointSingle.name);

        if (GetCheckpointList(actualPlayer).IndexOf(checkpointSingle) == nextCheckpointSingleIndex) {
            checkpointSingle.GetComponentInChildren<MeshRenderer>().enabled = false;

            if (nextCheckpointSingleIndex == GetCheckpointList(actualPlayer).Count - 1)
            {
          //      OnRefreshCheckpointMesh?.Invoke(this, EventArgs.Empty);
            }
            else if (nextCheckpointSingleIndex == 0 && !firstLap)
            {
             //   OnPlayerFinishLap?.Invoke(this, EventArgs.Empty);
            }

            nextCheckpointSingleIndex = (nextCheckpointSingleIndex + 1) % GetCheckpointList(actualPlayer).Count;
          //  OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
            firstLap = false;
        }
        else
        {
           // OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty);
        }
    }*/
}
