namespace Journal.Application.Dtos;

public record CreateJournalDto(string ShortDescription, string Text, int AuthorId);