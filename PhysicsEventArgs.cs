using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AssettoCorsaSharedMemory
{
    public class PhysicsEventArgs : EventArgs
    {
        public PhysicsEventArgs(Physics physics)
        {
            this.Physics = physics;
        }

        public string Gear(int gear)
        {
            if (gear > 1)
            {
                return (gear - 1).ToString();
            }

            if (gear == 0)
            {
                return "R";
            }

            return "N";
        }

        public Physics Physics { get; private set; }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Coordinates
    {
        public float X;
        public float Y;
        public float Z;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode)]
    [Serializable]
    public struct Physics
    {
        public int PacketId;
        public float Gas;
        public float Brake;
        public float Fuel;
        public int Gear;
        public int Rpms;
        public float SteerAngle;
        public float SpeedKmh;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] Velocity;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] AccG;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] WheelSlip;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] WheelLoad;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] WheelPressure;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] WheelAngularSpeed;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] TyreWear;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] TyreDirtyLevel;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] TyreCoreTemp;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] CamberRad;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] SuspensionTravel;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public float Drs;
        public float TC;
        public float Heading;
        public float Pitch;
        public float Roll;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public float CgHeight;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public float[] CarDamage;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public int NumberOfTyresOut;
        public int PitLimiterOn;
        public float Abs;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public float KersCharge;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public float KersInput;
        public int AutoShifterOn;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public float[] RideHeight;

        // since 1.5
        public float TurboBoost;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public float Ballast;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public float AirDensity;

        // since 1.6
        public float AirTemp;
        public float RoadTemp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] LocalAngularVelocity;
        public float FinalFF;

        // since 1.7

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public float PerformanceMeter;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public int EngineBrake;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public int ErsRecoveryLevel;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public int ErsPowerLevel;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public int ErsHeatCharging;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public int ErsisCharging;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public float KersCurrentKJ;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public int DrsAvailable;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public int DrsEnabled;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] BrakeTemp;

        // since 1.10
        public float Clutch;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] TyreTempI;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] TyreTempM;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] TyreTempO;

        // since 1.10.2
        public int IsAIControlled;

        // since 1.11
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        [NonSerialized]
        public Coordinates[] TyreContactPoint;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        [NonSerialized]
        public Coordinates[] TyreContactNormal;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        [NonSerialized]
        public Coordinates[] TyreContactHeading;
        public float BrakeBias;

        // since 1.12
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] LocalVelocity;

        // Added MR

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public int P2PActivation;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public int P2PStatus;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public float CurrentMaxRpm;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] MZ;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] FX;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] FY;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] SlipRatio;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] SlipAngle;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public int TCInAction;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public int ABSInAction;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] SuspensionDamage;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] TyreTemp;
        public float WaterTemp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] BreakPressure;
        public int FrontBreakCompound;
        public int RearBreakCompount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] PadLife;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] DiscLife;
        public int IgnitionOn;
        public int StarterEngineOn;
        public int IsEngineRunning;
        public float KerbVibration;
        public float SlipVibrations;
        public float GBibrations;
        public float ABSVibrations;
    }
}
