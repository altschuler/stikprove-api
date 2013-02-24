namespace Stikprove.Api.Models.Dto
{
    public class TranslationDto : AbstractDto
    {
        public string Label { get; set; }

        static public TranslationDto Create(Translation translation)
        {
            return new TranslationDto()
            {
                Id = translation.Id,
                Label = translation.Label
            };
        }
    }
}