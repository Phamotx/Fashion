using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;
    [SerializeField] private float walkspeed;
    [SerializeField] private GameObject barParent;
    [SerializeField] private Image orderIcon;
    private ItemType type;
    private bool run;
    private bool returnToBase;
    private Vector3 targetPosition;
    private Vector3 endPosition;
    private NavMeshPath path;
    private Renderer rend;
    public ItemType Type { get { return type; } set { type = value; } }

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();

        path = new NavMeshPath();
    }

    public void Init(Vector3 slotPosition, Vector3 endPosition)
    {
        //rend.material.color = normalColor;
        barParent.SetActive(false);
        targetPosition = slotPosition;
        this.endPosition = endPosition;
        type = (ItemType)Random.Range((int)ItemType.Red, ((int)ItemType.Yellow) + 1);
        agent.speed = walkspeed;
        agent.isStopped = false;
        agent.enabled = true;
        run = true;
        returnToBase = false;
    }
    public void ReturnToBase()
    {
        barParent.SetActive(false);
        agent.isStopped = false;
        targetPosition = endPosition;
        agent.speed = walkspeed;
        agent.enabled = true;
        run = true;
        returnToBase = true;
    }

    void Update()
    {

        if (run)
        {
            agent.CalculatePath(targetPosition, path);

            if (path.status != NavMeshPathStatus.PathComplete)
            {
                return;
            }

            agent.destination = targetPosition;

            anim.SetBool("walking", true);

            if (Vector3.Distance(transform.position, targetPosition) < agent.stoppingDistance + 2f)
            {
                barParent.SetActive(!returnToBase);
                orderIcon.color = GameManager.instance.GetColor(type);
                agent.isStopped = true;
                anim.SetBool("walking", false);
                run = false;
                if (returnToBase)
                {
                    GameManager.instance.CheckAndSpawn();
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
