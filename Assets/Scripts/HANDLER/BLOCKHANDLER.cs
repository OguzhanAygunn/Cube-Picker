using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BLOCKHANDLER : MonoBehaviour
{
    public static BLOCKHANDLER Instance;

    public List<GameObject> BlocksOfPlayer = new List<GameObject>();

    [SerializeField] int forSmall, forMedium, forLarge;
    Transform playerPos;
    // Start is called before the first frame update

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        Instance = this;
    }


    void blockCountControl()
    {
        int count = BlocksOfPlayer.Count;
        if(count >= forMedium)
        {
            if (count >= forLarge)
            {
                PLAYER.Instance.ChangePhase(PLAYER.phases.large);
                return;
            }
            PLAYER.Instance.ChangePhase(PLAYER.phases.medium);
        }
    }


    public void AddBlock(GameObject block)
    {
        BlocksOfPlayer.Add(block);
        blockCountControl();
        
    }
    public void DropTheBlocks(int count)
    {
        if(count <= BlocksOfPlayer.Count)
        {
            count = count; // :D
        }
        else
        {
            count = BlocksOfPlayer.Count;
        }
        while(count > 0)
        {
            int randomValue = Random.Range(0,BlocksOfPlayer.Count);
            GameObject Block = BlocksOfPlayer[randomValue];
            if (Block == null)
                return;
            Block.GetComponent<Collider>().enabled = true;
            Block.GetComponent<Collider>().isTrigger = false;
            Block.tag = "Untagged";
            Block.transform.parent = null;
            Block.AddComponent<Rigidbody>();
            BlocksOfPlayer.Remove(Block);
            count--;
        }
        blockCountControl();
    }

    public void DeathOP()
    {
        foreach(GameObject block in BlocksOfPlayer)
        {
            if (block)
            {

                Rigidbody blockRigid;
                Collider collider;
                collider = block.GetComponent<Collider>();
                if(block.GetComponent<Rigidbody>() == null)
                {
                    blockRigid = block.AddComponent<Rigidbody>();
                }
                else
                {
                    blockRigid = block.GetComponent<Rigidbody>();
                }
                block.transform.parent = null;
                blockRigid.constraints = RigidbodyConstraints.None;
                collider.enabled = true;
                collider.isTrigger = false;
                block.tag = "Untagged";
                blockRigid.AddExplosionForce(5, playerPos.position, 10f, 3f, ForceMode.Impulse);
            }
        }
    }

    public void CallBlocksToFigure()
    {
        StartCoroutine(nameof(BlocksToFigure));
    }

    IEnumerator BlocksToFigure()
    {
        int index,count;
        count = FinalFigure.Instance.PosS.Count;
        index = 0;
        foreach (GameObject block in BlocksOfPlayer)
        {
            index = BlocksOfPlayer.IndexOf(block);
            if (index < count)
            {
                Vector3 targetPos = FinalFigure.Instance.PosS[index];
                Vector3 defaultPos = block.transform.position;
                Color targetColor = FinalFigure.Instance.Colors[index];
                MeshRenderer blockRenderer = block.GetComponent<MeshRenderer>();
                block.transform.parent = null;
                block.transform.DOMoveX(defaultPos.x + ((block.transform.position.x > 0) ? 3 : -3), 0.5f).OnComplete( () => {
                    block.transform.DOMove(targetPos,2f);
                });
                block.transform.DOScale(Vector3.one * 2, 2f);
                block.transform.DORotate(Vector3.zero, 2f); ;
                blockRenderer.material.DOColor(targetColor, 2.5f);
            }
            yield return new WaitForSeconds(0.005f);
        }
        PLAYER.Instance.components.PLAYERROTATE.RandomRotate = false;

        yield return new WaitForSeconds(2.5f);
        if (index < count-1)
        {

            FinishLoseOP();
            yield return new WaitForSeconds(2f);
            PLAYER.Instance.DeathOP();
        }
        else
        {
            yield return new WaitForSeconds(1f);
            Win.Instance.ActiveOP();
        }
    }

    public void FinishLoseOP()
    {
        foreach(GameObject block in BlocksOfPlayer)
        {
            block.transform.parent = null;

            Rigidbody blockRigid = block.GetComponent<Rigidbody>();
            BoxCollider blockCollider = block.GetComponent<BoxCollider>();

            if (!blockRigid)
                blockRigid = block.AddComponent<Rigidbody>();
            if (!blockCollider)
                blockCollider = block.AddComponent<BoxCollider>();

            blockRigid.constraints = RigidbodyConstraints.None;
            blockCollider.enabled = true;
            blockCollider.isTrigger = false;
        }
    }
}
