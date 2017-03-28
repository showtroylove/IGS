using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using DevExpress.Data.Filtering;
using DevExpress.Data.Utils;
using DevExpress.DevAV.Common.ViewModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using IGS.Data.Model;

namespace DevExpress.DevAV.ViewModels {
    public class FilterTreeViewModel<TEntity, TPrimaryKey> : IFilterTreeViewModel where TEntity : class 
    {
        static FilterTreeViewModel() 
        {
            var enums = typeof(EmployeeStatus).Assembly.GetTypes().Where(t => t.IsEnum);
            foreach(var e in enums)
                EnumProcessingHelper.RegisterEnum(e);

            typeof(AppSettingsTypeEnum).Assembly.GetTypes().
                Where(t => t.IsEnum).ForEach(EnumProcessingHelper.RegisterEnum);
        }

        public static FilterTreeViewModel<TEntity, TPrimaryKey> Create(IFilterTreeModelPageSpecificSettings settings, IQueryable<TEntity> entities, Action<object, Action> registerEntityChangedMessageHandler) 
        {
            return ViewModelSource.Create(() => new FilterTreeViewModel<TEntity, TPrimaryKey>(settings, entities, registerEntityChangedMessageHandler));
        }

        private readonly IQueryable<TEntity> _entities;
        private readonly IFilterTreeModelPageSpecificSettings _settings;
        private object _viewModel;

        protected FilterTreeViewModel(IFilterTreeModelPageSpecificSettings settings, IQueryable<TEntity> entities, Action<object, Action> registerEntityChangedMessageHandler) 
        {
            _settings = settings;
            _entities = entities;
            StaticFilters = CreateFilterItems(settings.StaticFilters);
            CustomFilters = CreateFilterItems(settings.CustomFilters);
            SelectedItem = StaticFilters.FirstOrDefault();
            AllowFiltersContextMenu = true;

            Messenger.Default.Register<EntityMessage<TEntity, TPrimaryKey>>(this, message => UpdateFilters());
            Messenger.Default.Register<CreateCustomFilterMessage<TEntity>>(this, message => CreateCustomFilter());
            UpdateFilters();
            StaticCategoryName = settings.StaticFiltersTitle;
            CustomCategoryName = settings.CustomFiltersTitle;
        }

        public virtual ObservableCollection<FilterItem> StaticFilters { get; protected set; }
        public virtual ObservableCollection<FilterItem> CustomFilters { get; protected set; }
        public virtual FilterItem SelectedItem { get; set; }
        public virtual FilterItem ActiveFilterItem { get; set; }
        public Action NavigateAction { get; set; }
        public string StaticCategoryName { get; private set; }
        public string CustomCategoryName { get; private set; }
        public bool AllowCustomFilters => _settings.CustomFilters != null;

        public bool AllowFiltersContextMenu 
        {
            get 
            {
                return AllowCustomFilters && _allowFiltersContextMenu;
            }
            set 
            {
                _allowFiltersContextMenu = value;
            }
        }

        private bool _allowFiltersContextMenu;

        public void DeleteCustomFilter(FilterItem filterItem)
        {
            CustomFilters.Remove(filterItem);
            SaveCustomFilters();
        }

        public void DuplicateFilter(FilterItem filterItem) 
        {
            var newItem = filterItem.Clone("Copy of " + filterItem.Name, null);
            CustomFilters.Add(newItem);
            SaveCustomFilters();
        }

        public void ResetCustomFilters() 
        {
            if(CustomFilters.Contains(SelectedItem))
                SelectedItem = null;
            _settings.CustomFilters = new FilterInfoList();
            CustomFilters.Clear();
            _settings.SaveSettings();
        }

        public void ModifyCustomFilter(FilterItem existing) 
        {
            var clone = existing.Clone();
            var filterViewModel = CreateCustomFilterViewModel(clone, true);
            ShowFilter(clone, filterViewModel, () =>
            {
                existing.FilterCriteria = clone.FilterCriteria;
                existing.Name = clone.Name;
                SaveCustomFilters();
                if(existing == SelectedItem)
                    OnSelectedItemChanged();
            });
        }
        public void ModifyCustomFilterCriteria(FilterItem existing, CriteriaOperator criteria) 
        {
            if(!ReferenceEquals(existing.FilterCriteria, null) && !ReferenceEquals(criteria, null) && existing.FilterCriteria.ToString() == criteria.ToString())
                return;
            existing.FilterCriteria = criteria;
            SaveCustomFilters();
            if(existing == SelectedItem)
                OnSelectedItemChanged();
            UpdateFilters();
        }
        public void ResetToAll() => SelectedItem = StaticFilters[0];

        public void CreateCustomFilter() 
        {
            var filterItem = CreateFilterItem(string.Empty, null, null);
            var filterViewModel = CreateCustomFilterViewModel(filterItem, true);
            ShowFilter(filterItem, filterViewModel, () => AddNewCustomFilter(filterItem));
        }
        public void AddCustomFilter(string name, CriteriaOperator filterCriteria, string imageUri = null) => AddNewCustomFilter(CreateFilterItem(name, filterCriteria, imageUri));

