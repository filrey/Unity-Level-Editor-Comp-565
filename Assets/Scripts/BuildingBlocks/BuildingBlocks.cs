using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityFBXExporter;

public class BuildingBlocks : MonoBehaviour {

    public Material transparentMaterial;
    public Material[] availableMaterials;

    Material blockMaterial;
    int selectedMaterialIndex = 0;
    public GameObject createdLevel;
    public string newPath;

    // Use this for initialization
    void Start () {
        blockMaterial = availableMaterials[selectedMaterialIndex];
        createdLevel = new GameObject();
        createdLevel.name = "MyLevel";
        newPath = GetNewPath(createdLevel, null);
    }

    public bool Explode;
    // Update is called once per frame
    int index = 0;
    GameObject realtimeCube;
    RaycastHit realTimeHitInfo = new RaycastHit();

    //Vector3 previousMousePosition = new Vector3(0,0,0);
    void Update () {

        if (IsPointerOverUIObject())
        {
            if (realtimeCube)
                Destroy(realtimeCube);

            return;
        }

        if (Input.GetKeyUp(KeyCode.E))
            Explode = !Explode;

        if(!Explode)//previousMousePosition!=Input.mousePosition )
        {

            //RaycastHit realTimeHitInfo = new RaycastHit();
            bool realTimeHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out realTimeHitInfo);
            if (realTimeHit)
            {
                //if(!realTimeHitInfo.transform.name.Equals("TempCube"))
                {
                    if (realtimeCube == null)
                    {
                        realtimeCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        realtimeCube.name = "TempCube";
                        realtimeCube.layer = 2;
                        realtimeCube.AddComponent<Rigidbody>().useGravity = false; 
                        realtimeCube.AddComponent<BlockCollision>();
                        //realtimeCube.GetComponent<BoxCollider>().enabled = false;
                        realtimeCube.GetComponent<Renderer>().material = transparentMaterial;
                        realtimeCube.GetComponent<Renderer>().material.color = new Color(1, 1, 0, 0.5f);
                        realtimeCube.transform.position =
                            new Vector3(realTimeHitInfo.point.x,
                                        realTimeHitInfo.point.y + (0.5f),
                                        realTimeHitInfo.point.z);
                    }
                    else
                    {
                        // check to see if we 
                        if(realTimeHitInfo.transform.tag.Equals("Base"))
                        {
                            realtimeCube.GetComponent<Renderer>().material.color = new Color(1, 1, 0, 0.5f);

                            realtimeCube.transform.position = Vector3.Lerp(realtimeCube.transform.position,
                                                                            new Vector3(realTimeHitInfo.point.x,
                                                                            realTimeHitInfo.point.y + (0.5f),
                                                                            realTimeHitInfo.point.z),
                                                                            Time.deltaTime * 10);

                            //realtimeCube.transform.position = new Vector3(realTimeHitInfo.point.x,
                            //                                                realTimeHitInfo.point.y + (0.5f),
                            //                                                realTimeHitInfo.point.z);


                        }
                        else //if(realTimeHitInfo.transform.tag.Equals("MyCube"))
                        {
                            realtimeCube.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.5f);
                            if (realTimeHitInfo.normal == new Vector3(0, 0, 1))
                            {
                                realtimeCube.transform.position = new Vector3(realTimeHitInfo.transform.position.x, realTimeHitInfo.transform.position.y, realTimeHitInfo.point.z + (0.5f));
                            }
                            if (realTimeHitInfo.normal == new Vector3(1, 0, 0))
                            {
                                realtimeCube.transform.position = new Vector3(realTimeHitInfo.point.x + (0.5f), realTimeHitInfo.transform.position.y, realTimeHitInfo.transform.position.z);
                            }
                            if (realTimeHitInfo.normal == new Vector3(0, 1, 0))
                            {
                                realtimeCube.transform.position = new Vector3(realTimeHitInfo.transform.position.x, realTimeHitInfo.point.y + (0.5f), realTimeHitInfo.transform.position.z);
                            }
                            if (realTimeHitInfo.normal == new Vector3(0, 0, -1))
                            {
                                realtimeCube.transform.position = new Vector3(realTimeHitInfo.transform.position.x, realTimeHitInfo.transform.position.y, realTimeHitInfo.point.z - (0.5f));
                            }
                            if (realTimeHitInfo.normal == new Vector3(-1, 0, 0))
                            {
                                realtimeCube.transform.position = new Vector3(realTimeHitInfo.point.x - (0.5f), realTimeHitInfo.transform.position.y, realTimeHitInfo.transform.position.z);
                            }
                            if (realTimeHitInfo.normal == new Vector3(0, -1, 0))
                            {
                                realtimeCube.transform.position = new Vector3(realTimeHitInfo.transform.position.x, realTimeHitInfo.point.y - (0.5f), realTimeHitInfo.transform.position.z);
                            }
                        }
                    }
                }
            }
            else
            {
                if (realtimeCube)
                    Destroy(realtimeCube);
            }
        }
        else
        {
            if (realtimeCube)
                Destroy(realtimeCube);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(realtimeCube);

            #region Screen To World
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if(Explode)
                {
                    var t = hitInfo.transform.GetComponent<TriangleExplosion>();
                    StartCoroutine(t.SplitMesh(true));
                    return;
                }


                //var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //cube.name = index.ToString();
                //cube.tag = "MyCube";
                //index += 1;
                //cube.AddComponent<TriangleExplosion>();
                //cube.AddComponent<Rigidbody>();

                //var rb = cube.GetComponent<Rigidbody>();
                //rb.constraints = RigidbodyConstraints.FreezeAll;
                
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                var currentLevel = GameObject.Find("MyLevel");
                cube.tag = "MyCube";
                cube.AddComponent<TriangleExplosion>();
                //cube.AddComponent<Rigidbody>().useGravity = false;
                cube.GetComponent<BoxCollider>().isTrigger = true;
                cube.GetComponent<Renderer>().material = blockMaterial;
                cube.transform.parent = currentLevel.transform;

                #region HIDE
                if (hitInfo.transform.tag.Equals("Base"))
                {
                    cube.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y + (0.5f), hitInfo.point.z);
                }
                else
                {
                    if (hitInfo.normal == new Vector3(0, 0, 1))
                    {
                        cube.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.point.z + (0.5f));
                    }
                    if (hitInfo.normal == new Vector3(1, 0, 0))
                    {
                        cube.transform.position = new Vector3(hitInfo.point.x + (0.5f), hitInfo.transform.position.y, hitInfo.transform.position.z);
                    }
                    if (hitInfo.normal == new Vector3(0, 1, 0))
                    {
                        cube.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y + (0.5f), hitInfo.transform.position.z);
                    }
                    if (hitInfo.normal == new Vector3(0, 0, -1))
                    {
                        cube.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.point.z - (0.5f));
                    }
                    if (hitInfo.normal == new Vector3(-1, 0, 0))
                    {
                        cube.transform.position = new Vector3(hitInfo.point.x - (0.5f), hitInfo.transform.position.y, hitInfo.transform.position.z);
                    }
                    if (hitInfo.normal == new Vector3(0, -1, 0))
                    {
                        cube.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y - (0.5f), hitInfo.transform.position.z);
                    }
                }

                //Debug.DrawRay(hitInfo.point, hitInfo.normal, Color.red, 2, false);
                //Debug.Log(hitInfo.normal);
                #endregion
                

            }
            else
            {
                Debug.Log("No hit");
            }
            #endregion
        }
    }

    public void ChangeMaterial(Button button)
    {
        selectedMaterialIndex = Convert.ToInt32(button.name.Last().ToString()) - 1;

        blockMaterial = availableMaterials[selectedMaterialIndex];
    }

    /// <summary>
    /// Used to determine if we are over UI element or not.
    /// </summary>
    /// <returns></returns>
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        //foreach (var result in results)
        //{
        //    Debug.Log(result.gameObject.name);
        //}
        return results.Count > 0;
    }

    public void UiExporting() {
        FBXExporter exporter = new FBXExporter();
        // exporter.ExportGameObjToFBX(createdLevel, newPath, true, true);
    }

    private static string GetNewPath(GameObject gameObject, string oldPath = null)
    {
        // NOTE: This must return a path with the starting "Assets/" or else textures won't copy right

        string name = gameObject.name;

        string newPath = null;
        if (oldPath == null)
            newPath = EditorUtility.SaveFilePanelInProject("Export FBX File", name + ".fbx", "fbx", "Export " + name + " GameObject to a FBX file");
        else
        {
            if (oldPath.StartsWith("/Assets"))
            {
                oldPath = Application.dataPath.Remove(Application.dataPath.LastIndexOf("/Assets"), 7) + oldPath;
                oldPath = oldPath.Remove(oldPath.LastIndexOf('/'), oldPath.Length - oldPath.LastIndexOf('/'));
            }
            newPath = EditorUtility.SaveFilePanel("Export FBX File", oldPath, name + ".fbx", "fbx");
        }

        int assetsIndex = newPath.IndexOf("Assets");

        if (assetsIndex < 0)
            return null;

        if (assetsIndex > 0)
            newPath = newPath.Remove(0, assetsIndex);

        return newPath;
    }

}
