using System.Globalization;
using System.Resources;

namespace Core.Utilities.Localization;

public class LocalizationManager
{
    private static ResourceManager resourceManager = new ResourceManager("Core.Resources.Resources", typeof(Resources.Resources).Assembly);

    public static string GetLocalizedMessages(string key, string culture)
    {
        return resourceManager.GetString(key, CultureInfo.GetCultureInfo(culture));
    }
}