        protected virtual void OnSelectedItemChanged()
        {
            ActiveFilterItem = SelectedItem.Clone();
            UpdateFilterExpression();
            NavigateCore();
        }
        public virtual void Navigate() => NavigateCore();

        private void NavigateCore() => NavigateAction?.Invoke();

        void IFilterTreeViewModel.SetViewModel(object viewModel) 
        {
            _viewModel = viewModel;
            var viewModelContainer = viewModel as IFilterTreeViewModelContainer<TEntity, TPrimaryKey>;
            if(viewModelContainer != null)
                viewModelContainer.FilterTreeViewModel = this;
            UpdateFilterExpression();
        }

        private void UpdateFilterExpression() 
        {
            var viewModel = _viewModel as ISupportFiltering<TEntity>;
            if(viewModel != null)
                viewModel.FilterExpression = ActiveFilterItem == null ? null : GetWhereExpression(ActiveFilterItem.FilterCriteria);
        }

        private ObservableCollection<FilterItem> CreateFilterItems(IEnumerable<FilterInfo> filters) => filters == null ? new ObservableCollection<FilterItem>() : new ObservableCollection<FilterItem>(filters.Select(x => CreateFilterItem(x.Name, CriteriaOperator.Parse(x.FilterCriteria), x.ImageUri)));

        private const string NewFilterName = @"New Filter";

        private void AddNewCustomFilter(FilterItem filterItem) 
        {
            if(string.IsNullOrEmpty(filterItem.Name)) 
            {
                var prevIndex = CustomFilters.Select(fi => Regex.Match(fi.Name, NewFilterName + @" (?<index>\d+)")).Where(m => m.Success).Select(m => int.Parse(m.Groups["index"].Value)).DefaultIfEmpty(0).Max();
                filterItem.Name = $"{NewFilterName} {prevIndex + 1}";
            } 
            else 
            {
                var existing = CustomFilters.FirstOrDefault(fi => fi.Name == filterItem.Name);
                if(existing != null)
                    CustomFilters.Remove(existing);
            }
            CustomFilters.Add(filterItem);
            SaveCustomFilters();
        }

        private void SaveCustomFilters() 
        {
            _settings.CustomFilters = SaveToSettings(CustomFilters);
            _settings.SaveSettings();
        }

        private FilterInfoList SaveToSettings(ObservableCollection<FilterItem> filters) => new FilterInfoList(filters.Select(fi => new FilterInfo { Name = fi.Name, FilterCriteria = CriteriaOperator.ToString(fi.FilterCriteria) }));

        private void UpdateFilters()
        {
            //Parallel.ForEach(StaticFilters.Concat(CustomFilters), item => item.Update(GetEntityCount(item.FilterCriteria)));
            foreach(var item in StaticFilters.Concat(CustomFilters)) 
            {
                item.Update(GetEntityCount(item.FilterCriteria));
            }
        }

        private void ShowFilter(FilterItem filterItem, CustomFilterViewModel filterViewModel, Action onSave) 
        {
            if(FilterDialogService.ShowDialog(MessageButton.OKCancel, "Create Custom Filter", "CustomFilterView", filterViewModel) != MessageResult.OK)
                return;
            filterItem.FilterCriteria = filterViewModel.FilterCriteria;
            filterItem.Name = filterViewModel.FilterName;
            ActiveFilterItem = filterItem;
            if (!filterViewModel.Save) return;
            onSave();
            UpdateFilters();
        }

        private CustomFilterViewModel CreateCustomFilterViewModel(FilterItem existing, bool save) 
        {
            var viewModel = CustomFilterViewModel.Create(typeof(TEntity), _settings.HiddenFilterProperties, _settings.AdditionalFilterProperties);
            viewModel.FilterCriteria = existing.FilterCriteria;
            viewModel.FilterName = existing.Name;
            viewModel.Save = save;
            viewModel.SetParentViewModel(this);
            return viewModel;
        }

        private FilterItem CreateFilterItem(string name, CriteriaOperator filterCriteria, string imageUri) => FilterItem.Create(GetEntityCount(filterCriteria), name, filterCriteria, imageUri);

        private int GetEntityCount(CriteriaOperator criteria) => _entities.Where(GetWhereExpression(criteria)).Count();

        private Expression<Func<TEntity, bool>> GetWhereExpression(CriteriaOperator criteria) => this.IsInDesignMode()
            ? CriteriaOperatorToExpressionConverter.GetLinqToObjectsWhere<TEntity>(criteria)
            : CriteriaOperatorToExpressionConverter.GetGenericWhere<TEntity>(criteria);

        private IDialogService FilterDialogService => this.GetRequiredService<IDialogService>("FilterDialogService");
    }

    public interface IFilterTreeViewModelContainer<TEntity, TPrimaryKey> where TEntity : class 
    {
        FilterTreeViewModel<TEntity, TPrimaryKey> FilterTreeViewModel { get; set; }
    }

    public class CreateCustomFilterMessage<TEntity> where TEntity : class 
    {
    }

    public interface IFilterTreeViewModel 
    {
        void SetViewModel(object content);
        Action NavigateAction { get; set; }
    }
}
