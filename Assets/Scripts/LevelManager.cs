using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static   LevelManager Instance;

    [SerializeField] Player player;
    [SerializeField] Transform playerGameplayReadyPosition;
    [SerializeField] CameraSwitcher cameraSwitcher;
    [SerializeField] HubLevelTransition levelTransition;

    public static Action OnSoftReset;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SoftReset()
    {
        if (levelTransition.IsInHub) return;

        OnSoftReset?.Invoke();
        player.transform.position = playerGameplayReadyPosition.position;
        player.Reset();
        cameraSwitcher.SoftReset();
    }
}