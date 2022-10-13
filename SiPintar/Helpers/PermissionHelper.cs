using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace SiPintar.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class PermissionHelper
    {
        public static bool HasPermission(string AccessName)
        {
            bool result = false;

            if (Application.Current.Resources.Contains("Permissions") && Application.Current.Resources["Permissions"]?.ToString() != null)
            {
                var Permission = Application.Current.Resources["Permissions"].ToString();
                string[] PermissionList = Permission.Split(',');

                if (Array.IndexOf(PermissionList, AccessName) >= 0)
                    result = true;
            }

            return result;
        }

        public static bool HasPermission(List<string> AccessName) // Check Permission based on List (operation => OR)
        {
            bool result = false;

            if (Application.Current.Resources.Contains("Permissions") && Application.Current.Resources["Permissions"]?.ToString() != null)
            {
                var Permission = Application.Current.Resources["Permissions"].ToString();
                string[] PermissionList = Permission.Split(',');

                foreach (var item in AccessName)
                {
                    if (Array.IndexOf(PermissionList, item) >= 0)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
