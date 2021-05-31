//
// AutoMove.cs
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
using UnityEngine;
using UnityEngine.AI;


public class AutoMove : MonoBehaviour
{
    private NavMeshAgent agent;
    public float minDistance = 3f;
    public Transform target;
    private Animator anim;

    CPUControlHandler controller;
    bool isTargetSet = false;

    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();

        controller = this.GetComponent<CPUControlHandler>();
        Debug.Log( gameObject.name + " Awake ");
        agent.enabled = true;
    }

    private void Update()
    {
        if ( target != null)
        {
            if (!isTargetSet) {
                isTargetSet = true;
                SetDestination(target.position);
            } else if ( agent.destination != target.position ) {
                //position change
                agent.SetDestination(target.position);
            }
        }

        if (agent.enabled)
        {
            // 這裡要注意特判一下等於0的情況
            if (agent.remainingDistance != 0 && agent.remainingDistance < minDistance)
            {
                SwitchAgentOn(false);
                Debug.Log("no target");
            }
            else {
                //Debug.Log("remainingDistance ? " + agent.remainingDistance);
            }
        }
    }

    public void SetDestination(Vector3 targetPos)
    {
        Debug.Log("SetDestination");
        // 尋路的時候把鍵盤控制行走的指令碼遮蔽掉
        SwitchAgentOn(true);
        agent.SetDestination(targetPos);
        
    }

    void SwitchAgentOn(bool isON) {
        controller.enabled = !isON;
        agent.isStopped = controller.enabled;
        agent.enabled = isON;
        anim.SetBool("Move", isON);
    }
}

