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
    public static string GetRolePrefix(Role role)
    {
        return role switch
        {
            Role.Admin         => "AD",
            Role.Receptionist  => "RC",
            Role.Doctor        => "DR",
            Role.Nurse         => "NU",
            Role.Accountant    => "AC",
            Role.Laboratorist  => "LB",
            Role.Pharmacist    => "PH",
            _ => "UN" // Unknown
        };
    }
}