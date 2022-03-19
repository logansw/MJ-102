using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{

    [SerializeField] private Animator anim;

    private const float MeleeCD = 0.2f;

    private float meleeCDTimer;
    private bool meleeReady;

    // Start is called before the first frame update
    void Start()
    {
        meleeCDTimer = 0;
        meleeReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        meleeCDTimer += Time.deltaTime;
        if (meleeCDTimer > MeleeCD) {
            meleeReady = true;
        }
        HandleMelee();
    }

    private void HandleMelee()
    {
        if (meleeReady && Input.GetKeyDown(KeyCode.C)) {
            print("ATTACK");
            anim.SetTrigger("Attack");
            meleeCDTimer = 0;
            meleeReady = false;
        }
    }
}
