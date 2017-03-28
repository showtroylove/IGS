using System;
using System.Linq.Expressions;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;

namespace DevExpress.DevAV.ViewModels.AppSettings {
    partial class AppSettingsViewModel : ISupportFiltering<IGS.Data.Model.AppSettings>, IFilterTreeViewModelContainer<IGS.Data.Model.AppSettings, int>
    {
        public virtual FilterTreeViewModel<IGS.Data.Model.AppSettings, int> FilterTreeViewModel { get; set; }

        [Command]
        public void CreateCustomFilter() {
            Messenger.Default.Send(new CreateCustomFilterMessage<Product>());
        }

        #region ISupportFiltering
        Expression<Func<IGS.Data.Model.AppSettings, bool>> ISupportFiltering<IGS.Data.Model.AppSettings>.FilterExpression 
        {
            get { return null; }
            set {  }
        }
        #endregion
    }
}
