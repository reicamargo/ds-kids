package mono.com.crashlytics.android.core;


public class CrashlyticsListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.crashlytics.android.core.CrashlyticsListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_crashlyticsDidDetectCrashDuringPreviousExecution:()V:GetCrashlyticsDidDetectCrashDuringPreviousExecutionHandler:Com.Crashlytics.Android.Core.ICrashlyticsListenerInvoker, Crashlytics.Droid\n" +
			"";
		mono.android.Runtime.register ("Com.Crashlytics.Android.Core.ICrashlyticsListenerImplementor, Crashlytics.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CrashlyticsListenerImplementor.class, __md_methods);
	}


	public CrashlyticsListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CrashlyticsListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Crashlytics.Android.Core.ICrashlyticsListenerImplementor, Crashlytics.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void crashlyticsDidDetectCrashDuringPreviousExecution ()
	{
		n_crashlyticsDidDetectCrashDuringPreviousExecution ();
	}

	private native void n_crashlyticsDidDetectCrashDuringPreviousExecution ();

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
