using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Mesh unitMesh;
    [SerializeField] private Material unitMaterial;
    [SerializeField] private GameObject gameObjectPrefab;
    [SerializeField] private int xSize;
    [SerializeField] private int ySize;

    [Range(0.1f, 2f)]
    [SerializeField] private float spacing;

    private Entity entityPrefab;
    private World defaultWorld;
    private EntityManager entityManager;

    // Start is called before the first frame update
    private void Start()
    {
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        entityManager = defaultWorld.EntityManager;

        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(defaultWorld, null);
        entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefab, settings);
        InstantiateEntityGrid(xSize, ySize, spacing);
        // MakeEntity();
    }

    private void InstantiateEntity(float3 position)
    {
        Entity entity = entityManager.Instantiate(entityPrefab);
        entityManager.SetComponentData(entity, new Translation
        {
            Value = position
        });
    }

    private void InstantiateEntityGrid(int dimX, int dimY, float spacing = 1)
    {
        for (int i = 0; i < dimX; i++)
        {
            for (int y = 0; y < dimY; y++)
            {
                InstantiateEntity(new float3(i * spacing, y * spacing, 0f));
            }
        }
    }

    //Pure ECS
    private void MakeEntity()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityArchetype archetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(Rotation),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld)
            );

        Entity entity = entityManager.CreateEntity(archetype);

        entityManager.AddComponentData(entity,
            new Translation { Value = new float3(2f, 0f, 4f) });
        entityManager.AddSharedComponentData(entity,
            new RenderMesh { mesh = unitMesh, material = unitMaterial });
    }
}