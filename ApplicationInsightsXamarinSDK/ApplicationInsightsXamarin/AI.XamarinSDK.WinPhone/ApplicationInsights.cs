﻿using System;

namespace AI.XamarinSDK
{
	public class ApplicationInsights : IApplicationInsights
	{

		public ApplicationInsights (){}

		public void Setup(string instrumentationKey)
		{
		}

		public void Start ()
		{
		}

		public string GetServerUrl ()
		{
			return null;
		}

		public void SetServerUrl (string serverUrl)
		{
		}

		public void  SetCrashManagerDisabled (bool crashManagerDisabled)
		{
		}

		public void SetTelemetryManagerDisabled (bool telemetryManagerDisabled)
		{
		}

		public void SetAutoPageViewTrackingDisabled (bool autoPageViewTrackingDisabled)
		{
		}

		public void SetAutoSessionManagementDisabled (bool autoSessionManagementDisabled)
		{
		}

		public void SetUserId (string userId)
		{
		}

		public void StartNewSession (){
		}

		public void SetSessionExpirationTime (int appBackgroundTime)
		{
		}

		public void RenewSessionWithId (string sessionId)
		{
		}

		public bool GetDebugLogEnabled() 
		{
			return false;
		}

		public void SetDebugLogEnabled(bool debugLogEnabled) 
		{
		}
	}
}

