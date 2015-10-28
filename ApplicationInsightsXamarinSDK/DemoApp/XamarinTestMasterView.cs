﻿using System;
using Xamarin.Forms;
using AI.XamarinSDK;
using System.Collections.Generic;

namespace XamarinTest
{
	public class XamarinTestMasterView : ContentPage
	{

		TableView tableView;
		SwitchCell autoSessionManagementCell;
		SwitchCell autoPageViewsCell;
		EntryCell serverURLCell;
		EntryCell userIDCell;

		enum TelemetryType
		{
			None,
			Event,
			Message,
			Metric,
			PageView,
			Session,
			HandledException,
			UnhandledException,
			UnmanagedSignal,
			UnmanagedException
		};

		public XamarinTestMasterView ()
		{

			SetupActionCells ();

			// Set up navigation
			Title = "Xamarin SDK";
			NavigationPage.SetHasNavigationBar (this, true);

			// Set up page

			tableView = new TableView {
				Intent = TableIntent.Settings,
				Root = new TableRoot {
					new TableSection ("Crash reproting") {
						new TextCell { 
							Text = "Managed exception crash",
							Command = new Command (() => TrackTelemetryData(TelemetryType.UnhandledException))
						},
						new TextCell { 
							Text = "Managed handled exception",
							Command = new Command (() => TrackTelemetryData(TelemetryType.HandledException))
						},
						#if __IOS__
						new TextCell { 
							Text = "Unmanaged signal crash",
							Command = new Command (() => TrackTelemetryData(TelemetryType.UnmanagedSignal))
						},
						#elif __ANDROID__
						new TextCell { 
							Text = "Managed Java exception",
							Command = new Command (() => {
								throw new Java.Lang.NullPointerException();
							})
						},
						#endif
						new TextCell { 
							Text = "Unmanaged exception crash",
							Command = new Command (() => TrackTelemetryData(TelemetryType.UnmanagedException))
						}
					},
					new TableSection ("Telemetry data") {
						new TextCell { 
							Text = "Track event",
							Command = new Command (() => TrackTelemetryData(TelemetryType.Event))
						},
						new TextCell { 
							Text = "Track message" ,
							Command = new Command (() => TrackTelemetryData(TelemetryType.Message))
						},
						new TextCell { 
							Text = "Track metric",
							Command = new Command (() => TrackTelemetryData(TelemetryType.Metric))
						}
					},
					new TableSection ("Page views") {
						new TextCell { 
							Text = "Track page view" ,
							Command = new Command (() => TrackTelemetryData(TelemetryType.PageView))
						},
						#if __IOS__
						new TextCell { 
							Text = "Trigger auto page view" ,

							Command = new Command (() => Navigation.PushAsync (new XamarinTestDetailView("Page view triggered")))
						},
						#endif
						autoPageViewsCell
					},
					new TableSection ("Sessions") {
						autoSessionManagementCell
					},
					new TableSection ("Configuration") {
						userIDCell,
						serverURLCell
					},
				},
			};

			Content = tableView;
		}

		private void SetupActionCells(){

			// Set up switch cells
			autoSessionManagementCell = new SwitchCell { 
				Text = "Auto session management", 
				On = true ,
			};
			autoSessionManagementCell.OnChanged += (sender, ea) => {
                CrossApplicationInsights.Current.SetAutoSessionManagementDisabled(!autoSessionManagementCell.On);
			};

			autoPageViewsCell = new SwitchCell { 
				Text = "Auto page view tracking", 
				On = true ,
			};
			autoPageViewsCell.OnChanged += (sender, ea) => {
                CrossApplicationInsights.Current.SetAutoPageViewTrackingDisabled(!autoPageViewsCell.On);
			};

			// TODO: also update ApplicationInsights config if input field lost focus
			serverURLCell = new EntryCell(){ 
				Label = "Server URL", 
				Placeholder = "Custom server URL" 
			};
			serverURLCell.Completed += (sender, ea) => {
                CrossApplicationInsights.Current.SetServerUrl(serverURLCell.Text);
			};

			userIDCell = new EntryCell(){ 
				Label = "User ID", 
				Placeholder = "Custom user ID"
			};
			userIDCell.Completed += (sender, ea) => {
                CrossApplicationInsights.Current.SetUserId(userIDCell.Text);
			};
		}

		private void TrackTelemetryData(TelemetryType type){

			switch (type) {
			case TelemetryType.Event:
				Dictionary<string, string> properties = new Dictionary<string, string> ();
				properties.Add ("Xamarin Key", "Custom Property Value");
                CrossTelemetryManager.Current.TrackEvent("My custom event", properties);
				break;
			case TelemetryType.Metric:
                CrossTelemetryManager.Current.TrackMetric("My custom metric", 2.2);
				break;
			case TelemetryType.Message:
                CrossTelemetryManager.Current.TrackTrace("My custom message");
				break;
			case TelemetryType.PageView:
                CrossTelemetryManager.Current.TrackPageView("My custom page view");
				break;
			case TelemetryType.Session:
                CrossApplicationInsights.Current.RenewSessionWithId(new DateTime().Date.ToString());
				break;
			case TelemetryType.HandledException:
				try {            
					throw(new NullReferenceException());
				}catch (Exception e){ 
					// App shouldn't crash because of that
				}
				break;
			case TelemetryType.UnhandledException:
				int value = 1 / int.Parse("0");
				break;
			case TelemetryType.UnmanagedSignal:
				#if __IOS__
				DummyLibrary.TriggerSignalCrash ();
				#endif
				break;
			case TelemetryType.UnmanagedException:
				DummyLibrary.TriggerExceptionCrash ();
				break;
			default:
				break;
			}
		}
	}
}


