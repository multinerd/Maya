# Instance Checker
Used to prevent multiple instance of an application.



# Usage

#### In App.xaml.cs
```csharp
protected override void OnStartup(StartupEventArgs e)
{
    base.OnStartup(e);
    if (InstanceChecker.IsAppRunning(Process.GetCurrentProcess(), true))
        Current.Shutdown(0);
        
    // Non-prism
    //new MainWindow().Show(); 

    // Prism
    var bootstrapper = new Bootstrapper();
    bootstrapper.Run()'
}

protected override void OnExit(ExitEventArgs e)
{
    InstanceChecker.Cleanup();
    base.OnExit(e);
}
```