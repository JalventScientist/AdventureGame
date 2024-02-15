using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverTrail : MonoBehaviour
{
    public LineRenderer Trail;
    private bool TrailActive = false;
    private void Update()
    {
        if (TrailActive)
        {
            if (Trail.startWidth > 0)
                Trail.startWidth -= Time.deltaTime / 10;
            else
                Destroy(this.gameObject);
        }
    }
    public void setTrail(float DistanceFromCamera)
    {
        GameObject NewTrail = Instantiate(this.gameObject);
        NewTrail.transform.localPosition = this.transform.position;
        NewTrail.transform.rotation = this.transform.rotation;
        NewTrail.GetComponent<RevolverTrail>().EmitYes(DistanceFromCamera);
    }
    public void EmitYes(float DistanceFromCamera)
    {
        Trail.startWidth = .07f;
        TrailActive = true;
        Trail.SetPosition(1, new Vector3(-0.47f, 0.22f, DistanceFromCamera));
    }
}
