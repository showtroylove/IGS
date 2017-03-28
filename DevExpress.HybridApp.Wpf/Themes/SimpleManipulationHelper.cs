using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using DevExpress.Xpf.Core.Native;

namespace DevExpress.DevAV.Themes {
    public interface ISimpleManupulationSupport {
        void ScrollBy(double x, double y, bool isMouseManipulation);
        void ScaleBy(double factor, bool isMouseManipulation);
        void FinishManipulation(bool isMouseManipulation);
        double DesiredDeseleration { get; }
    }
    public class SimpleManipulationHelper {
        #region Dependency Properties
        public static readonly DependencyProperty OverrideManipulationProperty;
        static SimpleManipulationHelper() {
            var ownerType = typeof(SimpleManipulationHelper);
            OverrideManipulationProperty = DependencyProperty.RegisterAttached("OverrideManipulation", typeof(bool), ownerType, new PropertyMetadata(false));
        }
        #endregion
        public static bool GetOverrideManipulation(DependencyObject d) { return (bool)d.GetValue(OverrideManipulationProperty); }
        public static void SetOverrideManipulation(DependencyObject d, bool value) { d.SetValue(OverrideManipulationProperty, value); }

        private Point lastPosition;
        private FrameworkElement owner;
        private bool manipulationInProgress;
        private DependencyPropertyDescriptor isMouseManipulationEnabledDescriptor;
        private bool mouseMoveHandled;
        private bool doNotProcessMouse = false;

        public SimpleManipulationHelper(FrameworkElement owner) {
            this.owner = owner;
            var pd = TypeDescriptor.GetProperties(this.owner)["IsMouseManipulationEnabled"];
            this.isMouseManipulationEnabledDescriptor = pd == null ? null : DependencyPropertyDescriptor.FromProperty(pd);
            this.owner.Loaded += OnOwnerLoaded;
            this.owner.Unloaded += OnOwnerUnloaded;
            this.owner.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(OnOwnerManipulationDelta);
            this.owner.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(OnOwnerManipulationCompleted);
            this.owner.ManipulationInertiaStarting += OnOwnerManipulationInertiaStarting;
        }

        private void OnOwnerLoaded(object sender, RoutedEventArgs e) {
            if(this.isMouseManipulationEnabledDescriptor != null)
                this.isMouseManipulationEnabledDescriptor.AddValueChanged(this.owner, OnOwnerIsMouseManipulationEnabledChanged);
            OnOwnerIsMouseManipulationEnabledChanged(this.owner, EventArgs.Empty);
        }

        private void OnOwnerUnloaded(object sender, RoutedEventArgs e) {
            if(this.isMouseManipulationEnabledDescriptor != null)
                this.isMouseManipulationEnabledDescriptor.RemoveValueChanged(this.owner, OnOwnerIsMouseManipulationEnabledChanged);
            UnsubscribeFromMouseEvents();
        }

        private void OnOwnerIsMouseManipulationEnabledChanged(object sender, EventArgs e) {
            var isManipulationEnabled = this.isMouseManipulationEnabledDescriptor == null ? false : (bool)this.isMouseManipulationEnabledDescriptor.GetValue(this.owner);
            if(isManipulationEnabled)
                SubscribeToMouseEvents();
            else
                UnsubscribeFromMouseEvents();
        }

        private void SubscribeToMouseEvents() {
            this.owner.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(OnOwnerMouseLeftButtonDown);
            this.owner.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(OnOwnerMouseLeftButtonUp);
            this.owner.PreviewMouseMove += new MouseEventHandler(OnOwnerMouseMove);
            this.owner.PreviewMouseWheel += new MouseWheelEventHandler(OnOwnerMouseWheel);
        }

        private void UnsubscribeFromMouseEvents() {
            this.owner.PreviewMouseLeftButtonDown -= new MouseButtonEventHandler(OnOwnerMouseLeftButtonDown);
            this.owner.PreviewMouseLeftButtonUp -= new MouseButtonEventHandler(OnOwnerMouseLeftButtonUp);
            this.owner.PreviewMouseMove -= new MouseEventHandler(OnOwnerMouseMove);
            this.owner.PreviewMouseWheel -= new MouseWheelEventHandler(OnOwnerMouseWheel);
        }

        private void OnOwnerManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e) {
            var sms = this.owner as ISimpleManupulationSupport;
            if(sms != null)
                e.TranslationBehavior.DesiredDeceleration = sms.DesiredDeseleration;
            e.Handled = true;
        }

        private void OnOwnerManipulationCompleted(object sender, ManipulationCompletedEventArgs e) {
            e.Handled = true;
            var sms = this.owner as ISimpleManupulationSupport;
            if(sms != null)
                sms.FinishManipulation(false);
            if(e.TotalManipulation.Translation.X == 0.0 && e.TotalManipulation.Translation.Y == 0.0 && e.TotalManipulation.Scale.X == 1.0 && e.TotalManipulation.Scale.Y == 1.0)
                RaiseClick();
        }

        private void OnOwnerManipulationDelta(object sender, ManipulationDeltaEventArgs e) {
            var sms = this.owner as ISimpleManupulationSupport;
            if(sms != null) {
                var sx = 1.0 + (e.DeltaManipulation.Scale.X - 1.0) / 1.0;
                var sy = 1.0 + (e.DeltaManipulation.Scale.Y - 1.0) / 1.0;
                var prec = 0.0005;
                var b1 = Math.Abs(sx - 1.0) <= prec;
                var b2 = Math.Abs(sy - 1.0) <= prec;
                if(!b1 || !b2)
                    sms.ScaleBy(sx, false);
                else
                    sms.ScrollBy(-e.DeltaManipulation.Translation.X, -e.DeltaManipulation.Translation.Y, false);
            }
            e.Handled = true;
        }

