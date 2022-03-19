using FIMSpace.FTail;
using System.Collections.Generic;
using UnityEngine;

public class TailDemo_SegmentedTailGenerator : MonoBehaviour
{
    [Header("Tail will be generated on Start()", order = 0)]
    [Header("References", order = 1)]
    public TailAnimator2 TailWithSettings;
    public GameObject SegmentModel;
    private GameObject ilkSegment;

    [Header("Parameters")]
    public int SegmentsCount = 10;
    public Vector3 SegmentSeparation = Vector3.forward;
    public bool DetachForOptimization = false;

    [Header("Optional")]
    public bool Dynamic = false;
    public bool AddTailAnimToCuttedSegment = false;
    public bool DrawGizmos = true;
    public bool Cuttable = false;

    private GameObject hafizadakiResim;



    public void BaslangicAyarlari()
    {
        SegmentsCount = 0;

    }

    private void OnDrawGizmosSelected()
    {
        if (SegmentModel == null) return;
        if (!DrawGizmos) return;

        GetReferenceParameters();

        Vector3 refSize = new Vector3(SegmentSeparation.magnitude, SegmentSeparation.magnitude, 0f);
        MeshRenderer mesh = SegmentModel.GetComponentInChildren<MeshRenderer>();
        if (mesh) refSize = mesh.bounds.extents;

        Gizmos.matrix = transform.localToWorldMatrix;

        for (int i = 0; i < SegmentsCount; i++)
        {
            Vector3 targetPos = referenceOffset * (0.5f + i);
            Gizmos.DrawWireCube(targetPos, refSize + referenceOffset);
            Gizmos.DrawSphere(targetPos, refSize.sqrMagnitude * 0.1f);

            //Vector3 targetPos = transform.position + referenceOffset * (0.5f + i);
            //Gizmos.DrawWireCube(targetPos, refSize + referenceOffset);
        }

        Gizmos.matrix = Matrix4x4.identity;
    }

    Vector3 referenceOffset;

    void GetReferenceParameters()
    {
        referenceOffset = SegmentSeparation;

        MeshRenderer mesh = SegmentModel.GetComponentInChildren<MeshRenderer>();
        if (mesh)
            referenceOffset = SegmentSeparation * mesh.bounds.extents.z * 2f;
    }


    public void ExmapleCutAt(int index)
    {
        TailAnimator2.TailSegment cutSegment = TailWithSettings.TailSegments[index];

        if (cutSegment.transform)
        {
            cutSegment.transform.parent = null;

            if (cutSegment.transform.childCount > 0)
                if (AddTailAnimToCuttedSegment)
                {
                    // Creating parent for new tail so it can fly away with rigidbody 
                    // (tail animator is changing position of first bone so it would stay in place without parent)

                    GameObject newParent = new GameObject(name + "-Cutted");
                    newParent.transform.position = Vector3.Lerp(cutSegment.ParentBone.ProceduralPosition, cutSegment.ProceduralPosition, 0.5f);
                    newParent.transform.rotation = cutSegment.ParentBone.LastFinalRotation;

                    cutSegment.transform.SetParent(newParent.transform);

                    newParent.gameObject.AddComponent<SphereCollider>().radius = 0.2f;
                    Rigidbody rig = newParent.AddComponent<Rigidbody>();
                    rig.useGravity = false;
                    rig.freezeRotation = true;
                    rig.AddForce(new Vector3(4f, 1f, 0f), ForceMode.Impulse);
                    //rig.AddExplosionForce(100f, cutSegment.ParentBone.ProceduralPosition - cutSegment.ParentToFrontOffset(), Vector3.Distance(cutSegment.ParentBone.ProceduralPosition, cutSegment.ProceduralPosition) * 1.5f);

                    TailAnimator2 newT = cutSegment.transform.gameObject.AddComponent<TailAnimator2>();
                    newT.Axis2D = TailWithSettings.Axis2D;
                    newT.Slithery = TailWithSettings.Slithery;
                    newT.Curling = TailWithSettings.Curling;
                    newT.ReactionSpeed = TailWithSettings.ReactionSpeed;

                    // Creating list of transforms for new tail
                    // (it would work even without doing this, but we will keep shape of original tail thanks to lines below)
                    List<Transform> cutSegments = new List<Transform>();
                    TailAnimator2.TailSegment child = cutSegment;
                    while (child.ChildBone != null)
                    {
                        if (child.transform) cutSegments.Add(child.transform);
                        child = child.ChildBone;
                    }

                    newT.User_SetTailTransforms(cutSegments);

                    // Copying parameters of source segments to new ones
                    for (int i = 0; i < newT.TailSegments.Count; i++)
                    {
                        newT.TailSegments[i].ParamsFromAll(TailWithSettings.TailSegments[cutSegment.Index + i]);
                        newT.TailSegments[i].User_ReassignTransform(cutSegments[i]);
                    }
                }


            // After copying params we can cut tail for algorithm
            TailWithSettings.User_CutEndSegmentsTo(index);

            Transform cuts = cutSegment.transform;

            // Making new tail be uneable to cut tail because it would cut old tail instead of new
            while (cuts != null)
            {
                TailDemo_TailCutId cutIdComp = cuts.gameObject.GetComponent<TailDemo_TailCutId>();
                if (!cutIdComp) break;
                Destroy(cutIdComp);
                if (cuts.childCount > 0) cuts = cuts.GetChild(0); else break;
            }

            SegmentsCount = TailWithSettings.TailSegments.Count;
            dontReload = true;
        }
    }

