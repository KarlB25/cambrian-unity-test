using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Cinemachine;

public class ObjectManager : MonoBehaviour
{
    #region Singleton

    public static ObjectManager instance;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


    #endregion

    public static List<GameObject> alreadyLoadedCubes = new List<GameObject>();
    [SerializeField] private CinemachineTargetGroup targetGroupForCameraFollow;
    [SerializeField] private Material matForCubes;
    public void InstantiateObjectsFromJSON(string jsonStringWithObjects)
    {
        if (alreadyLoadedCubes.Count > 0)
        {
            foreach (GameObject cube in alreadyLoadedCubes)
            {
                Destroy(cube);
            }
            alreadyLoadedCubes.Clear();
        }

        Debug.Log("Object manager called with data: " + jsonStringWithObjects);
        JObject jsonObject = JObject.Parse(jsonStringWithObjects);

        targetGroupForCameraFollow.m_Targets = new CinemachineTargetGroup.Target[2];

        int i = 0;
        foreach (JObject obj in jsonObject["objects"])
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            float positionX = Convert.ToSingle(obj["px"]);
            float positionY = Convert.ToSingle(obj["py"]);
            float positionZ = Convert.ToSingle(obj["pz"]);

            float rotationX = Convert.ToSingle(obj["rx"]);
            float rotationY = Convert.ToSingle(obj["ry"]);
            float rotationZ = Convert.ToSingle(obj["rz"]);

            float scaleX = Convert.ToSingle(obj["sx"]);
            float scaleY = Convert.ToSingle(obj["sy"]);
            float scaleZ = Convert.ToSingle(obj["sz"]);

            cube.transform.position = new Vector3(positionX, positionY, positionZ);

            Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
            cube.transform.rotation = targetRotation;

            cube.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
            cube.GetComponent<Renderer>().material = matForCubes;

            CinemachineTargetGroup.Target target;
            target.target = cube.transform;
            target.weight = 1;
            target.radius = 0;

            targetGroupForCameraFollow.m_Targets.SetValue(target, i);

            alreadyLoadedCubes.Add(cube);
            i++;
        }
    }
}
