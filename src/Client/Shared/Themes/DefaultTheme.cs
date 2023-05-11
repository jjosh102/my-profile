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
            defaultDarkPallete.Dark = "#161b22";
            defaultDarkPallete.Primary = "#EEEEEE";
            defaultDarkPallete.AppbarBackground = "#22252B";
            defaultDarkPallete.DarkContrastText = "#ecf2f8";
            defaultDarkPallete.Background = "#22252B";
            defaultDarkPallete.DrawerBackground = "#303841";
            defaultDarkPallete.Surface = "#2B333B";
            defaultDarkPallete.TextPrimary = "#ecf2f8";
            defaultDarkPallete.PrimaryContrastText = "161b22";
            defaultDarkPallete.AppbarText = "#EEEEEE";
            defaultDarkPallete.Tertiary = "#22252B";
      

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