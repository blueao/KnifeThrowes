using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MainGameController : MonoBehaviour
{
    [SerializeField]
    public Knife knifeObject;
    float degreeKnifeZ;
    private Transform spriteKnife;
    bool isfly;
    void Start()
    {
        spriteKnife = knifeObject.spriteKnife.GetComponent<Transform>();
        spriteKnife.localPosition = new Vector3(spriteKnife.localPosition.x + knifeObject.spriteKnife.bounds.size.y, spriteKnife.localPosition.y, spriteKnife.localPosition.z);
    }
    void Initialized()
    {

    }
    private void FixedUpdate()
    {
        DragPosition();
        calculatorRotateKnife();
        calculatorRotateChildKnife();
    }
    public void calculatorRotateChildKnife()
    {
        if (knifeObject.isFly)
        {
            if (spriteKnife.localRotation.z == 0)
            {

                spriteKnife.DOLocalRotate(new Vector3(0, 0, -180), 0.2f).OnComplete(() =>
                {
                    isfly = true;
                });
            }
            if (isfly)
            {
                knifeObject.RBknife.isKinematic = false;
                knifeObject.RBknife.AddTorque(-0.4f,ForceMode2D.Force);
                spriteKnife.localPosition += Vector3.right * Time.fixedDeltaTime * 15f;
            }
        }
    }
    public void calculatorRotateKnife()
    {
            var pos = Input.mousePosition;
            pos.z = 10;
        if (knifeObject.isIdie)
        {
            Vector2 mouseCamera = Camera.main.ScreenToWorldPoint(pos) - knifeObject.knifeTransfom.position;
            float tan = Mathf.Atan2(mouseCamera.y, mouseCamera.x) * Mathf.Rad2Deg;
            if (tan > 0 && tan < 60f)
            {
                tan = Mathf.Clamp(tan, 1, 60f);
            }
            else if (tan <= 0 && tan > -60)
            {
                tan = Mathf.Clamp(tan, -60, 0);
            }
            else
            {
                if (tan > 60)
                {
                    tan = 60;
                }
                else if (tan < -60)
                {
                    tan = 300;
                }
            }
            knifeObject.knifeTransfom.rotation = Quaternion.Euler(new Vector3(knifeObject.knifeTransfom.rotation.x, knifeObject.knifeTransfom.rotation.y, tan));
        }
    }
    public void calculorDrag()
    {
        knifeObject.Fly();
    }

    public void DragPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            knifeObject.isDrag = true;
        }
        if (Input.GetMouseButtonUp(0) && knifeObject.isDrag)
        {
            knifeObject.isDrag = false;
            calculorDrag();
        }
    }

}
