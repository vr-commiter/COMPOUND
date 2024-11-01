using System.Reflection;
using MelonLoader;

[assembly: AssemblyTitle(COMPOUND_TrueGear.BuildInfo.Description)]
[assembly: AssemblyDescription(COMPOUND_TrueGear.BuildInfo.Description)]
[assembly: AssemblyCompany(COMPOUND_TrueGear.BuildInfo.Company)]
[assembly: AssemblyProduct(COMPOUND_TrueGear.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + COMPOUND_TrueGear.BuildInfo.Author)]
[assembly: AssemblyTrademark(COMPOUND_TrueGear.BuildInfo.Company)]
[assembly: AssemblyVersion(COMPOUND_TrueGear.BuildInfo.Version)]
[assembly: AssemblyFileVersion(COMPOUND_TrueGear.BuildInfo.Version)]
[assembly: MelonInfo(typeof(COMPOUND_TrueGear.COMPOUND_TrueGear), COMPOUND_TrueGear.BuildInfo.Name, COMPOUND_TrueGear.BuildInfo.Version, COMPOUND_TrueGear.BuildInfo.Author, COMPOUND_TrueGear.BuildInfo.DownloadLink)]
[assembly: MelonColor()]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame(null, null)]