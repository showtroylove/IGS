﻿using DevExpress.Mvvm.POCO;
using DevExpress.DevAV.DevAVDbDataModel;
using DevExpress.DevAV.Common.DataModel;
using DevExpress.DevAV.Common.ViewModel;

namespace DevExpress.DevAV.ViewModels
{
    /// <summary>
    /// Represents the single Employee object view model.
    /// </summary>
    public partial class EmployeeViewModel : SingleObjectViewModel<Employee, long, IPurchasingUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of EmployeeViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static EmployeeViewModel Create(IUnitOfWorkFactory<IPurchasingUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new EmployeeViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the EmployeeViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the EmployeeViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected EmployeeViewModel(IUnitOfWorkFactory<IPurchasingUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Employees, x => x.FullName)
        {
        }
        
        /// <summary>
        /// The view model for the EmployeeAssignedTasks detail collection.
        /// </summary>
        public CollectionViewModel<EmployeeTask, long, IPurchasingUnitOfWork> EmployeeAssignedTasksDetails
        {
            get { return GetDetailsCollectionViewModel((EmployeeViewModel x) => x.EmployeeAssignedTasksDetails, x => x.Tasks, x => x.AssignedEmployeeId, (x, key) => x.AssignedEmployeeId = key); }
        }

        /// <summary>
        /// The view model for the EmployeeOwnedTasks detail collection.
        /// </summary>
        public CollectionViewModel<EmployeeTask, long, IPurchasingUnitOfWork> EmployeeOwnedTasksDetails
        {
            get { return GetDetailsCollectionViewModel((EmployeeViewModel x) => x.EmployeeOwnedTasksDetails, x => x.Tasks, x => x.OwnerId, (x, key) => x.OwnerId = key); }
        }

        /// <summary>
        /// The view model for the EmployeeEvaluations detail collection.
        /// </summary>
        public CollectionViewModel<Evaluation, long, IPurchasingUnitOfWork> EmployeeEvaluationsDetails
        {
            get { return GetDetailsCollectionViewModel((EmployeeViewModel x) => x.EmployeeEvaluationsDetails, x => x.Evaluations, x => x.EmployeeId, (x, key) => x.EmployeeId = key); }
        }
    }
}
