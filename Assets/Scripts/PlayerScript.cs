using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerScript : NetworkBehaviour
{
    public PlayerSO playerInfo;

    public TextMesh playerNameText;
    public GameObject floatingInfo;

    private Material playerMaterialClone;

    private Animator animator;
    private NetworkAnimator networkAnimator;
    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor = Color.white;

    void OnNameChanged(string _Old, string _New)
    {
        playerNameText.text = playerName;
    }

    void OnColorChanged(Color _Old, Color _New)
    {
        playerNameText.color = _New;
        playerMaterialClone = new Material(GetComponent<Renderer>().material);
        playerMaterialClone.color = _New;
        GetComponent<Renderer>().material = playerMaterialClone;
    }

    public override void OnStartLocalPlayer()
    {
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0, 3.85f, -2.7f);

        floatingInfo.transform.localPosition = new Vector3(0, 4.35f, -1.5f);
        floatingInfo.transform.localScale = new Vector3(-0.2f, 0.2f, 0.1f);

        string name = playerInfo.playerName;
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        CmdSetupPlayer(name, color);
    }

    [Command]
    public void CmdSetupPlayer(string _name, Color _col)
    {
        // player info sent to server, then server updates sync vars which handles it on all clients
        playerName = _name;
        playerColor = _col;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            // make non-local players run this
            floatingInfo.transform.LookAt(Camera.main.transform);
            return;
        }

        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110.0f;
        float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 4f;
        if(moveZ != 0)
        {
            animator.SetBool("isWalking", true);
        } else
        {
            animator.SetBool("isWalking", false);
        }
        transform.Rotate(0, moveX, 0);
        transform.Translate(0, 0, moveZ);
    }
}
