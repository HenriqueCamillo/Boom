using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static   LevelManager Instance;

    [SerializeField] Player player;
    [SerializeField] Transform playerGameplayReadyPosition;
    private Vector3 playerInitialPosition;

    public static Action OnHardReset;
    public static Action OnSoftReset;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public void HardResetLevel()
    {
        OnHardReset?.Invoke();
        player.transform.position = playerInitialPosition;
    }

    public void SoftReset()
    {
        OnSoftReset?.Invoke();
        player.transform.position = playerGameplayReadyPosition.position;
    }


    private void Start()
    {
        playerInitialPosition = player.transform.position;
    }
}