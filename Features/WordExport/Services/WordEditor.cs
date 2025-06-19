using DFOW = DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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

        public void ReplaceImage(string altText, string imageResourceName)
        {
            // 1. Encontra o elemento de Desenho (Drawing) que corresponde à imagem
            Drawing drawing = _mainPart.Document.Body.Descendants<Drawing>().FirstOrDefault(d =>
            {
                // O Texto Alternativo fica na propriedade "Description" do DocProperties
                DocProperties docProperties = d.Descendants<DocProperties>().FirstOrDefault();

                // VERSÃO CORRIGIDA:
                // Verifica de forma segura se Description e seu valor existem antes de comparar.
                return docProperties?.Description?.Value == altText;
            });

            if (drawing == null) return; // Imagem com o Alt Text não encontrada

            // 2. Encontra o Blip dentro do Desenho
            var blip = drawing.Descendants<DFOW.Blip>().FirstOrDefault();
            if (blip == null) return;

            // 3. O resto do processo é o mesmo que já fizemos:
            // Pega o ID antigo, adiciona a nova imagem, pega o novo ID e atualiza o Blip.

            string oldImagePartId = blip.Embed;

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(imageResourceName))
            {
                if (stream == null) return;

                ImagePart novaImagePart = _mainPart.AddImagePart(ImagePartType.Png); // Adapte o tipo
                novaImagePart.FeedData(stream);
                string novoIdRelacionamento = _mainPart.GetIdOfPart(novaImagePart);

                blip.Embed = novoIdRelacionamento;

                // Apaga a parte da imagem antiga se ela não for mais usada
                if (_mainPart.Document.Body.Descendants<DFOW.Blip>().Count(b => b.Embed == oldImagePartId) == 0)
                {
                    _mainPart.DeletePart(_mainPart.GetPartById(oldImagePartId));
                }
            }
        }

        public void Dispose()
        {
            _document?.Dispose(); // Fecha e salva
        }
    }
}
