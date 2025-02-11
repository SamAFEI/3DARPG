using DG.Tweening;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Cutsenen01 : MonoBehaviour
{
    public TrollController Troll;
    public GameObject BossRoom;
    public GameObject Player;
    public PlayableDirector Director;
    public GameObject GamePass; 
    public GameObject GameFail;
    public Slider SD_Health;
    public CinemachineInputAxisController CinCameraInput;
    public float PassTime;
    public bool IsPlayed;
    public bool IsEnded;
    public float SalceX;
    private Collider _collider => GetComponent<Collider>();

    private void Start()
    {
        BossRoom.SetActive(false);
        GamePass.SetActive(false);
        GameFail.SetActive(false);
        SD_Health.gameObject.SetActive(false);
        Troll.enabled = false;
        PassTime = 3f;
    }

    private void Update()
    {
        if (IsPlayed)
        {
            if (Director.enabled && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Joystick1Button5)))
            {
                Director.time = Director.duration - 0.5f;
            }
            if (Director.enabled && Director.time >= Director.duration)
            {
                Director.enabled = false;
                StartCoroutine(BossTrigger());
            }
        }
        if (SD_Health.gameObject.activeSelf)
        {
            SD_Health.gameObject.transform.localScale = new Vector3(SalceX, 1, 1);
        }

        if (!IsEnded && Troll.enabled && Troll.settings.CurrentHp <= 0)
        {
            PassTime -= Time.deltaTime;
            if (PassTime < 0)
            {
                IsEnded = true;
                GamePass.SetActive(PassTime < 0);
                AudioManager.PlayBGM_Boss(true);
            }
        }
        if (!IsEnded && Player != null && Player.GetComponent<RoleController>().settings.CurrentHp <= 0)
        {
            PassTime -= Time.deltaTime;
            if (PassTime < 0)
            {
                IsEnded = true;
                GameFail.SetActive(PassTime < 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player = other.gameObject;
            Player.GetComponent<RoleController>().settings.ResetState();
            Player.GetComponent<RoleController>().enabled = false;
            Player.GetComponent<CharacterController>().enabled = false;
            Player.GetComponent<PlayerInputs>().enabled = false;
            Player.GetComponent<Animator>().enabled = true;
            Player.transform.position = transform.position;
            IsPlayed = true;
            CinCameraInput.enabled = false;
            Director.Play();
            _collider.enabled = false;
        }
    }
    public void ApplyBossHpBar()
    {
        SD_Health.gameObject.SetActive(true);
        SD_Health.gameObject.transform.localScale = new Vector3(SalceX, 1, 1);
        SalceX = 0f;
        DOTween.To(() => SalceX, x => SalceX = x, 1, 3f);
    }

    private IEnumerator BossTrigger()
    {
        GameManager.ApplySlowTime(1f);
        AudioManager.PlayBGM_Boss(false);
        ApplyBossHpBar();
        BossRoom.SetActive(true);
        Player.GetComponent<Animator>().enabled = false;
        Player.GetComponent<CharacterController>().enabled = true;
        Player.GetComponent<RoleController>().enabled = true;
        Player.GetComponent<RoleController>().settings.ResetState();
        CinCameraInput.enabled = true;
        Player.GetComponent<PlayerInputs>().enabled = true;
        yield return new WaitForSecondsRealtime(1f);
        Troll.enabled = true;
    }
}
