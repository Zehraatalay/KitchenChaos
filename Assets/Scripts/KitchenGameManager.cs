using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KitchenGameManager : MonoBehaviour
{

    public static KitchenGameManager Instance { get; private set; }


    public event EventHandler OnStateChanged;
  

    private enum State
    {
        WaitingToStart,//multiplayer oyunlarda tüm oyuncularýn baglanmasý lazým
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    //pause durumu oyun icinde yer aldýgýndan game manager'da kontrol etmek daha mantýklý
    private void Start()
    {
        GameInput.Instance.OnPauseAction += Instance_OnPauseAction;
    }

    private void Instance_OnPauseAction(object sender, EventArgs e )
    {
        PauseGame();
    }

    private State state;
    [SerializeField] private float waitingToStartTimer = 1f;
    [SerializeField] private float countdownToStartTimer = 3f;
    [SerializeField] private float gamePlayingTimer;
    [SerializeField] private float gamePlayingTimerMax = 60f;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
        Debug.Log(state);

    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetGamePlayingTimerNormalized()
    {
        //time geriye doðru aktýðý için reverse alýrýz
        //1 - yaparýz yani
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    private void PauseGame()
    {
        //multiplier so tum delta time'lar sifirlanir
        Time.timeScale = 0f;
    }

}
