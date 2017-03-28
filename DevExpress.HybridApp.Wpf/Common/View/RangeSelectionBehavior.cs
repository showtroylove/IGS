using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Editors.RangeControl;

namespace DevExpress.DevAV {
    public class RangeSelectionBehavior : Behavior<RangeControl> {
        public RangeSelectionBehavior() {
            MoveRangeLeftCommand = new DelegateCommand(() => MoveRangeLeft(), CanMoveRangeLeft);
            MoveRangeRightCommand = new DelegateCommand(() => MoveRangeRight(), CanMoveRangeRight);
        }

        private static readonly DependencyProperty SelectionStartProperty =
            DependencyProperty.Register("SelectionStart", typeof(object), typeof(RangeSelectionBehavior), new PropertyMetadata(null, (d, e) => ((RangeSelectionBehavior)d).SelectionChanged()));

        private static readonly DependencyProperty SelectionEndProperty =
            DependencyProperty.Register("SelectionEnd", typeof(object), typeof(RangeSelectionBehavior), new PropertyMetadata(null, (d, e) => ((RangeSelectionBehavior)d).SelectionChanged()));

        public ICommand MoveRangeLeftCommand { get; private set; }
        public ICommand MoveRangeRightCommand { get; private set; }
        public int MinimumRangeChange { get; set; }

        private void MoveRangeLeft() {
            var rangeChange = GetRangeChange();
            AssociatedObject.SelectionRangeStart = (DateTime)AssociatedObject.SelectionRangeStart - rangeChange;
            AssociatedObject.SelectionRangeEnd = (DateTime)AssociatedObject.SelectionRangeEnd - rangeChange;
        }

        private void MoveRangeRight() {
            var rangeChange = GetRangeChange();
            AssociatedObject.SelectionRangeEnd = (DateTime)AssociatedObject.SelectionRangeEnd + rangeChange;
            AssociatedObject.SelectionRangeStart = (DateTime)AssociatedObject.SelectionRangeStart + rangeChange;
        }

        private bool CanMoveRangeLeft() {
            return HasNullValues() ? false : (DateTime)AssociatedObject.SelectionRangeStart - (DateTime)AssociatedObject.RangeStart > GetRangeChange();
        }

        private bool CanMoveRangeRight() {
            return HasNullValues() ? false : (DateTime)AssociatedObject.RangeEnd - (DateTime)AssociatedObject.SelectionRangeEnd > GetRangeChange();
        }

        private TimeSpan GetRangeChange() {
            var rangeChange = (DateTime)AssociatedObject.SelectionRangeEnd - (DateTime)AssociatedObject.SelectionRangeStart;
            if(rangeChange.TotalDays < MinimumRangeChange)
                rangeChange = new TimeSpan(MinimumRangeChange, 0, 0, 0, 0);
            return rangeChange;
        }

        private bool HasNullValues() {
            return AssociatedObject == null || AssociatedObject.RangeStart == null || AssociatedObject.RangeEnd == null
                || AssociatedObject.SelectionRangeStart == null || AssociatedObject.SelectionRangeEnd == null;
        }

        protected override void OnAttached() {
            base.OnAttached();
            BindingOperations.SetBinding(this, SelectionStartProperty, new Binding(RangeControl.SelectionRangeStartProperty.Name) { Source = AssociatedObject });
            BindingOperations.SetBinding(this, SelectionEndProperty, new Binding(RangeControl.SelectionRangeEndProperty.Name) { Source = AssociatedObject });
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            BindingOperations.ClearBinding(this, SelectionStartProperty);
            BindingOperations.ClearBinding(this, SelectionEndProperty);
        }

        private void SelectionChanged() {
            ((DelegateCommand)MoveRangeLeftCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)MoveRangeRightCommand).RaiseCanExecuteChanged();
        }
    }
}
