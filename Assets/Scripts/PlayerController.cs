using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Editor Fields
    [SerializeField]
    private float score;
    public float scoreReference;
    [SerializeField]
    private float highScore;
    [SerializeField]
    private float timeAlive;
    [SerializeField]
    private int currentCombo;
    [SerializeField]
    private Text comboText;
    [SerializeField]
    private Animator comboAnim;
    [SerializeField]
    private float scoreRate;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text highScoreText;
    [SerializeField]
    private bool dead;

    [SerializeField]
    private string horizontalInput;
    [SerializeField]
    private string leftButton;
    [SerializeField]
    private string rightButton;

    [SerializeField]
    private float currentMoveSpeed;
    [SerializeField]
    private float moveSpeed;
    public float globalSpeedFactor;
    [SerializeField]
    private float speedRate;

    [SerializeField]
    private GameObject Canvas;
    [SerializeField]
    private Animator shipAnim;

    [SerializeField]
    private AudioClip rollClip;
    [SerializeField]
    private AudioClip explodeClip;
    [SerializeField]
    private AudioClip loseClip;
    [SerializeField]
    private AudioClip nearMissClip;
    #endregion

    #region Private Fields
    private Rigidbody rb;
    private AudioSource aS;
    Vector3 newPos;
    int buttonPressed;
    float rollOveride;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        aS = GetComponent<AudioSource>();
        newPos = Canvas.transform.position;
        currentMoveSpeed = moveSpeed;
        // ---- get local high score
        try
        {
            highScore = PlayerPrefs.GetFloat("highScore");
        }
        catch
        {
            highScore = 0;
        }
        // ----
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Vector3.right * (Input.GetAxisRaw(horizontalInput) + rollOveride) * Time.deltaTime * currentMoveSpeed * 100f;
        newPos.x = transform.position.x;
        Canvas.transform.position = newPos;
    }

    private void Update()
    {
        if (!dead)
        {
            //score += Time.deltaTime * scoreRate;
            globalSpeedFactor += Time.deltaTime * speedRate;
        }
        scoreText.text = score.ToString("000000");
        highScoreText.text = highScore.ToString("000000");
        comboText.text = (10 * currentCombo).ToString("00");
        scoreReference = score;

        // ------ LOCAL HIGH SCORE

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("highScore", highScore);
        }

        // ------

        shipAnim.SetFloat("Direction", Input.GetAxisRaw(horizontalInput));

        if(Input.GetButtonDown(leftButton))
        {
            StartCoroutine(ResetPress(0.2f));
            buttonPressed++;
            if(buttonPressed == 2)
            {
                Debug.Log("Double Pressed");
                shipAnim.SetTrigger("rollLeft");
                currentMoveSpeed = moveSpeed * 3;
                rollOveride = -1f;
                aS.PlayOneShot(rollClip, 0.25f);
            }
        }
        if(Input.GetButtonDown(rightButton))
        {
            StartCoroutine(ResetPress(0.2f));
            buttonPressed++;
            if (buttonPressed == 2)
            {
                Debug.Log("Double Pressed");
                shipAnim.SetTrigger("rollRight");
                currentMoveSpeed = moveSpeed * 3;
                rollOveride = 1f;
                aS.PlayOneShot(rollClip, 0.25f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            aS.PlayOneShot(nearMissClip, 0.5f);
            GameObject.FindGameObjectWithTag("CC").GetComponent<Animator>().SetTrigger("Flash");
            currentCombo++;
            score += 10 * currentCombo;
            comboAnim.SetTrigger("popup");
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        dead = true;
        aS.PlayOneShot(explodeClip, 0.25f);
        aS.PlayOneShot(loseClip, 1f);
        shipAnim.SetTrigger("Die");
        GetComponent<SphereCollider>().enabled = false;
        StartCoroutine(ResetGame(1f));
    }

    public IEnumerator ResetPress(float delay)
    {
        yield return new WaitForSeconds(delay);
        buttonPressed = 0;
        currentMoveSpeed = moveSpeed;
        rollOveride = Mathf.Lerp(rollOveride,0,Time.deltaTime * 100f); 
    }

    public IEnumerator ResetGame(float delay)
    {
        Time.timeScale = Mathf.Lerp(Time.timeScale,0,Time.deltaTime * 25f);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Play");
        Time.timeScale = 1f;

    }
}
