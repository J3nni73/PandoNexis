using AutoMapper;
using JetBrains.Annotations;
using Litium.FieldFramework;
using Litium.Web.Models;
using Litium.Web.Rendering;
using PandoNexis.Accelerator.Extensions.Extensions;

namespace PandoNexis.Accelerator.Extensions.Resolvers
{
    [UsedImplicitly]
    public class TextEditorResolver<TSource, TDest> : IMemberValueResolver<TSource, TDest, string, string>
       where TSource : FieldFrameworkModel
    {
        private readonly ContentProcessorService _contentProcessorService;

        public TextEditorResolver(ContentProcessorService contentProcessorService) =>
            _contentProcessorService = contentProcessorService;

        public string Resolve(TSource s, TDest d, string strA, string strB, ResolutionContext c) =>
            Parse(s.GetValue<string>(strA));

        private string Parse(string str) =>
            string.IsNullOrWhiteSpace(str)
                ? string.Empty
                : _contentProcessorService.Process(str);
    }

    [UsedImplicitly]
    public class TextEditorMultiCultureFieldResolver<TSource, TDest> : IMemberValueResolver<TSource, TDest, string, string>
        where TSource : MultiCultureFieldContainer
    {
        private readonly ContentProcessorService _contentProcessorService;

        public TextEditorMultiCultureFieldResolver(ContentProcessorService contentProcessorService) =>
            _contentProcessorService = contentProcessorService;

        public string Resolve(TSource s, TDest d, string strA, string strB, ResolutionContext c) =>
            Parse(s.GetValueOrDefault<string>(strA));

        private string Parse(string str) =>
            string.IsNullOrWhiteSpace(str)
                ? string.Empty
                : _contentProcessorService.Process(str);
    }

    [UsedImplicitly]
    public class TextEditorMultiFieldResolver<TSource, TDest> : IMemberValueResolver<TSource, TDest, string, string>
        where TSource : MultiFieldItem
    {
        private readonly ContentProcessorService _contentProcessorService;

        public TextEditorMultiFieldResolver(ContentProcessorService contentProcessorService) =>
            _contentProcessorService = contentProcessorService;

        public string Resolve(TSource s, TDest d, string strA, string strB, ResolutionContext c) =>
            Parse(s.Fields.GetValueOrDefault<string>(strA));

        private string Parse(string str) =>
            string.IsNullOrWhiteSpace(str)
                ? string.Empty
                : _contentProcessorService.Process(str);
    }
}
