using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class RobotSensor
    {
        public List<Place> RealTimeSensorPlace;

        public RobotSensor()
        {
            Engine.environment.actuator.RaiseChangeEnvironment += new EnvironmentActuator.ChangingEnvironmentActuator(robotSensor_OnEnvironmentChange);
        }

        private void robotSensor_OnEnvironmentChange(List<Place> places)
        {
            RealTimeSensorPlace = places;
        }
    }
}
