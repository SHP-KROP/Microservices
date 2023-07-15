﻿namespace AuthServer.Pages.Account.Register
{
    public class ViewModel
    {
        public bool AllowRememberLogin { get; set; } = true;
        public bool EnableLocalRegister { get; set; } = true;

        public IEnumerable<ViewModel.ExternalProvider> ExternalProviders { get; set; } =
            Enumerable.Empty<ExternalProvider>();

        public IEnumerable<ViewModel.ExternalProvider> VisibleExternalProviders =>
            ExternalProviders.Where(x => !String.IsNullOrWhiteSpace(x.DisplayName));

        public bool IsExternalLoginOnly => EnableLocalRegister == false && ExternalProviders?.Count() == 1;

        public string ExternalLoginScheme =>
            IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;

        public class ExternalProvider
        {
            public string DisplayName { get; set; }
            public string AuthenticationScheme { get; set; }
        }
    }
}
