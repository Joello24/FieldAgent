namespace FieldAgent.Core;

public class PensionListItem
{
    public string AgencyName { get; set; }
    public Guid BadgeId { get; set; }
    public string NameLastFirst { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime DeactivationDate { get; set; }
}