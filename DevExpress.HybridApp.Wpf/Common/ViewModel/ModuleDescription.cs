using System;

namespace DevExpress.DevAV.Common.ViewModel
{
    /// <summary>
    /// A base class representing a navigation list entry.
    /// </summary>
    /// <typeparam name="TModule">A navigation list entry type.</typeparam>
    public abstract class ModuleDescription<TModule> where TModule : ModuleDescription<TModule>
    {
        private readonly Func<TModule, object> _peekCollectionViewModelFactory;
        private object _peekCollectionViewModel;

        /// <summary>
        /// Initializes a new instance of the ModuleDescription class.
        /// </summary>
        /// <param name="title">A navigation list entry display text.</param>
        /// <param name="documentType">A string value that specifies the view type of corresponding document.</param>
        /// <param name="group">A navigation list entry group name.</param>
        /// <param name="peekCollectionViewModelFactory">An optional parameter that provides a function used to create a PeekCollectionViewModel that provides quick navigation between collection views.</param>
        protected ModuleDescription(string title, string documentType, string group, Func<TModule, object> peekCollectionViewModelFactory = null)
        {
            ModuleTitle = title;
            ModuleGroup = group;
            DocumentType = documentType;
            _peekCollectionViewModelFactory = peekCollectionViewModelFactory;
        }

        /// <summary>
        /// The navigation list entry display text.
        /// </summary>
        public string ModuleTitle { get; private set; }

        /// <summary>
        /// The navigation list entry group name.
        /// </summary>
        public string ModuleGroup { get; private set; }

        /// <summary>
        /// Contains the corresponding document view type.
        /// </summary>
        public string DocumentType { get; private set; }

        /// <summary>
        /// A primary instance of corresponding PeekCollectionViewModel used to quick navigation between collection views.
        /// </summary>
        public object PeekCollectionViewModel
        {
            get
            {
                if (_peekCollectionViewModelFactory == null)
                    return null;
                return _peekCollectionViewModel ?? (_peekCollectionViewModel = CreatePeekCollectionViewModel());
            }
        }

        /// <summary>
        /// Creates and returns a new instance of the corresponding PeekCollectionViewModel that provides quick navigation between collection views.
        /// </summary>
        public object CreatePeekCollectionViewModel() => _peekCollectionViewModelFactory((TModule)this);
    }
}
