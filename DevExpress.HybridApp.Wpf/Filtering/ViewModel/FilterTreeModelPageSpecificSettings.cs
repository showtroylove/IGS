using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq.Expressions;

namespace DevExpress.DevAV.ViewModels {
    public class FilterTreeModelPageSpecificSettings<TSettings> : IFilterTreeModelPageSpecificSettings where TSettings : ApplicationSettingsBase {
        private readonly string staticFiltersTitle;
        private readonly string customFiltersTitle;
        private readonly TSettings settings;
        private readonly PropertyDescriptor customFiltersProperty;
        private readonly PropertyDescriptor staticFiltersProperty;
        private readonly IEnumerable<string> hiddenFilterProperties;
        private readonly IEnumerable<string> additionalFilterProperties;

        public FilterTreeModelPageSpecificSettings(TSettings settings, string staticFiltersTitle,
            Expression<Func<TSettings, FilterInfoList>> getStaticFiltersExpression, Expression<Func<TSettings, FilterInfoList>> getCustomFiltersExpression,
            IEnumerable<string> hiddenFilterProperties = null, IEnumerable<string> additionalFilterProperties = null, string customFiltersTitle = "Custom Filters") {
            this.settings = settings;
            this.staticFiltersTitle = staticFiltersTitle;
            this.customFiltersTitle = customFiltersTitle;
            staticFiltersProperty = GetProperty(getStaticFiltersExpression);
            customFiltersProperty = GetProperty(getCustomFiltersExpression);
            this.hiddenFilterProperties = hiddenFilterProperties;
            this.additionalFilterProperties = additionalFilterProperties;
        }
        FilterInfoList IFilterTreeModelPageSpecificSettings.CustomFilters {
            get { return GetFilters(customFiltersProperty); }
            set { SetFilters(customFiltersProperty, value); }
        }
        FilterInfoList IFilterTreeModelPageSpecificSettings.StaticFilters {
            get { return GetFilters(staticFiltersProperty); }
            set { SetFilters(staticFiltersProperty, value); }
        }
        string IFilterTreeModelPageSpecificSettings.StaticFiltersTitle { get { return staticFiltersTitle; } }
        string IFilterTreeModelPageSpecificSettings.CustomFiltersTitle { get { return customFiltersTitle; } }
        IEnumerable<string> IFilterTreeModelPageSpecificSettings.HiddenFilterProperties { get { return hiddenFilterProperties; } }
        IEnumerable<string> IFilterTreeModelPageSpecificSettings.AdditionalFilterProperties { get { return additionalFilterProperties; } }
        void IFilterTreeModelPageSpecificSettings.SaveSettings() {
            settings.Save();
        }

        private PropertyDescriptor GetProperty(Expression<Func<TSettings, FilterInfoList>> expression) {
            if(expression != null)
                return TypeDescriptor.GetProperties(settings)[GetPropertyName(expression)];
            return null;
        }

        private FilterInfoList GetFilters(PropertyDescriptor property) {
            return property != null ? (FilterInfoList)property.GetValue(settings) : null;
        }

        private void SetFilters(PropertyDescriptor property, FilterInfoList value) {
            if(property != null)
                property.SetValue(settings, value);
        }

        private static string GetPropertyName(Expression<Func<TSettings, FilterInfoList>> expression) {
            var memberExpression = expression.Body as MemberExpression;
            if(memberExpression == null) {
                throw new ArgumentException("expression");
            }
            return memberExpression.Member.Name;
        }
    }
}
