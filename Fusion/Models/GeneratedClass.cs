using System;
using DocumentFormat.OpenXml.Packaging;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using Vt = DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using Pic = DocumentFormat.OpenXml.Drawing.Pictures;
using A14 = DocumentFormat.OpenXml.Office2010.Drawing;
using M = DocumentFormat.OpenXml.Math;
using Ovml = DocumentFormat.OpenXml.Vml.Office;
using V = DocumentFormat.OpenXml.Vml;
using System.IO;
namespace GeneratedCode
{
    public class GeneratedClass
    {
        public class Reclamation
        {
            public string organisation { get; set; }
            public string address { get; set; }
            public string restaurant { get; set; }
            public string vendor { get; set; }
            public DateTime date { get; set; }
            public string FIO { get; set; }
            public DateTime date_end { get; set; }
            public string nomenclature { get; set; }
            public int count { get; set; }
        }
        // Creates a WordprocessingDocument.
        public void CreatePackage(string filePath, Reclamation reclamation)
        {
            using (WordprocessingDocument package = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {
                CreateParts(package, reclamation);
            }
        }
        public byte[] CreatePackageAsBytes(Reclamation reclamation)
        {
            using (var mstm = new MemoryStream())
            {
                using (WordprocessingDocument package = WordprocessingDocument.Create(mstm, WordprocessingDocumentType.Document))
                {
                    CreateParts(package, reclamation);
                }
                mstm.Flush();
                mstm.Close();
                return mstm.ToArray();
            }
        }
        // Adds child parts and generates content of the specified part.
        private void CreateParts(WordprocessingDocument document, Reclamation reclamation)
        {
            ExtendedFilePropertiesPart extendedFilePropertiesPart1 = document.AddNewPart<ExtendedFilePropertiesPart>("rId3");
            GenerateExtendedFilePropertiesPart1Content(extendedFilePropertiesPart1);

            MainDocumentPart mainDocumentPart1 = document.AddMainDocumentPart();
            GenerateMainDocumentPart1Content(mainDocumentPart1, reclamation);

            ThemePart themePart1 = mainDocumentPart1.AddNewPart<ThemePart>("rId8");
            GenerateThemePart1Content(themePart1);

            WebSettingsPart webSettingsPart1 = mainDocumentPart1.AddNewPart<WebSettingsPart>("rId3");
            GenerateWebSettingsPart1Content(webSettingsPart1);

            FontTablePart fontTablePart1 = mainDocumentPart1.AddNewPart<FontTablePart>("rId7");
            GenerateFontTablePart1Content(fontTablePart1);

            DocumentSettingsPart documentSettingsPart1 = mainDocumentPart1.AddNewPart<DocumentSettingsPart>("rId2");
            GenerateDocumentSettingsPart1Content(documentSettingsPart1);

            StyleDefinitionsPart styleDefinitionsPart1 = mainDocumentPart1.AddNewPart<StyleDefinitionsPart>("rId1");
            GenerateStyleDefinitionsPart1Content(styleDefinitionsPart1);

            ImagePart imagePart1 = mainDocumentPart1.AddNewPart<ImagePart>("image/png", "rId4");
            GenerateImagePart1Content(imagePart1);

            mainDocumentPart1.AddHyperlinkRelationship(new System.Uri("mailto:avgirencko@tokyo-bar.ru", System.UriKind.Absolute), true, "rId6");
            mainDocumentPart1.AddHyperlinkRelationship(new System.Uri("mailto:iarudenko@tokyo-bar.ru", System.UriKind.Absolute), true, "rId5");
            SetPackageProperties(document);
        }

        // Generates content of extendedFilePropertiesPart1.
        private void GenerateExtendedFilePropertiesPart1Content(ExtendedFilePropertiesPart extendedFilePropertiesPart1)
        {
            Ap.Properties properties1 = new Ap.Properties();
            properties1.AddNamespaceDeclaration("vt", "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes");
            Ap.Template template1 = new Ap.Template();
            template1.Text = "Normal.dotm";
            Ap.TotalTime totalTime1 = new Ap.TotalTime();
            totalTime1.Text = "49";
            Ap.Pages pages1 = new Ap.Pages();
            pages1.Text = "1";
            Ap.Words words1 = new Ap.Words();
            words1.Text = "143";
            Ap.Characters characters1 = new Ap.Characters();
            characters1.Text = "820";
            Ap.Application application1 = new Ap.Application();
            application1.Text = "Microsoft Office Word";
            Ap.DocumentSecurity documentSecurity1 = new Ap.DocumentSecurity();
            documentSecurity1.Text = "0";
            Ap.Lines lines1 = new Ap.Lines();
            lines1.Text = "6";
            Ap.Paragraphs paragraphs1 = new Ap.Paragraphs();
            paragraphs1.Text = "1";
            Ap.ScaleCrop scaleCrop1 = new Ap.ScaleCrop();
            scaleCrop1.Text = "false";

            Ap.HeadingPairs headingPairs1 = new Ap.HeadingPairs();

            Vt.VTVector vTVector1 = new Vt.VTVector() { BaseType = Vt.VectorBaseValues.Variant, Size = (UInt32Value)2U };

            Vt.Variant variant1 = new Vt.Variant();
            Vt.VTLPSTR vTLPSTR1 = new Vt.VTLPSTR();
            vTLPSTR1.Text = "Название";

            variant1.Append(vTLPSTR1);

            Vt.Variant variant2 = new Vt.Variant();
            Vt.VTInt32 vTInt321 = new Vt.VTInt32();
            vTInt321.Text = "1";

            variant2.Append(vTInt321);

            vTVector1.Append(variant1);
            vTVector1.Append(variant2);

            headingPairs1.Append(vTVector1);

            Ap.TitlesOfParts titlesOfParts1 = new Ap.TitlesOfParts();

            Vt.VTVector vTVector2 = new Vt.VTVector() { BaseType = Vt.VectorBaseValues.Lpstr, Size = (UInt32Value)1U };
            Vt.VTLPSTR vTLPSTR2 = new Vt.VTLPSTR();
            vTLPSTR2.Text = "";

            vTVector2.Append(vTLPSTR2);

            titlesOfParts1.Append(vTVector2);
            Ap.Company company1 = new Ap.Company();
            company1.Text = "";
            Ap.LinksUpToDate linksUpToDate1 = new Ap.LinksUpToDate();
            linksUpToDate1.Text = "false";
            Ap.CharactersWithSpaces charactersWithSpaces1 = new Ap.CharactersWithSpaces();
            charactersWithSpaces1.Text = "962";
            Ap.SharedDocument sharedDocument1 = new Ap.SharedDocument();
            sharedDocument1.Text = "false";
            Ap.HyperlinksChanged hyperlinksChanged1 = new Ap.HyperlinksChanged();
            hyperlinksChanged1.Text = "false";
            Ap.ApplicationVersion applicationVersion1 = new Ap.ApplicationVersion();
            applicationVersion1.Text = "15.0000";

            properties1.Append(template1);
            properties1.Append(totalTime1);
            properties1.Append(pages1);
            properties1.Append(words1);
            properties1.Append(characters1);
            properties1.Append(application1);
            properties1.Append(documentSecurity1);
            properties1.Append(lines1);
            properties1.Append(paragraphs1);
            properties1.Append(scaleCrop1);
            properties1.Append(headingPairs1);
            properties1.Append(titlesOfParts1);
            properties1.Append(company1);
            properties1.Append(linksUpToDate1);
            properties1.Append(charactersWithSpaces1);
            properties1.Append(sharedDocument1);
            properties1.Append(hyperlinksChanged1);
            properties1.Append(applicationVersion1);

            extendedFilePropertiesPart1.Properties = properties1;
        }

        // Generates content of mainDocumentPart1.
        private void GenerateMainDocumentPart1Content(MainDocumentPart mainDocumentPart1, Reclamation reclamation)
        {
            Document document1 = new Document() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "w14 w15 wp14" } };
            document1.AddNamespaceDeclaration("wpc", "http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas");
            document1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            document1.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
            document1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            document1.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
            document1.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
            document1.AddNamespaceDeclaration("wp14", "http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing");
            document1.AddNamespaceDeclaration("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
            document1.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
            document1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            document1.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");
            document1.AddNamespaceDeclaration("w15", "http://schemas.microsoft.com/office/word/2012/wordml");
            document1.AddNamespaceDeclaration("wpg", "http://schemas.microsoft.com/office/word/2010/wordprocessingGroup");
            document1.AddNamespaceDeclaration("wpi", "http://schemas.microsoft.com/office/word/2010/wordprocessingInk");
            document1.AddNamespaceDeclaration("wne", "http://schemas.microsoft.com/office/word/2006/wordml");
            document1.AddNamespaceDeclaration("wps", "http://schemas.microsoft.com/office/word/2010/wordprocessingShape");

            Body body1 = new Body();

            Paragraph paragraph1 = new Paragraph() { RsidParagraphMarkRevision = "00D06234", RsidParagraphAddition = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            ParagraphProperties paragraphProperties1 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties1 = new ParagraphMarkRunProperties();
            Languages languages1 = new Languages() { Val = "en-US" };

            paragraphMarkRunProperties1.Append(languages1);

            paragraphProperties1.Append(paragraphMarkRunProperties1);

            Run run1 = new Run() { RsidRunProperties = "003B11C6" };

            RunProperties runProperties1 = new RunProperties();
            Bold bold1 = new Bold();
            NoProof noProof1 = new NoProof();
            FontSize fontSize1 = new FontSize() { Val = "32" };
            FontSizeComplexScript fontSizeComplexScript1 = new FontSizeComplexScript() { Val = "32" };
            Languages languages2 = new Languages() { EastAsia = "ru-RU" };

            runProperties1.Append(bold1);
            runProperties1.Append(noProof1);
            runProperties1.Append(fontSize1);
            runProperties1.Append(fontSizeComplexScript1);
            runProperties1.Append(languages2);

            Drawing drawing1 = new Drawing();

            Wp.Anchor anchor1 = new Wp.Anchor() { DistanceFromTop = (UInt32Value)0U, DistanceFromBottom = (UInt32Value)0U, DistanceFromLeft = (UInt32Value)114300U, DistanceFromRight = (UInt32Value)114300U, SimplePos = false, RelativeHeight = (UInt32Value)251658240U, BehindDoc = false, Locked = false, LayoutInCell = true, AllowOverlap = true };
            Wp.SimplePosition simplePosition1 = new Wp.SimplePosition() { X = 542925L, Y = 361950L };

            Wp.HorizontalPosition horizontalPosition1 = new Wp.HorizontalPosition() { RelativeFrom = Wp.HorizontalRelativePositionValues.Column };
            Wp.HorizontalAlignment horizontalAlignment1 = new Wp.HorizontalAlignment();
            horizontalAlignment1.Text = "left";

            horizontalPosition1.Append(horizontalAlignment1);

            Wp.VerticalPosition verticalPosition1 = new Wp.VerticalPosition() { RelativeFrom = Wp.VerticalRelativePositionValues.Paragraph };
            Wp.VerticalAlignment verticalAlignment1 = new Wp.VerticalAlignment();
            verticalAlignment1.Text = "top";

            verticalPosition1.Append(verticalAlignment1);
            Wp.Extent extent1 = new Wp.Extent() { Cx = 1390650L, Cy = 1218298L };
            Wp.EffectExtent effectExtent1 = new Wp.EffectExtent() { LeftEdge = 0L, TopEdge = 0L, RightEdge = 0L, BottomEdge = 1270L };
            Wp.WrapSquare wrapSquare1 = new Wp.WrapSquare() { WrapText = Wp.WrapTextValues.BothSides };
            Wp.DocProperties docProperties1 = new Wp.DocProperties() { Id = (UInt32Value)2U, Name = "Рисунок 2" };

            Wp.NonVisualGraphicFrameDrawingProperties nonVisualGraphicFrameDrawingProperties1 = new Wp.NonVisualGraphicFrameDrawingProperties();

            A.GraphicFrameLocks graphicFrameLocks1 = new A.GraphicFrameLocks() { NoChangeAspect = true };
            graphicFrameLocks1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            nonVisualGraphicFrameDrawingProperties1.Append(graphicFrameLocks1);

            A.Graphic graphic1 = new A.Graphic();
            graphic1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            A.GraphicData graphicData1 = new A.GraphicData() { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" };

            Pic.Picture picture1 = new Pic.Picture();
            picture1.AddNamespaceDeclaration("pic", "http://schemas.openxmlformats.org/drawingml/2006/picture");

            Pic.NonVisualPictureProperties nonVisualPictureProperties1 = new Pic.NonVisualPictureProperties();
            Pic.NonVisualDrawingProperties nonVisualDrawingProperties1 = new Pic.NonVisualDrawingProperties() { Id = (UInt32Value)0U, Name = "Picture 2" };

            Pic.NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties1 = new Pic.NonVisualPictureDrawingProperties();
            A.PictureLocks pictureLocks1 = new A.PictureLocks() { NoChangeAspect = true, NoChangeArrowheads = true };

            nonVisualPictureDrawingProperties1.Append(pictureLocks1);

            nonVisualPictureProperties1.Append(nonVisualDrawingProperties1);
            nonVisualPictureProperties1.Append(nonVisualPictureDrawingProperties1);

            Pic.BlipFill blipFill1 = new Pic.BlipFill();

            A.Blip blip1 = new A.Blip() { Embed = "rId4" };

            A.BlipExtensionList blipExtensionList1 = new A.BlipExtensionList();

            A.BlipExtension blipExtension1 = new A.BlipExtension() { Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}" };

            A14.UseLocalDpi useLocalDpi1 = new A14.UseLocalDpi() { Val = false };
            useLocalDpi1.AddNamespaceDeclaration("a14", "http://schemas.microsoft.com/office/drawing/2010/main");

            blipExtension1.Append(useLocalDpi1);

            blipExtensionList1.Append(blipExtension1);

            blip1.Append(blipExtensionList1);
            A.SourceRectangle sourceRectangle1 = new A.SourceRectangle();

            A.Stretch stretch1 = new A.Stretch();
            A.FillRectangle fillRectangle1 = new A.FillRectangle();

            stretch1.Append(fillRectangle1);

            blipFill1.Append(blip1);
            blipFill1.Append(sourceRectangle1);
            blipFill1.Append(stretch1);

            Pic.ShapeProperties shapeProperties1 = new Pic.ShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D1 = new A.Transform2D();
            A.Offset offset1 = new A.Offset() { X = 0L, Y = 0L };
            A.Extents extents1 = new A.Extents() { Cx = 1390650L, Cy = 1218298L };

            transform2D1.Append(offset1);
            transform2D1.Append(extents1);

            A.PresetGeometry presetGeometry1 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList1 = new A.AdjustValueList();

            presetGeometry1.Append(adjustValueList1);
            A.NoFill noFill1 = new A.NoFill();

            shapeProperties1.Append(transform2D1);
            shapeProperties1.Append(presetGeometry1);
            shapeProperties1.Append(noFill1);

            picture1.Append(nonVisualPictureProperties1);
            picture1.Append(blipFill1);
            picture1.Append(shapeProperties1);

            graphicData1.Append(picture1);

            graphic1.Append(graphicData1);

            anchor1.Append(simplePosition1);
            anchor1.Append(horizontalPosition1);
            anchor1.Append(verticalPosition1);
            anchor1.Append(extent1);
            anchor1.Append(effectExtent1);
            anchor1.Append(wrapSquare1);
            anchor1.Append(docProperties1);
            anchor1.Append(nonVisualGraphicFrameDrawingProperties1);
            anchor1.Append(graphic1);

            drawing1.Append(anchor1);

            run1.Append(runProperties1);
            run1.Append(drawing1);

            Run run2 = new Run() { RsidRunProperties = "003B11C6" };

            RunProperties runProperties2 = new RunProperties();
            Bold bold2 = new Bold();
            FontSize fontSize2 = new FontSize() { Val = "32" };
            FontSizeComplexScript fontSizeComplexScript2 = new FontSizeComplexScript() { Val = "32" };

            runProperties2.Append(bold2);
            runProperties2.Append(fontSize2);
            runProperties2.Append(fontSizeComplexScript2);
            Text text1 = new Text();
            text1.Text = "ООО";

            run2.Append(runProperties2);
            run2.Append(text1);

            Run run3 = new Run() { RsidRunProperties = "00D06234" };

            RunProperties runProperties3 = new RunProperties();
            Bold bold3 = new Bold();
            FontSize fontSize3 = new FontSize() { Val = "32" };
            FontSizeComplexScript fontSizeComplexScript3 = new FontSizeComplexScript() { Val = "32" };
            Languages languages3 = new Languages() { Val = "en-US" };

            runProperties3.Append(bold3);
            runProperties3.Append(fontSize3);
            runProperties3.Append(fontSizeComplexScript3);
            runProperties3.Append(languages3);
            Text text2 = new Text();
            text2.Text = "/";

            run3.Append(runProperties3);
            run3.Append(text2);

            Run run4 = new Run() { RsidRunProperties = "003B11C6" };

            RunProperties runProperties4 = new RunProperties();
            Bold bold4 = new Bold();
            FontSize fontSize4 = new FontSize() { Val = "32" };
            FontSizeComplexScript fontSizeComplexScript4 = new FontSizeComplexScript() { Val = "32" };

            runProperties4.Append(bold4);
            runProperties4.Append(fontSize4);
            runProperties4.Append(fontSizeComplexScript4);
            Text text3 = new Text();
            text3.Text = "ИП";

            run4.Append(runProperties4);
            run4.Append(text3);

            Run run5 = new Run() { RsidRunProperties = "00D06234", RsidRunAddition = "00D06234" };

            RunProperties runProperties5 = new RunProperties();
            Languages languages4 = new Languages() { Val = "en-US" };

            runProperties5.Append(languages4);
            Text text4 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text4.Text = " ";

            run5.Append(runProperties5);
            run5.Append(text4);

            Run run6 = new Run() { RsidRunAddition = "00705BC8" };

            RunProperties runProperties6 = new RunProperties();
            Languages languages5 = new Languages() { Val = "en-US" };

            runProperties6.Append(languages5);
            Text text5 = new Text();
            text5.Text = " " + reclamation.organisation;

            run6.Append(runProperties6);
            run6.Append(text5);

            paragraph1.Append(paragraphProperties1);
            paragraph1.Append(run1);
            paragraph1.Append(run2);
            paragraph1.Append(run3);
            paragraph1.Append(run4);
            paragraph1.Append(run5);
            paragraph1.Append(run6);

            Paragraph paragraph2 = new Paragraph() { RsidParagraphMarkRevision = "00D06234", RsidParagraphAddition = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            ParagraphProperties paragraphProperties2 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties2 = new ParagraphMarkRunProperties();
            Languages languages6 = new Languages() { Val = "en-US" };

            paragraphMarkRunProperties2.Append(languages6);

            paragraphProperties2.Append(paragraphMarkRunProperties2);

            Run run7 = new Run() { RsidRunProperties = "003B11C6" };

            RunProperties runProperties7 = new RunProperties();
            Bold bold5 = new Bold();
            FontSize fontSize5 = new FontSize() { Val = "32" };
            FontSizeComplexScript fontSizeComplexScript5 = new FontSizeComplexScript() { Val = "32" };

            runProperties7.Append(bold5);
            runProperties7.Append(fontSize5);
            runProperties7.Append(fontSizeComplexScript5);
            Text text6 = new Text();
            text6.Text = "Адрес";

            run7.Append(runProperties7);
            run7.Append(text6);

            Run run8 = new Run() { RsidRunAddition = "00705BC8" };

            RunProperties runProperties8 = new RunProperties();
            Languages languages7 = new Languages() { Val = "en-US" };

            runProperties8.Append(languages7);
            Text text7 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text7.Text = " " + reclamation.address;

            run8.Append(runProperties8);
            run8.Append(text7);

            paragraph2.Append(paragraphProperties2);
            paragraph2.Append(run7);
            paragraph2.Append(run8);

            Paragraph paragraph3 = new Paragraph() { RsidParagraphMarkRevision = "00D06234", RsidParagraphAddition = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            ParagraphProperties paragraphProperties3 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties3 = new ParagraphMarkRunProperties();
            Languages languages8 = new Languages() { Val = "en-US" };

            paragraphMarkRunProperties3.Append(languages8);

            paragraphProperties3.Append(paragraphMarkRunProperties3);

            Run run9 = new Run() { RsidRunProperties = "003B11C6" };

            RunProperties runProperties9 = new RunProperties();
            Bold bold6 = new Bold();
            FontSize fontSize6 = new FontSize() { Val = "32" };
            FontSizeComplexScript fontSizeComplexScript6 = new FontSizeComplexScript() { Val = "32" };

            runProperties9.Append(bold6);
            runProperties9.Append(fontSize6);
            runProperties9.Append(fontSizeComplexScript6);
            Text text8 = new Text();
            text8.Text = "Ресторан";

            run9.Append(runProperties9);
            run9.Append(text8);

            Run run10 = new Run() { RsidRunAddition = "00705BC8" };

            RunProperties runProperties10 = new RunProperties();
            Languages languages9 = new Languages() { Val = "en-US" };

            runProperties10.Append(languages9);
            Text text9 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text9.Text = " " + reclamation.restaurant;

            run10.Append(runProperties10);
            run10.Append(text9);

            Run run11 = new Run() { RsidRunProperties = "00D06234" };

            RunProperties runProperties11 = new RunProperties();
            Languages languages10 = new Languages() { Val = "en-US" };

            runProperties11.Append(languages10);
            Break break1 = new Break() { Type = BreakValues.TextWrapping, Clear = BreakTextRestartLocationValues.All };

            run11.Append(runProperties11);
            run11.Append(break1);

            paragraph3.Append(paragraphProperties3);
            paragraph3.Append(run9);
            paragraph3.Append(run10);
            paragraph3.Append(run11);

            Paragraph paragraph4 = new Paragraph() { RsidParagraphMarkRevision = "001A1126", RsidParagraphAddition = "003B11C6", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            ParagraphProperties paragraphProperties4 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties4 = new ParagraphMarkRunProperties();
            Languages languages11 = new Languages() { Val = "en-US" };

            paragraphMarkRunProperties4.Append(languages11);

            paragraphProperties4.Append(paragraphMarkRunProperties4);

            Run run12 = new Run() { RsidRunProperties = "00D81B6E" };

            RunProperties runProperties12 = new RunProperties();
            RunFonts runFonts1 = new RunFonts() { Ascii = "Calibri", HighAnsi = "Calibri", ComplexScript = "Calibri" };
            Kern kern1 = new Kern() { Val = (UInt32Value)2U };
            FontSize fontSize7 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript7 = new FontSizeComplexScript() { Val = "24" };
            Languages languages12 = new Languages() { EastAsia = "ar-SA" };

            runProperties12.Append(runFonts1);
            runProperties12.Append(kern1);
            runProperties12.Append(fontSize7);
            runProperties12.Append(fontSizeComplexScript7);
            runProperties12.Append(languages12);
            Text text10 = new Text();
            text10.Text = "от";

            run12.Append(runProperties12);
            run12.Append(text10);

            Run run13 = new Run() { RsidRunProperties = "001A1126" };

            RunProperties runProperties13 = new RunProperties();
            RunFonts runFonts2 = new RunFonts() { Ascii = "Plantagenet Cherokee", HighAnsi = "Plantagenet Cherokee" };
            Kern kern2 = new Kern() { Val = (UInt32Value)2U };
            FontSize fontSize8 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript8 = new FontSizeComplexScript() { Val = "24" };
            Languages languages13 = new Languages() { Val = "en-US", EastAsia = "ar-SA" };

            runProperties13.Append(runFonts2);
            runProperties13.Append(kern2);
            runProperties13.Append(fontSize8);
            runProperties13.Append(fontSizeComplexScript8);
            runProperties13.Append(languages13);
            Text text11 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text11.Text = " ";

            run13.Append(runProperties13);
            run13.Append(text11);

            Run run14 = new Run() { RsidRunProperties = "001A1126" };

            RunProperties runProperties14 = new RunProperties();
            RunFonts runFonts3 = new RunFonts() { Ascii = "Plantagenet Cherokee", HighAnsi = "Plantagenet Cherokee", ComplexScript = "Plantagenet Cherokee" };
            Kern kern3 = new Kern() { Val = (UInt32Value)2U };
            FontSize fontSize9 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript9 = new FontSizeComplexScript() { Val = "24" };
            Languages languages14 = new Languages() { Val = "en-US", EastAsia = "ar-SA" };

            runProperties14.Append(runFonts3);
            runProperties14.Append(kern3);
            runProperties14.Append(fontSize9);
            runProperties14.Append(fontSizeComplexScript9);
            runProperties14.Append(languages14);
            Text text12 = new Text();
            text12.Text = "«";

            run14.Append(runProperties14);
            run14.Append(text12);

            Run run15 = new Run() { RsidRunAddition = "00705BC8" };

            RunProperties runProperties15 = new RunProperties();
            Kern kern4 = new Kern() { Val = (UInt32Value)2U };
            FontSize fontSize10 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript10 = new FontSizeComplexScript() { Val = "24" };
            Languages languages15 = new Languages() { Val = "en-US", EastAsia = "ar-SA" };

            runProperties15.Append(kern4);
            runProperties15.Append(fontSize10);
            runProperties15.Append(fontSizeComplexScript10);
            runProperties15.Append(languages15);
            Text text13 = new Text();
            text13.Text = reclamation.date.Day.ToString();

            run15.Append(runProperties15);
            run15.Append(text13);

            Run run16 = new Run() { RsidRunProperties = "001A1126" };

            RunProperties runProperties16 = new RunProperties();
            RunFonts runFonts4 = new RunFonts() { Ascii = "Plantagenet Cherokee", HighAnsi = "Plantagenet Cherokee" };
            Kern kern5 = new Kern() { Val = (UInt32Value)2U };
            FontSize fontSize11 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript11 = new FontSizeComplexScript() { Val = "24" };
            Languages languages16 = new Languages() { Val = "en-US", EastAsia = "ar-SA" };

            runProperties16.Append(runFonts4);
            runProperties16.Append(kern5);
            runProperties16.Append(fontSize11);
            runProperties16.Append(fontSizeComplexScript11);
            runProperties16.Append(languages16);
            Text text14 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text14.Text = "» ";

            run16.Append(runProperties16);
            run16.Append(text14);

            Run run17 = new Run() { RsidRunAddition = "00705BC8" };

            RunProperties runProperties17 = new RunProperties();
            RunFonts runFonts5 = new RunFonts() { Ascii = "Cambria", HighAnsi = "Cambria", ComplexScript = "Cambria" };
            Kern kern6 = new Kern() { Val = (UInt32Value)2U };
            FontSize fontSize12 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript12 = new FontSizeComplexScript() { Val = "24" };
            Languages languages17 = new Languages() { Val = "en-US", EastAsia = "ar-SA" };

            runProperties17.Append(runFonts5);
            runProperties17.Append(kern6);
            runProperties17.Append(fontSize12);
            runProperties17.Append(fontSizeComplexScript12);
            runProperties17.Append(languages17);
            Text text15 = new Text();
            text15.Text = reclamation.date.Month.ToString();

            run17.Append(runProperties17);
            run17.Append(text15);

            Run run18 = new Run() { RsidRunProperties = "001A1126" };

            RunProperties runProperties18 = new RunProperties();
            RunFonts runFonts6 = new RunFonts() { Ascii = "Plantagenet Cherokee", HighAnsi = "Plantagenet Cherokee" };
            Kern kern7 = new Kern() { Val = (UInt32Value)2U };
            FontSize fontSize13 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript13 = new FontSizeComplexScript() { Val = "24" };
            Languages languages18 = new Languages() { Val = "en-US", EastAsia = "ar-SA" };

            runProperties18.Append(runFonts6);
            runProperties18.Append(kern7);
            runProperties18.Append(fontSize13);
            runProperties18.Append(fontSizeComplexScript13);
            runProperties18.Append(languages18);
            Text text16 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text16.Text = " 2017 ";

            run18.Append(runProperties18);
            run18.Append(text16);

            Run run19 = new Run() { RsidRunProperties = "00D81B6E" };

            RunProperties runProperties19 = new RunProperties();
            RunFonts runFonts7 = new RunFonts() { Ascii = "Cambria", HighAnsi = "Cambria", ComplexScript = "Cambria" };
            Kern kern8 = new Kern() { Val = (UInt32Value)2U };
            FontSize fontSize14 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript14 = new FontSizeComplexScript() { Val = "24" };
            Languages languages19 = new Languages() { EastAsia = "ar-SA" };

            runProperties19.Append(runFonts7);
            runProperties19.Append(kern8);
            runProperties19.Append(fontSize14);
            runProperties19.Append(fontSizeComplexScript14);
            runProperties19.Append(languages19);
            Text text17 = new Text();
            text17.Text = "г";

            run19.Append(runProperties19);
            run19.Append(text17);

            paragraph4.Append(paragraphProperties4);
            paragraph4.Append(run12);
            paragraph4.Append(run13);
            paragraph4.Append(run14);
            paragraph4.Append(run15);
            paragraph4.Append(run16);
            paragraph4.Append(run17);
            paragraph4.Append(run18);
            paragraph4.Append(run19);

            Paragraph paragraph5 = new Paragraph() { RsidParagraphMarkRevision = "001A1126", RsidParagraphAddition = "003B11C6", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            ParagraphProperties paragraphProperties5 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties5 = new ParagraphMarkRunProperties();
            Languages languages20 = new Languages() { Val = "en-US" };

            paragraphMarkRunProperties5.Append(languages20);

            paragraphProperties5.Append(paragraphMarkRunProperties5);

            paragraph5.Append(paragraphProperties5);

            Paragraph paragraph6 = new Paragraph() { RsidParagraphMarkRevision = "003B11C6", RsidParagraphAddition = "003B11C6", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            ParagraphProperties paragraphProperties6 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId1 = new ParagraphStyleId() { Val = "HTML" };
            Justification justification1 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties6 = new ParagraphMarkRunProperties();
            RunFonts runFonts8 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize15 = new FontSize() { Val = "36" };
            FontSizeComplexScript fontSizeComplexScript15 = new FontSizeComplexScript() { Val = "36" };
            Languages languages21 = new Languages() { EastAsia = "ru-RU" };

            paragraphMarkRunProperties6.Append(runFonts8);
            paragraphMarkRunProperties6.Append(fontSize15);
            paragraphMarkRunProperties6.Append(fontSizeComplexScript15);
            paragraphMarkRunProperties6.Append(languages21);

            paragraphProperties6.Append(paragraphStyleId1);
            paragraphProperties6.Append(justification1);
            paragraphProperties6.Append(paragraphMarkRunProperties6);

            Run run20 = new Run() { RsidRunProperties = "001A1126" };

            RunProperties runProperties20 = new RunProperties();
            Languages languages22 = new Languages() { Val = "en-US" };

            runProperties20.Append(languages22);
            TabChar tabChar1 = new TabChar();

            run20.Append(runProperties20);
            run20.Append(tabChar1);

            Run run21 = new Run() { RsidRunProperties = "003B11C6" };

            RunProperties runProperties21 = new RunProperties();
            RunFonts runFonts9 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            Bold bold7 = new Bold();
            BoldComplexScript boldComplexScript1 = new BoldComplexScript();
            FontSize fontSize16 = new FontSize() { Val = "36" };
            FontSizeComplexScript fontSizeComplexScript16 = new FontSizeComplexScript() { Val = "36" };
            Languages languages23 = new Languages() { EastAsia = "ru-RU" };

            runProperties21.Append(runFonts9);
            runProperties21.Append(bold7);
            runProperties21.Append(boldComplexScript1);
            runProperties21.Append(fontSize16);
            runProperties21.Append(fontSizeComplexScript16);
            runProperties21.Append(languages23);
            Text text18 = new Text();
            text18.Text = "Рекламация";

            run21.Append(runProperties21);
            run21.Append(text18);

            paragraph6.Append(paragraphProperties6);
            paragraph6.Append(run20);
            paragraph6.Append(run21);

            Paragraph paragraph7 = new Paragraph() { RsidParagraphAddition = "003B11C6", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            ParagraphProperties paragraphProperties7 = new ParagraphProperties();

            Tabs tabs1 = new Tabs();
            TabStop tabStop1 = new TabStop() { Val = TabStopValues.Left, Position = 3405 };

            tabs1.Append(tabStop1);

            paragraphProperties7.Append(tabs1);

            paragraph7.Append(paragraphProperties7);

            Paragraph paragraph8 = new Paragraph() { RsidParagraphMarkRevision = "00D06234", RsidParagraphAddition = "003B11C6", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            ParagraphProperties paragraphProperties8 = new ParagraphProperties();

            Tabs tabs2 = new Tabs();
            TabStop tabStop2 = new TabStop() { Val = TabStopValues.Left, Position = 916 };
            TabStop tabStop3 = new TabStop() { Val = TabStopValues.Left, Position = 1832 };
            TabStop tabStop4 = new TabStop() { Val = TabStopValues.Left, Position = 2748 };
            TabStop tabStop5 = new TabStop() { Val = TabStopValues.Left, Position = 3664 };
            TabStop tabStop6 = new TabStop() { Val = TabStopValues.Left, Position = 4580 };
            TabStop tabStop7 = new TabStop() { Val = TabStopValues.Left, Position = 5496 };
            TabStop tabStop8 = new TabStop() { Val = TabStopValues.Left, Position = 6412 };
            TabStop tabStop9 = new TabStop() { Val = TabStopValues.Left, Position = 7328 };
            TabStop tabStop10 = new TabStop() { Val = TabStopValues.Left, Position = 8244 };
            TabStop tabStop11 = new TabStop() { Val = TabStopValues.Left, Position = 9160 };
            TabStop tabStop12 = new TabStop() { Val = TabStopValues.Left, Position = 10076 };
            TabStop tabStop13 = new TabStop() { Val = TabStopValues.Left, Position = 10992 };
            TabStop tabStop14 = new TabStop() { Val = TabStopValues.Left, Position = 11908 };
            TabStop tabStop15 = new TabStop() { Val = TabStopValues.Left, Position = 12824 };
            TabStop tabStop16 = new TabStop() { Val = TabStopValues.Left, Position = 13740 };
            TabStop tabStop17 = new TabStop() { Val = TabStopValues.Left, Position = 14656 };

            tabs2.Append(tabStop2);
            tabs2.Append(tabStop3);
            tabs2.Append(tabStop4);
            tabs2.Append(tabStop5);
            tabs2.Append(tabStop6);
            tabs2.Append(tabStop7);
            tabs2.Append(tabStop8);
            tabs2.Append(tabStop9);
            tabs2.Append(tabStop10);
            tabs2.Append(tabStop11);
            tabs2.Append(tabStop12);
            tabs2.Append(tabStop13);
            tabs2.Append(tabStop14);
            tabs2.Append(tabStop15);
            tabs2.Append(tabStop16);
            tabs2.Append(tabStop17);
            SpacingBetweenLines spacingBetweenLines1 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

            ParagraphMarkRunProperties paragraphMarkRunProperties7 = new ParagraphMarkRunProperties();
            RunFonts runFonts10 = new RunFonts() { Ascii = "Courier New", HighAnsi = "Courier New", EastAsia = "Times New Roman", ComplexScript = "Courier New" };
            Languages languages24 = new Languages() { EastAsia = "ru-RU" };

            paragraphMarkRunProperties7.Append(runFonts10);
            paragraphMarkRunProperties7.Append(languages24);

            paragraphProperties8.Append(tabs2);
            paragraphProperties8.Append(spacingBetweenLines1);
            paragraphProperties8.Append(paragraphMarkRunProperties7);

            Run run22 = new Run() { RsidRunProperties = "003B11C6" };

            RunProperties runProperties22 = new RunProperties();
            RunFonts runFonts11 = new RunFonts() { Ascii = "Courier New", HighAnsi = "Courier New", EastAsia = "Times New Roman", ComplexScript = "Courier New" };
            Languages languages25 = new Languages() { EastAsia = "ru-RU" };

            runProperties22.Append(runFonts11);
            runProperties22.Append(languages25);
            Text text19 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text19.Text = "ПОСТАВЩИК ";

            run22.Append(runProperties22);
            run22.Append(text19);

            Run run23 = new Run() { RsidRunAddition = "00D06234" };

            RunProperties runProperties23 = new RunProperties();
            RunFonts runFonts12 = new RunFonts() { Ascii = "Courier New", HighAnsi = "Courier New", EastAsia = "Times New Roman", ComplexScript = "Courier New" };
            Languages languages26 = new Languages() { Val = "en-US", EastAsia = "ru-RU" };

            runProperties23.Append(runFonts12);
            runProperties23.Append(languages26);
            Text text20 = new Text();
            text20.Text = " " + reclamation.vendor;

            run23.Append(runProperties23);
            run23.Append(text20);

            paragraph8.Append(paragraphProperties8);
            paragraph8.Append(run22);
            paragraph8.Append(run23);
            Paragraph paragraph9 = new Paragraph() { RsidParagraphAddition = "003B11C6", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };
            Paragraph paragraph10 = new Paragraph() { RsidParagraphAddition = "003B11C6", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            Paragraph paragraph11 = new Paragraph() { RsidParagraphMarkRevision = "00D06234", RsidParagraphAddition = "003B11C6", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            ParagraphProperties paragraphProperties9 = new ParagraphProperties();

            Tabs tabs3 = new Tabs();
            TabStop tabStop18 = new TabStop() { Val = TabStopValues.Left, Position = 6150 };

            tabs3.Append(tabStop18);

            paragraphProperties9.Append(tabs3);

            Run run24 = new Run();
            TabChar tabChar2 = new TabChar();
            Text text21 = new Text();
            text21.Text = "ФИО составителя";

            run24.Append(tabChar2);
            run24.Append(text21);

            Run run25 = new Run() { RsidRunAddition = "00705BC8" };
            Text text22 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text22.Text = " ";

            run25.Append(text22);

            Run run26 = new Run() { RsidRunAddition = "00D06234" };

            RunProperties runProperties24 = new RunProperties();
            Languages languages27 = new Languages() { Val = "en-US" };

            runProperties24.Append(languages27);
            Text text23 = new Text();
            text23.Text = reclamation.FIO;

            run26.Append(runProperties24);
            run26.Append(text23);

            paragraph11.Append(paragraphProperties9);
            paragraph11.Append(run24);
            paragraph11.Append(run25);
            paragraph11.Append(run26);
            Paragraph paragraph12 = new Paragraph() { RsidParagraphMarkRevision = "003B11C6", RsidParagraphAddition = "003B11C6", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };
            Paragraph paragraph13 = new Paragraph() { RsidParagraphAddition = "003B11C6", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            Paragraph paragraph14 = new Paragraph() { RsidParagraphMarkRevision = "005E3022", RsidParagraphAddition = "003B11C6", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            ParagraphProperties paragraphProperties10 = new ParagraphProperties();
            Indentation indentation1 = new Indentation() { FirstLine = "708" };

            ParagraphMarkRunProperties paragraphMarkRunProperties8 = new ParagraphMarkRunProperties();
            Color color1 = new Color() { Val = "000000" };
            FontSize fontSize17 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript17 = new FontSizeComplexScript() { Val = "27" };
            Shading shading1 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            paragraphMarkRunProperties8.Append(color1);
            paragraphMarkRunProperties8.Append(fontSize17);
            paragraphMarkRunProperties8.Append(fontSizeComplexScript17);
            paragraphMarkRunProperties8.Append(shading1);

            paragraphProperties10.Append(indentation1);
            paragraphProperties10.Append(paragraphMarkRunProperties8);

            Run run27 = new Run();

            RunProperties runProperties25 = new RunProperties();
            Color color2 = new Color() { Val = "000000" };
            FontSize fontSize18 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript18 = new FontSizeComplexScript() { Val = "27" };
            Shading shading2 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            runProperties25.Append(color2);
            runProperties25.Append(fontSize18);
            runProperties25.Append(fontSizeComplexScript18);
            runProperties25.Append(shading2);
            Text text24 = new Text();
            text24.Text = "Дата поставки/номер накладной";

            run27.Append(runProperties25);
            run27.Append(text24);

            Run run28 = new Run() { RsidRunAddition = "00705BC8" };

            RunProperties runProperties26 = new RunProperties();
            Color color3 = new Color() { Val = "000000" };
            FontSize fontSize19 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript19 = new FontSizeComplexScript() { Val = "27" };
            Shading shading3 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            runProperties26.Append(color3);
            runProperties26.Append(fontSize19);
            runProperties26.Append(fontSizeComplexScript19);
            runProperties26.Append(shading3);
            Text text25 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text25.Text = " ";

            run28.Append(runProperties26);
            run28.Append(text25);

            Run run29 = new Run() { RsidRunAddition = "005E3022" };

            RunProperties runProperties27 = new RunProperties();
            Color color4 = new Color() { Val = "000000" };
            FontSize fontSize20 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript20 = new FontSizeComplexScript() { Val = "27" };
            Shading shading4 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };
            Languages languages28 = new Languages() { Val = "en-US" };

            runProperties27.Append(color4);
            runProperties27.Append(fontSize20);
            runProperties27.Append(fontSizeComplexScript20);
            runProperties27.Append(shading4);
            runProperties27.Append(languages28);
            Text text26 = new Text();
            text26.Text = " " + reclamation.date_end.ToString("dd.MM.yyyy");

            run29.Append(runProperties27);
            run29.Append(text26);

            paragraph14.Append(paragraphProperties10);
            paragraph14.Append(run27);
            paragraph14.Append(run28);
            paragraph14.Append(run29);

            Paragraph paragraph15 = new Paragraph() { RsidParagraphMarkRevision = "001A1126", RsidParagraphAddition = "00E13137", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            ParagraphProperties paragraphProperties11 = new ParagraphProperties();
            Indentation indentation2 = new Indentation() { FirstLine = "708" };

            ParagraphMarkRunProperties paragraphMarkRunProperties9 = new ParagraphMarkRunProperties();
            Color color5 = new Color() { Val = "000000" };
            FontSize fontSize21 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript21 = new FontSizeComplexScript() { Val = "27" };
            Shading shading5 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            paragraphMarkRunProperties9.Append(color5);
            paragraphMarkRunProperties9.Append(fontSize21);
            paragraphMarkRunProperties9.Append(fontSizeComplexScript21);
            paragraphMarkRunProperties9.Append(shading5);

            paragraphProperties11.Append(indentation2);
            paragraphProperties11.Append(paragraphMarkRunProperties9);

            Run run30 = new Run();

            RunProperties runProperties28 = new RunProperties();
            Color color6 = new Color() { Val = "000000" };
            FontSize fontSize22 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript22 = new FontSizeComplexScript() { Val = "27" };
            Shading shading6 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            runProperties28.Append(color6);
            runProperties28.Append(fontSize22);
            runProperties28.Append(fontSizeComplexScript22);
            runProperties28.Append(shading6);
            Text text27 = new Text();
            text27.Text = "Наименование товара";

            run30.Append(runProperties28);
            run30.Append(text27);

            Run run31 = new Run() { RsidRunAddition = "001A1126" };

            RunProperties runProperties29 = new RunProperties();
            Color color7 = new Color() { Val = "000000" };
            FontSize fontSize23 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript23 = new FontSizeComplexScript() { Val = "27" };
            Shading shading7 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            runProperties29.Append(color7);
            runProperties29.Append(fontSize23);
            runProperties29.Append(fontSizeComplexScript23);
            runProperties29.Append(shading7);
            Text text28 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text28.Text = " " + reclamation.nomenclature;

            run31.Append(runProperties29);
            run31.Append(text28);
            BookmarkStart bookmarkStart1 = new BookmarkStart() { Name = "_GoBack", Id = "0" };
            BookmarkEnd bookmarkEnd1 = new BookmarkEnd() { Id = "0" };

            paragraph15.Append(paragraphProperties11);
            paragraph15.Append(run30);
            paragraph15.Append(run31);
            paragraph15.Append(bookmarkStart1);
            paragraph15.Append(bookmarkEnd1);

            Paragraph paragraph16 = new Paragraph() { RsidParagraphMarkRevision = "00D06234", RsidParagraphAddition = "003B11C6", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            ParagraphProperties paragraphProperties12 = new ParagraphProperties();
            Indentation indentation3 = new Indentation() { FirstLine = "708" };

            ParagraphMarkRunProperties paragraphMarkRunProperties10 = new ParagraphMarkRunProperties();
            Color color8 = new Color() { Val = "000000" };
            FontSize fontSize24 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript24 = new FontSizeComplexScript() { Val = "27" };
            Shading shading8 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            paragraphMarkRunProperties10.Append(color8);
            paragraphMarkRunProperties10.Append(fontSize24);
            paragraphMarkRunProperties10.Append(fontSizeComplexScript24);
            paragraphMarkRunProperties10.Append(shading8);

            paragraphProperties12.Append(indentation3);
            paragraphProperties12.Append(paragraphMarkRunProperties10);

            Run run32 = new Run();

            RunProperties runProperties30 = new RunProperties();
            Color color9 = new Color() { Val = "000000" };
            FontSize fontSize25 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript25 = new FontSizeComplexScript() { Val = "27" };
            Shading shading9 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            runProperties30.Append(color9);
            runProperties30.Append(fontSize25);
            runProperties30.Append(fontSizeComplexScript25);
            runProperties30.Append(shading9);
            Text text29 = new Text();
            text29.Text = "Количество";

            run32.Append(runProperties30);
            run32.Append(text29);

            Run run33 = new Run() { RsidRunAddition = "00705BC8" };

            RunProperties runProperties31 = new RunProperties();
            Color color10 = new Color() { Val = "000000" };
            FontSize fontSize26 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript26 = new FontSizeComplexScript() { Val = "27" };
            Shading shading10 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            runProperties31.Append(color10);
            runProperties31.Append(fontSize26);
            runProperties31.Append(fontSizeComplexScript26);
            runProperties31.Append(shading10);
            Text text30 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text30.Text = " ";

            run33.Append(runProperties31);
            run33.Append(text30);

            Run run34 = new Run() { RsidRunAddition = "00D06234" };

            RunProperties runProperties32 = new RunProperties();
            Color color11 = new Color() { Val = "000000" };
            FontSize fontSize27 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript27 = new FontSizeComplexScript() { Val = "27" };
            Shading shading11 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };
            Languages languages29 = new Languages() { Val = "en-US" };

            runProperties32.Append(color11);
            runProperties32.Append(fontSize27);
            runProperties32.Append(fontSizeComplexScript27);
            runProperties32.Append(shading11);
            runProperties32.Append(languages29);
            Text text31 = new Text();
            text31.Text = " " + reclamation.count.ToString();

            run34.Append(runProperties32);
            run34.Append(text31);

            paragraph16.Append(paragraphProperties12);
            paragraph16.Append(run32);
            paragraph16.Append(run33);
            paragraph16.Append(run34);

            Paragraph paragraph17 = new Paragraph() { RsidParagraphAddition = "003B11C6", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            ParagraphProperties paragraphProperties13 = new ParagraphProperties();
            Indentation indentation4 = new Indentation() { FirstLine = "708" };

            ParagraphMarkRunProperties paragraphMarkRunProperties11 = new ParagraphMarkRunProperties();
            Color color12 = new Color() { Val = "000000" };
            FontSize fontSize28 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript28 = new FontSizeComplexScript() { Val = "27" };
            Shading shading12 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            paragraphMarkRunProperties11.Append(color12);
            paragraphMarkRunProperties11.Append(fontSize28);
            paragraphMarkRunProperties11.Append(fontSizeComplexScript28);
            paragraphMarkRunProperties11.Append(shading12);

            paragraphProperties13.Append(indentation4);
            paragraphProperties13.Append(paragraphMarkRunProperties11);

            Run run35 = new Run();

            RunProperties runProperties33 = new RunProperties();
            Color color13 = new Color() { Val = "000000" };
            FontSize fontSize29 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript29 = new FontSizeComplexScript() { Val = "27" };
            Shading shading13 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            runProperties33.Append(color13);
            runProperties33.Append(fontSize29);
            runProperties33.Append(fontSizeComplexScript29);
            runProperties33.Append(shading13);
            Text text32 = new Text();
            text32.Text = "Причина претензии_____________________________________________________";

            run35.Append(runProperties33);
            run35.Append(text32);

            paragraph17.Append(paragraphProperties13);
            paragraph17.Append(run35);

            Paragraph paragraph18 = new Paragraph() { RsidParagraphAddition = "003B11C6", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            ParagraphProperties paragraphProperties14 = new ParagraphProperties();
            Indentation indentation5 = new Indentation() { FirstLine = "708" };

            ParagraphMarkRunProperties paragraphMarkRunProperties12 = new ParagraphMarkRunProperties();
            Color color14 = new Color() { Val = "000000" };
            FontSize fontSize30 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript30 = new FontSizeComplexScript() { Val = "27" };
            Shading shading14 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            paragraphMarkRunProperties12.Append(color14);
            paragraphMarkRunProperties12.Append(fontSize30);
            paragraphMarkRunProperties12.Append(fontSizeComplexScript30);
            paragraphMarkRunProperties12.Append(shading14);

            paragraphProperties14.Append(indentation5);
            paragraphProperties14.Append(paragraphMarkRunProperties12);

            Run run36 = new Run();

            RunProperties runProperties34 = new RunProperties();
            Color color15 = new Color() { Val = "000000" };
            FontSize fontSize31 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript31 = new FontSizeComplexScript() { Val = "27" };
            Shading shading15 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            runProperties34.Append(color15);
            runProperties34.Append(fontSize31);
            runProperties34.Append(fontSizeComplexScript31);
            runProperties34.Append(shading15);
            Text text33 = new Text();
            text33.Text = "______________________________________________________________________";

            run36.Append(runProperties34);
            run36.Append(text33);

            paragraph18.Append(paragraphProperties14);
            paragraph18.Append(run36);

            Paragraph paragraph19 = new Paragraph() { RsidParagraphAddition = "003B11C6", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            ParagraphProperties paragraphProperties15 = new ParagraphProperties();
            Indentation indentation6 = new Indentation() { FirstLine = "708" };

            ParagraphMarkRunProperties paragraphMarkRunProperties13 = new ParagraphMarkRunProperties();
            Color color16 = new Color() { Val = "000000" };
            FontSize fontSize32 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript32 = new FontSizeComplexScript() { Val = "27" };
            Shading shading16 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            paragraphMarkRunProperties13.Append(color16);
            paragraphMarkRunProperties13.Append(fontSize32);
            paragraphMarkRunProperties13.Append(fontSizeComplexScript32);
            paragraphMarkRunProperties13.Append(shading16);

            paragraphProperties15.Append(indentation6);
            paragraphProperties15.Append(paragraphMarkRunProperties13);

            Run run37 = new Run();

            RunProperties runProperties35 = new RunProperties();
            Color color17 = new Color() { Val = "000000" };
            FontSize fontSize33 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript33 = new FontSizeComplexScript() { Val = "27" };
            Shading shading17 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            runProperties35.Append(color17);
            runProperties35.Append(fontSize33);
            runProperties35.Append(fontSizeComplexScript33);
            runProperties35.Append(shading17);
            Text text34 = new Text();
            text34.Text = "______________________________________________________________________";

            run37.Append(runProperties35);
            run37.Append(text34);

            paragraph19.Append(paragraphProperties15);
            paragraph19.Append(run37);

            Paragraph paragraph20 = new Paragraph() { RsidParagraphAddition = "003B11C6", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            ParagraphProperties paragraphProperties16 = new ParagraphProperties();
            Indentation indentation7 = new Indentation() { FirstLine = "708" };

            ParagraphMarkRunProperties paragraphMarkRunProperties14 = new ParagraphMarkRunProperties();
            Color color18 = new Color() { Val = "000000" };
            FontSize fontSize34 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript34 = new FontSizeComplexScript() { Val = "27" };
            Shading shading18 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            paragraphMarkRunProperties14.Append(color18);
            paragraphMarkRunProperties14.Append(fontSize34);
            paragraphMarkRunProperties14.Append(fontSizeComplexScript34);
            paragraphMarkRunProperties14.Append(shading18);

            paragraphProperties16.Append(indentation7);
            paragraphProperties16.Append(paragraphMarkRunProperties14);

            Run run38 = new Run();

            RunProperties runProperties36 = new RunProperties();
            Color color19 = new Color() { Val = "000000" };
            FontSize fontSize35 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript35 = new FontSizeComplexScript() { Val = "27" };
            Shading shading19 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            runProperties36.Append(color19);
            runProperties36.Append(fontSize35);
            runProperties36.Append(fontSizeComplexScript35);
            runProperties36.Append(shading19);
            Text text35 = new Text();
            text35.Text = " Сумма убытков_________________________________________________________";

            run38.Append(runProperties36);
            run38.Append(text35);

            paragraph20.Append(paragraphProperties16);
            paragraph20.Append(run38);

            Paragraph paragraph21 = new Paragraph() { RsidParagraphAddition = "00B81BC8", RsidParagraphProperties = "003B11C6", RsidRunAdditionDefault = "003B11C6" };

            ParagraphProperties paragraphProperties17 = new ParagraphProperties();
            Indentation indentation8 = new Indentation() { FirstLine = "708" };

            paragraphProperties17.Append(indentation8);

            Run run39 = new Run();

            RunProperties runProperties37 = new RunProperties();
            Color color20 = new Color() { Val = "000000" };
            FontSize fontSize36 = new FontSize() { Val = "27" };
            FontSizeComplexScript fontSizeComplexScript36 = new FontSizeComplexScript() { Val = "27" };
            Shading shading20 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "F1F1F1" };

            runProperties37.Append(color20);
            runProperties37.Append(fontSize36);
            runProperties37.Append(fontSizeComplexScript36);
            runProperties37.Append(shading20);
            Text text36 = new Text();
            text36.Text = "Требование к поставщику________________________________________________";

            run39.Append(runProperties37);
            run39.Append(text36);

            paragraph21.Append(paragraphProperties17);
            paragraph21.Append(run39);
            Paragraph paragraph22 = new Paragraph() { RsidParagraphMarkRevision = "00B81BC8", RsidParagraphAddition = "00B81BC8", RsidParagraphProperties = "00B81BC8", RsidRunAdditionDefault = "00B81BC8" };
            Paragraph paragraph23 = new Paragraph() { RsidParagraphAddition = "00B81BC8", RsidParagraphProperties = "00B81BC8", RsidRunAdditionDefault = "00B81BC8" };

            Paragraph paragraph24 = new Paragraph() { RsidParagraphMarkRevision = "00B81BC8", RsidParagraphAddition = "00B81BC8", RsidParagraphProperties = "00B81BC8", RsidRunAdditionDefault = "00B81BC8" };

            ParagraphProperties paragraphProperties18 = new ParagraphProperties();

            Tabs tabs4 = new Tabs();
            TabStop tabStop19 = new TabStop() { Val = TabStopValues.Left, Position = 916 };
            TabStop tabStop20 = new TabStop() { Val = TabStopValues.Left, Position = 1832 };
            TabStop tabStop21 = new TabStop() { Val = TabStopValues.Left, Position = 2748 };
            TabStop tabStop22 = new TabStop() { Val = TabStopValues.Left, Position = 3664 };
            TabStop tabStop23 = new TabStop() { Val = TabStopValues.Left, Position = 4580 };
            TabStop tabStop24 = new TabStop() { Val = TabStopValues.Left, Position = 5670 };
            TabStop tabStop25 = new TabStop() { Val = TabStopValues.Left, Position = 6412 };
            TabStop tabStop26 = new TabStop() { Val = TabStopValues.Left, Position = 7328 };
            TabStop tabStop27 = new TabStop() { Val = TabStopValues.Left, Position = 8244 };
            TabStop tabStop28 = new TabStop() { Val = TabStopValues.Left, Position = 9160 };
            TabStop tabStop29 = new TabStop() { Val = TabStopValues.Left, Position = 10076 };
            TabStop tabStop30 = new TabStop() { Val = TabStopValues.Left, Position = 10992 };
            TabStop tabStop31 = new TabStop() { Val = TabStopValues.Left, Position = 11908 };
            TabStop tabStop32 = new TabStop() { Val = TabStopValues.Left, Position = 12824 };
            TabStop tabStop33 = new TabStop() { Val = TabStopValues.Left, Position = 13740 };
            TabStop tabStop34 = new TabStop() { Val = TabStopValues.Left, Position = 14656 };

            tabs4.Append(tabStop19);
            tabs4.Append(tabStop20);
            tabs4.Append(tabStop21);
            tabs4.Append(tabStop22);
            tabs4.Append(tabStop23);
            tabs4.Append(tabStop24);
            tabs4.Append(tabStop25);
            tabs4.Append(tabStop26);
            tabs4.Append(tabStop27);
            tabs4.Append(tabStop28);
            tabs4.Append(tabStop29);
            tabs4.Append(tabStop30);
            tabs4.Append(tabStop31);
            tabs4.Append(tabStop32);
            tabs4.Append(tabStop33);
            tabs4.Append(tabStop34);
            SpacingBetweenLines spacingBetweenLines2 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

            ParagraphMarkRunProperties paragraphMarkRunProperties15 = new ParagraphMarkRunProperties();
            RunFonts runFonts13 = new RunFonts() { Ascii = "Courier New", HighAnsi = "Courier New", EastAsia = "Times New Roman", ComplexScript = "Courier New" };
            FontSize fontSize37 = new FontSize() { Val = "17" };
            FontSizeComplexScript fontSizeComplexScript37 = new FontSizeComplexScript() { Val = "17" };
            Languages languages30 = new Languages() { EastAsia = "ru-RU" };

            paragraphMarkRunProperties15.Append(runFonts13);
            paragraphMarkRunProperties15.Append(fontSize37);
            paragraphMarkRunProperties15.Append(fontSizeComplexScript37);
            paragraphMarkRunProperties15.Append(languages30);

            paragraphProperties18.Append(tabs4);
            paragraphProperties18.Append(spacingBetweenLines2);
            paragraphProperties18.Append(paragraphMarkRunProperties15);

            Run run40 = new Run() { RsidRunProperties = "00B81BC8" };

            RunProperties runProperties38 = new RunProperties();
            RunFonts runFonts14 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize38 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript38 = new FontSizeComplexScript() { Val = "24" };
            Languages languages31 = new Languages() { EastAsia = "ru-RU" };

            runProperties38.Append(runFonts14);
            runProperties38.Append(fontSize38);
            runProperties38.Append(fontSizeComplexScript38);
            runProperties38.Append(languages31);
            Text text37 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text37.Text = "Подпись: Представитель Поставщика (водитель, грузчик, иное лицо, передающее товар)                               ";

            run40.Append(runProperties38);
            run40.Append(text37);

            Run run41 = new Run() { RsidRunProperties = "00B81BC8" };

            RunProperties runProperties39 = new RunProperties();
            RunFonts runFonts15 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize39 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript39 = new FontSizeComplexScript() { Val = "24" };
            Highlight highlight1 = new Highlight() { Val = HighlightColorValues.LightGray };
            Languages languages32 = new Languages() { EastAsia = "ru-RU" };

            runProperties39.Append(runFonts15);
            runProperties39.Append(fontSize39);
            runProperties39.Append(fontSizeComplexScript39);
            runProperties39.Append(highlight1);
            runProperties39.Append(languages32);
            Text text38 = new Text();
            text38.Text = "___________________________________";

            run41.Append(runProperties39);
            run41.Append(text38);

            paragraph24.Append(paragraphProperties18);
            paragraph24.Append(run40);
            paragraph24.Append(run41);

            Paragraph paragraph25 = new Paragraph() { RsidParagraphAddition = "00B81BC8", RsidParagraphProperties = "00B81BC8", RsidRunAdditionDefault = "00B81BC8" };

            ParagraphProperties paragraphProperties19 = new ParagraphProperties();
            Indentation indentation9 = new Indentation() { FirstLine = "708" };

            paragraphProperties19.Append(indentation9);

            paragraph25.Append(paragraphProperties19);

            Paragraph paragraph26 = new Paragraph() { RsidParagraphAddition = "00B81BC8", RsidParagraphProperties = "00B81BC8", RsidRunAdditionDefault = "00B81BC8" };

            ParagraphProperties paragraphProperties20 = new ParagraphProperties();
            Indentation indentation10 = new Indentation() { FirstLine = "540" };
            Justification justification2 = new Justification() { Val = JustificationValues.Both };

            ParagraphMarkRunProperties paragraphMarkRunProperties16 = new ParagraphMarkRunProperties();
            BoldComplexScript boldComplexScript2 = new BoldComplexScript();

            paragraphMarkRunProperties16.Append(boldComplexScript2);

            paragraphProperties20.Append(indentation10);
            paragraphProperties20.Append(justification2);
            paragraphProperties20.Append(paragraphMarkRunProperties16);

            Run run42 = new Run() { RsidRunProperties = "002E27E4" };

            RunProperties runProperties40 = new RunProperties();
            BoldComplexScript boldComplexScript3 = new BoldComplexScript();

            runProperties40.Append(boldComplexScript3);
            Text text39 = new Text();
            text39.Text = "Подтверждающие документы прилагаю. Письменный ответ прошу направить по следующему адресу:";

            run42.Append(runProperties40);
            run42.Append(text39);

            Run run43 = new Run();

            RunProperties runProperties41 = new RunProperties();
            BoldComplexScript boldComplexScript4 = new BoldComplexScript();

            runProperties41.Append(boldComplexScript4);
            Text text40 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text40.Text = " ";

            run43.Append(runProperties41);
            run43.Append(text40);

            paragraph26.Append(paragraphProperties20);
            paragraph26.Append(run42);
            paragraph26.Append(run43);

            Paragraph paragraph27 = new Paragraph() { RsidParagraphAddition = "00B81BC8", RsidParagraphProperties = "00B81BC8", RsidRunAdditionDefault = "001A1126" };

            ParagraphProperties paragraphProperties21 = new ParagraphProperties();
            Indentation indentation11 = new Indentation() { FirstLine = "540" };
            Justification justification3 = new Justification() { Val = JustificationValues.Both };

            paragraphProperties21.Append(indentation11);
            paragraphProperties21.Append(justification3);

            Hyperlink hyperlink1 = new Hyperlink() { History = true, Id = "rId5" };

            Run run44 = new Run() { RsidRunProperties = "00864DD7", RsidRunAddition = "00B81BC8" };

            RunProperties runProperties42 = new RunProperties();
            RunStyle runStyle1 = new RunStyle() { Val = "a3" };

            runProperties42.Append(runStyle1);
            Text text41 = new Text();
            text41.Text = "iarudenko@tokyo-bar.ru";

            run44.Append(runProperties42);
            run44.Append(text41);

            hyperlink1.Append(run44);

            paragraph27.Append(paragraphProperties21);
            paragraph27.Append(hyperlink1);

            Paragraph paragraph28 = new Paragraph() { RsidParagraphAddition = "00B81BC8", RsidParagraphProperties = "00B81BC8", RsidRunAdditionDefault = "001A1126" };

            ParagraphProperties paragraphProperties22 = new ParagraphProperties();
            Indentation indentation12 = new Indentation() { FirstLine = "540" };
            Justification justification4 = new Justification() { Val = JustificationValues.Both };

            paragraphProperties22.Append(indentation12);
            paragraphProperties22.Append(justification4);

            Hyperlink hyperlink2 = new Hyperlink() { History = true, Id = "rId6" };

            Run run45 = new Run() { RsidRunProperties = "00864DD7", RsidRunAddition = "00B81BC8" };

            RunProperties runProperties43 = new RunProperties();
            RunStyle runStyle2 = new RunStyle() { Val = "a3" };

            runProperties43.Append(runStyle2);
            Text text42 = new Text();
            text42.Text = "avgirencko@tokyo-bar.ru";

            run45.Append(runProperties43);
            run45.Append(text42);

            hyperlink2.Append(run45);

            paragraph28.Append(paragraphProperties22);
            paragraph28.Append(hyperlink2);
            Paragraph paragraph29 = new Paragraph() { RsidParagraphAddition = "00B81BC8", RsidParagraphProperties = "00B81BC8", RsidRunAdditionDefault = "00B81BC8" };

            Paragraph paragraph30 = new Paragraph() { RsidParagraphMarkRevision = "00B81BC8", RsidParagraphAddition = "003B11C6", RsidParagraphProperties = "00B81BC8", RsidRunAdditionDefault = "00B81BC8" };

            ParagraphProperties paragraphProperties23 = new ParagraphProperties();

            Tabs tabs5 = new Tabs();
            TabStop tabStop35 = new TabStop() { Val = TabStopValues.Left, Position = 7740 };

            tabs5.Append(tabStop35);

            paragraphProperties23.Append(tabs5);

            Run run46 = new Run();
            TabChar tabChar3 = new TabChar();

            run46.Append(tabChar3);

            paragraph30.Append(paragraphProperties23);
            paragraph30.Append(run46);

            SectionProperties sectionProperties1 = new SectionProperties() { RsidRPr = "00B81BC8", RsidR = "003B11C6", RsidSect = "003B11C6" };
            PageSize pageSize1 = new PageSize() { Width = (UInt32Value)11906U, Height = (UInt32Value)16838U };
            PageMargin pageMargin1 = new PageMargin() { Top = 567, Right = (UInt32Value)850U, Bottom = 1134, Left = (UInt32Value)851U, Header = (UInt32Value)708U, Footer = (UInt32Value)708U, Gutter = (UInt32Value)0U };
            Columns columns1 = new Columns() { Space = "708" };
            DocGrid docGrid1 = new DocGrid() { LinePitch = 360 };

            sectionProperties1.Append(pageSize1);
            sectionProperties1.Append(pageMargin1);
            sectionProperties1.Append(columns1);
            sectionProperties1.Append(docGrid1);

            body1.Append(paragraph1);
            body1.Append(paragraph2);
            body1.Append(paragraph3);
            body1.Append(paragraph4);
            body1.Append(paragraph5);
            body1.Append(paragraph6);
            body1.Append(paragraph7);
            body1.Append(paragraph8);
            body1.Append(paragraph9);
            body1.Append(paragraph10);
            body1.Append(paragraph11);
            body1.Append(paragraph12);
            body1.Append(paragraph13);
            body1.Append(paragraph14);
            body1.Append(paragraph15);
            body1.Append(paragraph16);
            body1.Append(paragraph17);
            body1.Append(paragraph18);
            body1.Append(paragraph19);
            body1.Append(paragraph20);
            body1.Append(paragraph21);
            body1.Append(paragraph22);
            body1.Append(paragraph23);
            body1.Append(paragraph24);
            body1.Append(paragraph25);
            body1.Append(paragraph26);
            body1.Append(paragraph27);
            body1.Append(paragraph28);
            body1.Append(paragraph29);
            body1.Append(paragraph30);
            body1.Append(sectionProperties1);

            document1.Append(body1);

            mainDocumentPart1.Document = document1;
        }

        // Generates content of themePart1.
        private void GenerateThemePart1Content(ThemePart themePart1)
        {
            A.Theme theme1 = new A.Theme() { Name = "Тема Office" };
            theme1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            A.ThemeElements themeElements1 = new A.ThemeElements();

            A.ColorScheme colorScheme1 = new A.ColorScheme() { Name = "Стандартная" };

            A.Dark1Color dark1Color1 = new A.Dark1Color();
            A.SystemColor systemColor1 = new A.SystemColor() { Val = A.SystemColorValues.WindowText, LastColor = "000000" };

            dark1Color1.Append(systemColor1);

            A.Light1Color light1Color1 = new A.Light1Color();
            A.SystemColor systemColor2 = new A.SystemColor() { Val = A.SystemColorValues.Window, LastColor = "FFFFFF" };

            light1Color1.Append(systemColor2);

            A.Dark2Color dark2Color1 = new A.Dark2Color();
            A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex() { Val = "44546A" };

            dark2Color1.Append(rgbColorModelHex1);

            A.Light2Color light2Color1 = new A.Light2Color();
            A.RgbColorModelHex rgbColorModelHex2 = new A.RgbColorModelHex() { Val = "E7E6E6" };

            light2Color1.Append(rgbColorModelHex2);

            A.Accent1Color accent1Color1 = new A.Accent1Color();
            A.RgbColorModelHex rgbColorModelHex3 = new A.RgbColorModelHex() { Val = "5B9BD5" };

            accent1Color1.Append(rgbColorModelHex3);

            A.Accent2Color accent2Color1 = new A.Accent2Color();
            A.RgbColorModelHex rgbColorModelHex4 = new A.RgbColorModelHex() { Val = "ED7D31" };

            accent2Color1.Append(rgbColorModelHex4);

            A.Accent3Color accent3Color1 = new A.Accent3Color();
            A.RgbColorModelHex rgbColorModelHex5 = new A.RgbColorModelHex() { Val = "A5A5A5" };

            accent3Color1.Append(rgbColorModelHex5);

            A.Accent4Color accent4Color1 = new A.Accent4Color();
            A.RgbColorModelHex rgbColorModelHex6 = new A.RgbColorModelHex() { Val = "FFC000" };

            accent4Color1.Append(rgbColorModelHex6);

            A.Accent5Color accent5Color1 = new A.Accent5Color();
            A.RgbColorModelHex rgbColorModelHex7 = new A.RgbColorModelHex() { Val = "4472C4" };

            accent5Color1.Append(rgbColorModelHex7);

            A.Accent6Color accent6Color1 = new A.Accent6Color();
            A.RgbColorModelHex rgbColorModelHex8 = new A.RgbColorModelHex() { Val = "70AD47" };

            accent6Color1.Append(rgbColorModelHex8);

            A.Hyperlink hyperlink3 = new A.Hyperlink();
            A.RgbColorModelHex rgbColorModelHex9 = new A.RgbColorModelHex() { Val = "0563C1" };

            hyperlink3.Append(rgbColorModelHex9);

            A.FollowedHyperlinkColor followedHyperlinkColor1 = new A.FollowedHyperlinkColor();
            A.RgbColorModelHex rgbColorModelHex10 = new A.RgbColorModelHex() { Val = "954F72" };

            followedHyperlinkColor1.Append(rgbColorModelHex10);

            colorScheme1.Append(dark1Color1);
            colorScheme1.Append(light1Color1);
            colorScheme1.Append(dark2Color1);
            colorScheme1.Append(light2Color1);
            colorScheme1.Append(accent1Color1);
            colorScheme1.Append(accent2Color1);
            colorScheme1.Append(accent3Color1);
            colorScheme1.Append(accent4Color1);
            colorScheme1.Append(accent5Color1);
            colorScheme1.Append(accent6Color1);
            colorScheme1.Append(hyperlink3);
            colorScheme1.Append(followedHyperlinkColor1);

            A.FontScheme fontScheme1 = new A.FontScheme() { Name = "Стандартная" };

            A.MajorFont majorFont1 = new A.MajorFont();
            A.LatinFont latinFont1 = new A.LatinFont() { Typeface = "Calibri Light", Panose = "020F0302020204030204" };
            A.EastAsianFont eastAsianFont1 = new A.EastAsianFont() { Typeface = "" };
            A.ComplexScriptFont complexScriptFont1 = new A.ComplexScriptFont() { Typeface = "" };
            A.SupplementalFont supplementalFont1 = new A.SupplementalFont() { Script = "Jpan", Typeface = "ＭＳ ゴシック" };
            A.SupplementalFont supplementalFont2 = new A.SupplementalFont() { Script = "Hang", Typeface = "맑은 고딕" };
            A.SupplementalFont supplementalFont3 = new A.SupplementalFont() { Script = "Hans", Typeface = "宋体" };
            A.SupplementalFont supplementalFont4 = new A.SupplementalFont() { Script = "Hant", Typeface = "新細明體" };
            A.SupplementalFont supplementalFont5 = new A.SupplementalFont() { Script = "Arab", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont6 = new A.SupplementalFont() { Script = "Hebr", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont7 = new A.SupplementalFont() { Script = "Thai", Typeface = "Angsana New" };
            A.SupplementalFont supplementalFont8 = new A.SupplementalFont() { Script = "Ethi", Typeface = "Nyala" };
            A.SupplementalFont supplementalFont9 = new A.SupplementalFont() { Script = "Beng", Typeface = "Vrinda" };
            A.SupplementalFont supplementalFont10 = new A.SupplementalFont() { Script = "Gujr", Typeface = "Shruti" };
            A.SupplementalFont supplementalFont11 = new A.SupplementalFont() { Script = "Khmr", Typeface = "MoolBoran" };
            A.SupplementalFont supplementalFont12 = new A.SupplementalFont() { Script = "Knda", Typeface = "Tunga" };
            A.SupplementalFont supplementalFont13 = new A.SupplementalFont() { Script = "Guru", Typeface = "Raavi" };
            A.SupplementalFont supplementalFont14 = new A.SupplementalFont() { Script = "Cans", Typeface = "Euphemia" };
            A.SupplementalFont supplementalFont15 = new A.SupplementalFont() { Script = "Cher", Typeface = "Plantagenet Cherokee" };
            A.SupplementalFont supplementalFont16 = new A.SupplementalFont() { Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
            A.SupplementalFont supplementalFont17 = new A.SupplementalFont() { Script = "Tibt", Typeface = "Microsoft Himalaya" };
            A.SupplementalFont supplementalFont18 = new A.SupplementalFont() { Script = "Thaa", Typeface = "MV Boli" };
            A.SupplementalFont supplementalFont19 = new A.SupplementalFont() { Script = "Deva", Typeface = "Mangal" };
            A.SupplementalFont supplementalFont20 = new A.SupplementalFont() { Script = "Telu", Typeface = "Gautami" };
            A.SupplementalFont supplementalFont21 = new A.SupplementalFont() { Script = "Taml", Typeface = "Latha" };
            A.SupplementalFont supplementalFont22 = new A.SupplementalFont() { Script = "Syrc", Typeface = "Estrangelo Edessa" };
            A.SupplementalFont supplementalFont23 = new A.SupplementalFont() { Script = "Orya", Typeface = "Kalinga" };
            A.SupplementalFont supplementalFont24 = new A.SupplementalFont() { Script = "Mlym", Typeface = "Kartika" };
            A.SupplementalFont supplementalFont25 = new A.SupplementalFont() { Script = "Laoo", Typeface = "DokChampa" };
            A.SupplementalFont supplementalFont26 = new A.SupplementalFont() { Script = "Sinh", Typeface = "Iskoola Pota" };
            A.SupplementalFont supplementalFont27 = new A.SupplementalFont() { Script = "Mong", Typeface = "Mongolian Baiti" };
            A.SupplementalFont supplementalFont28 = new A.SupplementalFont() { Script = "Viet", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont29 = new A.SupplementalFont() { Script = "Uigh", Typeface = "Microsoft Uighur" };
            A.SupplementalFont supplementalFont30 = new A.SupplementalFont() { Script = "Geor", Typeface = "Sylfaen" };

            majorFont1.Append(latinFont1);
            majorFont1.Append(eastAsianFont1);
            majorFont1.Append(complexScriptFont1);
            majorFont1.Append(supplementalFont1);
            majorFont1.Append(supplementalFont2);
            majorFont1.Append(supplementalFont3);
            majorFont1.Append(supplementalFont4);
            majorFont1.Append(supplementalFont5);
            majorFont1.Append(supplementalFont6);
            majorFont1.Append(supplementalFont7);
            majorFont1.Append(supplementalFont8);
            majorFont1.Append(supplementalFont9);
            majorFont1.Append(supplementalFont10);
            majorFont1.Append(supplementalFont11);
            majorFont1.Append(supplementalFont12);
            majorFont1.Append(supplementalFont13);
            majorFont1.Append(supplementalFont14);
            majorFont1.Append(supplementalFont15);
            majorFont1.Append(supplementalFont16);
            majorFont1.Append(supplementalFont17);
            majorFont1.Append(supplementalFont18);
            majorFont1.Append(supplementalFont19);
            majorFont1.Append(supplementalFont20);
            majorFont1.Append(supplementalFont21);
            majorFont1.Append(supplementalFont22);
            majorFont1.Append(supplementalFont23);
            majorFont1.Append(supplementalFont24);
            majorFont1.Append(supplementalFont25);
            majorFont1.Append(supplementalFont26);
            majorFont1.Append(supplementalFont27);
            majorFont1.Append(supplementalFont28);
            majorFont1.Append(supplementalFont29);
            majorFont1.Append(supplementalFont30);

            A.MinorFont minorFont1 = new A.MinorFont();
            A.LatinFont latinFont2 = new A.LatinFont() { Typeface = "Calibri", Panose = "020F0502020204030204" };
            A.EastAsianFont eastAsianFont2 = new A.EastAsianFont() { Typeface = "" };
            A.ComplexScriptFont complexScriptFont2 = new A.ComplexScriptFont() { Typeface = "" };
            A.SupplementalFont supplementalFont31 = new A.SupplementalFont() { Script = "Jpan", Typeface = "ＭＳ 明朝" };
            A.SupplementalFont supplementalFont32 = new A.SupplementalFont() { Script = "Hang", Typeface = "맑은 고딕" };
            A.SupplementalFont supplementalFont33 = new A.SupplementalFont() { Script = "Hans", Typeface = "宋体" };
            A.SupplementalFont supplementalFont34 = new A.SupplementalFont() { Script = "Hant", Typeface = "新細明體" };
            A.SupplementalFont supplementalFont35 = new A.SupplementalFont() { Script = "Arab", Typeface = "Arial" };
            A.SupplementalFont supplementalFont36 = new A.SupplementalFont() { Script = "Hebr", Typeface = "Arial" };
            A.SupplementalFont supplementalFont37 = new A.SupplementalFont() { Script = "Thai", Typeface = "Cordia New" };
            A.SupplementalFont supplementalFont38 = new A.SupplementalFont() { Script = "Ethi", Typeface = "Nyala" };
            A.SupplementalFont supplementalFont39 = new A.SupplementalFont() { Script = "Beng", Typeface = "Vrinda" };
            A.SupplementalFont supplementalFont40 = new A.SupplementalFont() { Script = "Gujr", Typeface = "Shruti" };
            A.SupplementalFont supplementalFont41 = new A.SupplementalFont() { Script = "Khmr", Typeface = "DaunPenh" };
            A.SupplementalFont supplementalFont42 = new A.SupplementalFont() { Script = "Knda", Typeface = "Tunga" };
            A.SupplementalFont supplementalFont43 = new A.SupplementalFont() { Script = "Guru", Typeface = "Raavi" };
            A.SupplementalFont supplementalFont44 = new A.SupplementalFont() { Script = "Cans", Typeface = "Euphemia" };
            A.SupplementalFont supplementalFont45 = new A.SupplementalFont() { Script = "Cher", Typeface = "Plantagenet Cherokee" };
            A.SupplementalFont supplementalFont46 = new A.SupplementalFont() { Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
            A.SupplementalFont supplementalFont47 = new A.SupplementalFont() { Script = "Tibt", Typeface = "Microsoft Himalaya" };
            A.SupplementalFont supplementalFont48 = new A.SupplementalFont() { Script = "Thaa", Typeface = "MV Boli" };
            A.SupplementalFont supplementalFont49 = new A.SupplementalFont() { Script = "Deva", Typeface = "Mangal" };
            A.SupplementalFont supplementalFont50 = new A.SupplementalFont() { Script = "Telu", Typeface = "Gautami" };
            A.SupplementalFont supplementalFont51 = new A.SupplementalFont() { Script = "Taml", Typeface = "Latha" };
            A.SupplementalFont supplementalFont52 = new A.SupplementalFont() { Script = "Syrc", Typeface = "Estrangelo Edessa" };
            A.SupplementalFont supplementalFont53 = new A.SupplementalFont() { Script = "Orya", Typeface = "Kalinga" };
            A.SupplementalFont supplementalFont54 = new A.SupplementalFont() { Script = "Mlym", Typeface = "Kartika" };
            A.SupplementalFont supplementalFont55 = new A.SupplementalFont() { Script = "Laoo", Typeface = "DokChampa" };
            A.SupplementalFont supplementalFont56 = new A.SupplementalFont() { Script = "Sinh", Typeface = "Iskoola Pota" };
            A.SupplementalFont supplementalFont57 = new A.SupplementalFont() { Script = "Mong", Typeface = "Mongolian Baiti" };
            A.SupplementalFont supplementalFont58 = new A.SupplementalFont() { Script = "Viet", Typeface = "Arial" };
            A.SupplementalFont supplementalFont59 = new A.SupplementalFont() { Script = "Uigh", Typeface = "Microsoft Uighur" };
            A.SupplementalFont supplementalFont60 = new A.SupplementalFont() { Script = "Geor", Typeface = "Sylfaen" };

            minorFont1.Append(latinFont2);
            minorFont1.Append(eastAsianFont2);
            minorFont1.Append(complexScriptFont2);
            minorFont1.Append(supplementalFont31);
            minorFont1.Append(supplementalFont32);
            minorFont1.Append(supplementalFont33);
            minorFont1.Append(supplementalFont34);
            minorFont1.Append(supplementalFont35);
            minorFont1.Append(supplementalFont36);
            minorFont1.Append(supplementalFont37);
            minorFont1.Append(supplementalFont38);
            minorFont1.Append(supplementalFont39);
            minorFont1.Append(supplementalFont40);
            minorFont1.Append(supplementalFont41);
            minorFont1.Append(supplementalFont42);
            minorFont1.Append(supplementalFont43);
            minorFont1.Append(supplementalFont44);
            minorFont1.Append(supplementalFont45);
            minorFont1.Append(supplementalFont46);
            minorFont1.Append(supplementalFont47);
            minorFont1.Append(supplementalFont48);
            minorFont1.Append(supplementalFont49);
            minorFont1.Append(supplementalFont50);
            minorFont1.Append(supplementalFont51);
            minorFont1.Append(supplementalFont52);
            minorFont1.Append(supplementalFont53);
            minorFont1.Append(supplementalFont54);
            minorFont1.Append(supplementalFont55);
            minorFont1.Append(supplementalFont56);
            minorFont1.Append(supplementalFont57);
            minorFont1.Append(supplementalFont58);
            minorFont1.Append(supplementalFont59);
            minorFont1.Append(supplementalFont60);

            fontScheme1.Append(majorFont1);
            fontScheme1.Append(minorFont1);

            A.FormatScheme formatScheme1 = new A.FormatScheme() { Name = "Стандартная" };

            A.FillStyleList fillStyleList1 = new A.FillStyleList();

            A.SolidFill solidFill1 = new A.SolidFill();
            A.SchemeColor schemeColor1 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill1.Append(schemeColor1);

            A.GradientFill gradientFill1 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList1 = new A.GradientStopList();

            A.GradientStop gradientStop1 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor2 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.LuminanceModulation luminanceModulation1 = new A.LuminanceModulation() { Val = 110000 };
            A.SaturationModulation saturationModulation1 = new A.SaturationModulation() { Val = 105000 };
            A.Tint tint1 = new A.Tint() { Val = 67000 };

            schemeColor2.Append(luminanceModulation1);
            schemeColor2.Append(saturationModulation1);
            schemeColor2.Append(tint1);

            gradientStop1.Append(schemeColor2);

            A.GradientStop gradientStop2 = new A.GradientStop() { Position = 50000 };

            A.SchemeColor schemeColor3 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.LuminanceModulation luminanceModulation2 = new A.LuminanceModulation() { Val = 105000 };
            A.SaturationModulation saturationModulation2 = new A.SaturationModulation() { Val = 103000 };
            A.Tint tint2 = new A.Tint() { Val = 73000 };

            schemeColor3.Append(luminanceModulation2);
            schemeColor3.Append(saturationModulation2);
            schemeColor3.Append(tint2);

            gradientStop2.Append(schemeColor3);

            A.GradientStop gradientStop3 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor4 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.LuminanceModulation luminanceModulation3 = new A.LuminanceModulation() { Val = 105000 };
            A.SaturationModulation saturationModulation3 = new A.SaturationModulation() { Val = 109000 };
            A.Tint tint3 = new A.Tint() { Val = 81000 };

            schemeColor4.Append(luminanceModulation3);
            schemeColor4.Append(saturationModulation3);
            schemeColor4.Append(tint3);

            gradientStop3.Append(schemeColor4);

            gradientStopList1.Append(gradientStop1);
            gradientStopList1.Append(gradientStop2);
            gradientStopList1.Append(gradientStop3);
            A.LinearGradientFill linearGradientFill1 = new A.LinearGradientFill() { Angle = 5400000, Scaled = false };

            gradientFill1.Append(gradientStopList1);
            gradientFill1.Append(linearGradientFill1);

            A.GradientFill gradientFill2 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList2 = new A.GradientStopList();

            A.GradientStop gradientStop4 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor5 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.SaturationModulation saturationModulation4 = new A.SaturationModulation() { Val = 103000 };
            A.LuminanceModulation luminanceModulation4 = new A.LuminanceModulation() { Val = 102000 };
            A.Tint tint4 = new A.Tint() { Val = 94000 };

            schemeColor5.Append(saturationModulation4);
            schemeColor5.Append(luminanceModulation4);
            schemeColor5.Append(tint4);

            gradientStop4.Append(schemeColor5);

            A.GradientStop gradientStop5 = new A.GradientStop() { Position = 50000 };

            A.SchemeColor schemeColor6 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.SaturationModulation saturationModulation5 = new A.SaturationModulation() { Val = 110000 };
            A.LuminanceModulation luminanceModulation5 = new A.LuminanceModulation() { Val = 100000 };
            A.Shade shade1 = new A.Shade() { Val = 100000 };

            schemeColor6.Append(saturationModulation5);
            schemeColor6.Append(luminanceModulation5);
            schemeColor6.Append(shade1);

            gradientStop5.Append(schemeColor6);

            A.GradientStop gradientStop6 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor7 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.LuminanceModulation luminanceModulation6 = new A.LuminanceModulation() { Val = 99000 };
            A.SaturationModulation saturationModulation6 = new A.SaturationModulation() { Val = 120000 };
            A.Shade shade2 = new A.Shade() { Val = 78000 };

            schemeColor7.Append(luminanceModulation6);
            schemeColor7.Append(saturationModulation6);
            schemeColor7.Append(shade2);

            gradientStop6.Append(schemeColor7);

            gradientStopList2.Append(gradientStop4);
            gradientStopList2.Append(gradientStop5);
            gradientStopList2.Append(gradientStop6);
            A.LinearGradientFill linearGradientFill2 = new A.LinearGradientFill() { Angle = 5400000, Scaled = false };

            gradientFill2.Append(gradientStopList2);
            gradientFill2.Append(linearGradientFill2);

            fillStyleList1.Append(solidFill1);
            fillStyleList1.Append(gradientFill1);
            fillStyleList1.Append(gradientFill2);

            A.LineStyleList lineStyleList1 = new A.LineStyleList();

            A.Outline outline1 = new A.Outline() { Width = 6350, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill2 = new A.SolidFill();
            A.SchemeColor schemeColor8 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill2.Append(schemeColor8);
            A.PresetDash presetDash1 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };
            A.Miter miter1 = new A.Miter() { Limit = 800000 };

            outline1.Append(solidFill2);
            outline1.Append(presetDash1);
            outline1.Append(miter1);

            A.Outline outline2 = new A.Outline() { Width = 12700, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill3 = new A.SolidFill();
            A.SchemeColor schemeColor9 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill3.Append(schemeColor9);
            A.PresetDash presetDash2 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };
            A.Miter miter2 = new A.Miter() { Limit = 800000 };

            outline2.Append(solidFill3);
            outline2.Append(presetDash2);
            outline2.Append(miter2);

            A.Outline outline3 = new A.Outline() { Width = 19050, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill4 = new A.SolidFill();
            A.SchemeColor schemeColor10 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill4.Append(schemeColor10);
            A.PresetDash presetDash3 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };
            A.Miter miter3 = new A.Miter() { Limit = 800000 };

            outline3.Append(solidFill4);
            outline3.Append(presetDash3);
            outline3.Append(miter3);

            lineStyleList1.Append(outline1);
            lineStyleList1.Append(outline2);
            lineStyleList1.Append(outline3);

            A.EffectStyleList effectStyleList1 = new A.EffectStyleList();

            A.EffectStyle effectStyle1 = new A.EffectStyle();
            A.EffectList effectList1 = new A.EffectList();

            effectStyle1.Append(effectList1);

            A.EffectStyle effectStyle2 = new A.EffectStyle();
            A.EffectList effectList2 = new A.EffectList();

            effectStyle2.Append(effectList2);

            A.EffectStyle effectStyle3 = new A.EffectStyle();

            A.EffectList effectList3 = new A.EffectList();

            A.OuterShadow outerShadow1 = new A.OuterShadow() { BlurRadius = 57150L, Distance = 19050L, Direction = 5400000, Alignment = A.RectangleAlignmentValues.Center, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex11 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha1 = new A.Alpha() { Val = 63000 };

            rgbColorModelHex11.Append(alpha1);

            outerShadow1.Append(rgbColorModelHex11);

            effectList3.Append(outerShadow1);

            effectStyle3.Append(effectList3);

            effectStyleList1.Append(effectStyle1);
            effectStyleList1.Append(effectStyle2);
            effectStyleList1.Append(effectStyle3);

            A.BackgroundFillStyleList backgroundFillStyleList1 = new A.BackgroundFillStyleList();

            A.SolidFill solidFill5 = new A.SolidFill();
            A.SchemeColor schemeColor11 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill5.Append(schemeColor11);

            A.SolidFill solidFill6 = new A.SolidFill();

            A.SchemeColor schemeColor12 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint5 = new A.Tint() { Val = 95000 };
            A.SaturationModulation saturationModulation7 = new A.SaturationModulation() { Val = 170000 };

            schemeColor12.Append(tint5);
            schemeColor12.Append(saturationModulation7);

            solidFill6.Append(schemeColor12);

            A.GradientFill gradientFill3 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList3 = new A.GradientStopList();

            A.GradientStop gradientStop7 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor13 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint6 = new A.Tint() { Val = 93000 };
            A.SaturationModulation saturationModulation8 = new A.SaturationModulation() { Val = 150000 };
            A.Shade shade3 = new A.Shade() { Val = 98000 };
            A.LuminanceModulation luminanceModulation7 = new A.LuminanceModulation() { Val = 102000 };

            schemeColor13.Append(tint6);
            schemeColor13.Append(saturationModulation8);
            schemeColor13.Append(shade3);
            schemeColor13.Append(luminanceModulation7);

            gradientStop7.Append(schemeColor13);

            A.GradientStop gradientStop8 = new A.GradientStop() { Position = 50000 };

            A.SchemeColor schemeColor14 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint7 = new A.Tint() { Val = 98000 };
            A.SaturationModulation saturationModulation9 = new A.SaturationModulation() { Val = 130000 };
            A.Shade shade4 = new A.Shade() { Val = 90000 };
            A.LuminanceModulation luminanceModulation8 = new A.LuminanceModulation() { Val = 103000 };

            schemeColor14.Append(tint7);
            schemeColor14.Append(saturationModulation9);
            schemeColor14.Append(shade4);
            schemeColor14.Append(luminanceModulation8);

            gradientStop8.Append(schemeColor14);

            A.GradientStop gradientStop9 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor15 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade5 = new A.Shade() { Val = 63000 };
            A.SaturationModulation saturationModulation10 = new A.SaturationModulation() { Val = 120000 };

            schemeColor15.Append(shade5);
            schemeColor15.Append(saturationModulation10);

            gradientStop9.Append(schemeColor15);

            gradientStopList3.Append(gradientStop7);
            gradientStopList3.Append(gradientStop8);
            gradientStopList3.Append(gradientStop9);
            A.LinearGradientFill linearGradientFill3 = new A.LinearGradientFill() { Angle = 5400000, Scaled = false };

            gradientFill3.Append(gradientStopList3);
            gradientFill3.Append(linearGradientFill3);

            backgroundFillStyleList1.Append(solidFill5);
            backgroundFillStyleList1.Append(solidFill6);
            backgroundFillStyleList1.Append(gradientFill3);

            formatScheme1.Append(fillStyleList1);
            formatScheme1.Append(lineStyleList1);
            formatScheme1.Append(effectStyleList1);
            formatScheme1.Append(backgroundFillStyleList1);

            themeElements1.Append(colorScheme1);
            themeElements1.Append(fontScheme1);
            themeElements1.Append(formatScheme1);
            A.ObjectDefaults objectDefaults1 = new A.ObjectDefaults();
            A.ExtraColorSchemeList extraColorSchemeList1 = new A.ExtraColorSchemeList();

            A.ExtensionList extensionList1 = new A.ExtensionList();

            A.Extension extension1 = new A.Extension() { Uri = "{05A4C25C-085E-4340-85A3-A5531E510DB2}" };

            OpenXmlUnknownElement openXmlUnknownElement1 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<thm15:themeFamily xmlns:thm15=\"http://schemas.microsoft.com/office/thememl/2012/main\" name=\"Office Theme\" id=\"{62F939B6-93AF-4DB8-9C6B-D6C7DFDC589F}\" vid=\"{4A3C46E8-61CC-4603-A589-7422A47A8E4A}\" />");

            extension1.Append(openXmlUnknownElement1);

            extensionList1.Append(extension1);

            theme1.Append(themeElements1);
            theme1.Append(objectDefaults1);
            theme1.Append(extraColorSchemeList1);
            theme1.Append(extensionList1);

            themePart1.Theme = theme1;
        }

        // Generates content of webSettingsPart1.
        private void GenerateWebSettingsPart1Content(WebSettingsPart webSettingsPart1)
        {
            WebSettings webSettings1 = new WebSettings() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "w14 w15" } };
            webSettings1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            webSettings1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            webSettings1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            webSettings1.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");
            webSettings1.AddNamespaceDeclaration("w15", "http://schemas.microsoft.com/office/word/2012/wordml");
            OptimizeForBrowser optimizeForBrowser1 = new OptimizeForBrowser();
            AllowPNG allowPNG1 = new AllowPNG();

            webSettings1.Append(optimizeForBrowser1);
            webSettings1.Append(allowPNG1);

            webSettingsPart1.WebSettings = webSettings1;
        }

        // Generates content of fontTablePart1.
        private void GenerateFontTablePart1Content(FontTablePart fontTablePart1)
        {
            Fonts fonts1 = new Fonts() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "w14 w15" } };
            fonts1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            fonts1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            fonts1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            fonts1.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");
            fonts1.AddNamespaceDeclaration("w15", "http://schemas.microsoft.com/office/word/2012/wordml");

            Font font1 = new Font() { Name = "Calibri" };
            Panose1Number panose1Number1 = new Panose1Number() { Val = "020F0502020204030204" };
            FontCharSet fontCharSet1 = new FontCharSet() { Val = "CC" };
            FontFamily fontFamily1 = new FontFamily() { Val = FontFamilyValues.Swiss };
            Pitch pitch1 = new Pitch() { Val = FontPitchValues.Variable };
            FontSignature fontSignature1 = new FontSignature() { UnicodeSignature0 = "E0002AFF", UnicodeSignature1 = "C000247B", UnicodeSignature2 = "00000009", UnicodeSignature3 = "00000000", CodePageSignature0 = "000001FF", CodePageSignature1 = "00000000" };

            font1.Append(panose1Number1);
            font1.Append(fontCharSet1);
            font1.Append(fontFamily1);
            font1.Append(pitch1);
            font1.Append(fontSignature1);

            Font font2 = new Font() { Name = "Times New Roman" };
            Panose1Number panose1Number2 = new Panose1Number() { Val = "02020603050405020304" };
            FontCharSet fontCharSet2 = new FontCharSet() { Val = "CC" };
            FontFamily fontFamily2 = new FontFamily() { Val = FontFamilyValues.Roman };
            Pitch pitch2 = new Pitch() { Val = FontPitchValues.Variable };
            FontSignature fontSignature2 = new FontSignature() { UnicodeSignature0 = "E0002EFF", UnicodeSignature1 = "C000785B", UnicodeSignature2 = "00000009", UnicodeSignature3 = "00000000", CodePageSignature0 = "000001FF", CodePageSignature1 = "00000000" };

            font2.Append(panose1Number2);
            font2.Append(fontCharSet2);
            font2.Append(fontFamily2);
            font2.Append(pitch2);
            font2.Append(fontSignature2);

            Font font3 = new Font() { Name = "Consolas" };
            Panose1Number panose1Number3 = new Panose1Number() { Val = "020B0609020204030204" };
            FontCharSet fontCharSet3 = new FontCharSet() { Val = "CC" };
            FontFamily fontFamily3 = new FontFamily() { Val = FontFamilyValues.Modern };
            Pitch pitch3 = new Pitch() { Val = FontPitchValues.Fixed };
            FontSignature fontSignature3 = new FontSignature() { UnicodeSignature0 = "E00006FF", UnicodeSignature1 = "0000FCFF", UnicodeSignature2 = "00000001", UnicodeSignature3 = "00000000", CodePageSignature0 = "0000019F", CodePageSignature1 = "00000000" };

            font3.Append(panose1Number3);
            font3.Append(fontCharSet3);
            font3.Append(fontFamily3);
            font3.Append(pitch3);
            font3.Append(fontSignature3);

            Font font4 = new Font() { Name = "Plantagenet Cherokee" };
            AltName altName1 = new AltName() { Val = "Gadugi" };
            FontCharSet fontCharSet4 = new FontCharSet() { Val = "00" };
            FontFamily fontFamily4 = new FontFamily() { Val = FontFamilyValues.Roman };
            Pitch pitch4 = new Pitch() { Val = FontPitchValues.Variable };
            FontSignature fontSignature4 = new FontSignature() { UnicodeSignature0 = "00000003", UnicodeSignature1 = "00000000", UnicodeSignature2 = "00001000", UnicodeSignature3 = "00000000", CodePageSignature0 = "00000001", CodePageSignature1 = "00000000" };

            font4.Append(altName1);
            font4.Append(fontCharSet4);
            font4.Append(fontFamily4);
            font4.Append(pitch4);
            font4.Append(fontSignature4);

            Font font5 = new Font() { Name = "Cambria" };
            Panose1Number panose1Number4 = new Panose1Number() { Val = "02040503050406030204" };
            FontCharSet fontCharSet5 = new FontCharSet() { Val = "CC" };
            FontFamily fontFamily5 = new FontFamily() { Val = FontFamilyValues.Roman };
            Pitch pitch5 = new Pitch() { Val = FontPitchValues.Variable };
            FontSignature fontSignature5 = new FontSignature() { UnicodeSignature0 = "E00002FF", UnicodeSignature1 = "400004FF", UnicodeSignature2 = "00000000", UnicodeSignature3 = "00000000", CodePageSignature0 = "0000019F", CodePageSignature1 = "00000000" };

            font5.Append(panose1Number4);
            font5.Append(fontCharSet5);
            font5.Append(fontFamily5);
            font5.Append(pitch5);
            font5.Append(fontSignature5);

            Font font6 = new Font() { Name = "Courier New" };
            Panose1Number panose1Number5 = new Panose1Number() { Val = "02070309020205020404" };
            FontCharSet fontCharSet6 = new FontCharSet() { Val = "CC" };
            FontFamily fontFamily6 = new FontFamily() { Val = FontFamilyValues.Modern };
            Pitch pitch6 = new Pitch() { Val = FontPitchValues.Fixed };
            FontSignature fontSignature6 = new FontSignature() { UnicodeSignature0 = "E0002EFF", UnicodeSignature1 = "C0007843", UnicodeSignature2 = "00000009", UnicodeSignature3 = "00000000", CodePageSignature0 = "000001FF", CodePageSignature1 = "00000000" };

            font6.Append(panose1Number5);
            font6.Append(fontCharSet6);
            font6.Append(fontFamily6);
            font6.Append(pitch6);
            font6.Append(fontSignature6);

            Font font7 = new Font() { Name = "Calibri Light" };
            Panose1Number panose1Number6 = new Panose1Number() { Val = "020F0302020204030204" };
            FontCharSet fontCharSet7 = new FontCharSet() { Val = "CC" };
            FontFamily fontFamily7 = new FontFamily() { Val = FontFamilyValues.Swiss };
            Pitch pitch7 = new Pitch() { Val = FontPitchValues.Variable };
            FontSignature fontSignature7 = new FontSignature() { UnicodeSignature0 = "E0002AFF", UnicodeSignature1 = "C000247B", UnicodeSignature2 = "00000009", UnicodeSignature3 = "00000000", CodePageSignature0 = "000001FF", CodePageSignature1 = "00000000" };

            font7.Append(panose1Number6);
            font7.Append(fontCharSet7);
            font7.Append(fontFamily7);
            font7.Append(pitch7);
            font7.Append(fontSignature7);

            fonts1.Append(font1);
            fonts1.Append(font2);
            fonts1.Append(font3);
            fonts1.Append(font4);
            fonts1.Append(font5);
            fonts1.Append(font6);
            fonts1.Append(font7);

            fontTablePart1.Fonts = fonts1;
        }

        // Generates content of documentSettingsPart1.
        private void GenerateDocumentSettingsPart1Content(DocumentSettingsPart documentSettingsPart1)
        {
            Settings settings1 = new Settings() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "w14 w15" } };
            settings1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            settings1.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
            settings1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            settings1.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
            settings1.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
            settings1.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
            settings1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            settings1.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");
            settings1.AddNamespaceDeclaration("w15", "http://schemas.microsoft.com/office/word/2012/wordml");
            settings1.AddNamespaceDeclaration("sl", "http://schemas.openxmlformats.org/schemaLibrary/2006/main");
            Zoom zoom1 = new Zoom() { Percent = "100" };
            DefaultTabStop defaultTabStop1 = new DefaultTabStop() { Val = 708 };
            CharacterSpacingControl characterSpacingControl1 = new CharacterSpacingControl() { Val = CharacterSpacingValues.DoNotCompress };

            Compatibility compatibility1 = new Compatibility();
            CompatibilitySetting compatibilitySetting1 = new CompatibilitySetting() { Name = CompatSettingNameValues.CompatibilityMode, Uri = "http://schemas.microsoft.com/office/word", Val = "15" };
            CompatibilitySetting compatibilitySetting2 = new CompatibilitySetting() { Name = CompatSettingNameValues.OverrideTableStyleFontSizeAndJustification, Uri = "http://schemas.microsoft.com/office/word", Val = "1" };
            CompatibilitySetting compatibilitySetting3 = new CompatibilitySetting() { Name = CompatSettingNameValues.EnableOpenTypeFeatures, Uri = "http://schemas.microsoft.com/office/word", Val = "1" };
            CompatibilitySetting compatibilitySetting4 = new CompatibilitySetting() { Name = CompatSettingNameValues.DoNotFlipMirrorIndents, Uri = "http://schemas.microsoft.com/office/word", Val = "1" };
            CompatibilitySetting compatibilitySetting5 = new CompatibilitySetting() { Name = new EnumValue<CompatSettingNameValues>() { InnerText = "differentiateMultirowTableHeaders" }, Uri = "http://schemas.microsoft.com/office/word", Val = "1" };

            compatibility1.Append(compatibilitySetting1);
            compatibility1.Append(compatibilitySetting2);
            compatibility1.Append(compatibilitySetting3);
            compatibility1.Append(compatibilitySetting4);
            compatibility1.Append(compatibilitySetting5);

            Rsids rsids1 = new Rsids();
            RsidRoot rsidRoot1 = new RsidRoot() { Val = "007813C8" };
            Rsid rsid1 = new Rsid() { Val = "001A1126" };
            Rsid rsid2 = new Rsid() { Val = "002A2A90" };
            Rsid rsid3 = new Rsid() { Val = "003B11C6" };
            Rsid rsid4 = new Rsid() { Val = "005E3022" };
            Rsid rsid5 = new Rsid() { Val = "00705BC8" };
            Rsid rsid6 = new Rsid() { Val = "00772D14" };
            Rsid rsid7 = new Rsid() { Val = "007813C8" };
            Rsid rsid8 = new Rsid() { Val = "00B81BC8" };
            Rsid rsid9 = new Rsid() { Val = "00D06234" };
            Rsid rsid10 = new Rsid() { Val = "00E13137" };

            rsids1.Append(rsidRoot1);
            rsids1.Append(rsid1);
            rsids1.Append(rsid2);
            rsids1.Append(rsid3);
            rsids1.Append(rsid4);
            rsids1.Append(rsid5);
            rsids1.Append(rsid6);
            rsids1.Append(rsid7);
            rsids1.Append(rsid8);
            rsids1.Append(rsid9);
            rsids1.Append(rsid10);

            M.MathProperties mathProperties1 = new M.MathProperties();
            M.MathFont mathFont1 = new M.MathFont() { Val = "Cambria Math" };
            M.BreakBinary breakBinary1 = new M.BreakBinary() { Val = M.BreakBinaryOperatorValues.Before };
            M.BreakBinarySubtraction breakBinarySubtraction1 = new M.BreakBinarySubtraction() { Val = M.BreakBinarySubtractionValues.MinusMinus };
            M.SmallFraction smallFraction1 = new M.SmallFraction() { Val = M.BooleanValues.Zero };
            M.DisplayDefaults displayDefaults1 = new M.DisplayDefaults();
            M.LeftMargin leftMargin1 = new M.LeftMargin() { Val = (UInt32Value)0U };
            M.RightMargin rightMargin1 = new M.RightMargin() { Val = (UInt32Value)0U };
            M.DefaultJustification defaultJustification1 = new M.DefaultJustification() { Val = M.JustificationValues.CenterGroup };
            M.WrapIndent wrapIndent1 = new M.WrapIndent() { Val = (UInt32Value)1440U };
            M.IntegralLimitLocation integralLimitLocation1 = new M.IntegralLimitLocation() { Val = M.LimitLocationValues.SubscriptSuperscript };
            M.NaryLimitLocation naryLimitLocation1 = new M.NaryLimitLocation() { Val = M.LimitLocationValues.UnderOver };

            mathProperties1.Append(mathFont1);
            mathProperties1.Append(breakBinary1);
            mathProperties1.Append(breakBinarySubtraction1);
            mathProperties1.Append(smallFraction1);
            mathProperties1.Append(displayDefaults1);
            mathProperties1.Append(leftMargin1);
            mathProperties1.Append(rightMargin1);
            mathProperties1.Append(defaultJustification1);
            mathProperties1.Append(wrapIndent1);
            mathProperties1.Append(integralLimitLocation1);
            mathProperties1.Append(naryLimitLocation1);
            ThemeFontLanguages themeFontLanguages1 = new ThemeFontLanguages() { Val = "ru-RU" };
            ColorSchemeMapping colorSchemeMapping1 = new ColorSchemeMapping() { Background1 = ColorSchemeIndexValues.Light1, Text1 = ColorSchemeIndexValues.Dark1, Background2 = ColorSchemeIndexValues.Light2, Text2 = ColorSchemeIndexValues.Dark2, Accent1 = ColorSchemeIndexValues.Accent1, Accent2 = ColorSchemeIndexValues.Accent2, Accent3 = ColorSchemeIndexValues.Accent3, Accent4 = ColorSchemeIndexValues.Accent4, Accent5 = ColorSchemeIndexValues.Accent5, Accent6 = ColorSchemeIndexValues.Accent6, Hyperlink = ColorSchemeIndexValues.Hyperlink, FollowedHyperlink = ColorSchemeIndexValues.FollowedHyperlink };

            ShapeDefaults shapeDefaults1 = new ShapeDefaults();
            Ovml.ShapeDefaults shapeDefaults2 = new Ovml.ShapeDefaults() { Extension = V.ExtensionHandlingBehaviorValues.Edit, MaxShapeId = 1026 };

            Ovml.ShapeLayout shapeLayout1 = new Ovml.ShapeLayout() { Extension = V.ExtensionHandlingBehaviorValues.Edit };
            Ovml.ShapeIdMap shapeIdMap1 = new Ovml.ShapeIdMap() { Extension = V.ExtensionHandlingBehaviorValues.Edit, Data = "1" };

            shapeLayout1.Append(shapeIdMap1);

            shapeDefaults1.Append(shapeDefaults2);
            shapeDefaults1.Append(shapeLayout1);
            DecimalSymbol decimalSymbol1 = new DecimalSymbol() { Val = "," };
            ListSeparator listSeparator1 = new ListSeparator() { Val = ";" };
            OpenXmlUnknownElement openXmlUnknownElement2 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<w15:chartTrackingRefBased xmlns:w15=\"http://schemas.microsoft.com/office/word/2012/wordml\" />");

            OpenXmlUnknownElement openXmlUnknownElement3 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<w15:docId w15:val=\"{BA5533DD-8087-4D76-92FB-55BAC1BBFA56}\" xmlns:w15=\"http://schemas.microsoft.com/office/word/2012/wordml\" />");

            settings1.Append(zoom1);
            settings1.Append(defaultTabStop1);
            settings1.Append(characterSpacingControl1);
            settings1.Append(compatibility1);
            settings1.Append(rsids1);
            settings1.Append(mathProperties1);
            settings1.Append(themeFontLanguages1);
            settings1.Append(colorSchemeMapping1);
            settings1.Append(shapeDefaults1);
            settings1.Append(decimalSymbol1);
            settings1.Append(listSeparator1);
            settings1.Append(openXmlUnknownElement2);
            settings1.Append(openXmlUnknownElement3);

            documentSettingsPart1.Settings = settings1;
        }

        // Generates content of styleDefinitionsPart1.
        private void GenerateStyleDefinitionsPart1Content(StyleDefinitionsPart styleDefinitionsPart1)
        {
            Styles styles1 = new Styles() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "w14 w15" } };
            styles1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            styles1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            styles1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            styles1.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");
            styles1.AddNamespaceDeclaration("w15", "http://schemas.microsoft.com/office/word/2012/wordml");

            DocDefaults docDefaults1 = new DocDefaults();

            RunPropertiesDefault runPropertiesDefault1 = new RunPropertiesDefault();

            RunPropertiesBaseStyle runPropertiesBaseStyle1 = new RunPropertiesBaseStyle();
            RunFonts runFonts16 = new RunFonts() { AsciiTheme = ThemeFontValues.MinorHighAnsi, HighAnsiTheme = ThemeFontValues.MinorHighAnsi, EastAsiaTheme = ThemeFontValues.MinorHighAnsi, ComplexScriptTheme = ThemeFontValues.MinorBidi };
            FontSize fontSize40 = new FontSize() { Val = "22" };
            FontSizeComplexScript fontSizeComplexScript40 = new FontSizeComplexScript() { Val = "22" };
            Languages languages33 = new Languages() { Val = "ru-RU", EastAsia = "en-US", Bidi = "ar-SA" };

            runPropertiesBaseStyle1.Append(runFonts16);
            runPropertiesBaseStyle1.Append(fontSize40);
            runPropertiesBaseStyle1.Append(fontSizeComplexScript40);
            runPropertiesBaseStyle1.Append(languages33);

            runPropertiesDefault1.Append(runPropertiesBaseStyle1);

            ParagraphPropertiesDefault paragraphPropertiesDefault1 = new ParagraphPropertiesDefault();

            ParagraphPropertiesBaseStyle paragraphPropertiesBaseStyle1 = new ParagraphPropertiesBaseStyle();
            SpacingBetweenLines spacingBetweenLines3 = new SpacingBetweenLines() { After = "160", Line = "259", LineRule = LineSpacingRuleValues.Auto };

            paragraphPropertiesBaseStyle1.Append(spacingBetweenLines3);

            paragraphPropertiesDefault1.Append(paragraphPropertiesBaseStyle1);

            docDefaults1.Append(runPropertiesDefault1);
            docDefaults1.Append(paragraphPropertiesDefault1);

            LatentStyles latentStyles1 = new LatentStyles() { DefaultLockedState = false, DefaultUiPriority = 99, DefaultSemiHidden = false, DefaultUnhideWhenUsed = false, DefaultPrimaryStyle = false, Count = 371 };
            LatentStyleExceptionInfo latentStyleExceptionInfo1 = new LatentStyleExceptionInfo() { Name = "Normal", UiPriority = 0, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo2 = new LatentStyleExceptionInfo() { Name = "heading 1", UiPriority = 9, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo3 = new LatentStyleExceptionInfo() { Name = "heading 2", UiPriority = 9, SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo4 = new LatentStyleExceptionInfo() { Name = "heading 3", UiPriority = 9, SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo5 = new LatentStyleExceptionInfo() { Name = "heading 4", UiPriority = 9, SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo6 = new LatentStyleExceptionInfo() { Name = "heading 5", UiPriority = 9, SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo7 = new LatentStyleExceptionInfo() { Name = "heading 6", UiPriority = 9, SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo8 = new LatentStyleExceptionInfo() { Name = "heading 7", UiPriority = 9, SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo9 = new LatentStyleExceptionInfo() { Name = "heading 8", UiPriority = 9, SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo10 = new LatentStyleExceptionInfo() { Name = "heading 9", UiPriority = 9, SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo11 = new LatentStyleExceptionInfo() { Name = "index 1", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo12 = new LatentStyleExceptionInfo() { Name = "index 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo13 = new LatentStyleExceptionInfo() { Name = "index 3", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo14 = new LatentStyleExceptionInfo() { Name = "index 4", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo15 = new LatentStyleExceptionInfo() { Name = "index 5", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo16 = new LatentStyleExceptionInfo() { Name = "index 6", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo17 = new LatentStyleExceptionInfo() { Name = "index 7", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo18 = new LatentStyleExceptionInfo() { Name = "index 8", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo19 = new LatentStyleExceptionInfo() { Name = "index 9", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo20 = new LatentStyleExceptionInfo() { Name = "toc 1", UiPriority = 39, SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo21 = new LatentStyleExceptionInfo() { Name = "toc 2", UiPriority = 39, SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo22 = new LatentStyleExceptionInfo() { Name = "toc 3", UiPriority = 39, SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo23 = new LatentStyleExceptionInfo() { Name = "toc 4", UiPriority = 39, SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo24 = new LatentStyleExceptionInfo() { Name = "toc 5", UiPriority = 39, SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo25 = new LatentStyleExceptionInfo() { Name = "toc 6", UiPriority = 39, SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo26 = new LatentStyleExceptionInfo() { Name = "toc 7", UiPriority = 39, SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo27 = new LatentStyleExceptionInfo() { Name = "toc 8", UiPriority = 39, SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo28 = new LatentStyleExceptionInfo() { Name = "toc 9", UiPriority = 39, SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo29 = new LatentStyleExceptionInfo() { Name = "Normal Indent", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo30 = new LatentStyleExceptionInfo() { Name = "footnote text", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo31 = new LatentStyleExceptionInfo() { Name = "annotation text", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo32 = new LatentStyleExceptionInfo() { Name = "header", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo33 = new LatentStyleExceptionInfo() { Name = "footer", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo34 = new LatentStyleExceptionInfo() { Name = "index heading", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo35 = new LatentStyleExceptionInfo() { Name = "caption", UiPriority = 35, SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo36 = new LatentStyleExceptionInfo() { Name = "table of figures", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo37 = new LatentStyleExceptionInfo() { Name = "envelope address", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo38 = new LatentStyleExceptionInfo() { Name = "envelope return", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo39 = new LatentStyleExceptionInfo() { Name = "footnote reference", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo40 = new LatentStyleExceptionInfo() { Name = "annotation reference", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo41 = new LatentStyleExceptionInfo() { Name = "line number", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo42 = new LatentStyleExceptionInfo() { Name = "page number", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo43 = new LatentStyleExceptionInfo() { Name = "endnote reference", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo44 = new LatentStyleExceptionInfo() { Name = "endnote text", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo45 = new LatentStyleExceptionInfo() { Name = "table of authorities", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo46 = new LatentStyleExceptionInfo() { Name = "macro", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo47 = new LatentStyleExceptionInfo() { Name = "toa heading", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo48 = new LatentStyleExceptionInfo() { Name = "List", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo49 = new LatentStyleExceptionInfo() { Name = "List Bullet", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo50 = new LatentStyleExceptionInfo() { Name = "List Number", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo51 = new LatentStyleExceptionInfo() { Name = "List 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo52 = new LatentStyleExceptionInfo() { Name = "List 3", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo53 = new LatentStyleExceptionInfo() { Name = "List 4", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo54 = new LatentStyleExceptionInfo() { Name = "List 5", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo55 = new LatentStyleExceptionInfo() { Name = "List Bullet 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo56 = new LatentStyleExceptionInfo() { Name = "List Bullet 3", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo57 = new LatentStyleExceptionInfo() { Name = "List Bullet 4", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo58 = new LatentStyleExceptionInfo() { Name = "List Bullet 5", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo59 = new LatentStyleExceptionInfo() { Name = "List Number 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo60 = new LatentStyleExceptionInfo() { Name = "List Number 3", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo61 = new LatentStyleExceptionInfo() { Name = "List Number 4", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo62 = new LatentStyleExceptionInfo() { Name = "List Number 5", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo63 = new LatentStyleExceptionInfo() { Name = "Title", UiPriority = 10, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo64 = new LatentStyleExceptionInfo() { Name = "Closing", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo65 = new LatentStyleExceptionInfo() { Name = "Signature", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo66 = new LatentStyleExceptionInfo() { Name = "Default Paragraph Font", UiPriority = 1, SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo67 = new LatentStyleExceptionInfo() { Name = "Body Text", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo68 = new LatentStyleExceptionInfo() { Name = "Body Text Indent", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo69 = new LatentStyleExceptionInfo() { Name = "List Continue", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo70 = new LatentStyleExceptionInfo() { Name = "List Continue 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo71 = new LatentStyleExceptionInfo() { Name = "List Continue 3", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo72 = new LatentStyleExceptionInfo() { Name = "List Continue 4", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo73 = new LatentStyleExceptionInfo() { Name = "List Continue 5", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo74 = new LatentStyleExceptionInfo() { Name = "Message Header", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo75 = new LatentStyleExceptionInfo() { Name = "Subtitle", UiPriority = 11, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo76 = new LatentStyleExceptionInfo() { Name = "Salutation", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo77 = new LatentStyleExceptionInfo() { Name = "Date", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo78 = new LatentStyleExceptionInfo() { Name = "Body Text First Indent", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo79 = new LatentStyleExceptionInfo() { Name = "Body Text First Indent 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo80 = new LatentStyleExceptionInfo() { Name = "Note Heading", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo81 = new LatentStyleExceptionInfo() { Name = "Body Text 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo82 = new LatentStyleExceptionInfo() { Name = "Body Text 3", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo83 = new LatentStyleExceptionInfo() { Name = "Body Text Indent 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo84 = new LatentStyleExceptionInfo() { Name = "Body Text Indent 3", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo85 = new LatentStyleExceptionInfo() { Name = "Block Text", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo86 = new LatentStyleExceptionInfo() { Name = "Hyperlink", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo87 = new LatentStyleExceptionInfo() { Name = "FollowedHyperlink", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo88 = new LatentStyleExceptionInfo() { Name = "Strong", UiPriority = 22, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo89 = new LatentStyleExceptionInfo() { Name = "Emphasis", UiPriority = 20, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo90 = new LatentStyleExceptionInfo() { Name = "Document Map", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo91 = new LatentStyleExceptionInfo() { Name = "Plain Text", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo92 = new LatentStyleExceptionInfo() { Name = "E-mail Signature", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo93 = new LatentStyleExceptionInfo() { Name = "HTML Top of Form", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo94 = new LatentStyleExceptionInfo() { Name = "HTML Bottom of Form", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo95 = new LatentStyleExceptionInfo() { Name = "Normal (Web)", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo96 = new LatentStyleExceptionInfo() { Name = "HTML Acronym", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo97 = new LatentStyleExceptionInfo() { Name = "HTML Address", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo98 = new LatentStyleExceptionInfo() { Name = "HTML Cite", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo99 = new LatentStyleExceptionInfo() { Name = "HTML Code", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo100 = new LatentStyleExceptionInfo() { Name = "HTML Definition", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo101 = new LatentStyleExceptionInfo() { Name = "HTML Keyboard", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo102 = new LatentStyleExceptionInfo() { Name = "HTML Preformatted", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo103 = new LatentStyleExceptionInfo() { Name = "HTML Sample", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo104 = new LatentStyleExceptionInfo() { Name = "HTML Typewriter", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo105 = new LatentStyleExceptionInfo() { Name = "HTML Variable", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo106 = new LatentStyleExceptionInfo() { Name = "Normal Table", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo107 = new LatentStyleExceptionInfo() { Name = "annotation subject", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo108 = new LatentStyleExceptionInfo() { Name = "No List", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo109 = new LatentStyleExceptionInfo() { Name = "Outline List 1", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo110 = new LatentStyleExceptionInfo() { Name = "Outline List 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo111 = new LatentStyleExceptionInfo() { Name = "Outline List 3", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo112 = new LatentStyleExceptionInfo() { Name = "Table Simple 1", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo113 = new LatentStyleExceptionInfo() { Name = "Table Simple 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo114 = new LatentStyleExceptionInfo() { Name = "Table Simple 3", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo115 = new LatentStyleExceptionInfo() { Name = "Table Classic 1", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo116 = new LatentStyleExceptionInfo() { Name = "Table Classic 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo117 = new LatentStyleExceptionInfo() { Name = "Table Classic 3", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo118 = new LatentStyleExceptionInfo() { Name = "Table Classic 4", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo119 = new LatentStyleExceptionInfo() { Name = "Table Colorful 1", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo120 = new LatentStyleExceptionInfo() { Name = "Table Colorful 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo121 = new LatentStyleExceptionInfo() { Name = "Table Colorful 3", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo122 = new LatentStyleExceptionInfo() { Name = "Table Columns 1", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo123 = new LatentStyleExceptionInfo() { Name = "Table Columns 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo124 = new LatentStyleExceptionInfo() { Name = "Table Columns 3", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo125 = new LatentStyleExceptionInfo() { Name = "Table Columns 4", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo126 = new LatentStyleExceptionInfo() { Name = "Table Columns 5", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo127 = new LatentStyleExceptionInfo() { Name = "Table Grid 1", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo128 = new LatentStyleExceptionInfo() { Name = "Table Grid 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo129 = new LatentStyleExceptionInfo() { Name = "Table Grid 3", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo130 = new LatentStyleExceptionInfo() { Name = "Table Grid 4", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo131 = new LatentStyleExceptionInfo() { Name = "Table Grid 5", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo132 = new LatentStyleExceptionInfo() { Name = "Table Grid 6", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo133 = new LatentStyleExceptionInfo() { Name = "Table Grid 7", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo134 = new LatentStyleExceptionInfo() { Name = "Table Grid 8", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo135 = new LatentStyleExceptionInfo() { Name = "Table List 1", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo136 = new LatentStyleExceptionInfo() { Name = "Table List 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo137 = new LatentStyleExceptionInfo() { Name = "Table List 3", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo138 = new LatentStyleExceptionInfo() { Name = "Table List 4", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo139 = new LatentStyleExceptionInfo() { Name = "Table List 5", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo140 = new LatentStyleExceptionInfo() { Name = "Table List 6", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo141 = new LatentStyleExceptionInfo() { Name = "Table List 7", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo142 = new LatentStyleExceptionInfo() { Name = "Table List 8", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo143 = new LatentStyleExceptionInfo() { Name = "Table 3D effects 1", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo144 = new LatentStyleExceptionInfo() { Name = "Table 3D effects 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo145 = new LatentStyleExceptionInfo() { Name = "Table 3D effects 3", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo146 = new LatentStyleExceptionInfo() { Name = "Table Contemporary", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo147 = new LatentStyleExceptionInfo() { Name = "Table Elegant", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo148 = new LatentStyleExceptionInfo() { Name = "Table Professional", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo149 = new LatentStyleExceptionInfo() { Name = "Table Subtle 1", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo150 = new LatentStyleExceptionInfo() { Name = "Table Subtle 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo151 = new LatentStyleExceptionInfo() { Name = "Table Web 1", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo152 = new LatentStyleExceptionInfo() { Name = "Table Web 2", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo153 = new LatentStyleExceptionInfo() { Name = "Table Web 3", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo154 = new LatentStyleExceptionInfo() { Name = "Balloon Text", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo155 = new LatentStyleExceptionInfo() { Name = "Table Grid", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo156 = new LatentStyleExceptionInfo() { Name = "Table Theme", SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo157 = new LatentStyleExceptionInfo() { Name = "Placeholder Text", SemiHidden = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo158 = new LatentStyleExceptionInfo() { Name = "No Spacing", UiPriority = 1, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo159 = new LatentStyleExceptionInfo() { Name = "Light Shading", UiPriority = 60 };
            LatentStyleExceptionInfo latentStyleExceptionInfo160 = new LatentStyleExceptionInfo() { Name = "Light List", UiPriority = 61 };
            LatentStyleExceptionInfo latentStyleExceptionInfo161 = new LatentStyleExceptionInfo() { Name = "Light Grid", UiPriority = 62 };
            LatentStyleExceptionInfo latentStyleExceptionInfo162 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1", UiPriority = 63 };
            LatentStyleExceptionInfo latentStyleExceptionInfo163 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2", UiPriority = 64 };
            LatentStyleExceptionInfo latentStyleExceptionInfo164 = new LatentStyleExceptionInfo() { Name = "Medium List 1", UiPriority = 65 };
            LatentStyleExceptionInfo latentStyleExceptionInfo165 = new LatentStyleExceptionInfo() { Name = "Medium List 2", UiPriority = 66 };
            LatentStyleExceptionInfo latentStyleExceptionInfo166 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1", UiPriority = 67 };
            LatentStyleExceptionInfo latentStyleExceptionInfo167 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2", UiPriority = 68 };
            LatentStyleExceptionInfo latentStyleExceptionInfo168 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3", UiPriority = 69 };
            LatentStyleExceptionInfo latentStyleExceptionInfo169 = new LatentStyleExceptionInfo() { Name = "Dark List", UiPriority = 70 };
            LatentStyleExceptionInfo latentStyleExceptionInfo170 = new LatentStyleExceptionInfo() { Name = "Colorful Shading", UiPriority = 71 };
            LatentStyleExceptionInfo latentStyleExceptionInfo171 = new LatentStyleExceptionInfo() { Name = "Colorful List", UiPriority = 72 };
            LatentStyleExceptionInfo latentStyleExceptionInfo172 = new LatentStyleExceptionInfo() { Name = "Colorful Grid", UiPriority = 73 };
            LatentStyleExceptionInfo latentStyleExceptionInfo173 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 1", UiPriority = 60 };
            LatentStyleExceptionInfo latentStyleExceptionInfo174 = new LatentStyleExceptionInfo() { Name = "Light List Accent 1", UiPriority = 61 };
            LatentStyleExceptionInfo latentStyleExceptionInfo175 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 1", UiPriority = 62 };
            LatentStyleExceptionInfo latentStyleExceptionInfo176 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 1", UiPriority = 63 };
            LatentStyleExceptionInfo latentStyleExceptionInfo177 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 1", UiPriority = 64 };
            LatentStyleExceptionInfo latentStyleExceptionInfo178 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 1", UiPriority = 65 };
            LatentStyleExceptionInfo latentStyleExceptionInfo179 = new LatentStyleExceptionInfo() { Name = "Revision", SemiHidden = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo180 = new LatentStyleExceptionInfo() { Name = "List Paragraph", UiPriority = 34, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo181 = new LatentStyleExceptionInfo() { Name = "Quote", UiPriority = 29, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo182 = new LatentStyleExceptionInfo() { Name = "Intense Quote", UiPriority = 30, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo183 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 1", UiPriority = 66 };
            LatentStyleExceptionInfo latentStyleExceptionInfo184 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 1", UiPriority = 67 };
            LatentStyleExceptionInfo latentStyleExceptionInfo185 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 1", UiPriority = 68 };
            LatentStyleExceptionInfo latentStyleExceptionInfo186 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 1", UiPriority = 69 };
            LatentStyleExceptionInfo latentStyleExceptionInfo187 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 1", UiPriority = 70 };
            LatentStyleExceptionInfo latentStyleExceptionInfo188 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 1", UiPriority = 71 };
            LatentStyleExceptionInfo latentStyleExceptionInfo189 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 1", UiPriority = 72 };
            LatentStyleExceptionInfo latentStyleExceptionInfo190 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 1", UiPriority = 73 };
            LatentStyleExceptionInfo latentStyleExceptionInfo191 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 2", UiPriority = 60 };
            LatentStyleExceptionInfo latentStyleExceptionInfo192 = new LatentStyleExceptionInfo() { Name = "Light List Accent 2", UiPriority = 61 };
            LatentStyleExceptionInfo latentStyleExceptionInfo193 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 2", UiPriority = 62 };
            LatentStyleExceptionInfo latentStyleExceptionInfo194 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 2", UiPriority = 63 };
            LatentStyleExceptionInfo latentStyleExceptionInfo195 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 2", UiPriority = 64 };
            LatentStyleExceptionInfo latentStyleExceptionInfo196 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 2", UiPriority = 65 };
            LatentStyleExceptionInfo latentStyleExceptionInfo197 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 2", UiPriority = 66 };
            LatentStyleExceptionInfo latentStyleExceptionInfo198 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 2", UiPriority = 67 };
            LatentStyleExceptionInfo latentStyleExceptionInfo199 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 2", UiPriority = 68 };
            LatentStyleExceptionInfo latentStyleExceptionInfo200 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 2", UiPriority = 69 };
            LatentStyleExceptionInfo latentStyleExceptionInfo201 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 2", UiPriority = 70 };
            LatentStyleExceptionInfo latentStyleExceptionInfo202 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 2", UiPriority = 71 };
            LatentStyleExceptionInfo latentStyleExceptionInfo203 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 2", UiPriority = 72 };
            LatentStyleExceptionInfo latentStyleExceptionInfo204 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 2", UiPriority = 73 };
            LatentStyleExceptionInfo latentStyleExceptionInfo205 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 3", UiPriority = 60 };
            LatentStyleExceptionInfo latentStyleExceptionInfo206 = new LatentStyleExceptionInfo() { Name = "Light List Accent 3", UiPriority = 61 };
            LatentStyleExceptionInfo latentStyleExceptionInfo207 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 3", UiPriority = 62 };
            LatentStyleExceptionInfo latentStyleExceptionInfo208 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 3", UiPriority = 63 };
            LatentStyleExceptionInfo latentStyleExceptionInfo209 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 3", UiPriority = 64 };
            LatentStyleExceptionInfo latentStyleExceptionInfo210 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 3", UiPriority = 65 };
            LatentStyleExceptionInfo latentStyleExceptionInfo211 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 3", UiPriority = 66 };
            LatentStyleExceptionInfo latentStyleExceptionInfo212 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 3", UiPriority = 67 };
            LatentStyleExceptionInfo latentStyleExceptionInfo213 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 3", UiPriority = 68 };
            LatentStyleExceptionInfo latentStyleExceptionInfo214 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 3", UiPriority = 69 };
            LatentStyleExceptionInfo latentStyleExceptionInfo215 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 3", UiPriority = 70 };
            LatentStyleExceptionInfo latentStyleExceptionInfo216 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 3", UiPriority = 71 };
            LatentStyleExceptionInfo latentStyleExceptionInfo217 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 3", UiPriority = 72 };
            LatentStyleExceptionInfo latentStyleExceptionInfo218 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 3", UiPriority = 73 };
            LatentStyleExceptionInfo latentStyleExceptionInfo219 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 4", UiPriority = 60 };
            LatentStyleExceptionInfo latentStyleExceptionInfo220 = new LatentStyleExceptionInfo() { Name = "Light List Accent 4", UiPriority = 61 };
            LatentStyleExceptionInfo latentStyleExceptionInfo221 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 4", UiPriority = 62 };
            LatentStyleExceptionInfo latentStyleExceptionInfo222 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 4", UiPriority = 63 };
            LatentStyleExceptionInfo latentStyleExceptionInfo223 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 4", UiPriority = 64 };
            LatentStyleExceptionInfo latentStyleExceptionInfo224 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 4", UiPriority = 65 };
            LatentStyleExceptionInfo latentStyleExceptionInfo225 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 4", UiPriority = 66 };
            LatentStyleExceptionInfo latentStyleExceptionInfo226 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 4", UiPriority = 67 };
            LatentStyleExceptionInfo latentStyleExceptionInfo227 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 4", UiPriority = 68 };
            LatentStyleExceptionInfo latentStyleExceptionInfo228 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 4", UiPriority = 69 };
            LatentStyleExceptionInfo latentStyleExceptionInfo229 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 4", UiPriority = 70 };
            LatentStyleExceptionInfo latentStyleExceptionInfo230 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 4", UiPriority = 71 };
            LatentStyleExceptionInfo latentStyleExceptionInfo231 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 4", UiPriority = 72 };
            LatentStyleExceptionInfo latentStyleExceptionInfo232 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 4", UiPriority = 73 };
            LatentStyleExceptionInfo latentStyleExceptionInfo233 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 5", UiPriority = 60 };
            LatentStyleExceptionInfo latentStyleExceptionInfo234 = new LatentStyleExceptionInfo() { Name = "Light List Accent 5", UiPriority = 61 };
            LatentStyleExceptionInfo latentStyleExceptionInfo235 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 5", UiPriority = 62 };
            LatentStyleExceptionInfo latentStyleExceptionInfo236 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 5", UiPriority = 63 };
            LatentStyleExceptionInfo latentStyleExceptionInfo237 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 5", UiPriority = 64 };
            LatentStyleExceptionInfo latentStyleExceptionInfo238 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 5", UiPriority = 65 };
            LatentStyleExceptionInfo latentStyleExceptionInfo239 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 5", UiPriority = 66 };
            LatentStyleExceptionInfo latentStyleExceptionInfo240 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 5", UiPriority = 67 };
            LatentStyleExceptionInfo latentStyleExceptionInfo241 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 5", UiPriority = 68 };
            LatentStyleExceptionInfo latentStyleExceptionInfo242 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 5", UiPriority = 69 };
            LatentStyleExceptionInfo latentStyleExceptionInfo243 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 5", UiPriority = 70 };
            LatentStyleExceptionInfo latentStyleExceptionInfo244 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 5", UiPriority = 71 };
            LatentStyleExceptionInfo latentStyleExceptionInfo245 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 5", UiPriority = 72 };
            LatentStyleExceptionInfo latentStyleExceptionInfo246 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 5", UiPriority = 73 };
            LatentStyleExceptionInfo latentStyleExceptionInfo247 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 6", UiPriority = 60 };
            LatentStyleExceptionInfo latentStyleExceptionInfo248 = new LatentStyleExceptionInfo() { Name = "Light List Accent 6", UiPriority = 61 };
            LatentStyleExceptionInfo latentStyleExceptionInfo249 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 6", UiPriority = 62 };
            LatentStyleExceptionInfo latentStyleExceptionInfo250 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 6", UiPriority = 63 };
            LatentStyleExceptionInfo latentStyleExceptionInfo251 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 6", UiPriority = 64 };
            LatentStyleExceptionInfo latentStyleExceptionInfo252 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 6", UiPriority = 65 };
            LatentStyleExceptionInfo latentStyleExceptionInfo253 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 6", UiPriority = 66 };
            LatentStyleExceptionInfo latentStyleExceptionInfo254 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 6", UiPriority = 67 };
            LatentStyleExceptionInfo latentStyleExceptionInfo255 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 6", UiPriority = 68 };
            LatentStyleExceptionInfo latentStyleExceptionInfo256 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 6", UiPriority = 69 };
            LatentStyleExceptionInfo latentStyleExceptionInfo257 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 6", UiPriority = 70 };
            LatentStyleExceptionInfo latentStyleExceptionInfo258 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 6", UiPriority = 71 };
            LatentStyleExceptionInfo latentStyleExceptionInfo259 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 6", UiPriority = 72 };
            LatentStyleExceptionInfo latentStyleExceptionInfo260 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 6", UiPriority = 73 };
            LatentStyleExceptionInfo latentStyleExceptionInfo261 = new LatentStyleExceptionInfo() { Name = "Subtle Emphasis", UiPriority = 19, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo262 = new LatentStyleExceptionInfo() { Name = "Intense Emphasis", UiPriority = 21, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo263 = new LatentStyleExceptionInfo() { Name = "Subtle Reference", UiPriority = 31, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo264 = new LatentStyleExceptionInfo() { Name = "Intense Reference", UiPriority = 32, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo265 = new LatentStyleExceptionInfo() { Name = "Book Title", UiPriority = 33, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo266 = new LatentStyleExceptionInfo() { Name = "Bibliography", UiPriority = 37, SemiHidden = true, UnhideWhenUsed = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo267 = new LatentStyleExceptionInfo() { Name = "TOC Heading", UiPriority = 39, SemiHidden = true, UnhideWhenUsed = true, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo268 = new LatentStyleExceptionInfo() { Name = "Plain Table 1", UiPriority = 41 };
            LatentStyleExceptionInfo latentStyleExceptionInfo269 = new LatentStyleExceptionInfo() { Name = "Plain Table 2", UiPriority = 42 };
            LatentStyleExceptionInfo latentStyleExceptionInfo270 = new LatentStyleExceptionInfo() { Name = "Plain Table 3", UiPriority = 43 };
            LatentStyleExceptionInfo latentStyleExceptionInfo271 = new LatentStyleExceptionInfo() { Name = "Plain Table 4", UiPriority = 44 };
            LatentStyleExceptionInfo latentStyleExceptionInfo272 = new LatentStyleExceptionInfo() { Name = "Plain Table 5", UiPriority = 45 };
            LatentStyleExceptionInfo latentStyleExceptionInfo273 = new LatentStyleExceptionInfo() { Name = "Grid Table Light", UiPriority = 40 };
            LatentStyleExceptionInfo latentStyleExceptionInfo274 = new LatentStyleExceptionInfo() { Name = "Grid Table 1 Light", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo275 = new LatentStyleExceptionInfo() { Name = "Grid Table 2", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo276 = new LatentStyleExceptionInfo() { Name = "Grid Table 3", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo277 = new LatentStyleExceptionInfo() { Name = "Grid Table 4", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo278 = new LatentStyleExceptionInfo() { Name = "Grid Table 5 Dark", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo279 = new LatentStyleExceptionInfo() { Name = "Grid Table 6 Colorful", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo280 = new LatentStyleExceptionInfo() { Name = "Grid Table 7 Colorful", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo281 = new LatentStyleExceptionInfo() { Name = "Grid Table 1 Light Accent 1", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo282 = new LatentStyleExceptionInfo() { Name = "Grid Table 2 Accent 1", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo283 = new LatentStyleExceptionInfo() { Name = "Grid Table 3 Accent 1", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo284 = new LatentStyleExceptionInfo() { Name = "Grid Table 4 Accent 1", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo285 = new LatentStyleExceptionInfo() { Name = "Grid Table 5 Dark Accent 1", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo286 = new LatentStyleExceptionInfo() { Name = "Grid Table 6 Colorful Accent 1", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo287 = new LatentStyleExceptionInfo() { Name = "Grid Table 7 Colorful Accent 1", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo288 = new LatentStyleExceptionInfo() { Name = "Grid Table 1 Light Accent 2", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo289 = new LatentStyleExceptionInfo() { Name = "Grid Table 2 Accent 2", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo290 = new LatentStyleExceptionInfo() { Name = "Grid Table 3 Accent 2", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo291 = new LatentStyleExceptionInfo() { Name = "Grid Table 4 Accent 2", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo292 = new LatentStyleExceptionInfo() { Name = "Grid Table 5 Dark Accent 2", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo293 = new LatentStyleExceptionInfo() { Name = "Grid Table 6 Colorful Accent 2", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo294 = new LatentStyleExceptionInfo() { Name = "Grid Table 7 Colorful Accent 2", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo295 = new LatentStyleExceptionInfo() { Name = "Grid Table 1 Light Accent 3", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo296 = new LatentStyleExceptionInfo() { Name = "Grid Table 2 Accent 3", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo297 = new LatentStyleExceptionInfo() { Name = "Grid Table 3 Accent 3", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo298 = new LatentStyleExceptionInfo() { Name = "Grid Table 4 Accent 3", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo299 = new LatentStyleExceptionInfo() { Name = "Grid Table 5 Dark Accent 3", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo300 = new LatentStyleExceptionInfo() { Name = "Grid Table 6 Colorful Accent 3", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo301 = new LatentStyleExceptionInfo() { Name = "Grid Table 7 Colorful Accent 3", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo302 = new LatentStyleExceptionInfo() { Name = "Grid Table 1 Light Accent 4", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo303 = new LatentStyleExceptionInfo() { Name = "Grid Table 2 Accent 4", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo304 = new LatentStyleExceptionInfo() { Name = "Grid Table 3 Accent 4", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo305 = new LatentStyleExceptionInfo() { Name = "Grid Table 4 Accent 4", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo306 = new LatentStyleExceptionInfo() { Name = "Grid Table 5 Dark Accent 4", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo307 = new LatentStyleExceptionInfo() { Name = "Grid Table 6 Colorful Accent 4", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo308 = new LatentStyleExceptionInfo() { Name = "Grid Table 7 Colorful Accent 4", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo309 = new LatentStyleExceptionInfo() { Name = "Grid Table 1 Light Accent 5", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo310 = new LatentStyleExceptionInfo() { Name = "Grid Table 2 Accent 5", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo311 = new LatentStyleExceptionInfo() { Name = "Grid Table 3 Accent 5", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo312 = new LatentStyleExceptionInfo() { Name = "Grid Table 4 Accent 5", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo313 = new LatentStyleExceptionInfo() { Name = "Grid Table 5 Dark Accent 5", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo314 = new LatentStyleExceptionInfo() { Name = "Grid Table 6 Colorful Accent 5", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo315 = new LatentStyleExceptionInfo() { Name = "Grid Table 7 Colorful Accent 5", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo316 = new LatentStyleExceptionInfo() { Name = "Grid Table 1 Light Accent 6", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo317 = new LatentStyleExceptionInfo() { Name = "Grid Table 2 Accent 6", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo318 = new LatentStyleExceptionInfo() { Name = "Grid Table 3 Accent 6", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo319 = new LatentStyleExceptionInfo() { Name = "Grid Table 4 Accent 6", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo320 = new LatentStyleExceptionInfo() { Name = "Grid Table 5 Dark Accent 6", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo321 = new LatentStyleExceptionInfo() { Name = "Grid Table 6 Colorful Accent 6", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo322 = new LatentStyleExceptionInfo() { Name = "Grid Table 7 Colorful Accent 6", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo323 = new LatentStyleExceptionInfo() { Name = "List Table 1 Light", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo324 = new LatentStyleExceptionInfo() { Name = "List Table 2", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo325 = new LatentStyleExceptionInfo() { Name = "List Table 3", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo326 = new LatentStyleExceptionInfo() { Name = "List Table 4", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo327 = new LatentStyleExceptionInfo() { Name = "List Table 5 Dark", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo328 = new LatentStyleExceptionInfo() { Name = "List Table 6 Colorful", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo329 = new LatentStyleExceptionInfo() { Name = "List Table 7 Colorful", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo330 = new LatentStyleExceptionInfo() { Name = "List Table 1 Light Accent 1", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo331 = new LatentStyleExceptionInfo() { Name = "List Table 2 Accent 1", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo332 = new LatentStyleExceptionInfo() { Name = "List Table 3 Accent 1", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo333 = new LatentStyleExceptionInfo() { Name = "List Table 4 Accent 1", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo334 = new LatentStyleExceptionInfo() { Name = "List Table 5 Dark Accent 1", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo335 = new LatentStyleExceptionInfo() { Name = "List Table 6 Colorful Accent 1", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo336 = new LatentStyleExceptionInfo() { Name = "List Table 7 Colorful Accent 1", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo337 = new LatentStyleExceptionInfo() { Name = "List Table 1 Light Accent 2", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo338 = new LatentStyleExceptionInfo() { Name = "List Table 2 Accent 2", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo339 = new LatentStyleExceptionInfo() { Name = "List Table 3 Accent 2", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo340 = new LatentStyleExceptionInfo() { Name = "List Table 4 Accent 2", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo341 = new LatentStyleExceptionInfo() { Name = "List Table 5 Dark Accent 2", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo342 = new LatentStyleExceptionInfo() { Name = "List Table 6 Colorful Accent 2", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo343 = new LatentStyleExceptionInfo() { Name = "List Table 7 Colorful Accent 2", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo344 = new LatentStyleExceptionInfo() { Name = "List Table 1 Light Accent 3", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo345 = new LatentStyleExceptionInfo() { Name = "List Table 2 Accent 3", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo346 = new LatentStyleExceptionInfo() { Name = "List Table 3 Accent 3", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo347 = new LatentStyleExceptionInfo() { Name = "List Table 4 Accent 3", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo348 = new LatentStyleExceptionInfo() { Name = "List Table 5 Dark Accent 3", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo349 = new LatentStyleExceptionInfo() { Name = "List Table 6 Colorful Accent 3", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo350 = new LatentStyleExceptionInfo() { Name = "List Table 7 Colorful Accent 3", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo351 = new LatentStyleExceptionInfo() { Name = "List Table 1 Light Accent 4", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo352 = new LatentStyleExceptionInfo() { Name = "List Table 2 Accent 4", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo353 = new LatentStyleExceptionInfo() { Name = "List Table 3 Accent 4", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo354 = new LatentStyleExceptionInfo() { Name = "List Table 4 Accent 4", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo355 = new LatentStyleExceptionInfo() { Name = "List Table 5 Dark Accent 4", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo356 = new LatentStyleExceptionInfo() { Name = "List Table 6 Colorful Accent 4", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo357 = new LatentStyleExceptionInfo() { Name = "List Table 7 Colorful Accent 4", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo358 = new LatentStyleExceptionInfo() { Name = "List Table 1 Light Accent 5", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo359 = new LatentStyleExceptionInfo() { Name = "List Table 2 Accent 5", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo360 = new LatentStyleExceptionInfo() { Name = "List Table 3 Accent 5", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo361 = new LatentStyleExceptionInfo() { Name = "List Table 4 Accent 5", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo362 = new LatentStyleExceptionInfo() { Name = "List Table 5 Dark Accent 5", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo363 = new LatentStyleExceptionInfo() { Name = "List Table 6 Colorful Accent 5", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo364 = new LatentStyleExceptionInfo() { Name = "List Table 7 Colorful Accent 5", UiPriority = 52 };
            LatentStyleExceptionInfo latentStyleExceptionInfo365 = new LatentStyleExceptionInfo() { Name = "List Table 1 Light Accent 6", UiPriority = 46 };
            LatentStyleExceptionInfo latentStyleExceptionInfo366 = new LatentStyleExceptionInfo() { Name = "List Table 2 Accent 6", UiPriority = 47 };
            LatentStyleExceptionInfo latentStyleExceptionInfo367 = new LatentStyleExceptionInfo() { Name = "List Table 3 Accent 6", UiPriority = 48 };
            LatentStyleExceptionInfo latentStyleExceptionInfo368 = new LatentStyleExceptionInfo() { Name = "List Table 4 Accent 6", UiPriority = 49 };
            LatentStyleExceptionInfo latentStyleExceptionInfo369 = new LatentStyleExceptionInfo() { Name = "List Table 5 Dark Accent 6", UiPriority = 50 };
            LatentStyleExceptionInfo latentStyleExceptionInfo370 = new LatentStyleExceptionInfo() { Name = "List Table 6 Colorful Accent 6", UiPriority = 51 };
            LatentStyleExceptionInfo latentStyleExceptionInfo371 = new LatentStyleExceptionInfo() { Name = "List Table 7 Colorful Accent 6", UiPriority = 52 };

            latentStyles1.Append(latentStyleExceptionInfo1);
            latentStyles1.Append(latentStyleExceptionInfo2);
            latentStyles1.Append(latentStyleExceptionInfo3);
            latentStyles1.Append(latentStyleExceptionInfo4);
            latentStyles1.Append(latentStyleExceptionInfo5);
            latentStyles1.Append(latentStyleExceptionInfo6);
            latentStyles1.Append(latentStyleExceptionInfo7);
            latentStyles1.Append(latentStyleExceptionInfo8);
            latentStyles1.Append(latentStyleExceptionInfo9);
            latentStyles1.Append(latentStyleExceptionInfo10);
            latentStyles1.Append(latentStyleExceptionInfo11);
            latentStyles1.Append(latentStyleExceptionInfo12);
            latentStyles1.Append(latentStyleExceptionInfo13);
            latentStyles1.Append(latentStyleExceptionInfo14);
            latentStyles1.Append(latentStyleExceptionInfo15);
            latentStyles1.Append(latentStyleExceptionInfo16);
            latentStyles1.Append(latentStyleExceptionInfo17);
            latentStyles1.Append(latentStyleExceptionInfo18);
            latentStyles1.Append(latentStyleExceptionInfo19);
            latentStyles1.Append(latentStyleExceptionInfo20);
            latentStyles1.Append(latentStyleExceptionInfo21);
            latentStyles1.Append(latentStyleExceptionInfo22);
            latentStyles1.Append(latentStyleExceptionInfo23);
            latentStyles1.Append(latentStyleExceptionInfo24);
            latentStyles1.Append(latentStyleExceptionInfo25);
            latentStyles1.Append(latentStyleExceptionInfo26);
            latentStyles1.Append(latentStyleExceptionInfo27);
            latentStyles1.Append(latentStyleExceptionInfo28);
            latentStyles1.Append(latentStyleExceptionInfo29);
            latentStyles1.Append(latentStyleExceptionInfo30);
            latentStyles1.Append(latentStyleExceptionInfo31);
            latentStyles1.Append(latentStyleExceptionInfo32);
            latentStyles1.Append(latentStyleExceptionInfo33);
            latentStyles1.Append(latentStyleExceptionInfo34);
            latentStyles1.Append(latentStyleExceptionInfo35);
            latentStyles1.Append(latentStyleExceptionInfo36);
            latentStyles1.Append(latentStyleExceptionInfo37);
            latentStyles1.Append(latentStyleExceptionInfo38);
            latentStyles1.Append(latentStyleExceptionInfo39);
            latentStyles1.Append(latentStyleExceptionInfo40);
            latentStyles1.Append(latentStyleExceptionInfo41);
            latentStyles1.Append(latentStyleExceptionInfo42);
            latentStyles1.Append(latentStyleExceptionInfo43);
            latentStyles1.Append(latentStyleExceptionInfo44);
            latentStyles1.Append(latentStyleExceptionInfo45);
            latentStyles1.Append(latentStyleExceptionInfo46);
            latentStyles1.Append(latentStyleExceptionInfo47);
            latentStyles1.Append(latentStyleExceptionInfo48);
            latentStyles1.Append(latentStyleExceptionInfo49);
            latentStyles1.Append(latentStyleExceptionInfo50);
            latentStyles1.Append(latentStyleExceptionInfo51);
            latentStyles1.Append(latentStyleExceptionInfo52);
            latentStyles1.Append(latentStyleExceptionInfo53);
            latentStyles1.Append(latentStyleExceptionInfo54);
            latentStyles1.Append(latentStyleExceptionInfo55);
            latentStyles1.Append(latentStyleExceptionInfo56);
            latentStyles1.Append(latentStyleExceptionInfo57);
            latentStyles1.Append(latentStyleExceptionInfo58);
            latentStyles1.Append(latentStyleExceptionInfo59);
            latentStyles1.Append(latentStyleExceptionInfo60);
            latentStyles1.Append(latentStyleExceptionInfo61);
            latentStyles1.Append(latentStyleExceptionInfo62);
            latentStyles1.Append(latentStyleExceptionInfo63);
            latentStyles1.Append(latentStyleExceptionInfo64);
            latentStyles1.Append(latentStyleExceptionInfo65);
            latentStyles1.Append(latentStyleExceptionInfo66);
            latentStyles1.Append(latentStyleExceptionInfo67);
            latentStyles1.Append(latentStyleExceptionInfo68);
            latentStyles1.Append(latentStyleExceptionInfo69);
            latentStyles1.Append(latentStyleExceptionInfo70);
            latentStyles1.Append(latentStyleExceptionInfo71);
            latentStyles1.Append(latentStyleExceptionInfo72);
            latentStyles1.Append(latentStyleExceptionInfo73);
            latentStyles1.Append(latentStyleExceptionInfo74);
            latentStyles1.Append(latentStyleExceptionInfo75);
            latentStyles1.Append(latentStyleExceptionInfo76);
            latentStyles1.Append(latentStyleExceptionInfo77);
            latentStyles1.Append(latentStyleExceptionInfo78);
            latentStyles1.Append(latentStyleExceptionInfo79);
            latentStyles1.Append(latentStyleExceptionInfo80);
            latentStyles1.Append(latentStyleExceptionInfo81);
            latentStyles1.Append(latentStyleExceptionInfo82);
            latentStyles1.Append(latentStyleExceptionInfo83);
            latentStyles1.Append(latentStyleExceptionInfo84);
            latentStyles1.Append(latentStyleExceptionInfo85);
            latentStyles1.Append(latentStyleExceptionInfo86);
            latentStyles1.Append(latentStyleExceptionInfo87);
            latentStyles1.Append(latentStyleExceptionInfo88);
            latentStyles1.Append(latentStyleExceptionInfo89);
            latentStyles1.Append(latentStyleExceptionInfo90);
            latentStyles1.Append(latentStyleExceptionInfo91);
            latentStyles1.Append(latentStyleExceptionInfo92);
            latentStyles1.Append(latentStyleExceptionInfo93);
            latentStyles1.Append(latentStyleExceptionInfo94);
            latentStyles1.Append(latentStyleExceptionInfo95);
            latentStyles1.Append(latentStyleExceptionInfo96);
            latentStyles1.Append(latentStyleExceptionInfo97);
            latentStyles1.Append(latentStyleExceptionInfo98);
            latentStyles1.Append(latentStyleExceptionInfo99);
            latentStyles1.Append(latentStyleExceptionInfo100);
            latentStyles1.Append(latentStyleExceptionInfo101);
            latentStyles1.Append(latentStyleExceptionInfo102);
            latentStyles1.Append(latentStyleExceptionInfo103);
            latentStyles1.Append(latentStyleExceptionInfo104);
            latentStyles1.Append(latentStyleExceptionInfo105);
            latentStyles1.Append(latentStyleExceptionInfo106);
            latentStyles1.Append(latentStyleExceptionInfo107);
            latentStyles1.Append(latentStyleExceptionInfo108);
            latentStyles1.Append(latentStyleExceptionInfo109);
            latentStyles1.Append(latentStyleExceptionInfo110);
            latentStyles1.Append(latentStyleExceptionInfo111);
            latentStyles1.Append(latentStyleExceptionInfo112);
            latentStyles1.Append(latentStyleExceptionInfo113);
            latentStyles1.Append(latentStyleExceptionInfo114);
            latentStyles1.Append(latentStyleExceptionInfo115);
            latentStyles1.Append(latentStyleExceptionInfo116);
            latentStyles1.Append(latentStyleExceptionInfo117);
            latentStyles1.Append(latentStyleExceptionInfo118);
            latentStyles1.Append(latentStyleExceptionInfo119);
            latentStyles1.Append(latentStyleExceptionInfo120);
            latentStyles1.Append(latentStyleExceptionInfo121);
            latentStyles1.Append(latentStyleExceptionInfo122);
            latentStyles1.Append(latentStyleExceptionInfo123);
            latentStyles1.Append(latentStyleExceptionInfo124);
            latentStyles1.Append(latentStyleExceptionInfo125);
            latentStyles1.Append(latentStyleExceptionInfo126);
            latentStyles1.Append(latentStyleExceptionInfo127);
            latentStyles1.Append(latentStyleExceptionInfo128);
            latentStyles1.Append(latentStyleExceptionInfo129);
            latentStyles1.Append(latentStyleExceptionInfo130);
            latentStyles1.Append(latentStyleExceptionInfo131);
            latentStyles1.Append(latentStyleExceptionInfo132);
            latentStyles1.Append(latentStyleExceptionInfo133);
            latentStyles1.Append(latentStyleExceptionInfo134);
            latentStyles1.Append(latentStyleExceptionInfo135);
            latentStyles1.Append(latentStyleExceptionInfo136);
            latentStyles1.Append(latentStyleExceptionInfo137);
            latentStyles1.Append(latentStyleExceptionInfo138);
            latentStyles1.Append(latentStyleExceptionInfo139);
            latentStyles1.Append(latentStyleExceptionInfo140);
            latentStyles1.Append(latentStyleExceptionInfo141);
            latentStyles1.Append(latentStyleExceptionInfo142);
            latentStyles1.Append(latentStyleExceptionInfo143);
            latentStyles1.Append(latentStyleExceptionInfo144);
            latentStyles1.Append(latentStyleExceptionInfo145);
            latentStyles1.Append(latentStyleExceptionInfo146);
            latentStyles1.Append(latentStyleExceptionInfo147);
            latentStyles1.Append(latentStyleExceptionInfo148);
            latentStyles1.Append(latentStyleExceptionInfo149);
            latentStyles1.Append(latentStyleExceptionInfo150);
            latentStyles1.Append(latentStyleExceptionInfo151);
            latentStyles1.Append(latentStyleExceptionInfo152);
            latentStyles1.Append(latentStyleExceptionInfo153);
            latentStyles1.Append(latentStyleExceptionInfo154);
            latentStyles1.Append(latentStyleExceptionInfo155);
            latentStyles1.Append(latentStyleExceptionInfo156);
            latentStyles1.Append(latentStyleExceptionInfo157);
            latentStyles1.Append(latentStyleExceptionInfo158);
            latentStyles1.Append(latentStyleExceptionInfo159);
            latentStyles1.Append(latentStyleExceptionInfo160);
            latentStyles1.Append(latentStyleExceptionInfo161);
            latentStyles1.Append(latentStyleExceptionInfo162);
            latentStyles1.Append(latentStyleExceptionInfo163);
            latentStyles1.Append(latentStyleExceptionInfo164);
            latentStyles1.Append(latentStyleExceptionInfo165);
            latentStyles1.Append(latentStyleExceptionInfo166);
            latentStyles1.Append(latentStyleExceptionInfo167);
            latentStyles1.Append(latentStyleExceptionInfo168);
            latentStyles1.Append(latentStyleExceptionInfo169);
            latentStyles1.Append(latentStyleExceptionInfo170);
            latentStyles1.Append(latentStyleExceptionInfo171);
            latentStyles1.Append(latentStyleExceptionInfo172);
            latentStyles1.Append(latentStyleExceptionInfo173);
            latentStyles1.Append(latentStyleExceptionInfo174);
            latentStyles1.Append(latentStyleExceptionInfo175);
            latentStyles1.Append(latentStyleExceptionInfo176);
            latentStyles1.Append(latentStyleExceptionInfo177);
            latentStyles1.Append(latentStyleExceptionInfo178);
            latentStyles1.Append(latentStyleExceptionInfo179);
            latentStyles1.Append(latentStyleExceptionInfo180);
            latentStyles1.Append(latentStyleExceptionInfo181);
            latentStyles1.Append(latentStyleExceptionInfo182);
            latentStyles1.Append(latentStyleExceptionInfo183);
            latentStyles1.Append(latentStyleExceptionInfo184);
            latentStyles1.Append(latentStyleExceptionInfo185);
            latentStyles1.Append(latentStyleExceptionInfo186);
            latentStyles1.Append(latentStyleExceptionInfo187);
            latentStyles1.Append(latentStyleExceptionInfo188);
            latentStyles1.Append(latentStyleExceptionInfo189);
            latentStyles1.Append(latentStyleExceptionInfo190);
            latentStyles1.Append(latentStyleExceptionInfo191);
            latentStyles1.Append(latentStyleExceptionInfo192);
            latentStyles1.Append(latentStyleExceptionInfo193);
            latentStyles1.Append(latentStyleExceptionInfo194);
            latentStyles1.Append(latentStyleExceptionInfo195);
            latentStyles1.Append(latentStyleExceptionInfo196);
            latentStyles1.Append(latentStyleExceptionInfo197);
            latentStyles1.Append(latentStyleExceptionInfo198);
            latentStyles1.Append(latentStyleExceptionInfo199);
            latentStyles1.Append(latentStyleExceptionInfo200);
            latentStyles1.Append(latentStyleExceptionInfo201);
            latentStyles1.Append(latentStyleExceptionInfo202);
            latentStyles1.Append(latentStyleExceptionInfo203);
            latentStyles1.Append(latentStyleExceptionInfo204);
            latentStyles1.Append(latentStyleExceptionInfo205);
            latentStyles1.Append(latentStyleExceptionInfo206);
            latentStyles1.Append(latentStyleExceptionInfo207);
            latentStyles1.Append(latentStyleExceptionInfo208);
            latentStyles1.Append(latentStyleExceptionInfo209);
            latentStyles1.Append(latentStyleExceptionInfo210);
            latentStyles1.Append(latentStyleExceptionInfo211);
            latentStyles1.Append(latentStyleExceptionInfo212);
            latentStyles1.Append(latentStyleExceptionInfo213);
            latentStyles1.Append(latentStyleExceptionInfo214);
            latentStyles1.Append(latentStyleExceptionInfo215);
            latentStyles1.Append(latentStyleExceptionInfo216);
            latentStyles1.Append(latentStyleExceptionInfo217);
            latentStyles1.Append(latentStyleExceptionInfo218);
            latentStyles1.Append(latentStyleExceptionInfo219);
            latentStyles1.Append(latentStyleExceptionInfo220);
            latentStyles1.Append(latentStyleExceptionInfo221);
            latentStyles1.Append(latentStyleExceptionInfo222);
            latentStyles1.Append(latentStyleExceptionInfo223);
            latentStyles1.Append(latentStyleExceptionInfo224);
            latentStyles1.Append(latentStyleExceptionInfo225);
            latentStyles1.Append(latentStyleExceptionInfo226);
            latentStyles1.Append(latentStyleExceptionInfo227);
            latentStyles1.Append(latentStyleExceptionInfo228);
            latentStyles1.Append(latentStyleExceptionInfo229);
            latentStyles1.Append(latentStyleExceptionInfo230);
            latentStyles1.Append(latentStyleExceptionInfo231);
            latentStyles1.Append(latentStyleExceptionInfo232);
            latentStyles1.Append(latentStyleExceptionInfo233);
            latentStyles1.Append(latentStyleExceptionInfo234);
            latentStyles1.Append(latentStyleExceptionInfo235);
            latentStyles1.Append(latentStyleExceptionInfo236);
            latentStyles1.Append(latentStyleExceptionInfo237);
            latentStyles1.Append(latentStyleExceptionInfo238);
            latentStyles1.Append(latentStyleExceptionInfo239);
            latentStyles1.Append(latentStyleExceptionInfo240);
            latentStyles1.Append(latentStyleExceptionInfo241);
            latentStyles1.Append(latentStyleExceptionInfo242);
            latentStyles1.Append(latentStyleExceptionInfo243);
            latentStyles1.Append(latentStyleExceptionInfo244);
            latentStyles1.Append(latentStyleExceptionInfo245);
            latentStyles1.Append(latentStyleExceptionInfo246);
            latentStyles1.Append(latentStyleExceptionInfo247);
            latentStyles1.Append(latentStyleExceptionInfo248);
            latentStyles1.Append(latentStyleExceptionInfo249);
            latentStyles1.Append(latentStyleExceptionInfo250);
            latentStyles1.Append(latentStyleExceptionInfo251);
            latentStyles1.Append(latentStyleExceptionInfo252);
            latentStyles1.Append(latentStyleExceptionInfo253);
            latentStyles1.Append(latentStyleExceptionInfo254);
            latentStyles1.Append(latentStyleExceptionInfo255);
            latentStyles1.Append(latentStyleExceptionInfo256);
            latentStyles1.Append(latentStyleExceptionInfo257);
            latentStyles1.Append(latentStyleExceptionInfo258);
            latentStyles1.Append(latentStyleExceptionInfo259);
            latentStyles1.Append(latentStyleExceptionInfo260);
            latentStyles1.Append(latentStyleExceptionInfo261);
            latentStyles1.Append(latentStyleExceptionInfo262);
            latentStyles1.Append(latentStyleExceptionInfo263);
            latentStyles1.Append(latentStyleExceptionInfo264);
            latentStyles1.Append(latentStyleExceptionInfo265);
            latentStyles1.Append(latentStyleExceptionInfo266);
            latentStyles1.Append(latentStyleExceptionInfo267);
            latentStyles1.Append(latentStyleExceptionInfo268);
            latentStyles1.Append(latentStyleExceptionInfo269);
            latentStyles1.Append(latentStyleExceptionInfo270);
            latentStyles1.Append(latentStyleExceptionInfo271);
            latentStyles1.Append(latentStyleExceptionInfo272);
            latentStyles1.Append(latentStyleExceptionInfo273);
            latentStyles1.Append(latentStyleExceptionInfo274);
            latentStyles1.Append(latentStyleExceptionInfo275);
            latentStyles1.Append(latentStyleExceptionInfo276);
            latentStyles1.Append(latentStyleExceptionInfo277);
            latentStyles1.Append(latentStyleExceptionInfo278);
            latentStyles1.Append(latentStyleExceptionInfo279);
            latentStyles1.Append(latentStyleExceptionInfo280);
            latentStyles1.Append(latentStyleExceptionInfo281);
            latentStyles1.Append(latentStyleExceptionInfo282);
            latentStyles1.Append(latentStyleExceptionInfo283);
            latentStyles1.Append(latentStyleExceptionInfo284);
            latentStyles1.Append(latentStyleExceptionInfo285);
            latentStyles1.Append(latentStyleExceptionInfo286);
            latentStyles1.Append(latentStyleExceptionInfo287);
            latentStyles1.Append(latentStyleExceptionInfo288);
            latentStyles1.Append(latentStyleExceptionInfo289);
            latentStyles1.Append(latentStyleExceptionInfo290);
            latentStyles1.Append(latentStyleExceptionInfo291);
            latentStyles1.Append(latentStyleExceptionInfo292);
            latentStyles1.Append(latentStyleExceptionInfo293);
            latentStyles1.Append(latentStyleExceptionInfo294);
            latentStyles1.Append(latentStyleExceptionInfo295);
            latentStyles1.Append(latentStyleExceptionInfo296);
            latentStyles1.Append(latentStyleExceptionInfo297);
            latentStyles1.Append(latentStyleExceptionInfo298);
            latentStyles1.Append(latentStyleExceptionInfo299);
            latentStyles1.Append(latentStyleExceptionInfo300);
            latentStyles1.Append(latentStyleExceptionInfo301);
            latentStyles1.Append(latentStyleExceptionInfo302);
            latentStyles1.Append(latentStyleExceptionInfo303);
            latentStyles1.Append(latentStyleExceptionInfo304);
            latentStyles1.Append(latentStyleExceptionInfo305);
            latentStyles1.Append(latentStyleExceptionInfo306);
            latentStyles1.Append(latentStyleExceptionInfo307);
            latentStyles1.Append(latentStyleExceptionInfo308);
            latentStyles1.Append(latentStyleExceptionInfo309);
            latentStyles1.Append(latentStyleExceptionInfo310);
            latentStyles1.Append(latentStyleExceptionInfo311);
            latentStyles1.Append(latentStyleExceptionInfo312);
            latentStyles1.Append(latentStyleExceptionInfo313);
            latentStyles1.Append(latentStyleExceptionInfo314);
            latentStyles1.Append(latentStyleExceptionInfo315);
            latentStyles1.Append(latentStyleExceptionInfo316);
            latentStyles1.Append(latentStyleExceptionInfo317);
            latentStyles1.Append(latentStyleExceptionInfo318);
            latentStyles1.Append(latentStyleExceptionInfo319);
            latentStyles1.Append(latentStyleExceptionInfo320);
            latentStyles1.Append(latentStyleExceptionInfo321);
            latentStyles1.Append(latentStyleExceptionInfo322);
            latentStyles1.Append(latentStyleExceptionInfo323);
            latentStyles1.Append(latentStyleExceptionInfo324);
            latentStyles1.Append(latentStyleExceptionInfo325);
            latentStyles1.Append(latentStyleExceptionInfo326);
            latentStyles1.Append(latentStyleExceptionInfo327);
            latentStyles1.Append(latentStyleExceptionInfo328);
            latentStyles1.Append(latentStyleExceptionInfo329);
            latentStyles1.Append(latentStyleExceptionInfo330);
            latentStyles1.Append(latentStyleExceptionInfo331);
            latentStyles1.Append(latentStyleExceptionInfo332);
            latentStyles1.Append(latentStyleExceptionInfo333);
            latentStyles1.Append(latentStyleExceptionInfo334);
            latentStyles1.Append(latentStyleExceptionInfo335);
            latentStyles1.Append(latentStyleExceptionInfo336);
            latentStyles1.Append(latentStyleExceptionInfo337);
            latentStyles1.Append(latentStyleExceptionInfo338);
            latentStyles1.Append(latentStyleExceptionInfo339);
            latentStyles1.Append(latentStyleExceptionInfo340);
            latentStyles1.Append(latentStyleExceptionInfo341);
            latentStyles1.Append(latentStyleExceptionInfo342);
            latentStyles1.Append(latentStyleExceptionInfo343);
            latentStyles1.Append(latentStyleExceptionInfo344);
            latentStyles1.Append(latentStyleExceptionInfo345);
            latentStyles1.Append(latentStyleExceptionInfo346);
            latentStyles1.Append(latentStyleExceptionInfo347);
            latentStyles1.Append(latentStyleExceptionInfo348);
            latentStyles1.Append(latentStyleExceptionInfo349);
            latentStyles1.Append(latentStyleExceptionInfo350);
            latentStyles1.Append(latentStyleExceptionInfo351);
            latentStyles1.Append(latentStyleExceptionInfo352);
            latentStyles1.Append(latentStyleExceptionInfo353);
            latentStyles1.Append(latentStyleExceptionInfo354);
            latentStyles1.Append(latentStyleExceptionInfo355);
            latentStyles1.Append(latentStyleExceptionInfo356);
            latentStyles1.Append(latentStyleExceptionInfo357);
            latentStyles1.Append(latentStyleExceptionInfo358);
            latentStyles1.Append(latentStyleExceptionInfo359);
            latentStyles1.Append(latentStyleExceptionInfo360);
            latentStyles1.Append(latentStyleExceptionInfo361);
            latentStyles1.Append(latentStyleExceptionInfo362);
            latentStyles1.Append(latentStyleExceptionInfo363);
            latentStyles1.Append(latentStyleExceptionInfo364);
            latentStyles1.Append(latentStyleExceptionInfo365);
            latentStyles1.Append(latentStyleExceptionInfo366);
            latentStyles1.Append(latentStyleExceptionInfo367);
            latentStyles1.Append(latentStyleExceptionInfo368);
            latentStyles1.Append(latentStyleExceptionInfo369);
            latentStyles1.Append(latentStyleExceptionInfo370);
            latentStyles1.Append(latentStyleExceptionInfo371);

            Style style1 = new Style() { Type = StyleValues.Paragraph, StyleId = "a", Default = true };
            StyleName styleName1 = new StyleName() { Val = "Normal" };
            PrimaryStyle primaryStyle1 = new PrimaryStyle();

            style1.Append(styleName1);
            style1.Append(primaryStyle1);

            Style style2 = new Style() { Type = StyleValues.Character, StyleId = "a0", Default = true };
            StyleName styleName2 = new StyleName() { Val = "Default Paragraph Font" };
            UIPriority uIPriority1 = new UIPriority() { Val = 1 };
            SemiHidden semiHidden1 = new SemiHidden();
            UnhideWhenUsed unhideWhenUsed1 = new UnhideWhenUsed();

            style2.Append(styleName2);
            style2.Append(uIPriority1);
            style2.Append(semiHidden1);
            style2.Append(unhideWhenUsed1);

            Style style3 = new Style() { Type = StyleValues.Table, StyleId = "a1", Default = true };
            StyleName styleName3 = new StyleName() { Val = "Normal Table" };
            UIPriority uIPriority2 = new UIPriority() { Val = 99 };
            SemiHidden semiHidden2 = new SemiHidden();
            UnhideWhenUsed unhideWhenUsed2 = new UnhideWhenUsed();

            StyleTableProperties styleTableProperties1 = new StyleTableProperties();
            TableIndentation tableIndentation1 = new TableIndentation() { Width = 0, Type = TableWidthUnitValues.Dxa };

            TableCellMarginDefault tableCellMarginDefault1 = new TableCellMarginDefault();
            TopMargin topMargin1 = new TopMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellLeftMargin tableCellLeftMargin1 = new TableCellLeftMargin() { Width = 108, Type = TableWidthValues.Dxa };
            BottomMargin bottomMargin1 = new BottomMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellRightMargin tableCellRightMargin1 = new TableCellRightMargin() { Width = 108, Type = TableWidthValues.Dxa };

            tableCellMarginDefault1.Append(topMargin1);
            tableCellMarginDefault1.Append(tableCellLeftMargin1);
            tableCellMarginDefault1.Append(bottomMargin1);
            tableCellMarginDefault1.Append(tableCellRightMargin1);

            styleTableProperties1.Append(tableIndentation1);
            styleTableProperties1.Append(tableCellMarginDefault1);

            style3.Append(styleName3);
            style3.Append(uIPriority2);
            style3.Append(semiHidden2);
            style3.Append(unhideWhenUsed2);
            style3.Append(styleTableProperties1);

            Style style4 = new Style() { Type = StyleValues.Numbering, StyleId = "a2", Default = true };
            StyleName styleName4 = new StyleName() { Val = "No List" };
            UIPriority uIPriority3 = new UIPriority() { Val = 99 };
            SemiHidden semiHidden3 = new SemiHidden();
            UnhideWhenUsed unhideWhenUsed3 = new UnhideWhenUsed();

            style4.Append(styleName4);
            style4.Append(uIPriority3);
            style4.Append(semiHidden3);
            style4.Append(unhideWhenUsed3);

            Style style5 = new Style() { Type = StyleValues.Paragraph, StyleId = "HTML" };
            StyleName styleName5 = new StyleName() { Val = "HTML Preformatted" };
            BasedOn basedOn1 = new BasedOn() { Val = "a" };
            LinkedStyle linkedStyle1 = new LinkedStyle() { Val = "HTML0" };
            UIPriority uIPriority4 = new UIPriority() { Val = 99 };
            SemiHidden semiHidden4 = new SemiHidden();
            UnhideWhenUsed unhideWhenUsed4 = new UnhideWhenUsed();
            Rsid rsid11 = new Rsid() { Val = "003B11C6" };

            StyleParagraphProperties styleParagraphProperties1 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines4 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

            styleParagraphProperties1.Append(spacingBetweenLines4);

            StyleRunProperties styleRunProperties1 = new StyleRunProperties();
            RunFonts runFonts17 = new RunFonts() { Ascii = "Consolas", HighAnsi = "Consolas" };
            FontSize fontSize41 = new FontSize() { Val = "20" };
            FontSizeComplexScript fontSizeComplexScript41 = new FontSizeComplexScript() { Val = "20" };

            styleRunProperties1.Append(runFonts17);
            styleRunProperties1.Append(fontSize41);
            styleRunProperties1.Append(fontSizeComplexScript41);

            style5.Append(styleName5);
            style5.Append(basedOn1);
            style5.Append(linkedStyle1);
            style5.Append(uIPriority4);
            style5.Append(semiHidden4);
            style5.Append(unhideWhenUsed4);
            style5.Append(rsid11);
            style5.Append(styleParagraphProperties1);
            style5.Append(styleRunProperties1);

            Style style6 = new Style() { Type = StyleValues.Character, StyleId = "HTML0", CustomStyle = true };
            StyleName styleName6 = new StyleName() { Val = "Стандартный HTML Знак" };
            BasedOn basedOn2 = new BasedOn() { Val = "a0" };
            LinkedStyle linkedStyle2 = new LinkedStyle() { Val = "HTML" };
            UIPriority uIPriority5 = new UIPriority() { Val = 99 };
            SemiHidden semiHidden5 = new SemiHidden();
            Rsid rsid12 = new Rsid() { Val = "003B11C6" };

            StyleRunProperties styleRunProperties2 = new StyleRunProperties();
            RunFonts runFonts18 = new RunFonts() { Ascii = "Consolas", HighAnsi = "Consolas" };
            FontSize fontSize42 = new FontSize() { Val = "20" };
            FontSizeComplexScript fontSizeComplexScript42 = new FontSizeComplexScript() { Val = "20" };

            styleRunProperties2.Append(runFonts18);
            styleRunProperties2.Append(fontSize42);
            styleRunProperties2.Append(fontSizeComplexScript42);

            style6.Append(styleName6);
            style6.Append(basedOn2);
            style6.Append(linkedStyle2);
            style6.Append(uIPriority5);
            style6.Append(semiHidden5);
            style6.Append(rsid12);
            style6.Append(styleRunProperties2);

            Style style7 = new Style() { Type = StyleValues.Character, StyleId = "a3" };
            StyleName styleName7 = new StyleName() { Val = "Hyperlink" };
            BasedOn basedOn3 = new BasedOn() { Val = "a0" };
            UIPriority uIPriority6 = new UIPriority() { Val = 99 };
            UnhideWhenUsed unhideWhenUsed5 = new UnhideWhenUsed();
            Rsid rsid13 = new Rsid() { Val = "00B81BC8" };

            StyleRunProperties styleRunProperties3 = new StyleRunProperties();
            Color color21 = new Color() { Val = "0563C1", ThemeColor = ThemeColorValues.Hyperlink };
            Underline underline1 = new Underline() { Val = UnderlineValues.Single };

            styleRunProperties3.Append(color21);
            styleRunProperties3.Append(underline1);

            style7.Append(styleName7);
            style7.Append(basedOn3);
            style7.Append(uIPriority6);
            style7.Append(unhideWhenUsed5);
            style7.Append(rsid13);
            style7.Append(styleRunProperties3);

            styles1.Append(docDefaults1);
            styles1.Append(latentStyles1);
            styles1.Append(style1);
            styles1.Append(style2);
            styles1.Append(style3);
            styles1.Append(style4);
            styles1.Append(style5);
            styles1.Append(style6);
            styles1.Append(style7);

            styleDefinitionsPart1.Styles = styles1;
        }

        // Generates content of imagePart1.
        private void GenerateImagePart1Content(ImagePart imagePart1)
        {
            System.IO.Stream data = GetBinaryDataStream(imagePart1Data);
            imagePart1.FeedData(data);
            data.Close();
        }

        private void SetPackageProperties(OpenXmlPackage document)
        {
            document.PackageProperties.Creator = "Руденко Ирина Анатольевна";
            document.PackageProperties.Title = "";
            document.PackageProperties.Subject = "";
            document.PackageProperties.Keywords = "";
            document.PackageProperties.Description = "";
            document.PackageProperties.Revision = "9";
            document.PackageProperties.Created = System.Xml.XmlConvert.ToDateTime("2017-10-19T03:21:00Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
            document.PackageProperties.Modified = System.Xml.XmlConvert.ToDateTime("2017-12-18T04:22:00Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
            document.PackageProperties.LastModifiedBy = "Головчак Андрей Васильевич";
        }

        #region Binary Data
        private string imagePart1Data = "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";

        private System.IO.Stream GetBinaryDataStream(string base64String)
        {
            return new System.IO.MemoryStream(System.Convert.FromBase64String(base64String));
        }

        #endregion

    }
}
