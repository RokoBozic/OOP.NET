using PdfSharp.Fonts;
using System;
using System.IO;
using System.Reflection;

public class CustomFontResolver : IFontResolver
{
    public static readonly CustomFontResolver Instance = new CustomFontResolver();

    public string DefaultFontName => "Arial";

    public byte[] GetFont(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return faceName switch
        {
            "Arial#Regular" => LoadFontData("WorldCupWinForms.Resources.Arial.ttf"),
            "Arial#Bold" => LoadFontData("WorldCupWinForms.Resources.Arial-Bold.ttf"),
            _ => throw new ArgumentException($"Font face '{faceName}' is not available.")
        };
    }

    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        if (familyName.ToLower().Contains("arial"))
        {
            return isBold
                ? new FontResolverInfo("Arial#Bold")
                : new FontResolverInfo("Arial#Regular");
        }

        // fallback
        return new FontResolverInfo("Arial#Regular");
    }

    private byte[] LoadFontData(string resourceName)
    {
        using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Font resource not found: {resourceName}");

        using MemoryStream ms = new();
        stream.CopyTo(ms);
        return ms.ToArray();
    }
}


