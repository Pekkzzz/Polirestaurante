public static class UtilityInternalTools
{
    public static bool NameValidityCheck(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length < 5 || value.Length > 255)
        {
            return false;
        }
        return true;
    }
    public static bool UsernameValidityCheck(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length < 5 || value.Length > 255)
        {
            return false;
        }
        return true;
    }

    public static bool EmailValidityCheck(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length < 5 || value.Length > 255)
        {
            return false;
        }
        return true;
    }

    public static bool PasswordValidityCheck(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length < 5 || value.Length > 255)
        {
            return false;
        }
        return true;
    }

    

    

}