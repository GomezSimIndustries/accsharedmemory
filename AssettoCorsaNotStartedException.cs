using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Timers;

namespace AssettoCorsaSharedMemory
{
    public delegate void PhysicsUpdatedHandler(object sender, PhysicsEventArgs e);

    public delegate void GraphicsUpdatedHandler(object sender, GraphicsEventArgs e);

    public delegate void StaticInfoUpdatedHandler(object sender, StaticInfoEventArgs e);

    public delegate void GameStatusChangedHandler(object sender, GameStatusEventArgs e);

    public delegate void PitStatusChangedHandler(object sender, PitStatusEventArgs e);

    public delegate void SessionTypeChangedHandler(object sender, SessionTypeEventArgs e);

    [Serializable]
    public class AssettoCorsaNotStartedException : Exception
    {
        public AssettoCorsaNotStartedException()
            : base("Shared Memory not connected, is Assetto Corsa running and have you run assettoCorsa.Start()?")
        {
        }

        protected AssettoCorsaNotStartedException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }

    internal enum AC_MEMORY_STATUS
    {
        DISCONNECTED,
        CONNECTING,
        CONNECTED,
    }

#pragma warning disable CA1001 // Types that own disposable fields should be disposable
    public class AssettoCorsa
#pragma warning restore CA1001 // Types that own disposable fields should be disposable
    {
        private Timer _sharedMemoryRetryTimer;
        private AC_MEMORY_STATUS _memoryStatus = AC_MEMORY_STATUS.DISCONNECTED;

        public bool IsRunning
        {
            get
            {
                return _memoryStatus == AC_MEMORY_STATUS.CONNECTED;
            }
        }

        private AC_STATUS _gameStatus = AC_STATUS.AC_OFF;
        private int _pitStatus = 1;
        private AC_SESSION_TYPE _sessionType = AC_SESSION_TYPE.AC_UNKNOWN;

        public event GameStatusChangedHandler GameStatusChanged;

        public virtual void OnGameStatusChanged(GameStatusEventArgs e)
        {
            if (GameStatusChanged != null)
            {
                GameStatusChanged(this, e);
            }
        }

        public event PitStatusChangedHandler PitStatusChanged;

        public virtual void OnPitStatusChanged(PitStatusEventArgs e)
        {
            if (PitStatusChanged != null)
            {
                PitStatusChanged(this, e);
            }
        }

        public event SessionTypeChangedHandler SessionTypeChanged;

        public virtual void OnSessionTypeChangedHandler(PitStatusEventArgs e)
        {
            if (PitStatusChanged != null)
            {
                PitStatusChanged(this, e);
            }
        }

        public static readonly Dictionary<AC_STATUS, string> StatusNameLookup = new Dictionary<AC_STATUS, string>
        {
            { AC_STATUS.AC_OFF, "Off" },
            { AC_STATUS.AC_LIVE, "Live" },
            { AC_STATUS.AC_PAUSE, "Pause" },
            { AC_STATUS.AC_REPLAY, "Replay" },
        };

        public AssettoCorsa()
        {
            _sharedMemoryRetryTimer = new Timer(2000);
            _sharedMemoryRetryTimer.AutoReset = true;
            _sharedMemoryRetryTimer.Elapsed += sharedMemoryRetryTimer_Elapsed;

            _physicsTimer = new Timer();
            _physicsTimer.AutoReset = true;
            _physicsTimer.Elapsed += physicsTimer_Elapsed;
            PhysicsInterval = 10;

            _graphicsTimer = new Timer();
            _graphicsTimer.AutoReset = true;
            _graphicsTimer.Elapsed += graphicsTimer_Elapsed;
            GraphicsInterval = 1000;

            _staticInfoTimer = new Timer();
            _staticInfoTimer.AutoReset = true;
            _staticInfoTimer.Elapsed += staticInfoTimer_Elapsed;
            StaticInfoInterval = 1000;

            Stop();
        }

        /// <summary>
        /// Connect to the shared memory and start the update timers.
        /// </summary>
        public void Start()
        {
            _sharedMemoryRetryTimer.Start();
        }

        private void sharedMemoryRetryTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ConnectToSharedMemory();
        }

