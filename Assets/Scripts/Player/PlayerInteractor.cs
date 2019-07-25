using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour 
{

    public Transform SelectionBlock;
    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Mouse0))
        // {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5))
        {
            if (hit.transform)
            {
                var chunk = hit.transform.GetComponent<ChunkGenerator>();
                var local = chunk.Reference.WorldToLocal(hit.point - (hit.normal/2));
                SelectionBlock.gameObject.SetActive(true);
                Vector3 blockPos = local + chunk.Reference.WorldPos;
                SelectionBlock.position = blockPos;

                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    chunk.Reference.ApplyBlock((int)local.x, (int)local.y, (int)local.z, Blocks.Air);
                    chunk.UpdateMesh();
                }

                if(Input.GetKeyDown(KeyCode.Mouse1))
                {
                    var placedPos = chunk.Reference.WorldToLocal(hit.point + (hit.normal / 2));
                    chunk.Reference.ApplyBlock((int)placedPos.x, (int)placedPos.y, (int)placedPos.z, 
                        Blocks.Stone);
                    chunk.UpdateMesh();
                }
            }
        }
        else
        {
            SelectionBlock.gameObject.SetActive(false);
        }
        // }
    }
}
