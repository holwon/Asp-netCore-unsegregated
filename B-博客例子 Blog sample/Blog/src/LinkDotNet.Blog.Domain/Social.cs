namespace LinkDotNet.Blog.Domain;

public record Social
{
    public string LinkedinAccountUrl { get; init; }

    public bool HasLinkedinAccount => !string.IsNullOrEmpty(LinkedinAccountUrl);

    public string GithubAccountUrl { get; init; }

    public bool HasGithubAccount => !string.IsNullOrEmpty(GithubAccountUrl);

    public string TwitterAccountUrl { get; init; }

    public bool HasTwitterAccount => !string.IsNullOrEmpty(TwitterAccountUrl);
}