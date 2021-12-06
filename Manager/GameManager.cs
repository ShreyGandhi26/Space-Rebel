using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public string ToLoadScene;

    private SoldierCharacter _soldier;
    public Animator animator;
    public TextMeshProUGUI text;

    private bool SoldierCamera = true;
    public static bool canChnageLevel = false;
    public static float noOfCageInLevel;
    public static float totalHostageFree;
    public static float timeLeft;
    public static float EnemiesKilled;
    public string LastSceneName;
    private HostageDoor[] cageInLevel;
    //private TechncianCharacter _technician;
    [SerializeField]
    private bool _soldierIsActive;


    // Start is called before the first frame update
    void Start()
    {
        canChnageLevel = false;
        _soldier = FindObjectOfType<SoldierCharacter>();
        cageInLevel = GameObject.FindObjectsOfType<HostageDoor>();
        noOfCageInLevel = cageInLevel.Length;
        Cursor.lockState = CursorLockMode.Confined;
        //_technician = FindObjectOfType<TechncianCharacter>();
        //_technician.controller.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        LastSceneName = SceneManager.GetActiveScene().name;
        if (LastSceneName == "TutorialWinScene" || LastSceneName == "WinSceneLevel11" || LastSceneName == "WinSceneLevel2")
        {
            FindObjectOfType<AudioManager>().StopSound("Alarm");
        }
        if (LastSceneName == "GameOver" || LastSceneName == "StartMenu")
        {
            Cursor.visible = true;
        }
        else
        {
            if (_soldier == null && canChnageLevel == false)
            {
                canChnageLevel = true;
            }

            if (noOfCageInLevel < 1)
            {
                SwitchSystem.canTurnSwitch = true;
            }
            text.text = noOfCageInLevel + "/" + cageInLevel.Length;
        }

        // if (Input.GetKeyDown(KeyCode.Tab))
        // {
        //     if (_soldierIsActive)
        //     {
        //         _soldier.GetComponent<SoldierCharacter>().enabled = false;
        //         _soldier.GetComponentInChildren<Weapon>().enabled = false;
        //         //_technician.GetComponent<TechncianCharacter>().enabled = true;
        //         //_technician.GetComponent<Knife>().enabled = true;
        //         _soldier.controller.rb.velocity = Vector2.zero;
        //         _soldier.controller.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        //         _soldierIsActive = false;
        //         _soldier.Marker.SetActive(false);
        //        // _technician.Marker.SetActive(true);
        //        // _technician.controller.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //         animator.Play("Technician");
        //     }
        //     else
        //     {
        //         _soldier.GetComponent<SoldierCharacter>().enabled = true;
        //         _soldier.GetComponentInChildren<Weapon>().enabled = true;
        //         _technician.GetComponent<TechncianCharacter>().enabled = false;
        //         _technician.GetComponent<Knife>().enabled = false;
        //         _technician.controller.rb.velocity = Vector2.zero;
        //         _technician.controller.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        //         _soldierIsActive = true;
        //         _soldier.controller.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //         _soldier.Marker.SetActive(true);
        //         _technician.Marker.SetActive(false);
        //         animator.Play("Soldier");
        //     }
        //}
        // if (_soldier == null)
        // {
        //     //_soldier.GetComponent<SoldierCharacter>().enabled = false;
        //     //_soldier.GetComponentInChildren<Weapon>().enabled = false;
        //     _technician.GetComponent<TechncianCharacter>().enabled = true;
        //     _technician.GetComponent<Knife>().enabled = true;
        //     // _soldier.controller.rb.velocity = Vector2.zero;
        //     //_soldier.controller.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        //     //_soldierIsActive = false;
        //     //_soldier.Marker.SetActive(false);
        //     _technician.Marker.SetActive(true);
        //     _technician.controller.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //     animator.Play("Technician");
        // }
        // if (_technician == null)
        // {
        //     _soldier.GetComponent<SoldierCharacter>().enabled = true;
        //     _soldier.GetComponentInChildren<Weapon>().enabled = true;
        //     //_technician.GetComponent<TechncianCharacter>().enabled = false;
        //     // _technician.GetComponent<Knife>().enabled = false;
        //     //_technician.controller.rb.velocity = Vector2.zero;
        //     //  _technician.controller.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        //     _soldierIsActive = true;
        //     _soldier.controller.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //     _soldier.Marker.SetActive(true);
        //     //  _technician.Marker.SetActive(false);
        //     animator.Play("Soldier");
        // }
    }

    // void SaveScene()
    // {
    //     PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
    // }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
        totalHostageFree = 0;
        EnemiesKilled = 0;
    }
}
