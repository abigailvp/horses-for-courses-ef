public record Competence
{
    public string Name { get; init; }
    public int Level { get; init; }

    public Competence(string name, int level)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name must be filled in");
        if (level <= 0 || level > 10)
            throw new ArgumentException("Level must be between 0 and 10");
        Name = name;
        Level = level;
    }
}