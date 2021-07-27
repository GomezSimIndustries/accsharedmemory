using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AssettoCorsaSharedMemory
{
    public class StaticInfoEventArgs : EventArgs
    {
        public StaticInfoEventArgs (StaticInfo staticInfo)
        {
            this.StaticInfo = staticInfo;
        }

        public StaticInfo StaticInfo { get; private set; }
    }

    [StructLayout (LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode)]
    [Serializable]
    public struct StaticInfo
    {
        [MarshalAs (UnmanagedType.ByValTStr, SizeConst = 15)]
        public String SMVersion;
        [MarshalAs (UnmanagedType.ByValTStr, SizeConst = 15)]
        public String ACVersion;

        // session static info
        public int NumberOfSessions;
        public int NumCars;
        [MarshalAs (UnmanagedType.ByValTStr, SizeConst = 33)]
        public String CarModel;
        [MarshalAs (UnmanagedType.ByValTStr, SizeConst = 33)]
        public String Track;
        [MarshalAs (UnmanagedType.ByValTStr, SizeConst = 33)]
        public String PlayerName;
        [MarshalAs (UnmanagedType.ByValTStr, SizeConst = 33)]
        public String PlayerSurname;
        [MarshalAs (UnmanagedType.ByValTStr, SizeConst = 33)]
        public String PlayerNick;

        public int SectorCount;

        // car static info
        /// <summary>
        /// Not used in ACC
        /// </summary>
        public float MaxTorque;
        /// <summary>
        /// Not used in ACC
        /// </summary>
        public float MaxPower;
        public int MaxRpm;
        public float MaxFuel;
        /// <summary>
        /// Not used in ACC
        /// </summary>
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] SuspensionMaxTravel;
        /// <summary>
        /// Not used in ACC
        /// </summary>
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] TyreRadius;

        // since 1.5
        /// <summary>
        /// Not used in ACC
        /// </summary>
        public float MaxTurboBoost;
        /// <summary>
        /// Not used in ACC
        /// </summary>
        public float Deprecated1; // AirTemp since 1.6 in physic
        /// <summary>
        /// Not used in ACC
        /// </summary>
        public float Deprecated2; // RoadTemp since 1.6 in physic
        public int PenaltiesEnabled;
        public float AidFuelRate;
        public float AidTireRate;
        public float AidMechanicalDamage;
        public float AidAllowTyreBlankets;
        public float AidStability;
        public int AidAutoClutch;
        public int AidAutoBlip;

        // since 1.7.1
        /// <summary>
        /// Not used in ACC
        /// </summary>
        public int HasDRS;
        /// <summary>
        /// Not used in ACC
        /// </summary>
        public int HasERS;
        /// <summary>
        /// Not used in ACC
        /// </summary>
        public int HasKERS;
        /// <summary>
        /// Not used in ACC
        /// </summary>
        public float KersMaxJoules;
        /// <summary>
        /// Not used in ACC
        /// </summary>
        public int EngineBrakeSettingsCount;
        /// <summary>
        /// Not used in ACC
        /// </summary>
        public int ErsPowerControllerCount;

        // since 1.7.2
        /// <summary>
        /// Not used in ACC
        /// </summary>
        public float TrackSPlineLength;
        /// <summary>
        /// Not used in ACC
        /// </summary>
        [MarshalAs (UnmanagedType.ByValTStr, SizeConst = 33)]
        public string TrackConfiguration;

        // since 1.10.2
        /// <summary>
        /// Not used in ACC
        /// </summary>
        public float ErsMaxJ;

        // since 1.13
        /// <summary>
        /// Not used in ACC
        /// </summary>
        public int IsTimedRace;
        /// <summary>
        /// Not used in ACC
        /// </summary>
        public int HasExtraLap;
        /// <summary>
        /// Not used in ACC
        /// </summary>
        [MarshalAs (UnmanagedType.ByValTStr, SizeConst = 33)]
        public String CarSkin;
        /// <summary>
        /// Not used in ACC
        /// </summary>
        public int ReversedGridPositions;
        public int PitWindowStart;
        public int PitWindowEnd;
        public int IsOnline;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public String DryTyresName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public String WetTyresName;
    }
}