    bool dontReload = false;

    public void OnValidate()
    {

        if (!Application.isPlaying) return;
        if (!Dynamic) return;
        if (!TailWithSettings) return;
        if (!TailWithSettings.IsInitialized) return;

        if (dontReload)
        {
            dontReload = false;
            return;
        }

        // Refreshing tail with new settings
        if (SegmentsCount != TailWithSettings.TailSegments.Count)
        {

            TailAnimator2.TailSegment refSegment = TailWithSettings.TailSegments[TailWithSettings.TailSegments.Count - 1];
            Transform iTr = TailWithSettings.TailSegments[TailWithSettings.TailSegments.Count - 1].transform;

            int toAdd = SegmentsCount - TailWithSettings.TailSegments.Count;

            for (int i = 0; i < toAdd; i++)
            {
                Vector3 targetPos = iTr.position + refSegment.ParentToFrontOffset();
                GameObject segment = Instantiate(SegmentModel);
                segment.transform.rotation = refSegment.transform.rotation;
                segment.transform.localScale = refSegment.transform.lossyScale;
                segment.transform.parent = transform;
                segment.transform.position = targetPos;


                TailWithSettings.User_AddTailTransform(segment.transform);

                iTr = segment.transform;

            }
        }
    }


    public void FotografEkle()
    {
        SegmentsCount++;
        if (SegmentsCount == 1)
        {
            IlkResimiEkle();
        }
        else if (SegmentsCount == 2)
        {
            IkinciResimiEkle();
        }
        else if (SegmentsCount > 2)
        {
            OnValidate();
        }

    }

    private void IlkResimiEkle()
    {
        ilkSegment = SegmentModel; //ikinci resim ekleme olayi biraz zorlu oldugu icin bole bir ekleme yapilmistir


        Vector3 targetPos = transform.position + transform.TransformVector(referenceOffset * (0.1f));
        GameObject segment = Instantiate(SegmentModel);
        segment.transform.rotation = transform.rotation;
        segment.transform.localScale = Vector3.up * transform.lossyScale.y / 1.5f + Vector3.right * transform.lossyScale.x / 40 + Vector3.forward * transform.lossyScale.x / 1.15f;// Vector3.up * transform.lossyScale.y / 1.5f + Vector3.right * transform.lossyScale.x / 20 + Vector3.forward * transform.lossyScale.x / 1.25f;     //transform.lossyScale;
        segment.transform.parent = transform;
        segment.transform.position = targetPos;

        hafizadakiResim = segment;
    }

    private void IkinciResimiEkle()
    {
        Destroy(hafizadakiResim);

        GetReferenceParameters();

        List<Transform> tailSegments = new List<Transform>();
        Transform preVSegment = transform;

        for (int i = 0; i < SegmentsCount; i++)
        {
            Vector3 targetPos = transform.position + transform.TransformVector(referenceOffset * (0.1f + i * 1.35f));  //sagdaki sayi ile 1. ve 2. resim arasindaki fark ayarlanabilir
            GameObject segment;

            if (i == 0)
            {
                segment = Instantiate(ilkSegment);

                segment.transform.rotation = transform.rotation;
                segment.transform.localScale = Vector3.up * transform.lossyScale.y / 1.5f + Vector3.right * transform.lossyScale.x / 40 + Vector3.forward * transform.lossyScale.x / 1.15f;// Vector3.up * transform.lossyScale.y / 1.5f + Vector3.right * transform.lossyScale.x / 20 + Vector3.forward * transform.lossyScale.x / 1.25f;     //transform.lossyScale;
                segment.transform.SetParent(preVSegment, true);
                segment.transform.parent = transform;
                segment.transform.position = targetPos;
                preVSegment = segment.transform;

                tailSegments.Add(segment.transform);
            }
            else if (i == 1)
            {
                segment = Instantiate(SegmentModel);

                segment.transform.rotation = transform.rotation;
                segment.transform.localScale = Vector3.up * transform.lossyScale.y / 1.5f + Vector3.right * transform.lossyScale.x / 40 + Vector3.forward * transform.lossyScale.x / 1.15f;// Vector3.up * transform.lossyScale.y / 1.5f + Vector3.right * transform.lossyScale.x / 20 + Vector3.forward * transform.lossyScale.x / 1.25f;     //transform.lossyScale;
                segment.transform.SetParent(preVSegment, true);
                segment.transform.parent = transform;
                segment.transform.position = targetPos;
                preVSegment = segment.transform;

                tailSegments.Add(segment.transform);
            }


        }

        if (TailWithSettings)
        {
            TailWithSettings.DetachChildren = DetachForOptimization;
            TailWithSettings.User_SetTailTransforms(tailSegments);
            TailWithSettings.enabled = true;
        }
        else
        {
            TailWithSettings = gameObject.AddComponent<TailAnimator2>();
            TailWithSettings.DetachChildren = DetachForOptimization;
            TailWithSettings.User_SetTailTransforms(tailSegments);
            TailWithSettings.enabled = true;
        }


    }


}
