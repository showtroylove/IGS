using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using DevExpress.Images;
using DevExpress.Internal;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;

namespace DevExpress.DevAV {
    public partial class App : Application 
    {
        //private static IDisposable singleInstanceApplicationGuard;

        protected override void OnStartup(StartupEventArgs e) 
        {
            Start(() => base.OnStartup(e), this);
        }

        //public static void Start(Action baseStart)
        //{
        //    ExceptionHelper.Initialize();
        //    //DataDirectoryHelper.SetWebBrowserMode();
        //    LoadPlugins();
        //    ApplicationThemeHelper.ApplicationThemeName = Theme.MetropolisDarkName;
        //    baseStart();
        //    Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata(200));
        //    SetCultureInfo();
        //}
        //#region LoadPlugins
        //static void LoadPlugins()
        //{
        //    foreach (var file in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "DevExpress.RealtorWorld.Xpf.Plugins.*.exe"))
        //    {
        //        var plugin = Assembly.LoadFrom(file);
        //        if (plugin == null) continue;
        //        var entry = plugin.GetType("Global.Program");
        //        if (entry == null) continue;
        //        var start = entry.GetMethod("Start", BindingFlags.Static | BindingFlags.Public, null, new Type[] { }, null);
        //        if (start == null) continue;
        //        start.Invoke(null, new object[] { });
        //    }
        //}
        //#endregion
        //static void SetCultureInfo()
        //{
        //    var demoCI = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
        //    demoCI.NumberFormat.CurrencySymbol = "$";
        //    Thread.CurrentThread.CurrentCulture = demoCI;
        //    var demoUI = (CultureInfo)Thread.CurrentThread.CurrentUICulture.Clone();
        //    demoUI.NumberFormat.CurrencySymbol = "$";
        //    Thread.CurrentThread.CurrentUICulture = demoUI;
        //}
        public static void Start(Action baseStart, Application application)
        {
            ExceptionHelper.Initialize();
            AppDomain.CurrentDomain.AssemblyResolve += OnCurrentDomainAssemblyResolve;
            DataDirectoryHelper.LocalPrefix = "WpfHybridApp";
            DataDirectoryHelper.SetWebBrowserMode();

            ImagesAssemblyLoader.Load();
            Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata(200));
            LoadPlugins();
            ApplicationThemeHelper.ApplicationThemeName = Theme.MetropolisDark.Name;
            baseStart();
            ViewLocator.Default = new ViewLocator(typeof(App).Assembly);
            //singleInstanceApplicationGuard = DataDirectoryHelper.SingleInstanceApplicationGuard("DevExpressWpfHybridApp", out bool exit);
            //if (exit)
            //{
            //    application.Shutdown();
            //    return;
            //}
            ApplicationThemeHelper.ApplicationThemeName = Theme.HybridApp.Name;
            SetCultureInfo();
            SetLocalization();
        }

        private static void SetCultureInfo()
        {
            var demoCI = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            demoCI.NumberFormat.CurrencySymbol = "$";
            Thread.CurrentThread.CurrentCulture = demoCI;
            var demoUI = (CultureInfo)Thread.CurrentThread.CurrentUICulture.Clone();
            demoUI.NumberFormat.CurrencySymbol = "$";
            Thread.CurrentThread.CurrentUICulture = demoUI;
        }

        private static void SetLocalization()
        {
            EditorLocalizer.Active = new HybridAppEditorLocalizer();
        }

        private static Assembly OnCurrentDomainAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var partialName = Utils.AssemblyHelper.GetPartialName(args.Name).ToLower();

            if (partialName != "entityframework" && partialName != "system.data.sqlite" &&
                partialName != "system.data.sqlite.ef6") return null;

            var assemblyLocation = typeof(App).Assembly.Location;
            if (assemblyLocation == null) return null;

            var path = Path.Combine(Path.GetDirectoryName(assemblyLocation), $"{partialName}.dll");
            return Assembly.LoadFrom(path);
        }
        #region LoadPlugins

        private static void LoadPlugins()
        {
            foreach (var file in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "DevExpress.HybridApp.Wpf.Plugins.*.exe"))
            {
                Assembly.LoadFrom(file)
                    .With(x => x.GetType("Global.Program"))
                    .With(x => x.GetMethod("Start", BindingFlags.Static | BindingFlags.Public, null, new Type[] { }, null))
                    .Do(x => x.Invoke(null, new object[] { }));
            }
        }
        #endregion
    }
    public class HybridAppEditorLocalizer : EditorLocalizer 
    {
        protected override void PopulateStringTable() 
        {
            base.PopulateStringTable();
            AddString(EditorStringId.LookUpSearch, "Enter text to search...");
        }
    }
}
