using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterControlHandler
{
    public GameObject emit_bullet;
    public Transform pointer;


    protected override void MainUpdate()
    {
        base.MainUpdate();
        //Debug.Log("PlayerController self update");
    }

    protected override void OnJump()
    {
        base.OnJump();
    }

    protected override void OnAttck()
    {
        base.OnAttck();

        if (emit_bullet != null) {
            Vector3 create_locate = pointer.position; 
            Transform bullet = Instantiate<Transform>(emit_bullet.transform, create_locate, Quaternion.identity);

            Vector3 shootDir = create_locate - transform.position;
            bullet.GetComponent<bullet>().setup(shootDir);
        }
    }

    protected override void UpdateMove(float leftRight, float frontback)
    {
        if (Mathf.Abs(leftRight) > 0.01f || Mathf.Abs(frontback) > 0.01f)
        {
            // call
            anim.SetBool("Move", true);
            // 這裡旋轉人物使他朝向我們想移動的方向
            if (controlType == ControlType.rotateByAngle)
            {
                if (Mathf.Abs(leftRight) > 0.01f)
                {
                    angles += (1 * speed_rotate) * (leftRight > 0 ? 1 : -1);
                    //Debug.Log("Horizontal value ? " + h + " , angles ? " + angles);
                    transform.rotation = Quaternion.Euler(0, angles, 0);
                }

                if (Mathf.Abs(frontback) > 0.01f)
                {
                    //Debug.Log("Vertical value ? " + v);
                    controller.Move(transform.TransformDirection(Vector3.forward * Time.deltaTime * frontback * speed) + new Vector3(0,total_force,0));
                }
            }
            else if (controlType == ControlType.rotateByDirection)
            {
                base.UpdateMove(leftRight, frontback);
            }
        }
        else
        {
            // 切換到靜止動畫
            anim.SetBool("Move", false);
            controller.Move( new Vector3(0, total_force, 0));
        }
    }
}
