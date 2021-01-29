using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

//VERSIONEINIZIALE A 50 FPS SOLO SUL MAIN THREAD
//public class WaveSystem : ComponentSystem
//{
//    protected override void OnUpdate()
//    {
//        Entities.ForEach((ref Translation trans, ref MoveSppedComponentData moveSpped, ref WaveComponentData wave) =>
//        {
//            float zPosition = wave.amplitude * math.sin((float)Time.ElapsedTime * moveSpped.Value
//                + trans.Value.x * wave.xOffset + trans.Value.y * wave.yOffset); // add the sin phase shift
//            trans.Value = new float3(trans.Value.x, trans.Value.y, zPosition);
//        });
//    }
//}

/// <summary>
/// VERSIONE MULTI THREAD VA A 250 FPS
/// </summary>
//public class WaveSystem : JobComponentSystem
//{
//    protected override JobHandle OnUpdate(JobHandle inputDependencies)
//    {
//        float TimeElapsed = (float)Time.ElapsedTime;
//        JobHandle jobHandle = Entities.ForEach((ref Translation trans, in MoveSppedComponentData moveSpped, in WaveComponentData wave) =>
//         {
//             float zPosition = wave.amplitude * math.sin(TimeElapsed * moveSpped.Value
//                 + trans.Value.x * wave.xOffset + trans.Value.y * wave.yOffset); // add the sin phase shift
//             trans.Value = new float3(trans.Value.x, trans.Value.y, zPosition);
//         }).Schedule(inputDependencies);

//        return jobHandle;
//    }
//}

////Versione che viene eseguita solo nel MAIN thread PERO USA I JOB SYSTEM. RISPETTO ALLA VERSIONE DI SOPRA SE CI RIPENSIAMO E VOGLIAMO TRASFORMALRLO TUTTO SUL MAIN THREAD
//[AlwaysSynchronizeSystem]
//public class WaveSystem : JobComponentSystem
//{
//    protected override JobHandle OnUpdate(JobHandle inputDependencies)
//    {
//        float TimeElapsed = (float)Time.ElapsedTime;
//        JobHandle jobHandle = Entities.ForEach((ref Translation trans, in MoveSppedComponentData moveSpped, in WaveComponentData wave) =>
//        {
//            float zPosition = wave.amplitude * math.sin(TimeElapsed * moveSpped.Value
//                + trans.Value.x * wave.xOffset + trans.Value.y * wave.yOffset); // add the sin phase shift
//            trans.Value = new float3(trans.Value.x, trans.Value.y, zPosition);
//        }).Run();

//        return default;
//    }
//}

/// <summary>
/// VERSIONE MULTI THREAD VA A 250 FPS
/// </summary>
public class WaveSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float TimeElapsed = (float)Time.ElapsedTime;
        Entities.ForEach((ref Translation trans, in MoveSppedComponentData moveSpped, in WaveComponentData wave) =>
        {
            float zPosition = wave.amplitude * math.sin(TimeElapsed * moveSpped.Value
                + trans.Value.x * wave.xOffset + trans.Value.y * wave.yOffset); // add the sin phase shift
            trans.Value = new float3(trans.Value.x, trans.Value.y, zPosition);
        }).ScheduleParallel();
    }
}