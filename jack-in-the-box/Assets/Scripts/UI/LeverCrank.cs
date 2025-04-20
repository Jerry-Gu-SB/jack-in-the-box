using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeverCrank : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [Header("Crank Settings")]
    public float minCrankAngle = 1440f;
    public float maxCrankAngle = 14400f;

    [Header("Audio")]
    public AudioSource musicSource;
    public float minPitch = 0.8f;
    public float maxPitch = 1.5f;
    public float pitchSpeedMultiplier = 0.01f;

    [Header("References")]
    public PlayerMovement player;

    // === Internal Crank State ===
    private float totalRotation;
    private float crankTarget;
    private float lastAngle;
    private float lastCrankTime;
    private float lastUpdateTime;

    void Start()
    {
        ResetCrank();
        SetLastUpdateTime();
        ErrorCheckObjectReferences();
    }

    void Update()
    {
        if (musicSource.isPlaying && Time.time - lastCrankTime > 0.3f)
        {
            musicSource.Stop();
        }

        if (totalRotation >= crankTarget)
        {
            player.TriggerJump();
            musicSource.Stop();
            ResetCrank();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SetLastAngle(GetMouseAngle());
        StartMusic();
    }

    public void OnDrag(PointerEventData eventData)
    {
        float deltaTime = GetDeltaTime();
        float deltaAngle = GetDeltaAngle();
        float crankSpeed = Mathf.Abs(deltaAngle) / deltaTime;

        ApplyRotation(deltaAngle);
        UpdateLastCrankTime();
        UpdateMusicPitch(crankSpeed);
    }

    private float GetDeltaTime()
    {
        float now = Time.time;
        float delta = now - lastUpdateTime;
        lastUpdateTime = now;
        return Mathf.Max(delta, 0.001f);
    }

    private float GetDeltaAngle()
    {
        float currentAngle = GetMouseAngle();
        float delta = Mathf.DeltaAngle(lastAngle, currentAngle);
        SetLastAngle(currentAngle);
        return delta;
    }

    private void ApplyRotation(float deltaAngle)
    {
        transform.Rotate(Vector3.forward, deltaAngle);
        totalRotation += Mathf.Abs(deltaAngle);
    }

    private void UpdateLastCrankTime()
    {
        lastCrankTime = Time.time;
    }

    private float GetMouseAngle()
    {
        Vector2 leverPos = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
        Vector2 mousePos = Input.mousePosition;
        return Mathf.Atan2(mousePos.y - leverPos.y, mousePos.x - leverPos.x) * Mathf.Rad2Deg;
    }

    private void SetLastAngle(float angle)
    {
        lastAngle = angle;
    }

    private void SetLastUpdateTime()
    {
        lastUpdateTime = Time.time;
    }

    private void ResetCrank()
    {
        totalRotation = 0f;
        crankTarget = Random.Range(minCrankAngle, maxCrankAngle);
    }

    private void StartMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    private void UpdateMusicPitch(float crankSpeed)
    {
        float targetPitch = Mathf.Clamp(crankSpeed * pitchSpeedMultiplier, minPitch, maxPitch);
        musicSource.pitch = Mathf.Lerp(musicSource.pitch, targetPitch, Time.deltaTime * 10f);
    }

    private void ErrorCheckObjectReferences()
    {
        if (musicSource == null)
        {
            musicSource = GetComponent<AudioSource>();
        }

        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>();
        }
    }
}
