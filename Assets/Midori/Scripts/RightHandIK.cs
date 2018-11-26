using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 右手のIK
namespace Invector.CharacterController
{
    public class RightHandIK : MonoBehaviour
    {
        //　右手位置用のTransform
        [SerializeField]
        private Transform rightHandTransform;
        private Animator animator;

        // 右手の重み
        [SerializeField]
        private float RightHandUpWeight = 1.0f;

        // スピード
        [SerializeField]
        private float RightHandUpSpeed = 0.01f;

        // 親
        private GameObject parentPlayer;

        void Start()
        {
            // アニメーターの取得
            animator = GetComponent<Animator>();

            // 親(プレイヤー)の取得
            parentPlayer = transform.root.gameObject;
        }

        private void Update()
        {
            // 放射中かどうかの確認
            bool IsSplash = parentPlayer.GetComponent<vThirdPersonController>().GetIsSplashing;

            // 放射中
            if (IsSplash == true)
            {
                // 手を徐々に上げる
                RightHandUpWeight += RightHandUpSpeed;
            }
            else
            {
                // 手を徐々に下げる
                RightHandUpWeight -= RightHandUpSpeed;
            }

            // ウェイトを0～1に設定
            RightHandUpWeight = Mathf.Max(0.0f, RightHandUpWeight);
            RightHandUpWeight = Mathf.Min(1.0f, RightHandUpWeight);
        }

        void OnAnimatorIK()
        {
            //　左手、右手のIK設定, 重み
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, RightHandUpWeight);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, RightHandUpWeight);

            // 位置、回転
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTransform.transform.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTransform.transform.rotation);
        }
    }
}