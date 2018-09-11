namespace Training.XApi.Engine.Enums
{
    public enum WorkflowStatus
    {
        None,
        Custom,
        AwaitingApproval,
        AwaitingAutoApproval,
        AwaitingFraudResponse,
        Rejected,
        OnHold
    }
}
