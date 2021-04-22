using System;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.CoordinatorLayout.Widget;
using Google.Android.Material.AppBar;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.LifecycleEvents;

namespace Microsoft.Maui
{
	public partial class MauiAppCompatActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle? savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			var mauiApp = MauiApplication.Current.Application;
			if (mauiApp == null)
				throw new InvalidOperationException($"The {nameof(IApplication)} instance was not found.");

			var services = MauiApplication.Current.Services;
			if (mauiApp == null)
				throw new InvalidOperationException($"The {nameof(IServiceProvider)} instance was not found.");

			var mauiContext = new MauiContext(services, this);

			var state = new ActivationState(mauiContext, savedInstanceState);
			var window = mauiApp.CreateWindow(state);

			MauiApplication.Current._windows.Add(window);

			var events = MauiApplication.Current.Services.GetRequiredService<ILifecycleEventService>();
			events?.InvokeEvents<Action<IWindow>>(nameof(IApplication.CreateWindow), action => action(window));

			window.MauiContext = mauiContext;

			// Create the root native layout and set the Activity's content to it
			var handler = window.ToNative(window.MauiContext);
			var matchParent = ViewGroup.LayoutParams.MatchParent;
			//var matchParent = ViewGroup.LayoutParams.MatchParent;
			var wrap = ViewGroup.LayoutParams.WrapContent;

			// Create the root native layout and set the Activity's content to it
			//var nativeRootLayout = new CoordinatorLayout(this);
			var nativeRootLayout = new LinearLayoutCompat(this);
			SetContentView(nativeRootLayout, new ViewGroup.LayoutParams(wrap, wrap));

			//AddToolbar(parent);

			var matchParent = ViewGroup.LayoutParams.MatchParent;

			SetContentView(handler, new ViewGroup.LayoutParams(matchParent, matchParent));

			//AddToolbar(parent);
			var nativePage = page.ToContainerView(window.MauiContext);

			// Add the IPage to the root layout; use match parent so the page automatically has the dimensions of the activity
			// nativeRootLayout.AddView(nativePage, new CoordinatorLayout.LayoutParams(wrap, wrap));
			nativeRootLayout.AddView(nativePage, new LinearLayoutCompat.LayoutParams(wrap, wrap));
		}

		void AddToolbar(ViewGroup parent)
		{
			Toolbar toolbar = new Toolbar(this);
			var appbarLayout = new AppBarLayout(this);

			appbarLayout.AddView(toolbar, new ViewGroup.LayoutParams(AppBarLayout.LayoutParams.MatchParent, global::Android.Resource.Attribute.ActionBarSize));
			SetSupportActionBar(toolbar);
			parent.AddView(appbarLayout, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent));
		}
	}
}
