using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gondora : MonoBehaviour
{
    public GameObject m_anchor;        //圆点
    public float g = 9.8f;            //重力加速度
    private Vector3 m_rotateAxis;    //旋转轴
    private float w = 0;                //角速度
    public GameObject ship;
    // Use this for initialization
    void Start()
    {
        //求出旋转轴
        m_rotateAxis = Vector3.Cross(ship.transform.position - m_anchor.transform.position, Vector3.right);
    }
    void DoPhysics()
    {
        float r = Vector3.Distance(m_anchor.transform.position, ship.transform.position);
        float l = Vector3.Distance(new Vector3(m_anchor.transform.position.x, ship.transform.position.y, m_anchor.transform.position.z), ship.transform.position);
        //当钟摆摆动到另外一侧时，l为负，则角加速度alpha为负。
        Vector3 axis = Vector3.Cross(ship.transform.position - m_anchor.transform.position, Vector3.down);
        if (Vector3.Dot(axis, m_rotateAxis) < 0)
        {
            l = -l;
        }
        float cosalpha = l / r;
        //求角加速度
        float alpha = (cosalpha * g) / r;
        //累计角速度
        w += alpha * Time.deltaTime;
        //求角位移(乘以180/PI 是为了将弧度转换为角度)
        float thelta = w * Time.deltaTime * 180.0f / Mathf.PI;
        //绕圆点m_ahchor的旋转轴m_rotateAxis旋转thelta角度
        ship.transform.RotateAround(m_anchor.transform.position, m_rotateAxis, thelta);
    }
    // Update is called once per frame
    void Update()
    {
        DoPhysics();
    }
}
