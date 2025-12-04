namespace HospitalManagementSystem.Utilities;

public enum Role
{
    Admin,
    Receptionist,
    Doctor,
    Nurse,
    Accountant,
    Laboratorist,
    Pharmacist
}

public static class RoleCodeGenerator
{
    public static string GetRolePrefix(string role)
    {
        var rolePrefix = role.ToLower();
        return rolePrefix switch
        {
            RoleVariable.Admin => "AD",
            RoleVariable.Receptionist  => "RC",
            RoleVariable.Doctor        => "DR",
            RoleVariable.Nurse         => "NU",
            RoleVariable.Accountant    => "AC",
            RoleVariable.Laboratorist  => "LB",
            RoleVariable.Pharmacist    => "PH",
            _ => "UN" // Unknown
        };
    }
}