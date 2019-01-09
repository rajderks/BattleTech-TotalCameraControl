using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TotalCameraControl
{
    class Settings
    {
        public bool jump = true;
        public bool DFA = true;
        public bool melee = true;
        public bool artilleryObjective = true;
        public bool artillery = true;
        public bool move = true;
        public bool startup = true;
        public bool stand = true;
        public bool sensorLock = true;
        public bool showActor = true;
        public bool strafe = true;
        public bool deathCam = true;
        public bool debug = false;

        // Ignored properties
        public static bool multi = false;
    }
}