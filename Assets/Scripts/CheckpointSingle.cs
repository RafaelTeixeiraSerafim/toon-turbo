using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    private TrackCheckpoints trackCheckpoints;
    private int index;
    public bool isFinalCheckpoint; 
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private BoxCollider collider;
    [SerializeField] private Enums.PlayerList playerToCollide;

    public Enums.PlayerList PlayerToCollide { get => playerToCollide; }
    public int Index { get => index; set => index = value; }

    public void Show()
    {
        meshRenderer.enabled = true;
        collider.enabled = true;
    }

    public void Hide()
    {
        meshRenderer.enabled = false;
        collider.enabled = false;
    }
}
