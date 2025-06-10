using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace ProjetaARQ.Features.WordExport.Services
{
    internal class WordEditor : IDisposable
    {
        private WordprocessingDocument _document;
        private MainDocumentPart _mainPart;
        private string _filePath;

        public WordEditor(string filePath)
        {
            _filePath = filePath;
            Open();
        }

        private void Open()
        {
            _document = WordprocessingDocument.Open(_filePath, true);
            _mainPart = _document.MainDocumentPart;
        }

        public void ReplaceTextInContentControl(string tag, string newText)
        {
            var sdt = _mainPart.Document.Descendants<SdtBlock>()
                .FirstOrDefault(e => (e.SdtProperties.GetFirstChild<Tag>()?.Val?.Value ?? "") == tag);

            if (sdt != null)
            {
                // Limpa o conteúdo anterior
                sdt.SdtContentBlock.RemoveAllChildren();

                // Cria novo parágrafo com texto destacado
                var run = new Run(
                    new RunProperties(
                        new Highlight { Val = HighlightColorValues.Green }
                    ),
                    new Text(newText)
                );

                var paragraph = new Paragraph(run);
                sdt.SdtContentBlock.AppendChild(paragraph);
            }
        }

        //public void ReplaceTextInContentControl(string tag, string newText)
        //{
        //    // Procura tanto por blocos quanto por runs
        //    var sdt = _mainPart.Document.Descendants<SdtElement>()
        //                .FirstOrDefault(e => e.SdtProperties?.GetFirstChild<Tag>()?.Val?.Value == tag);

        //    if (sdt is SdtBlock block)
        //    {
        //        block.SdtContentBlock.RemoveAllChildren();
        //        block.SdtContentBlock.AppendChild(new Paragraph(
        //            new Run(
        //                new RunProperties(new Highlight { Val = HighlightColorValues.Green }),
        //                new Text(newText)
        //            )));
        //    }
        //    else if (sdt is SdtRun run)
        //    {
        //        run.SdtContentRun.RemoveAllChildren();
        //        run.SdtContentRun.AppendChild(
        //            new Run(
        //                new RunProperties(new Highlight { Val = HighlightColorValues.Green }),
        //                new Text(newText)
        //            ));
        //    }
        //}

        public void ReplaceTextInsideRun(string tag, string oldWord, string newWord)
        {
            var sdt = _mainPart.Document.Descendants<SdtRun>()
                        .FirstOrDefault(e => (e.SdtProperties.GetFirstChild<Tag>()?.Val?.Value ?? "") == tag);

            if (sdt != null)
            {
                var run = sdt.Descendants<Run>().FirstOrDefault(r => r.InnerText.Contains(oldWord));
                if (run != null)
                {
                    var text = run.GetFirstChild<Text>();
                    if (text != null)
                    {
                        text.Text = text.Text.Replace(oldWord, newWord);
                        text.Parent.InsertBeforeSelf(
                            new Run(
                                new RunProperties(new Highlight { Val = HighlightColorValues.Green }),
                                new Text(newWord)
                                )
                            );

                        text.Remove(); // remove o antigo, se necessário
                    }
                }
            }
        }

        public void Dispose()
        {
            _document?.Dispose(); // Fecha e salva
        }
    }
}
