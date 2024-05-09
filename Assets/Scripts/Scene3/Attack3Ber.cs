using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attack3Ber : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private GameObject origin;
    [SerializeField] private GameObject[,] targets = new GameObject[3,3];
    [SerializeField] private int randomIndex;
    [SerializeField] private int stage = 0;
    [SerializeField] private Camera playerCam;
    [SerializeField] private Camera berCamera;
    [SerializeField] private Animator berAnimator;
    [SerializeField] private AudioSource jumpscareAudio;
    [SerializeField] private AudioSource clangAudio;
    private float stage1T;
    private float stage2T;
    private float stage3T;
    private float stage4T;
    private bool isActive = false;
    private bool isAway = false;
    private bool aggression = false;
    public bool attacking = false;
    private float timer = 0f;
    private float timer2 = 0f;
    private static readonly int IsScared = Animator.StringToHash("isScared");
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        playerCam.enabled = true;
        berCamera.enabled = false;
        targets[0,0] = GameObject.Find("T");
        targets[0,1] = GameObject.Find("T (1)");
        targets[0,2] = GameObject.Find("T (2)");
        targets[1,0] = GameObject.Find("T (3)");
        targets[1,1] = GameObject.Find("T (4)");
        targets[1,2] = GameObject.Find("T (5)");
        targets[2,0] = GameObject.Find("T (6)");
        targets[2,1] = GameObject.Find("T (7)");
        targets[2,2] = GameObject.Find("T (8)");
        stage1T = Random.Range(20f, 29f);
        stage2T = Random.Range(40f, 49f);
        stage3T = Random.Range(60f, 69f);
        stage4T = Random.Range(75f, 78f);
        randomIndex = Random.Range(0, 3);
        if(randomIndex == 0){
            transform.rotation = Quaternion.Euler(90, 90, 0);
        } else if(randomIndex == 1){
            transform.rotation = Quaternion.Euler(0, -90, 0);
        } else{
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        transform.position = origin.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);
        if(attacking){
            timer += Time.deltaTime;
        }
        if(isActive){
            if(GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds)){
                if(IsWithinReach() == true && isAway == false){
                    timer2 += Time.deltaTime;
                    Debug.Log(timer2);
                    if(timer2>=2f){
                        isAway = true;
                        Debug.Log(IsWithinReach());
                        StartCoroutine(ResetTimer());
                    }
                }
            }

            // change stages
            if (timer >= stage1T && timer <= stage1T+1){
                timer2 = 0f;
                stage = 1;
                transform.position = targets[randomIndex,2].transform.position;
            } else if (timer >= stage2T && timer <= stage2T+1){
                stage = 2;
                transform.position = targets[randomIndex,1].transform.position;
                clangAudio.Play();
            } else if (timer >= stage3T && timer <= stage3T+1){
                stage = 3;
                transform.position = targets[randomIndex,0].transform.position;
                clangAudio.Play();
            } else if (timer >= stage4T && timer <= stage4T+1 && !aggression){
                stage = 4;
                aggression = true;
                StartCoroutine(JumpscareSequence());
            }
        }
    }

    public IEnumerator JumpscareSequence()
    {
        SaveSystem.SaveHint("ber","room3");
        Debug.Log("KILL!!");
        var playerPos = Player.Player.Instance.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.transform.position = new Vector3(playerPos.x - 2f, playerPos.y + 0.5f, playerPos.z);
        playerCam.enabled = false;
        berCamera.enabled = true;
        berAnimator.SetBool(IsScared, true);
        jumpscareAudio.Play();
        yield return new WaitForSeconds(2.25f);

        SceneManager.LoadScene("GameOverScene");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator ResetTimer(){
        Debug.Log("RESET");
        isActive = false;
        stage = 0;
        transform.position = origin.transform.position;
        yield return new WaitForSeconds(Random.Range(10f, 20f));
        timer2 = 0f;
        timer = 0f;
        isAway = false;
        isActive = true;
        stage1T = Random.Range(20f, 29f);
        stage2T = Random.Range(40f, 49f);
        stage3T = Random.Range(60f, 69f);
        stage4T = Random.Range(75f, 78f);
        randomIndex = Random.Range(0, 3);
        if(randomIndex == 0){
            transform.rotation = Quaternion.Euler(90, 90, 0);
        } else if(randomIndex == 1){
            transform.rotation = Quaternion.Euler(0, -90, 0);
        } else{
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
