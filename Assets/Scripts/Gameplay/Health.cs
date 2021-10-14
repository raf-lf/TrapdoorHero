using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    public float hp;
    [HideInInspector]
    public float hpMax;

    public List<AttackType> attackableBy = new List<AttackType>();

    public bool dead;

    public ParticleSystem vfxDamage;
    public ParticleSystem vfxHeal;

    private Material standartMeshMaterial;
    [SerializeField]
    private Material damagedMaterial;
    private CreatureCanvas connectedCanvas;

    private void Awake()
    {
        hpMax = hp;
        standartMeshMaterial = GetComponentInChildren<MeshRenderer>().material;
        connectedCanvas = GetComponentInChildren<CreatureCanvas>();
    }

    public void Damage(int value, AttackType attackType)
    {
        if (attackableBy.Contains(attackType))
        {
            if (GetComponent<Player>())
                value = GetComponent<Player>().DamagePlayer(value, attackType);

            HPChange(-value);
        }
    }

    public void HPChange(int value)
    {
        if (value < 0)
        {
            if (vfxDamage != null)
            {
                vfxDamage.Play();
                if (vfxDamage.GetComponentInChildren<PlaySfx>())
                    vfxDamage.GetComponentInChildren<PlaySfx>().PlayInspectorSfx();
            }

            if (damagedMaterial != null)
            {
                StopAllCoroutines();
                StartCoroutine(MaterialSwitch());
            }

        }
        if (value > 0)
        {
            if (vfxHeal != null)
            {
                vfxHeal.Play();
                if (vfxHeal.GetComponentInChildren<PlaySfx>())
                    vfxHeal.GetComponentInChildren<PlaySfx>().PlayInspectorSfx();
            }
        }

        hp = Mathf.Clamp(hp + value, 0, hpMax);

        if (value != 0)
        {
            if (GetComponent<Player>())
                GameManager.scriptHud.ChangeHP(value);
            if (connectedCanvas != null)
                connectedCanvas.UpdateValues();
        }

        if (hp == 0)
            Death();
    }

    public void Death()
    {
        dead = true;
        gameObject.SetActive(false);
    }

    IEnumerator MaterialSwitch()
    {
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer mesh in meshes)
        {
            mesh.material = damagedMaterial;
        }
        yield return new WaitForSeconds(.15f);

        foreach (MeshRenderer mesh in meshes)
        {
            mesh.material = standartMeshMaterial;
        }
    }
}
