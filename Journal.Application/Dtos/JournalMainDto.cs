namespace Journal.Application.Dtos;

public record JournalMainDto(
    string NormalizedId,
    string ShortDescription,
    string Text,
    List<string> Pictures,
    string Nick,
    string ImgAvatar,
    int SumJournal,
    DateTime CreateAccount,
    DateTime LastLogin,
    string TheBestJournal);

