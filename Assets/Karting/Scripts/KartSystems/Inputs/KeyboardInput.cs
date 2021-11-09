// using UnityEngine;
// using EasyJoystick;

// namespace KartGame.KartSystems {

//     public class KeyboardInput : BaseInput
//     {
//         public Joystick joystick;

//         public string TurnInputName = "Horizontal";
//         public string AccelerateButtonName = "Accelerate";
//         public string BrakeButtonName = "Brake";

//         public override InputData GenerateInput() { 
//             return new InputData
//             {
//                 Accelerate = joystick.Vertical() > 0 ? true : false,
//                 Brake = joystick.Vertical() < 0 ? true : false,
//                 TurnInput = joystick.Horizontal()
//                 // Accelerate = Input.GetButton(AccelerateButtonName),
//                 // Brake = Input.GetButton(BrakeButtonName),
//                 // TurnInput = Input.GetAxis(TurnInputName)
//             };
//         }
//     }
// }   

using UnityEngine;

namespace KartGame.KartSystems {

    public class KeyboardInput : BaseInput
    {
        public string TurnInputName = "Horizontal";
        public string AccelerateButtonName = "Accelerate";
        public string BrakeButtonName = "Brake";
        private float TurnFactor = 2f;

        public override InputData GenerateInput() {
            if (Application.platform != RuntimePlatform.Android)
            {
                return new InputData
                {
                    Accelerate = Input.GetButton(AccelerateButtonName),
                    Brake = Input.GetButton(BrakeButtonName),
                    TurnInput = Input.GetAxis("Horizontal")
                };
            } else
            {
                bool _acc = (Input.GetTouch(0).position.x > Screen.width / 2);
                return new InputData
                {
                    Accelerate = _acc,
                    Brake = !_acc,
                    TurnInput = Mathf.Clamp(TurnFactor * Input.acceleration.normalized.x, -1f, 1f),
                };
            }
        }
    }
}