using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class JobStystemExample : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        DoExample();
    }

    private void DoExample()
    {
        //1 Istantiate
        SimpleJob myJob = new SimpleJob();
        AnotherJob secondJob = new AnotherJob();
        //2 Initialize
        myJob.a = 5f;
        myJob.result = new NativeArray<float>(1, Allocator.TempJob);

        secondJob.result = myJob.result;
        //3 Schedule
        JobHandle jobHandle = myJob.Schedule();
        JobHandle secondJobHandle = secondJob.Schedule(jobHandle);

        jobHandle.Complete();
        secondJobHandle.Complete();

        float resultingValue = myJob.result[0];
        Debug.LogError(resultingValue + " " + myJob.a);

        myJob.result.Dispose();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private struct SimpleJob : IJob
    {
        public float a;
        public NativeArray<float> result;

        public void Execute()
        {
            result[0] = a;
            a = 23;
        }
    }

    private struct AnotherJob : IJob
    {
        public float a;
        public NativeArray<float> result;

        public void Execute()
        {
            result[0]++;
        }
    }

    /////////////////////7
    ///
    //public class RotationSpeedSystem_ForEach : JobComponentSystem
    //{
    //    protected override JobHandle OnUpdate(JobHandle inputDeps)
    //    {
    //        float deltaTime = Time.DeltaTime;
    //        //schedule job to rotate around up vector

    //        var handle = Entities.WithName("RotationSpeedSystem_ForEach")
    //            .ForEach((ref Rotation rotation, in RotationSpeed_ForEach rotationSpeed) =>
    //            {
    //                rotation.Value = math.mul(math.normalize(rotation.Value), quaternion.AxisAngle(math.up(), rotationSpeed.radians));

    //            }).Schedule();
}