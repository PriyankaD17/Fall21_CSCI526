using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartGame.KartSystems
{

    public class Vibration : MonoBehaviour
    {
        private static bool vibrationSupported = false;
        private static AndroidJavaObject vibrator;
        private static AndroidJavaClass vibrationEffectClass;
        private static int DEFAULT_AMPLITUDE;

        // Initialization before scene loads
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialise()
        {
            if (Application.platform == RuntimePlatform.Android &&
                    (new AndroidJavaClass("android.os.Build$VERSION")).GetStatic<int>("SDK_INT") >= 26)
                //vibrationSupported = true;
            	return;
            else return;

            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
            vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
            DEFAULT_AMPLITUDE = vibrationEffectClass.GetStatic<int>("DEFAULT_AMPLITUDE");
            Handheld.Vibrate();
        }

        private static void SystemVibrate(string android_function, params object[] args)
        {
            if (!vibrationSupported)
                return;

            AndroidJavaObject vibrationEffect = vibrationEffectClass.CallStatic<AndroidJavaObject>(android_function, args);
            vibrator.Call("vibrate", vibrationEffect);
        }

        public static void Vibrate(long duration, int amplitude=0)
        {
            if (!vibrationSupported)
                return;

            if (amplitude == 0)
                amplitude = DEFAULT_AMPLITUDE;
            SystemVibrate("createOneShot", new object[] { duration, amplitude });
        }

        public static void Cancel()
        {
            if (!vibrationSupported)
                return;

            vibrator.Call("cancel");
        }
    }
}