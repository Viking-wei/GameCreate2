using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering;

[ExecuteAlways]
public class CameraDirector : MonoBehaviour
{
    [Required]public Transform target;
    public float distance;
    public float angle;
    private Vector3 _pos;
    private Vector3 _offSet;
    
    
    void LateUpdate()
    {
        //FIXME:remember move this to Start()
        _offSet=new Vector3(0,distance*Mathf.Sin(angle/180),-distance*Mathf.Cos(angle/57));

        transform.position=target.position+_offSet;
        transform.LookAt(target.transform);
    }

    // RaycastHit HitInfo;
    // List<GameObject> TransparentObjects = new List<GameObject>();
 
    // void Update()
    // {
 
    //     SetTrans();
 
    // }
 
    // private void SetTrans()
    // {
 
    //     Vector3 Direction = target.position - transform.position;//射线方向为摄像头指向人物
 
    //     RaycastHit[] hits;
        
    //     if (Physics.Raycast(transform.position,Direction,out HitInfo))
    //     {
    //         if (!HitInfo.transform.CompareTag("Player"))//若人物被遮挡了
    //         {
    //             //只检测Default图层的物体，其它图层检测不到
    //             hits = Physics.RaycastAll(transform.position, Direction, distance,1<< LayerMask.NameToLayer("Default"));

    //             for (int i = 0; i < hits.Length; i++)
    //             {
    //                 var hit = hits[i];
    //                 if (hit.transform.CompareTag("Player"))
    //                 {
    //                     return;
    //                 }
    //                 else
    //                 {
    //                     ClearTransparentObjects();
    //                     //若障碍物带有碰撞器组件
    //                     if (hit.transform.GetComponent<MeshRenderer>())
    //                     {   //若障碍物的透明度不为0.2
    //                         if(hit.transform.GetComponent<MeshRenderer>().material.color.a != 0.2f)
    //                         {
    //                             var ChangeColor = hit.transform.GetComponent<MeshRenderer>().material.color;
    //                             ChangeColor.a = 0.2f;
 
    //                             SetMaterialRenderingMode(hit.transform.GetComponent<MeshRenderer>().material, RenderingMode.Transparent);
 
    //                             hit.transform.GetComponent<MeshRenderer>().material.color = ChangeColor;

    //                             //将透明物体添加到数组中
    //                             TransparentObjects.Add(hit.transform.gameObject);

    //                             Debug.Log("射线检测到的物体名为：" + hit.transform.name);
    //                         }                       
    //                     }
                        
    //                 }
 
    //             }
    //         }
    //         else//若人物没有被遮挡
    //         {
    //             ClearTransparentObjects();
    //         }
            
    //     }
        
    // }
 
    // void ClearTransparentObjects()//将透明物体恢复不透明
    // {
    //     if (TransparentObjects != null)
    //     {
    //         for (int i = 0; i < TransparentObjects.Count; i++)
    //         {
    //             var ChangeColor = TransparentObjects[i].transform.GetComponent<MeshRenderer>().material.color;
    //             ChangeColor.a = 1f;
           
    //             TransparentObjects[i].transform.GetComponent<MeshRenderer>().material.color = ChangeColor;
    //         }
    //         TransparentObjects.Clear();//清除数组
    //     }
    // }


}
