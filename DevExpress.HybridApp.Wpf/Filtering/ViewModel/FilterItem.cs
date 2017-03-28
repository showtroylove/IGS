using DevExpress.Data.Filtering;
using DevExpress.Mvvm.POCO;

namespace DevExpress.DevAV.ViewModels {
    public class FilterItem {
        public static FilterItem Create(int entitiesCount, string name, CriteriaOperator filterCriteria, string imageUri) {
            return ViewModelSource.Create(() => new FilterItem(entitiesCount, name, filterCriteria, imageUri));
        }

        protected FilterItem(int entitiesCount, string name, CriteriaOperator filterCriteria, string imageUri) {
            Name = name;
            FilterCriteria = filterCriteria;
            ImageUri = imageUri;
            Update(entitiesCount);
        }

        public virtual string Name { get; set; }

        public virtual CriteriaOperator FilterCriteria { get; set; }

        public virtual int EntitiesCount { get; protected set; }

        public virtual string DisplayText { get; protected set; }

        public virtual string ImageUri { get; protected set; }

        public void Update(int entitiesCount) {
            EntitiesCount = entitiesCount;
            DisplayText = string.Format("{0} ({1})", Name, entitiesCount);
        }

        public FilterItem Clone() {
            return Create(EntitiesCount, Name, FilterCriteria, ImageUri);
        }
        public FilterItem Clone(string name, string imageUri) {
            return Create(EntitiesCount, name, FilterCriteria, imageUri);
        }

        protected virtual void OnNameChanged() {
            Update(EntitiesCount);
        }
    }
}