        private bool ConnectToSharedMemory()
        {
            try
            {
                _memoryStatus = AC_MEMORY_STATUS.CONNECTING;

                // Connect to shared memory
                _physicsMMF = MemoryMappedFile.OpenExisting("Local\\acpmf_physics");
                _graphicsMMF = MemoryMappedFile.OpenExisting("Local\\acpmf_graphics");
                _staticInfoMMF = MemoryMappedFile.OpenExisting("Local\\acpmf_static");

                // Start the timers
                _staticInfoTimer.Start();
                ProcessStaticInfo();

                _graphicsTimer.Start();
                ProcessGraphics();

                _physicsTimer.Start();
                ProcessPhysics();

                // Stop retry timer
                _sharedMemoryRetryTimer.Stop();
                _memoryStatus = AC_MEMORY_STATUS.CONNECTED;
                return true;
            }
            catch (FileNotFoundException)
            {
                _staticInfoTimer.Stop();
                _graphicsTimer.Stop();
                _physicsTimer.Stop();
                return false;
            }
        }

        /// <summary>
        /// Stop the timers and dispose of the shared memory handles.
        /// </summary>
        public void Stop()
        {
            _memoryStatus = AC_MEMORY_STATUS.DISCONNECTED;
            _sharedMemoryRetryTimer.Stop();

            // Stop the timers
            _physicsTimer.Stop();
            _graphicsTimer.Stop();
            _staticInfoTimer.Stop();
        }

        /// <summary>
        /// Interval for physics updates in milliseconds.
        /// </summary>
#pragma warning disable SA1623 // PropertySummaryDocumentationMustMatchAccessors
        public double PhysicsInterval
        {
            get
            {
                return _physicsTimer.Interval;
            }

            set
            {
                _physicsTimer.Interval = value;
            }
        }

        /// <summary>
        /// Interval for graphics updates in milliseconds.
        /// </summary>
        public double GraphicsInterval
        {
            get
            {
                return _graphicsTimer.Interval;
            }

            set
            {
                _graphicsTimer.Interval = value;
            }
        }

        /// <summary>
        /// Interval for static info updates in milliseconds.
        /// </summary>
        public double StaticInfoInterval
        {
            get
            {
                return _staticInfoTimer.Interval;
            }

            set
            {
                _staticInfoTimer.Interval = value;
            }
        }

        private MemoryMappedFile _physicsMMF;
        private MemoryMappedFile _graphicsMMF;
        private MemoryMappedFile _staticInfoMMF;

        private Timer _physicsTimer;
        private Timer _graphicsTimer;
        private Timer _staticInfoTimer;

        /// <summary>
        /// Represents the method that will handle the physics update events
        /// </summary>
        public event PhysicsUpdatedHandler PhysicsUpdated;

        /// <summary>
        /// Represents the method that will handle the graphics update events
        /// </summary>
        public event GraphicsUpdatedHandler GraphicsUpdated;

        /// <summary>
        /// Represents the method that will handle the static info update events
        /// </summary>
        public event StaticInfoUpdatedHandler StaticInfoUpdated;

        public virtual void OnPhysicsUpdated(PhysicsEventArgs e)
        {
            PhysicsUpdated?.Invoke(this, e);
        }

