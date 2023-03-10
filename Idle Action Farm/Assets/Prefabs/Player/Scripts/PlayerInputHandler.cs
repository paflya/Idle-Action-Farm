using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInputHandler : MonoBehaviour
{
    public Joystick joystick;
    public CharacterController controller;
    public Animator animator;
    public float playerSpeed = 10f;

    public GameObject sickle;
    public bool readyToHarvest;
    public float scanRadius;
    public float maxScanRange;
    public float cutRange;
    public GameObject closestObject;


    void Update()
    {
        Vector3 move = new Vector3(-joystick.Vertical, 0, joystick.Horizontal);
        controller.Move(playerSpeed * Time.deltaTime * move);
        controller.Move(Time.deltaTime * -Vector3.up);
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            animator.SetTrigger("isWalking");
            readyToHarvest = false;
            sickle.SetActive(false);
            animator.ResetTrigger("isAttacking");
        }
        else
        {
            if (readyToHarvest)
            {
                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, cutRange, 8))
                {
                    sickle.SetActive(true);
                    animator.SetTrigger("isAttacking");
                }
            }
            animator.ResetTrigger("isWalking");
            readyToHarvest = true;
        }
        
        Collider[] InteractionColliders = Physics.OverlapSphere(transform.position, maxScanRange,128);
        if (InteractionColliders.Length > 0)
        {
            closestObject = GetClosestObject(InteractionColliders);
            if (closestObject != null)
            {
               CollectHay(closestObject);
            }
        }
    }

    private void CollectHay(GameObject hayBlock)
    {
        if (gameObject.GetComponent<PlayerInventory>().currentCropsAmount< gameObject.GetComponent<PlayerInventory>().maxCropsAmount)
        {
            hayBlock.SendMessage("CallToPlayer",transform);
        }
    }
    private void SendCoins()
    {
        if (gameObject.GetComponent<PlayerInventory>().currentCropsAmount > 0)
        {
           closestObject.SendMessage("GetCoins",transform);
        }
            
    }
    public GameObject GetClosestObject(Collider[] colliders)
    {
        float closest = maxScanRange;
        GameObject closestObject = null;
        for (int i = 0; i < colliders.Length; i++)  
        {
            float dist = Vector3.Distance(colliders[i].transform.position, this.transform.position);
            if (dist < closest)
            {
                closest = dist;
                closestObject = colliders[i].gameObject;
            }
        }
        return closestObject;
    }

    public void ToggleWeaponState(string isActive)
    {
        BoxCollider boxCollider = sickle.GetComponent<BoxCollider>();
        boxCollider.enabled = string.IsNullOrEmpty(isActive);
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxScanRange);
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.TransformDirection(Vector3.forward));
    }
    

}


