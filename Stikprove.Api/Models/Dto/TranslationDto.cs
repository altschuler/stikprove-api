namespace Stikprove.Api.Models.Dto
{
    public class TranslationDto : AbstractDto
    {
        public string TranslationId { get; set; }
        public string Label { get; set; }
        public string HelpText { get; set; }

        static public TranslationDto Create(Translation translation)
        {
            return new TranslationDto()
            {
                Id = translation.Id,
                Label = translation.Label,
                HelpText = translation.HelpText,
                TranslationId = translation.TranslationId
            };
        }
    }
}