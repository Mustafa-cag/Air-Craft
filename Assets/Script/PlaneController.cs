using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PlaneController : MonoBehaviour
{
    public float planeSpeed = 5;
    public float YawAmount = 120;
    public float Yaw;
    public bool walkToFloor = false;
    public float gear;
    public Slider slider;
    public FloatingJoystick joystick;

    public int Score;
    public TextMeshProUGUI scoreText;

    public Rigidbody rb;
    public GameObject explosion;
    public GameObject confetti;

    public GameObject firstPropeller;

    public AudioSource planeSound;
    public GameObject levelEndCamera;
    public LevelEndCamera levelEndSc;
    public GameObject mainCamera;

    public Animator scoreAnim;
    public GameObject wrongPanel;
    public Image BlackFade;
    public float allPoint;

    #region Singleton
    public static PlaneController instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    void Update()
    {
        if(walkToFloor == true)
        {
            joystick.AxisOptions = AxisOptions.Both;
            rb.constraints = RigidbodyConstraints.None;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        gear = Mathf.Ceil(slider.value);

        #region PropertyPattern

        //Plane Speed
        float PlaneSpeed(PlaneController benimLevel, float deneme)
            => benimLevel switch
            {
                { gear: 0 } => planeSpeed = 0f,
                { gear: 1 } => planeSpeed = 2f,
                { gear: 2 } => planeSpeed = 4f,
                { gear: 3 } => planeSpeed = 6f,
                { gear: 4 } => planeSpeed = 8f,
                { gear: 5 } => planeSpeed = 10f,
                { gear: 6 } => planeSpeed = 12f,
                { gear: 7 } => planeSpeed = 14f,
                { gear: 8 } => planeSpeed = 16f,
                { gear: 9 } => planeSpeed = 18f,
                { gear: 10 } => planeSpeed = 20f,
                _ => gear
            };
        PlaneSpeed(this, gear);

        //Plane Sound
        float PlaneSound(PlaneController benimLevel, float deneme)
            => benimLevel switch
            {
                { gear: 0 } => planeSound.pitch = 0.1f,
                { gear: 1 } => planeSound.pitch = 0.2f,
                { gear: 2 } => planeSound.pitch = 0.3f,
                { gear: 3 } => planeSound.pitch = 0.4f,
                { gear: 4 } => planeSound.pitch = 0.5f,
                { gear: 5 } => planeSound.pitch = 0.6f,
                { gear: 6 } => planeSound.pitch = 0.7f,
                { gear: 7 } => planeSound.pitch = 0.8f,
                { gear: 8 } => planeSound.pitch = 0.9f,
                { gear: 9 } => planeSound.pitch = 1f,
                { gear: 10 } => planeSound.pitch = 1f,
                _ => gear
            };
        PlaneSound(this, gear);

        #endregion

        if (planeSpeed != 0)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
        else
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }


        transform.position += transform.forward * planeSpeed * Time.deltaTime;

        float HorizontalInput = joystick.Horizontal;
        float VerticalInput = -joystick.Vertical;

        Yaw += HorizontalInput * YawAmount * Time.deltaTime;

        float pitch = Mathf.Lerp(0, 20, Mathf.Abs(VerticalInput)) * Mathf.Sign(VerticalInput);
        float roll = Mathf.Lerp(0, 30, Mathf.Abs(HorizontalInput)) * -Mathf.Sign(HorizontalInput);

        Quaternion rot = Quaternion.Euler(Vector3.up * Yaw + Vector3.right * pitch + Vector3.forward * roll);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, 2f * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Point"))
        {
            Destroy(other.gameObject);
            allPoint++;
            Score++;
            scoreText.text = "Score = " + Score.ToString();
            scoreAnim.SetTrigger("score");
            Instantiate(confetti, transform.position, transform.rotation);
            if (allPoint == 5)
            {
                StartCoroutine(GameManager.instance.winActiveTime());
                GetComponent<PlaneController>().enabled = false;
                GetComponent<PlaneOther>().enabled = true;
                GameManager.instance.gamePanel.SetActive(false);
                mainCamera.SetActive(false);
                levelEndCamera.SetActive(true);
                levelEndCamera.transform.parent = null;
            }

            if (wrongPanel.activeInHierarchy == true)
            {
                wrongPanel.SetActive(false);
            }
        }

        if (other.gameObject.CompareTag("Wrong"))
        {
            Score--;
            scoreText.text = "Score = " + Score.ToString();
            scoreAnim.SetTrigger("score");
            StartCoroutine(wrongSystem());
        }

        if(other.gameObject.CompareTag("Sea"))
        {
            if (firstPropeller.GetComponent<BoxCollider>() == null)
            {
                StartCoroutine(GameManager.instance.losePanelActiveTime());
                GameManager.instance.gamePanel.SetActive(false);
                slider.value = 0;
                gear = 0;
                Instantiate(explosion, transform.position, transform.rotation);

                for (int i = 0; i < 4; i++)
                {
                    transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
                    transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
                    transform.GetChild(i).transform.parent = null;
                }
            }
        }
    }

    IEnumerator wrongSystem()
    {
        wrongPanel.SetActive(true);
        BlackFade.canvasRenderer.SetAlpha(1.0f);
        FadeOut();
        StartCoroutine(Close());
        yield return new WaitForSeconds(3);
        wrongPanel.SetActive(false);
    }


    void FadeOut()
    {
        BlackFade.CrossFadeAlpha(0, 1f, false);
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(1.5f);
        BlackFade.canvasRenderer.SetAlpha(0.0f);
        BlackFade.CrossFadeAlpha(1, 1f, false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Building") || other.gameObject.CompareTag("Floor"))
        {
            if(firstPropeller.GetComponent<BoxCollider>() == null)
            {
                StartCoroutine(GameManager.instance.losePanelActiveTime());
                GameManager.instance.gamePanel.SetActive(false);
                slider.value = 0;
                gear = 0;
                Instantiate(explosion, transform.position, transform.rotation);

                for (int i = 0; i < 4; i++)
                {
                    transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
                    transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
                    transform.GetChild(i).transform.parent = null;
                }
            }
        }
    }
}