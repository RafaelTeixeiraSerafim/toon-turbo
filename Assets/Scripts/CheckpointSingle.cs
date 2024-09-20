using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    private TrackCheckpoints trackCheckpoints;
    [SerializeField] private MeshRenderer meshRenderer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ColliderGetCoin colliderGetCoin))
        {
            //trackCheckpoints.PlayerThroughCheckpoint(this);
            if (this.CompareTag(colliderGetCoin.tag + " Collision")) 
            {
                Debug.Log(this.tag + "//////" + colliderGetCoin.tag);
                meshRenderer.enabled = false;
            }
        }
    }

    public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints)
    {
        this.trackCheckpoints = trackCheckpoints;
        //trackCheckpoints.OnRefreshCheckpointMesh += TrackCheckpoints_OnRefreshCheckpointMesh;
    }

    private void TrackCheckpoints_OnRefreshCheckpointMesh(object sender, EventArgs e)
    {
        meshRenderer.enabled = true;
    }

    private void OnDisable()
    {
        //trackCheckpoints.OnRefreshCheckpointMesh -= TrackCheckpoints_OnRefreshCheckpointMesh;
    }
}
