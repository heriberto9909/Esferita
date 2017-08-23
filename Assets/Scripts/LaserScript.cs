using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserScript : MonoBehaviour {

    public float mFireRate = .5f;
    public float mFireRange = 50f;
    public float mHitForce = 100f;
    public int mLaserDamage = 100;

    private LineRenderer mLaserLine;

    private bool mLaserLineEnabled;

    private WaitForSeconds mLaserDuration = new WaitForSeconds(0.05f);

    private float mNextFire;

    private AudioSource laser;

    int contador = 0;

    public Text puntuacion;

    void Start()
    {
        mLaserLine = GetComponent<LineRenderer>();
        laser = GetComponent<AudioSource>();
    }

    private void Fire()
    {
        Transform cam = Camera.main.transform;

        mNextFire = Time.time + mFireRate;

        Vector3 rayOrigin = cam.position;

        mLaserLine.SetPosition(0, transform.up * -10f);

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, cam.forward, out hit, mFireRange))
        {
            mLaserLine.SetPosition(1, hit.point);

            CubeBehaviorScript cubeCtr = hit.collider.GetComponent<CubeBehaviorScript>();
            CubeBehaviorScript2 cubeCtr2 = hit.collider.GetComponent<CubeBehaviorScript2>();
            if (cubeCtr != null)
            {
                if (hit.rigidbody != null)
                {
                    contador = contador + 5;
                    Debug.Log("Tiro");
                    Debug.Log("Contador " + contador);
                    puntuacion.text = "Marcador: " + contador;
                    hit.rigidbody.AddForce(-hit.normal * mHitForce);
                    cubeCtr.Hit(mLaserDamage);
                }
            }

            if (cubeCtr2 !=null)
            {
                if(hit.rigidbody != null)
                {
                    Debug.Log("Incorrecto");
                    hit.rigidbody.AddForce(-hit.normal * mHitForce);
                    cubeCtr2.Hit(mLaserDamage);
                }
            }

        }
        else
        {
            mLaserLine.SetPosition(1, cam.forward * mFireRange);
        }

        StartCoroutine(LaserFx());
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > mNextFire)
        {
            Fire();
            laser.Play();
        }
    }

    private IEnumerator LaserFx()
    {
        mLaserLine.enabled = true;


        yield return mLaserDuration;
        mLaserLine.enabled = false;
    }
}
