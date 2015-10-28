﻿using System;
using System.Collections.Generic;

namespace AI.XamarinSDK
{

	public class TelemetryManager : ITelemetryManager
	{

		public TelemetryManager(){}

		public void TrackEvent (string eventName)
		{
		}

		public void TrackEvent (string eventName, Dictionary<string, string> properties)
		{
		}

		public void TrackTrace (string message)
		{
		}

		public void TrackTrace (string message, Dictionary<string, string> properties)
		{
		}

		public void TrackMetric (string metricName, double value)
		{
		}

		public void TrackMetric (string metricName, double value, Dictionary<string, string> properties)
		{
		}

		public void TrackPageView (string pageName)
		{
		}

		public void TrackPageView (string pageName, int duration)
		{
		}

		public void TrackPageView (string pageName, int duration, Dictionary<string, string> properties)
		{
		}

		public void TrackManagedException (Exception  exception, bool handled)
		{
		}
	}
}

