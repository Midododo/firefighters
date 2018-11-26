using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour {

    public float m_DampTime = 0.2f;                 // カメラが再度フォーカスするのに必要なおおよその時間
    public float m_ScreenEdgeBuffer = 4f;           // 最上/最下のターゲットと画面端の間のスペース
    public float m_MinSize = 6.5f;                  // カメラに可能な最小の平行投影サイズ
    /*[HideInInspector]*/ public Transform[] m_Targets; // カメラが含む必要のあるすべてのターゲット

    private int Camera_Num = 2;

    private Camera m_Camera;                        // カメラの参照に使用
    private float m_ZoomSpeed;                      // 平行投影サイズを緩やかに縮小するための参照速度
    private Vector3 m_MoveVelocity;                 // 緩やかに移動するための参照速度
    private Vector3 m_DesiredPosition;              // カメラが移動する目的位置


    private void Awake()
    {
        m_Targets = new Transform[Camera_Num];

        m_Camera = GetComponentInChildren<Camera>();
    }

    public void SetCamera(GameObject Player, int i)
    {
        m_Targets[i] = Player.transform;
    }

    private void FixedUpdate()
    {
        m_Targets[0] = GameObject.FindWithTag("Player1").transform;
        m_Targets[1] = GameObject.FindWithTag("Player2").transform;

        //任意の位置にカメラを移動
        Move();

        // カメラを基本のサイズに変更
        Zoom();
    }


    private void Move()
    {
        // ターゲットの中間点を見つけます
        FindAveragePosition();

        // 緩やかにその位置に移動
        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }


    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;

        // すべてのターゲットをチェックし、それらの位置を合計します
        for (int i = 0; i < m_Targets.Length; i++)
        {
            // そのターゲットがアクティブでない場合は、その次をチェックします
            if (!m_Targets[i].gameObject.activeSelf)
                continue;

            //  averagePos に位置の値を追加して、ターゲットの数も増加します。
            averagePos += m_Targets[i].position;
            numTargets++;
        }

        //　ターゲットの数が0でなければ、位置の値の合計をターゲット数で割り、中間点を求めます。
        if (numTargets > 0)
            averagePos /= numTargets;

        // 　y 値でも同じことを行います。
        averagePos.y = transform.position.y;

        // 　DesiredPosition （目的位置）を中間点にします。
        m_DesiredPosition = averagePos;
    }


    private void Zoom()
    {
        // DesiredPosition （目的位置）に基づいて必用サイズを求め、緩やかにサイズを変更します。
        float requiredSize = FindRequiredSize();
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
    }


    private float FindRequiredSize()
    {
        // ローカル空間でカメラリグが移動する位置を見つけます。
        Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);

        // カメラのサイズの計算は０から開始します。
        float size = 0f;

        // すべてのターゲットをチェックします...
        for (int i = 0; i < m_Targets.Length; i++)
        {
            // ... そして、ターゲットがアクティブでないならば、次のターゲットをチェックします
            if (!m_Targets[i].gameObject.activeSelf)
                continue;

            // アクティブなら、カメラのローカル空間のターゲットの位置を見つけます。
            Vector3 targetLocalPos = transform.InverseTransformPoint(m_Targets[i].position);

            // カメラのローカル空間の desired position （目的位置）からのターゲット位置を求めます。
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            // 現在のサイズとカメラからタンクまでの距離 (上下) を比べ、大きい方を選びます。
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            // 現在のサイズとカメラからタンクまでの計算された距離 (左右) を比べ、大きい方を選びます。
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
        }

        //サイズにエッジバッファを加えます
        size += m_ScreenEdgeBuffer;

        // カメラサイズが最小値より小さくならないように注意します
        size = Mathf.Max(size, m_MinSize);

        return size;
    }


    public void SetStartPositionAndSize()
    {
        // 目的位置を求めます
        FindAveragePosition();

        // カメラ位置を DesiredPosition (目的位置) に設定 (緩やかな移動ではないので damp を使用しません)
        transform.position = m_DesiredPosition;

        // カメラの必要サイズを見つけ、設定します
        m_Camera.orthographicSize = FindRequiredSize();
    }
}
