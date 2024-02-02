using DinkToPdf;
using DinkToPdf.Contracts;

public class PdfGenerator
{
	private readonly IConverter _converter;

	public PdfGenerator(IConverter converter)
	{
		_converter = converter;
	}

	public byte[] GeneratePdf(string htmlContent)
	{
		var globalSettings = new GlobalSettings
		{
			ColorMode = ColorMode.Color,
			Orientation = Orientation.Portrait,
			PaperSize = PaperKind.A4,
			Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 }
		};

		var objectSettings = new ObjectSettings
		{
			PagesCount = true,
			HtmlContent = htmlContent,
			WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css", "styles.css") },
			HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
			FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Footer" }
		};

		var pdf = _converter.Convert(new HtmlToPdfDocument
		{
			GlobalSettings = globalSettings,
			Objects = { objectSettings }
		});

		return pdf;
	}
}