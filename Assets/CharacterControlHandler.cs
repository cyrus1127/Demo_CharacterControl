//
// CharacterController.cs
//
// Author:
//       cyruslam <sluggishchildcreategroup>
//
// Copyright (c) 2021 SluggishChildCreateGroup
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlHandler : MonoBehaviour
{
    public enum ControlType
    {
        rotateByDirection,
        rotateByAngle
    }

    public ControlType controlType = ControlType.rotateByAngle;
    
    public float speed = 0.3f; // 移動速度
    public float speed_rotate = 0.3f;
    public float force_raise = 0.3f;
    public float max_JumpForce = 5f;
    public Animator anim;// 動畫

    protected CharacterController controller; // 角色控制器
    protected float angles = 0f;
    protected float total_force = -1f;
    protected bool isJumping = false;

    private void Awake()
    {
        controller = this.GetComponent<CharacterController>();
        angles = transform.rotation.eulerAngles.y;
        ChildAwake();
    }

    private void Update()
    {
        //Debug.Log("CharacterControlHandler self update");
        MainUpdate();
    }

    protected virtual void ChildAwake() { }

    //For child to override
    protected virtual void MainUpdate() {
        
        
        UpdateForce();

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        UpdateMove(h, v);

        float fire = Input.GetAxis("Fire1");
        if (Input.GetKeyDown("left ctrl")
            || Input.GetMouseButtonUp(0) // left button
            //|| Mathf.Abs(fire) > 0.01f
            )
        {
            OnAttck();
        }

    }

    protected virtual void UpdateForce() {

        float jump = Input.GetAxis("Jump");
        if (Input.GetKeyDown("space")
            || Mathf.Abs(jump) > 0.01f)
        {
            if (!isJumping && total_force == -1)
            {
                isJumping = true;
                total_force = 0;
                OnJump();
            }
        }

        if (isJumping)
        {
            if (total_force < max_JumpForce)
            {
                total_force += Time.deltaTime * force_raise;
                if (total_force > max_JumpForce)
                {
                    isJumping = false;
                    total_force = max_JumpForce;
                }
                //Debug.Log("force increasing => total_force ? " + total_force);
            }
        }
        else
        {
            if (total_force > -1)
            {
                total_force -= Time.deltaTime * force_raise;
                if (total_force < -1)
                {
                    total_force = -1;
                }
                //Debug.Log("force decreasing => total_force ? " + total_force);
            }
        }
    }
    protected virtual void UpdateMove(float leftRight , float frontback) {
        if (Mathf.Abs(leftRight) > 0.01f || Mathf.Abs(frontback) > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(-leftRight, 0, -frontback));
            Vector3 movedir = new Vector3(-leftRight, total_force, -frontback);
            controller.Move(movedir);
        }
    }
    protected virtual void OnJump() {
        
    }
    protected virtual void OnAttck() {}

}
