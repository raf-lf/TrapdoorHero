using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointer : MonoBehaviour
{
    public Animator anim;
    public Transform enemyTransform;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyTransform = GetComponentInParent<Enemy>().transform;

    }

    private void Start()
    {
        transform.parent = GameManager.scriptGameplay.mainCamera.transform;
        transform.localPosition = new Vector3(0, 1, 3);
    }

    private void Update()
    {
        if(enemyTransform != null)
            transform.LookAt(new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z));
        else
            Destroy(gameObject);

    }

}
