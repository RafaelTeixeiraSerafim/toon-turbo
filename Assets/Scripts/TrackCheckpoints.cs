using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrackCheckpoints : MonoBehaviour
{
    List<CheckpointSingle> checkPointSingleList;
    public event EventHandler OnPlayerCorrectCheckpoint;
    public event EventHandler OnPlayerWrongCheckpoint;
    public event EventHandler OnPlayerFinishLap;
    public event EventHandler OnRefreshCheckpointMesh;
    private int nextCheckpointSingleIndex;
    private bool firstLap = true;

    private void Awake()
    {
        Transform checkpointsTransform = transform.Find("Checkpoints");

        checkPointSingleList = new List<CheckpointSingle>();

        nextCheckpointSingleIndex = 0;

        foreach (Transform checkpointSingleTransform in checkpointsTransform)
        {
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();

            checkpointSingle.SetTrackCheckpoints(this);

            checkPointSingleList.Add(checkpointSingle);
        }
    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle)
    {
        Debug.Log(checkpointSingle.name);

        if (checkPointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex) {
            checkpointSingle.GetComponentInChildren<MeshRenderer>().enabled = false;

            if (nextCheckpointSingleIndex == checkPointSingleList.Count - 1)
            {
                OnRefreshCheckpointMesh?.Invoke(this, EventArgs.Empty);
            }
            else if (nextCheckpointSingleIndex == 0 && !firstLap)
            {
                OnPlayerFinishLap?.Invoke(this, EventArgs.Empty);
            }

            nextCheckpointSingleIndex = (nextCheckpointSingleIndex + 1) % checkPointSingleList.Count;
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
            firstLap = false;
        }
        else
        {
            OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty);
        }
    }
}
