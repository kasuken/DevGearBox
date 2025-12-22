using Bogus;

namespace DevGearbox.Utils;

public static class LoremIpsumGenerator
{
    private static readonly Faker _faker = new Faker();

    public enum TextLength
    {
        Short,      // 1 paragraph
        Medium,     // 3 paragraphs
        Long,       // 5 paragraphs
        VeryLong,   // 10 paragraphs
        Custom      // User-defined
    }

    public static string Generate(TextLength length, int customParagraphs = 1)
    {
        int paragraphCount = length switch
        {
            TextLength.Short => 1,
            TextLength.Medium => 3,
            TextLength.Long => 5,
            TextLength.VeryLong => 10,
            TextLength.Custom => customParagraphs,
            _ => 1
        };

        return _faker.Lorem.Paragraphs(paragraphCount);
    }

    public static string GenerateSentences(int count)
    {
        return _faker.Lorem.Sentences(count);
    }

    public static string GenerateWords(int count)
    {
        return string.Join(" ", _faker.Lorem.Words(count));
    }

    public static string GenerateParagraph()
    {
        return _faker.Lorem.Paragraph();
    }

    public static string GenerateText(int minCharacters)
    {
        return _faker.Lorem.Text();
    }
}

