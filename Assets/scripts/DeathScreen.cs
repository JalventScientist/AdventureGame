using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DeathScreen : MonoBehaviour
{
    public GameObject UI;
    public GameObject SkullPos;
    public GameObject SkullRot;
    public RawImage SkullImage;

    public Texture2D Skull1;
    public Texture2D Skull2;

    public AudioSource SkullAudioSource;

    public bool IsDead = false;

    private bool HasPressed = false;
    private bool ImFuckingDead;
    public KeyCode RestartButton = KeyCode.Return;

    [Header("References")]
    public PauseGame GamePauser; // Required to make Pausing unable during death
    public musicHandler Music;
    public Health PlayerHealth;
    public PlayerCam PlayerCam;
    public Rigidbody rb;
    public PlayerMovement PlrScript;

    bool hasGoneOnce;
    [Header("Debug")]
    public KeyCode FuckingKillYourselfWith = KeyCode.Backspace;
    public bool CanDieByKeybind;

    private IEnumerator Coroutine;
    IEnumerator AnimateSkull()
    {
        while (IsDead)
        {
            SkullImage.texture = Skull1;
            SkullAudioSource.Play();
            for(int i = 0; i < 50; i++)
            {
                Vector3 NewLocation = new Vector3(Screen.width * Random.Range(0.498f,0.502f), Screen.height * Random.Range(0.495f, 0.505f), 0);
                Quaternion NewRotation = Quaternion.Euler(0, 0, Random.Range(-1, 1));
                SkullPos.GetComponent<RectTransform>().position = NewLocation;
                SkullRot.GetComponent<RectTransform>().rotation = NewRotation;
                yield return new WaitForSecondsRealtime(0.01f);
            }
            SkullImage.texture = Skull2;
            for (int i = 0; i < 50; i++)
            {
                Vector3 NewLocation = new Vector3(Screen.width * Random.Range(0.498f, 0.502f), Screen.height * Random.Range(0.495f, 0.505f), 0);
                Quaternion NewRotation = Quaternion.Euler(0, 0, Random.Range(-1, 1));
                SkullPos.GetComponent<RectTransform>().position = NewLocation;
                SkullRot.GetComponent<RectTransform>().rotation = NewRotation;
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }
    }

    private void Start()
    {
        Coroutine = AnimateSkull();
    }

    private void Update()
    {

        if (IsDead)
        {
            if (Input.GetKeyDown(RestartButton))
            {
                if (!HasPressed)
                {
                    Time.timeScale = 1;
                    PlayerHealth.HealPlayer(100);
                    PlayerHealth.DeathConfirmed = false;
                    IsDead = false;
                    HasPressed = true;
                    StopCoroutine(Coroutine);
                    GamePauser.CanPressMenu = true;
                    PlayerCam.enabled = true;
                    rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeAll;
                    PlrScript.enabled = true;
                    GamePauser.RestartLevel(true);
                }
            }
            else if (Input.GetKeyUp(RestartButton))
            {
                HasPressed = false;
            }
        }
        if (Input.GetKeyDown(FuckingKillYourselfWith))
        {
            if (!ImFuckingDead && CanDieByKeybind)
            {
                ImFuckingDead = true;
                print("Kurwa");
                PlayerHealth.DamagePlayer(100);
            }
            
        } else if (Input.GetKeyUp(FuckingKillYourselfWith))
        {
            if (ImFuckingDead) {
            ImFuckingDead = false;
            }
        }
            
    }

    public void Die()
    {
        IsDead = true;
        PlayerCam.enabled = false;
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.None;
        PlrScript.enabled = false;
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0, 3).SetUpdate(true);
        GamePauser.CanPressMenu = false;
        Music.SetGlobalVolume(0f);
        UI.SetActive(true);
        StartCoroutine(Coroutine);
    }
}
