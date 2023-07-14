using Microsoft.AspNetCore.SignalR;

namespace SignalR;

public abstract class CustomHubBase<TContract> : Hub<TContract> 
    where TContract : class
{
    
}