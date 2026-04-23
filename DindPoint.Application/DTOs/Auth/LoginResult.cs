namespace DindPoint.Application.DTOs.Auth;

public record LoginResult
{
    public bool Success { get; init; }
    public string? Message { get; init; }
    public int? UserId { get; init; }
    public string? UserName { get; init; }
    public string? EmployeeName { get; init; }
    public string? RoleName { get; init; }
    public int? DepartmentId { get; init; }
    public int HierarchyLevel { get; init; }
    public string? RedirectUrl { get; init; }
    
    public static LoginResult Failed(string message) => 
        new() { Success = false, Message = message };
        
    public static LoginResult SuccessResult(int userId, string userName, string employeeName, string? roleName, int? departmentId, int hierarchyLevel, string? returnUrl)
    {
        var redirectUrl = returnUrl ?? GetDefaultRedirectUrl(roleName);
        return new LoginResult 
        { 
            Success = true,
            UserId = userId,
            UserName = userName,
            EmployeeName = employeeName,
            RoleName = roleName,
            DepartmentId = departmentId,
            HierarchyLevel = hierarchyLevel,
            RedirectUrl = redirectUrl
        };
    }
    
    private static string GetDefaultRedirectUrl(string? roleName)
    {
        return roleName switch
        {
            "SuperAdmin" => "/Department",
            "HOD" => "/Dashboard/Department",
            "Supervisor" => "/Dashboard/Team",
            _ => "/Dashboard/MyKPI"
        };
    }
}