namespace SecretsMocker.Models.Terraform;

public class Attributes
{
    public DateTime createdat { get; set; }
    public object lastusedat { get; set; }

    //The description of the team token. Each description must be unique within the context of the team.
    public string description { get; set; }

    public string token { get; set; }

    // The UTC date and time that the Team Token will expire, in ISO 8601 format. If omitted or set to null the token will never expire.
    public DateTime expiredat { get; set; }
}