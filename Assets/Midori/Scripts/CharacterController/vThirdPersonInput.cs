using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace Invector.CharacterController
{
    public class vThirdPersonInput : MonoBehaviour
    {
        #region variables

        [Header("Default Inputs")]
        public string horizontalInput = "Horizontal";
        public string verticallInput = "Vertical";
        public string horizontalInput2 = "Horizontal2";
        public string verticallInput2 = "Vertical2";
        public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;
        public KeyCode splashInput = KeyCode.L;
        public KeyCode spreadInput = KeyCode.K;

        [Header("Camera Settings")]
        //public string rotateCameraXInput ="Mouse X";
        //public string rotateCameraYInput = "Mouse Y";

        //protected vThirdPersonCamera tpCamera;                // acess camera info        
        [HideInInspector]
        public string customCameraState;                    // generic string to change the CameraState        
        [HideInInspector]
        public string customlookAtPoint;                    // generic string to change the CameraPoint of the Fixed Point Mode        
        [HideInInspector]
        public bool changeCameraState;                      // generic bool to change the CameraState        
        [HideInInspector]
        public bool smoothCameraState;                      // generic bool to know if the state will change with or without lerp  
        [HideInInspector]
        public bool keepDirection;                          // keep the current direction in case you change the cameraState

        protected vThirdPersonController cc;                // access the ThirdPersonController component                

        private Joycon Joycon1_Left = null;
        private Joycon Joycon1_Right = null;
        private Joycon Joycon2_Left = null;
        private Joycon Joycon2_Right = null;
        public int m_PlayerIdx;
        private int JoycoCount;

        #endregion

        protected virtual void Start()
        {
            JoycoCount = JoyconManager.Instance.JoyconList.Count;

            // 一本繋がってる
            if (JoycoCount == 2)
            {
                Joycon1_Left = JoyconManager.Instance.JoyconList[0];
                Joycon1_Right = JoyconManager.Instance.JoyconList[1];
            }

            // 二本繋がっている
            else if (JoycoCount == 4)
            {
                Joycon1_Left = JoyconManager.Instance.JoyconList[0];
                Joycon1_Right = JoyconManager.Instance.JoyconList[1];
                Joycon2_Left = JoyconManager.Instance.JoyconList[2];
                Joycon2_Right = JoyconManager.Instance.JoyconList[3];
            }

            CharacterInit();
        }

        protected virtual void CharacterInit()
        {
            cc = GetComponent<vThirdPersonController>();
            if (cc != null)
                cc.Init();

            //tpCamera = FindObjectOfType<vThirdPersonCamera>();
            //if (tpCamera) tpCamera.SetMainTarget(this.transform);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

        protected virtual void LateUpdate()
        {
            if (cc == null) return;             // returns if didn't find the controller		    
            InputHandle();                      // update input methods
            UpdateCameraStates();               // update camera states
        }

        protected virtual void FixedUpdate()
        {
            cc.AirControl();
            CameraInput();
        }

        protected virtual void Update()
        {
            cc.UpdateMotor();                   // call ThirdPersonMotor methods               
            cc.UpdateAnimator();                // call ThirdPersonAnimator methods		               
        }

        protected virtual void InputHandle()
        {
            ExitGameInput();
            CameraInput();

            if (!cc.lockMovement)
            {
                MoveCharacter();
                //SprintInput();
                //StrafeInput();
                JumpInput();
                SplashInput();
                SpreadInput();
            }
        }

        #region Basic Locomotion Inputs      

        protected virtual void MoveCharacter()
        {

            if (transform.tag == "Player1" && JoycoCount > 0)
            {
                cc.input.x = JoyconManager.Instance.GetComponent<JoyconInput>().GetStick(0, true).x;
                cc.input.y = JoyconManager.Instance.GetComponent<JoyconInput>().GetStick(0, true).y;

                //cc.input.x = Input.GetAxis("Axis 10");
                //cc.input.y = Input.GetAxis("Axis 9");
                //cc.input.x = Joycon1_Left.GetVector().x;
                //cc.input.y = Joycon1_Left.GetVector().z;

                //if (Input.GetAxis("Axis 10") != 0 || Input.GetAxis("Axis 9") != 0)
                //{
                //    cc.input.x = Input.GetAxis("Axis 10");
                //    cc.input.y = Input.GetAxis("Axis 9");
                //}
                //else if (Input.GetAxis(horizontalInput) != 0 || Input.GetAxis(verticallInput) != 0)
                //{
                //    cc.input.x = Input.GetAxis(horizontalInput);
                //    cc.input.y = Input.GetAxis(verticallInput);
                //}
            }
            else if (transform.tag == "Player2" && JoycoCount == 4)
            {
                cc.input.x = JoyconManager.Instance.GetComponent<JoyconInput>().GetStick(2, true).x;
                cc.input.y = JoyconManager.Instance.GetComponent<JoyconInput>().GetStick(2, true).y;

                //cc.input.x = Input.GetAxis("Axis 10");
                //cc.input.y = Input.GetAxis("Axis 9");

                //if (Input.GetAxis("Axis 10") != 0 || Input.GetAxis("Axis 9") != 0)
                //{
                //    cc.input.x = Input.GetAxis("Axis 10");
                //    cc.input.y = Input.GetAxis("Axis 9");
                //}
                //else if (Input.GetAxis(horizontalInput2) != 0 || Input.GetAxis(verticallInput2) != 0)
                //{
                //    cc.input.x = Input.GetAxis(horizontalInput2);
                //    cc.input.y = Input.GetAxis(verticallInput2);
                //}
            }
            else
            {
                cc.input.x = Input.GetAxis(horizontalInput);
                cc.input.y = Input.GetAxis(verticallInput);
            }
        }

        //protected virtual void StrafeInput()
        //{
        //    if (Input.GetKeyDown(strafeInput))
        //        cc.Strafe();
        //}

        //protected virtual void SprintInput()
        //{
        //    if (Input.GetKeyDown(sprintInput))
        //        cc.Sprint(true);
        //    else if(Input.GetKeyUp(sprintInput))
        //        cc.Sprint(false);
        //}

        protected virtual void JumpInput()
        {
            if (Input.GetKeyDown(jumpInput))
                cc.Jump();
        }

        protected virtual void SplashInput()
        {
            if (transform.tag == "Player1" && JoycoCount > 0)
            {
                if (Joycon1_Right.GetButtonDown(Joycon.Button.DPAD_RIGHT) || Input.GetKeyDown(splashInput))
                {
                    cc.Splash(true);
                }
                else if (Joycon1_Right.GetButtonUp(Joycon.Button.DPAD_RIGHT) || Input.GetKeyUp(splashInput))
                {
                    cc.Splash(false);
                }

                //if (Input.GetKeyDown(splashInput) || Input.GetKeyDown(KeyCode.JoystickButton0))
                //    cc.Splash(true);
                //else if (Input.GetKeyUp(splashInput) || Input.GetKeyUp(KeyCode.JoystickButton0))
                //    cc.Splash(false);

            }
            else if (transform.tag == "Player2" && JoycoCount == 4)
            {
                if (Joycon2_Right.GetButtonDown(Joycon.Button.DPAD_RIGHT))
                {
                    cc.Splash(true);
                }
                else if (Joycon2_Right.GetButtonUp(Joycon.Button.DPAD_RIGHT))
                {
                    cc.Splash(false);
                }
            }
            else
            {
                if (Input.GetKeyDown(splashInput))
                {
                    cc.Splash(true);
                }
                else if (Input.GetKeyUp(splashInput))
                {
                    cc.Splash(false);
                }
            }
            //if (m_Input.GetComponent<JoyconInput>().GetTrigger(Joycon.Button.DPAD_RIGHT, m_PlayerIdx, false))
            //{
            //    Debug.Log("s");
            //}
        }

        protected virtual void SpreadInput()
        {
            if (transform.tag == "Player1" && JoycoCount > 0)
            {
                if (Joycon1_Right.GetButtonDown(Joycon.Button.DPAD_UP) || Input.GetKeyDown(spreadInput))
                {
                    cc.Spread(true);
                }
                else if (Joycon1_Right.GetButtonUp(Joycon.Button.DPAD_UP) || Input.GetKeyUp(spreadInput))
                {
                    cc.Spread(false);
                }

                //if (Input.GetKeyDown(splashInput) || Input.GetKeyDown(KeyCode.JoystickButton0))
                //    cc.Splash(true);
                //else if (Input.GetKeyUp(splashInput) || Input.GetKeyUp(KeyCode.JoystickButton0))
                //    cc.Splash(false);

            }
            else if (transform.tag == "Player2" && JoycoCount == 4)
            {
                if (Joycon2_Right.GetButtonDown(Joycon.Button.DPAD_UP))
                {
                    cc.Spread(true);
                }
                else if (Joycon2_Right.GetButtonUp(Joycon.Button.DPAD_UP))
                {
                    cc.Spread(false);
                }
            }
            else
            {
                if (Input.GetKeyDown(spreadInput))
                {
                    cc.Spread(true);
                }
                else if (Input.GetKeyUp(spreadInput))
                {
                    cc.Spread(false);
                }
            }
            //if (m_Input.GetComponent<JoyconInput>().GetTrigger(Joycon.Button.DPAD_RIGHT, m_PlayerIdx, false))
            //{
            //    Debug.Log("s");
            //}
        }

        protected virtual void ExitGameInput()
        {
            // just a example to quit the application 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!Cursor.visible)
                    Cursor.visible = true;
                else
                    Application.Quit();
            }
        }

        #endregion

        #region Camera Methods

        protected virtual void CameraInput()
        {
            //if (tpCamera == null)
            //    return;
            var Y = Input.GetAxis("Vertical");
            var X = Input.GetAxis("Horizontal");

            //tpCamera.RotateCamera(X, Y);

            // tranform Character direction from camera if not KeepDirection
            //if (!keepDirection)
            cc.UpdateTargetDirection(Camera.main != null ? Camera.main.transform : null);
            // rotate the character with the camera while strafing        
            RotateWithCamera(Camera.main.transform != null ? Camera.main.transform : null);            
        }

        protected virtual void UpdateCameraStates()
        {
            // CAMERA STATE - you can change the CameraState here, the bool means if you want lerp of not, make sure to use the same CameraState String that you named on TPCameraListData
            //if (tpCamera == null)
            //{
            //    tpCamera = FindObjectOfType<vThirdPersonCamera>();
            //    if (tpCamera == null)
            //        return;
            //    if (tpCamera)
            //    {
            //        tpCamera.SetMainTarget(this.transform);
            //        tpCamera.Init();
            //    }
            //}            
        }

        protected virtual void RotateWithCamera(Transform cameraTransform)
        {
            if (cc.isStrafing && !cc.lockMovement && !cc.lockMovement)
            {                
                cc.RotateWithAnotherTransform(cameraTransform);                
            }
        }

        #endregion     
    }
}