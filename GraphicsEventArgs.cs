using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AssettoCorsaSharedMemory
{
    public enum AC_PENALTY_TYPE
    {
        ACC_None = 0,
        ACC_DriveThrough_Cutting = 1,
        ACC_StopAndGo_10_Cutting = 2,
        ACC_StopAndGo_20_Cutting = 3,
        ACC_StopAndGo_30_Cutting = 4,
        ACC_Disqualified_Cutting = 5,
        ACC_RemoveBestLaptime_Cutting = 6,
        ACC_DriveThrough_PitSpeeding = 7,
        ACC_StopAndGo_10_PitSpeeding = 8,
        ACC_StopAndGo_20_PitSpeeding = 9,
        ACC_StopAndGo_30_PitSpeeding = 10,
        ACC_Disqualified_PitSpeeding = 11,
        ACC_RemoveBestLaptime_PitSpeeding = 12,
        ACC_Disqualified_IgnoredMandatoryPit = 13,
        ACC_PostRaceTime = 14,
        ACC_Disqualified_Trolling = 15,
        ACC_Disqualified_PitEntry = 16,
        ACC_Disqualified_PitExit = 17,
        ACC_Disqualified_Wrongway = 18,
        ACC_DriveThrough_IgnoredDriverStint = 19,
        ACC_Disqualified_IgnoredDriverStint = 20,
        ACC_Disqualified_ExceededDriverStintLimit = 21,
    }

    public enum AC_FLAG_TYPE
    {
        AC_NO_FLAG = 0,
        AC_BLUE_FLAG = 1,
        AC_YELLOW_FLAG = 2,
        AC_BLACK_FLAG = 3,
        AC_WHITE_FLAG = 4,
        AC_CHECKERED_FLAG = 5,
        AC_PENALTY_FLAG = 6,
        AC_GREEN_FLAG = 7,
        AC_ORANGE_FLAG = 8,
    }

    public enum AC_STATUS
    {
        AC_OFF = 0,
        AC_REPLAY = 1,
        AC_LIVE = 2,
        AC_PAUSE = 3,
    }

    public enum AC_SESSION_TYPE
    {
        AC_UNKNOWN = -1,
        AC_PRACTICE = 0,
        AC_QUALIFY = 1,
        AC_RACE = 2,
        AC_HOTLAP = 3,
        AC_TIME_ATTACK = 4,
        AC_DRIFT = 5,
        AC_DRAG = 6,
    }

    public enum AC_WHEELS_TYPE
    {
        AC_FrontLeft = 0,
        AC_FrontRight = 1,
        AC_RearLeft = 2,
        AC_RearRight = 3,
    }

    public enum AC_TRACK_GRIP_STATUS
    {
        AC_GREEN = 0,
        AC_FAST = 1,
        AC_OPTIMUM = 2,
        AC_GREASY = 3,
        AC_DEMP = 4,
        AC_WET = 5,
        AC_FLOODED = 6,
    }

    public enum AC_RAIN_INTENSITY
    {
        AC_NO_RAIN = 0,
        AC_DRIZZLE = 1,
        AC_LIGHT_RAIN = 2,
        AC_MEDIUM_RAIN = 3,
        AC_HEAVY_RAIN = 4,
        AC_THUNDERSTORM = 5,
    }

    public class GraphicsEventArgs : EventArgs
    {
        public GraphicsEventArgs(Graphics graphics)
        {
            this.Graphics = graphics;
        }

        public Graphics Graphics { get; private set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode)]
    [Serializable]
    public struct Graphics
    {
        public int PacketId;
        public AC_STATUS Status;
        public AC_SESSION_TYPE Session;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
        public string CurrentTime;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
        public string LastTime;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
        public string BestTime;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
        public string Split;
        public int CompletedLaps;
        public int Position;
        public int ICurrentTime;
        public int ILastTime;
        public int IBestTime;
        public float SessionTimeLeft;
        public float DistanceTraveled;
        public int IsInPit;
        public int CurrentSectorIndex;
        public int LastSectorTime;
        public int NumberOfLaps;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string TyreCompound;

        /// <summary>
        /// Not used in ACC.
        /// </summary>
        public float ReplayTimeMultiplier;
        public float NormalizedCarPosition;

        // Added MR
        public int ActiveCars;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 180)]
        public float[] CarCoordinates;

        // Added MR
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]

        // Added MR
        public int[] CarID;

        // Added MR
        public int PlayerCarID;
        public float PenaltyTime;
        public AC_FLAG_TYPE Flag;

        // Added MR
        public AC_PENALTY_TYPE Penalty;

        public int IdealLineOn;

        // since 1.5
        public int IsInPitLane;
        public float SurfaceGrip;

        // since 1.13
        public int MandatoryPitDone;

        // Added MR
        public float WindSpeed;
        public float WindDirection;
        public int IsSetupMenuVisible;
        public int MainDisplayIndex;
        public int SecondaryDisplayIndex;
        public int TC;
        public int TCCUT;
        public int EngineMap;
        public int ABS;
        public float FuelXLap;
        public int RainLights;
        public int FlashingLights;
        public int LightsStage;
        public float ExhaustTemperature;
        public int WhiperLV;
        public int DriverStintTotalTimeLeft;
        public int DriverStintTimeLeft;
        public int RainTyres;
        public int SessionIndex;
        public float UsedFuel;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
        public string DeltaLapTime;
        public int IDeltaLapTime;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
        public string EstimatedLapTime;
        public int IEstimatedLapTime;
        public int IsDeltaPositive;
        public int IsSplit;
        public int IsValidLap;
        public float FuelEstimatedLaps;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string TrackStatus;
        public int MissingMandatoryPits;
        public float Clock;
        public int DirectionLightsLeft;
        public int DirectionLightsRight;
        public int GlobalYellow;
        public int GlobalYellow1;
        public int GlobalYellow2;
        public int GlobalYellow3;
        public int GlobalGreen;
        public int GlobalChequered;
        public int GlobalRed;
        public int MfdTyreSet;
        public float MfdFuelToAdd;
        public float MfdTyrePressureLF;
        public float MfdTyrePressureRF;
        public float MfdTyrePressureLR;
        public float MfdTyrePressureRR;
        public AC_TRACK_GRIP_STATUS TrackGripStatus;
        public AC_RAIN_INTENSITY RainIntensity;
        public AC_RAIN_INTENSITY RainIntensityIn10min;
        public AC_RAIN_INTENSITY RainIntensityIn30min;
        public int CurrentTyreSet;
        public int StrategyTyreSet;
    }
}
