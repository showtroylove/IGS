using DevExpress.Mvvm.POCO;
using DevExpress.DevAV.DevAVDbDataModel;
using DevExpress.DevAV.Common.DataModel;
using DevExpress.DevAV.Common.ViewModel;

namespace DevExpress.DevAV.ViewModels {
    /// <summary>
    /// Represents the Products collection view model.
    /// </summary>
    public partial class ProductCollectionViewModel : CollectionViewModel<Product, ProductInfoWithSales, long, IPurchasingUnitOfWork> 
    {

        /// <summary>
        /// Creates a new instance of ProductCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static ProductCollectionViewModel Create(IUnitOfWorkFactory<IPurchasingUnitOfWork> unitOfWorkFactory = null) => 
        ViewModelSource.Create(() => new ProductCollectionViewModel(unitOfWorkFactory));

        /// <summary>
        /// Initializes a new instance of the ProductCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the ProductCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected ProductCollectionViewModel(IUnitOfWorkFactory<IPurchasingUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Products, QueriesHelper.GetProductInfoWithSales) 
        {
        }
    }
}
