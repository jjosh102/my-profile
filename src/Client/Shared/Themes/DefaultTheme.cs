using MudBlazor;
namespace MyProfile.Shared.Themes;
public static class DefaultTheme
{
     public static MudTheme Init()
        {
            var defaultTheme = new MudTheme();
            var defaultDarkPallete = defaultTheme.PaletteDark;
            var defaultLightPallete = defaultTheme.Palette;
            //Dark 
            defaultDarkPallete.Dark = "#202020";
            defaultDarkPallete.Primary = "#F5F5F5";
            defaultDarkPallete.AppbarBackground = "#121212";
            defaultDarkPallete.DarkContrastText = "#F5F5F5";
            defaultDarkPallete.Background = "#121212";
            defaultDarkPallete.DrawerBackground = "#2A2A2A";
            defaultDarkPallete.Surface = "#2A2A2A";
            defaultDarkPallete.TextPrimary = "#ecf2f8";
            defaultDarkPallete.PrimaryContrastText = "161b22";
            defaultDarkPallete.AppbarText = "#F5F5F5";
            defaultDarkPallete.Tertiary = "#121212";
      

            //Light
            defaultLightPallete.Primary = "#161b22";
            defaultLightPallete.PrimaryContrastText = "#EEEEEE";
            defaultLightPallete.AppbarText = "#EEEEEE";
            defaultLightPallete.TextPrimary = "#161b22";
            defaultLightPallete.AppbarBackground = "#FFFFFF";
            defaultLightPallete.Tertiary = "#EEEEEE";

            return defaultTheme;  
        }
    
}