namespace SecretsMocker.Models.Terraform;

public class TerraformData
{
    public string id { get; set; }
    public string type { get; set; }
    public Attributes attributes { get; set; }
    public Relationships relationships { get; set; }
}