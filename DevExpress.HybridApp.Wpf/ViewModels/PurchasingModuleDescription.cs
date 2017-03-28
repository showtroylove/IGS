using System;
using DevExpress.DevAV.Common.ViewModel;

namespace DevExpress.DevAV.ViewModels
{
    public class PurchasingModuleDescription : ModuleDescription<PurchasingModuleDescription> 
    {
        public PurchasingModuleDescription(string title, string documentType, string group, IFilterTreeViewModel filterTreeViewModel, string imgtitle = null)
            : base(title, documentType, group, null) 
        {
            ImageSource = new Uri(
                $@"pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Menu/{imgtitle ?? title}.png");
            FilterTreeViewModel = filterTreeViewModel;
        }
        public Uri ImageSource { get; private set; }

        public IFilterTreeViewModel FilterTreeViewModel { get; }
    }
}