using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        public void ReplaceImage(string tag, string nomeCompletoDoRecurso)
        {
            // Pega a assembly atual para acessar os recursos embutidos
            var assembly = Assembly.GetExecutingAssembly();

            // Tenta obter o fluxo (stream) do recurso. Se não encontrar, interrompe a execução.
            using (Stream stream = assembly.GetManifestResourceStream(nomeCompletoDoRecurso))
            {
                if (stream == null)
                {
                    // O recurso não foi encontrado. Verifique se o nome está correto e se a Build Action é "Embedded Resource".
                    return;
                }

                var sdt = _mainPart.Document.Body.Descendants<SdtElement>()
                    .FirstOrDefault(e => (e.SdtProperties.GetFirstChild<Tag>()?.Val?.Value ?? "") == tag);

                if (sdt == null) return;

                var blip = sdt.Descendants<DocumentFormat.OpenXml.Drawing.Blip>().FirstOrDefault();
                if (blip == null) return;

                // 1. Guarda o ID da imagem antiga
                string oldImagePartId = blip.Embed;

                // 2. Adiciona a nova imagem a partir do STREAM do recurso
                ImagePart novaImagePart = _mainPart.AddImagePart(ImagePartType.Png);
                novaImagePart.FeedData(stream);
                string novoIdRelacionamento = _mainPart.GetIdOfPart(novaImagePart);

                // 3. Substitui a referência no Blip
                blip.Embed = novoIdRelacionamento;

                //// 4. Verifica se a imagem antiga ainda está sendo usada
                //int usosRestantes = _mainPart.Document.Body.Descendants<DocumentFormat.OpenXml.Drawing.Blip>()
                //                           .Count(b => b.Embed == oldImagePartId);

                //if (usosRestantes == 0)
                //{
                //    // 5. Apaga a parte da imagem antiga do pacote
                //    _mainPart.DeletePart(_mainPart.GetPartById(oldImagePartId));
                //}
            }
        }

        public void Dispose()
        {
            _document?.Dispose(); // Fecha e salva
        }
    }
}
