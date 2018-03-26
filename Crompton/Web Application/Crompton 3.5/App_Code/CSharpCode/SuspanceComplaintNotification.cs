using System.Collections.Generic;

/// <summary>
/// Summary description for SuspanceComplaintNotification
/// </summary>
public class SuspanceComplaintNotification
{
    public int productDivisionId { get; set; }
    public int stateSno { get; set; }
    public string stateDesc { get; set; }
    public int totalCount { get; set; }
    public string rerionDesc { get; set; }
}

public class SuspanceWrapper
{
    public List<SuspanceComplaintNotification> SuspanceDtls { get; set; }
    public List<SuspanceComplaintNotification> SuspanceHeaderDtls { get; set; }
}
