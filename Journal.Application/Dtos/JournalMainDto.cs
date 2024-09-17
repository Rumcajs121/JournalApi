namespace Journal.Application.Dtos;

public class JournalMainDto
{
        public string NormalizedId { get; set; }
        public string ShortDescription{ get; set; }
        public string Text{ get; set; }
        public List<PictureDto> Pictures{ get; set; }
        public string Nick{ get; set; }
        public string ImgAvatar{ get; set; }
        public int SumJournal{ get; set; }
        public DateTime CreateAccount{ get; set; }
        public DateTime LastLogin{ get; set; }
        public string TheBestJournal{ get; set; }

}
public class PictureDto
{
        public string? GuidNormalizedName { get; set; }
        public string UriBlobStorage { get; set; }
}