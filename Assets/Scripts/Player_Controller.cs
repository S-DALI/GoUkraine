using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    private CharacterController PlayerControl;
    private Vector3 direction;
    [SerializeField] private Transform ShellSkins;
    [SerializeField] private GameObject[] AllSkins;
    [SerializeField] private float speed;
    [SerializeField] public float FJump;
    [SerializeField] private float GravityScale;
    private CapsuleCollider ColliderPlayer;
    private int NumLine = 1;
    private bool PlayerSlide = false;
    private bool PlayerDead = false;
    [SerializeField] private float LineDistance;
    [SerializeField] private int —ollected—oins;
    private int AllCoins;
    [SerializeField] private Text CurrentCoins;
    [SerializeField] private Animator PlayerAnimation;
    [SerializeField] private Score ScoreObj;
    private int FJumpBonus = 5;
    private bool X2Coins, X2Score = false;

    private void Awake()
    {
        for(int i =0; i < ShellSkins.childCount; i++)
        {
            if ( i == PlayerPrefs.GetInt("SelectedSkin"))
            {
                AllSkins[i].SetActive(true);
                PlayerAnimation = AllSkins[i].GetComponent<Animator>();
            }
            else AllSkins[i].SetActive(false);
        }
    }
    private void Start()
    {
        PlayerControl= GetComponent<CharacterController>();
        ColliderPlayer = GetComponent<CapsuleCollider>();
        Time.timeScale = 1;
        AllCoins = PlayerPrefs.GetInt("coins");
        StartCoroutine(SpeedUp());
    }
    private void Update()
    {
        if (!PlayerDead)
        {
            if (Swipe_Controller.SwipeRight)
            {
                if (NumLine < 2)
                {
                    NumLine++;
                }
            }

            if (Swipe_Controller.SwipeLeft)
            {
                if (NumLine > 0)
                {
                    NumLine--;
                }
            }

            if (PlayerControl.isGrounded && !PlayerSlide)
                PlayerAnimation.SetBool("Run", true);
            else
                PlayerAnimation.SetBool("Run", false);

            if (Swipe_Controller.SwipeUp)
            {
                if (PlayerControl.isGrounded)
                {
                    JumpPlayer();
                }
            }
            if (Swipe_Controller.SwipeDown /*&& PlayerControl.isGrounded*/ && !PlayerSlide)
            {
                direction.y = -FJump;
                StartCoroutine(Slide());
                PlayerAnimation.SetTrigger("Slide");
            }
            Vector3 Target = transform.position.z * transform.forward + transform.position.y * transform.up;
            if (NumLine == 0)
            {
                Target += Vector3.left * LineDistance;
            }
            else if (NumLine == 2)
            {
                Target += Vector3.right * LineDistance;
            }
            if (transform.position == Target)
                return;
            Vector3 Dif = Target - transform.position;
            Vector3 MoveDirection = Dif.normalized * 25 * Time.deltaTime;
            if (MoveDirection.sqrMagnitude < Dif.sqrMagnitude)
                PlayerControl.Move(MoveDirection);
            else
                PlayerControl.Move(Dif);
        }
    }
    private void FixedUpdate()
    {
        direction.z = speed;
        direction.y += GravityScale * Time.deltaTime;
        PlayerControl.Move(direction * Time.deltaTime);
    }
    private void JumpPlayer()
    {
        direction.y = FJump;
        PlayerAnimation.SetTrigger("Jump");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "let")
        {
            speed = 0;
            FJump= 0;
            PlayerDead = true;
            PlayerPrefs.SetInt("coins", AllCoins + —ollected—oins);
            int LastScore = int.Parse(ScoreObj.ScoreText.text.ToString());
            PlayerPrefs.SetInt("LastScore", LastScore);
            PlayerAnimation.SetBool("Player_dead", PlayerDead);
        }
    }

    private IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(15);
        speed += 2f;
        StartCoroutine(SpeedUp());
    }

    private IEnumerator Slide()
    {
        PlayerSlide = true;
        PlayerControl.center = new Vector3(PlayerControl.center.x, 0.5f, PlayerControl.center.z);
        PlayerControl.height = 1;
        ColliderPlayer.center = new Vector3(PlayerControl.center.x, 0.5f, PlayerControl.center.z);
        ColliderPlayer.height = 1;
        yield return new WaitForSeconds(0.7f);
        PlayerSlide = false;
        PlayerControl.center = new Vector3(PlayerControl.center.x, 0.8f, PlayerControl.center.z);
        ColliderPlayer.center = new Vector3(PlayerControl.center.x, 0.8f, PlayerControl.center.z);
        PlayerControl.height = 1.7f;
        ColliderPlayer.height = 1.7f;
    }
    private IEnumerator ActiveConsumables(GameObject coin)
    {
        yield return new WaitForSeconds(0.5f);
        coin.SetActive(true);
    }

    private IEnumerator ResetFJump() 
    {
        yield return new WaitForSeconds(8f);
        FJump = 10;
    }

    private IEnumerator ResetCoinsBonus()
    {
        yield return new WaitForSeconds(8f);
        X2Coins = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "coins" )
        {
            if (X2Coins)
                —ollected—oins += 2;
            else
                —ollected—oins++;

            CurrentCoins.text = —ollected—oins.ToString();
            other.gameObject.SetActive(false);
            if(!PlayerDead)
                StartCoroutine(ActiveConsumables(other.gameObject));
        }
        if(other.gameObject.tag == "JumpBoots")
        {
            if(FJump<15)
                FJump += FJumpBonus;
            other.gameObject.SetActive(false);
            if (!PlayerDead)
            {
                StopCoroutine(ResetFJump());
                StartCoroutine(ActiveConsumables(other.gameObject));
                StartCoroutine(ResetFJump());
            }
        }
        if(other.gameObject.tag=="CoinsBonus")
        {
            X2Coins = true;
            other.gameObject.SetActive(false);
            if(!PlayerDead)
            {
                StopCoroutine(ResetCoinsBonus());
                StartCoroutine(ActiveConsumables(other.gameObject));
                StartCoroutine(ResetCoinsBonus());
            }
        }
    }
}