        private void RaiseClick() {
            var result = VisualTreeHelper.HitTest(this.owner, Mouse.GetPosition(this.owner));
            if(result == null) return;
            var element = GetUIVisualHit(result.VisualHit);
            if(RaiseButtonClick(element)) return;
            if(RaiseFlowDocumentClick(element)) return;
            if(element == null)
                element = this.owner;
            var down = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left) { RoutedEvent = Mouse.MouseDownEvent, Source = element };
            element.RaiseEvent(down);
            InputManager.Current.ProcessInput(down);
            var up = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left) { RoutedEvent = Mouse.MouseUpEvent, Source = element };
            element.RaiseEvent(up);
            InputManager.Current.ProcessInput(up);
        }

        private bool RaiseButtonClick(UIElement element) {
            var button = element == null ? null : LayoutHelper.FindParentObject<ButtonBase>(element);
            if(button == null) return false;
            var click = new RoutedEventArgs(ButtonBase.ClickEvent, button);
            button.RaiseEvent(click);
            var command = button.Command;
            if(command != null && command.CanExecute(button.CommandParameter) && button.IsEnabled)
                command.Execute(button.CommandParameter);
            return true;
        }

        private bool RaiseFlowDocumentClick(UIElement element) {
            if(element == null || element.GetType().FullName != "MS.Internal.Documents.FlowDocumentView") return false;
            var scrollViewer = LayoutHelper.FindParentObject<ScrollViewer>(element);
            var rtb = LayoutHelper.FindParentObject<RichTextBox>(element);
            if(scrollViewer == null || rtb == null || rtb.Document == null) return false;
            var position = Mouse.GetPosition(scrollViewer);
            foreach(var block in rtb.Document.Blocks) {
                var paragraph = block as System.Windows.Documents.Paragraph;
                if(paragraph == null) continue;
                foreach(var inline in paragraph.Inlines) {
                    var hyperlink = inline as System.Windows.Documents.Hyperlink;
                    if(hyperlink == null) continue;
                    var start = hyperlink.ContentStart.GetCharacterRect(System.Windows.Documents.LogicalDirection.Forward);
                    var end = hyperlink.ContentEnd.GetCharacterRect(System.Windows.Documents.LogicalDirection.Backward);
                    if(position.X < start.Left || position.X > end.Right) continue;
                    if(position.Y < start.Top || position.Y > end.Bottom) continue;
                    RaiseHyperlinkClick(hyperlink);
                    return true;
                }
            }
            return false;
        }

        private void RaiseHyperlinkClick(System.Windows.Documents.Hyperlink hyperlink) {
            var click = new RoutedEventArgs(System.Windows.Documents.Hyperlink.ClickEvent, hyperlink);
            hyperlink.RaiseEvent(click);
            var command = hyperlink.Command;
            if(command != null && command.CanExecute(hyperlink.CommandParameter) && hyperlink.IsEnabled)
                command.Execute(hyperlink.CommandParameter);
        }

        private UIElement GetUIVisualHit(DependencyObject d) {
            if(d == null) return null;
            var element = d as UIElement;
            if(element != null) return element;
            return LayoutHelper.FindParentObject<UIElement>(d);
        }

        private void OnOwnerMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if(doNotProcessMouse) return;
            var result = VisualTreeHelper.HitTest(this.owner, e.GetPosition(this.owner));
            var element = result == null ? null : result.VisualHit as UIElement;
            var dom = element == null ? null : LayoutHelper.FindLayoutOrVisualParentObject(element, d => GetOverrideManipulation(d));
            if(dom != null) return;
            if(!this.owner.CaptureMouse()) return;
            mouseMoveHandled = false;
            e.Handled = true;
            this.lastPosition = e.GetPosition(this.owner);
            this.manipulationInProgress = true;
        }

        private void OnOwnerMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            if(!this.manipulationInProgress) return;
            e.Handled = true;
            this.manipulationInProgress = false;
            this.owner.ReleaseMouseCapture();
            var sms = this.owner as ISimpleManupulationSupport;
            if(sms != null)
                sms.FinishManipulation(true);
            if(!mouseMoveHandled) {
                doNotProcessMouse = true;
                RaiseClick();
                doNotProcessMouse = false;
            }
        }

        private void OnOwnerMouseMove(object sender, MouseEventArgs e) {
            if(!this.manipulationInProgress) return;
            mouseMoveHandled = true;
            var newPosition = e.GetPosition(this.owner);
            var sms = this.owner as ISimpleManupulationSupport;
            if(sms != null)
                sms.ScrollBy(this.lastPosition.X - newPosition.X, this.lastPosition.Y - newPosition.Y, true);
            this.lastPosition = newPosition;
        }

        private void OnOwnerMouseWheel(object sender, MouseWheelEventArgs e) {
            if((Keyboard.Modifiers & ModifierKeys.Control) == 0) {
                e.Handled = true;
                var sms = this.owner as ISimpleManupulationSupport;
                if(sms != null) {
                    var rsv = this.owner as RenderScrollViewer;
                    if(rsv == null) return;
                    var vScroll = rsv.ComputedVerticalScrollBarVisibility == Visibility.Visible;
                    var hScroll = rsv.ComputedHorizontalScrollBarVisibility == Visibility.Visible;
                    var delta = -e.Delta * 1.0;
                    if(vScroll)
                        sms.ScrollBy(0, delta, true);
                    else if(hScroll)
                        sms.ScrollBy(delta, 0, true);
                }
            } else {
                e.Handled = true;
                var sms = this.owner as ISimpleManupulationSupport;
                if(sms != null)
                    sms.ScaleBy((double)e.Delta, true);
            }
        }
    }
}