        public virtual void OnGraphicsUpdated(GraphicsEventArgs e)
        {
            if (GraphicsUpdated != null)
            {
                GraphicsUpdated(this, e);
                if (_gameStatus != e.Graphics.Status)
                {
                    _gameStatus = e.Graphics.Status;
                    GameStatusChanged?.Invoke(this, new GameStatusEventArgs(_gameStatus));
                }

                if (_pitStatus != e.Graphics.IsInPit)
                {
                    _pitStatus = e.Graphics.IsInPit;
                    PitStatusChanged?.Invoke(this, new PitStatusEventArgs(_pitStatus));
                }

                if (_sessionType != e.Graphics.Session)
                {
                    _sessionType = e.Graphics.Session;
                    SessionTypeChanged?.Invoke(this, new SessionTypeEventArgs(_sessionType));
                }
            }
        }

        public virtual void OnStaticInfoUpdated(StaticInfoEventArgs e)
        {
            StaticInfoUpdated?.Invoke(this, e);
        }

        private void physicsTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ProcessPhysics();
        }

        private void graphicsTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ProcessGraphics();
        }

        private void staticInfoTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ProcessStaticInfo();
        }

        private void ProcessPhysics()
        {
            if (_memoryStatus == AC_MEMORY_STATUS.DISCONNECTED)
            {
            return;
            }

            try
            {
                Physics physics = ReadPhysics();
                OnPhysicsUpdated(new PhysicsEventArgs(physics));
            }
            catch (AssettoCorsaNotStartedException)
            {
            }
        }

        private void ProcessGraphics()
        {
            if (_memoryStatus == AC_MEMORY_STATUS.DISCONNECTED)
            {
                return;
            }

            try
            {
                Graphics graphics = ReadGraphics();
                OnGraphicsUpdated(new GraphicsEventArgs(graphics));
            }
            catch (AssettoCorsaNotStartedException)
            {
            }
        }

        private void ProcessStaticInfo()
        {
            if (_memoryStatus == AC_MEMORY_STATUS.DISCONNECTED)
            {
                return;
            }

            try
            {
                StaticInfo staticInfo = ReadStaticInfo();
                OnStaticInfoUpdated(new StaticInfoEventArgs(staticInfo));
            }
            catch (AssettoCorsaNotStartedException)
            {
            }
        }

        /// <summary>
        /// Read the current physics data from shared memory.
        /// </summary>
        /// <returns>A Physics object representing the current status, or null if not available.</returns>
        public Physics ReadPhysics()
        {
            if (_memoryStatus == AC_MEMORY_STATUS.DISCONNECTED || _physicsMMF == null)
            {
                throw new AssettoCorsaNotStartedException();
            }

            using (var stream = _physicsMMF.CreateViewStream())
            {
                using (var reader = new BinaryReader(stream))
                {
                    var size = Marshal.SizeOf(typeof(Physics));
                    var bytes = reader.ReadBytes(size);
                    var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                    var data = (Physics)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(Physics));
                    handle.Free();
                    return data;
                }
            }
        }

        public Graphics ReadGraphics()
        {
            if (_memoryStatus == AC_MEMORY_STATUS.DISCONNECTED || _graphicsMMF == null)
            {
                throw new AssettoCorsaNotStartedException();
            }

            using (var stream = _graphicsMMF.CreateViewStream())
            {
                using (var reader = new BinaryReader(stream))
                {
                    var size = Marshal.SizeOf(typeof(Graphics));
                    var bytes = reader.ReadBytes(size);
                    var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                    var data = (Graphics)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(Graphics));
                    handle.Free();
                    return data;
                }
            }
        }

        public StaticInfo ReadStaticInfo()
        {
            if (_memoryStatus == AC_MEMORY_STATUS.DISCONNECTED || _staticInfoMMF == null)
            {
                throw new AssettoCorsaNotStartedException();
            }

            using (var stream = _staticInfoMMF.CreateViewStream())
            {
                using (var reader = new BinaryReader(stream))
                {
                    var size = Marshal.SizeOf(typeof(StaticInfo));
                    var bytes = reader.ReadBytes(size);
                    var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                    var data = (StaticInfo)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(StaticInfo));
                    handle.Free();
                    return data;
                }
            }
        }
    }
}
