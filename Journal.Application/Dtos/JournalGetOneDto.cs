namespace Journal.Application.Dtos;

public record JournalGetOneDto(
    string NormalizedId,
    string ShortDescription,
    string Text,
    List<string> Pictures,
    string Nick,
    string ImgAvatar);
