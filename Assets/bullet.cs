//
// bullet.cs
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
public class bullet : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float lifeCycle = 2f;

    private Vector3 myDirection;

    public void setup(Vector3 shootDir)
    {
        myDirection = shootDir;
    }

    private void Update()
    {
        transform.position += myDirection * moveSpeed * Time.deltaTime;

        lifeCycle -= Time.deltaTime;
        if (lifeCycle <= 0) {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Destroy(this.gameObject);
    }

}
