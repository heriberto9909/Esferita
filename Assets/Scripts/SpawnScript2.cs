using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SpawnScript2 : MonoBehaviour {

    public GameObject mCubeObj;

    public int mTotalCubes = 10;

    public float mTimeToSpawn = 1f;

    private GameObject[] mCubes;

    private bool mPositionSet;

    private bool SetPosition()
    {

        Transform cam = Camera.main.transform;


        transform.position = cam.forward * 10;
        return true;
    }

    private IEnumerator SpawnLoop()
    {
        StartCoroutine(ChangePosition());

        yield return new WaitForSeconds(0.2f);

        int i = 0;
        while (i <= (mTotalCubes - 1))
        {

            mCubes[i] = SpawnElement();
            i++;
            yield return new WaitForSeconds(Random.Range(mTimeToSpawn, mTimeToSpawn * 3));
        }
    }

    private GameObject SpawnElement()
    {
        GameObject cube = Instantiate(mCubeObj, (Random.insideUnitSphere * 4) + transform.position, transform.rotation) as GameObject;
        float scale = Random.Range(0.5f, 2f);
        cube.transform.localScale = new Vector3(scale, scale, scale);
        return cube;
    }

    void Start()
    {
        StartCoroutine(SpawnLoop());

        mCubes = new GameObject[mTotalCubes];
    }


    private IEnumerator ChangePosition()
    {

        yield return new WaitForSeconds(0.2f);

        if (!mPositionSet)
        {

            if (VuforiaBehaviour.Instance.enabled)
                SetPosition();
        }
    }
}
