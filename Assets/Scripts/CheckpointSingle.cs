using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    private TrackCheckpoints trackCheckpoints;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ColliderGetCoin>(out ColliderGetCoin colliderGetCoin))
        {
            trackCheckpoints.PlayerThroughCheckpoint(this);
        }
    }

    public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints)
    {
        this.trackCheckpoints = trackCheckpoints;
        trackCheckpoints.OnRefreshCheckpointMesh += TrackCheckpoints_OnRefreshCheckpointMesh;
    }

    private void TrackCheckpoints_OnRefreshCheckpointMesh(object sender, EventArgs e)
    {
        meshRenderer.enabled = true;
    }

    private void OnDisable()
    {
        trackCheckpoints.OnRefreshCheckpointMesh -= TrackCheckpoints_OnRefreshCheckpointMesh;
    }
}
